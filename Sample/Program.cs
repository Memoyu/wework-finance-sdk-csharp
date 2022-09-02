using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Sample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("进入程序");
                var client = new FinanceSample("[企业Id]", "[企业会话存档Secret]");
                var privateKey = @"[会话存档RSA私钥(xml格式)]";
                var (data, seq) = client.GetChatData(
                    new Dictionary<int, string> { { 2, privateKey } },
                    0,
                    10);

                Console.WriteLine("拉取会话完成：数据量：{0}", data.Count);
                var outputs = new List<string>();
                // 输出拉取的数据
                foreach (var item in data)
                {
                    string fileName;
                    byte[] bs;
                    if (item.MsgType == "text")
                    {
                        outputs.Add($"text - time:{ToDateTimeLocal(item.MsgTime)}, content:{((ChatText)item).Text.Content}");
                    }

                    if (item.MsgType == "video")
                    {
                        fileName = $"{Guid.NewGuid()}.mp4";
                        bs = client.GetMediaData(((ChatVideo)item).Video.SdkFileId);
                        OutputFile(item, fileName, bs);
                    }

                    if (item.MsgType == "voice")
                    {
                        fileName = $"{Guid.NewGuid()}.amr";
                        bs = client.GetMediaData(((ChatVoice)item).Voice.SdkFileId);
                        OutputFile(item, fileName, bs);

                    }

                    if (item.MsgType == "image")
                    {
                        fileName = $"{Guid.NewGuid()}.png";
                        bs = client.GetMediaData(((ChatImage)item).Image.SdkFileId);
                        OutputFile(item, fileName, bs);
                    }

                    if (item.MsgType == "file")
                    {
                        var file = (ChatFile)item;
                        fileName = $"{Guid.NewGuid()}.{file.File.FileExt}";
                        bs = client.GetMediaData(file.File.SdkFileId);
                        OutputFile(item, fileName, bs);
                        
                    }
                }

                Console.WriteLine("-----------------分割线------------------");

                foreach (var item in outputs)
                {
                    Console.WriteLine(item);
                }

                void OutputFile(ChatBase item, string fileName, byte[] bs)
                {
                    FileStream fs = new FileStream($@".\media\{fileName}", FileMode.Create);
                    fs.Write(bs, 0, bs.Length);
                    fs.Flush();
                    fs.Close();
                    outputs.Add($"{item.MsgType} - time:{ToDateTimeLocal(item.MsgTime)}, content:{fileName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("会话存档测试异常");
            }
        }

        /// <summary>
        /// 转换时间戳为DateTime(本地时区)
        /// </summary>
        /// <param name="timestamp">时间戳</param>
        public static DateTime ToDateTimeLocal(long timestamp)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(timestamp).LocalDateTime;
        }
    }
}