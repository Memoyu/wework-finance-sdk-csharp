using CSharp_sdk;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;

namespace Sample
{
    internal class FinanceSample
    {
        private readonly IntPtr _sdk;

        public FinanceSample(string corpId, string secret)
        {
            _sdk = Finance.NewSdk();
            var res = Finance.Init(_sdk, corpId, secret);
            if (res != 0)
                throw new Exception("企微会话存档:SDK初始化失败");
        }

        /// <summary>
        /// 获取会话数据
        /// </summary>
        /// <param name="verKey">会话加密私钥</param>
        /// <param name="seq">指定的seq开始拉取消息，注意的是返回的消息从seq+1开始返回，seq为之前接口返回的最大seq值。首次使用请使用seq:0</param>
        /// <param name="limit">一次拉取的消息条数，最大值1000条，超过1000条会返回错误</param>
        /// <param name="timeout">超时时间，单位秒。默认60s</param>
        /// <returns>解密后的ChatData, 最大Seq</returns>
        /// <exception cref="Exception"></exception>
        public (List<ChatBase> ChatData, long Seq) GetChatData(Dictionary<int, string> verKey, long seq, int limit, int timeout = 60)
        {
            var slice = Finance.NewSlice();
            try
            {
                var chatDatas = new List<ChatBase>();
                var res = Finance.GetChatData(_sdk, seq, limit, "", "", timeout, slice);
                if (res != 0)
                    throw new Exception("企微会话存档:获取会话数据失败");
                var content = GetContentFromSlice(slice);
                var encryptChatData = JsonConvert.DeserializeObject<GetChatData>(content);
                if (encryptChatData.Errcode != 0)
                    throw new Exception($"企微会话存档:转换会话数据返回错误；ErrCode:{encryptChatData.Errcode};ErrMsg{encryptChatData.Errmsg}");

                if (!encryptChatData.ChatDatas.Any()) return (new List<ChatBase>(), 0);
                var maxSeq = encryptChatData.ChatDatas.Max(s => s.Seq);
                foreach (var encryptChat in encryptChatData.ChatDatas)
                {
                    var flag = verKey.TryGetValue(encryptChat.PublicKeyVer, out var privateKey);
                    if (!flag) continue;
                    var key = Decrypt(privateKey, encryptChat.EncryptRandomKey);
                    if (string.IsNullOrWhiteSpace(key)) continue;
                    var msgSlice = Finance.NewSlice();
                    var deRes = Finance.DecryptData(key, encryptChat.EncryptChatMsg, msgSlice);
                    if (deRes != 0) continue;
                    var chatJson = GetContentFromSlice(msgSlice);
                    Finance.FreeSlice(msgSlice);
                    if (!string.IsNullOrWhiteSpace(chatJson))
                    {
                        var chatData = JsonConvert.DeserializeObject<ChatBase>(chatJson);
                        chatDatas.Add((ChatBase)JsonConvert.DeserializeObject(chatJson, ChatMsgType.GetMsgType(chatData.MsgType)));
                    }
                }

                return (chatDatas, maxSeq);
            }
            catch (Exception ex)
            {
                throw new Exception("获取会话数据数据异常", ex);
            }
            finally
            {
                Finance.FreeSlice(slice);
            }
        }

        /// <summary>
        /// 读取媒体数据
        /// </summary>
        /// <param name="fileId">文件Id</param>
        /// <returns>byte[]</returns>
        /// <exception cref="Exception"></exception>
        public byte[] GetMediaData(string fileId)
        {
            var byteList = new List<byte>();
            var outIndexBuf = "";
            while (true)
            {
                int retryCount = 0;
            RetryGetMedia:
                var mediaData = Finance.NewMediaData();
                var res = Finance.GetMediaData(_sdk, outIndexBuf, fileId, "", "", 60, mediaData);
                if (res != 0)
                {
                    if (res == 10002 && retryCount < 3)
                    {
                        retryCount++;
                        Thread.Sleep(500);
                        goto RetryGetMedia;
                    }
                    else
                    {
                        Finance.FreeMediaData(mediaData);
                        throw new Exception($"企微会话存档:获取会话媒体数据失败，res:{res}");
                    }
                }

                var dataIntPtr = Finance.GetData(mediaData);
                var dataLen = Finance.GetDataLen(mediaData);
                var bytes = new byte[dataLen];
                Marshal.Copy(dataIntPtr, bytes, 0, bytes.Length);
                byteList.AddRange(bytes);

                // 校验文件是否已经读取完毕
                if (Finance.IsMediaDataFinish(mediaData) == 1)
                {
                    Finance.FreeMediaData(mediaData);
                    break;
                }
                else
                {
                    var oibPtr = Finance.GetOutIndexBuf(mediaData);
                    outIndexBuf = Marshal.PtrToStringAnsi(oibPtr);
                    Finance.FreeMediaData(mediaData);
                }
            }

            return byteList.ToArray();
        }

        /// <summary>
        /// 获取文本
        /// </summary>
        /// <param name="slice">slice</param>
        /// <returns>文本</returns>
        private string GetContentFromSlice(IntPtr slice)
        {
            var ptr = Finance.GetContentFromSlice(slice);
            return Marshal.PtrToStringUTF8(ptr);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="xmlPriKey">xml格式的密钥</param>
        /// <param name="content">解密内容</param>
        /// <returns>明文</returns>
        private string Decrypt(string xmlPriKey, string content)
        {
            var rsa = new RSACryptoServiceProvider();
            var bytes = Convert.FromBase64String(content);
            rsa.FromXmlString(xmlPriKey);
            var result = rsa.Decrypt(bytes, false);
            return Encoding.UTF8.GetString(result);
        }
    }
}
