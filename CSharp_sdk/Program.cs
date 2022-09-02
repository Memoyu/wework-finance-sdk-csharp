using System;
using System.IO;
using System.Runtime.InteropServices;

namespace CSharp_sdk
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //seq 表示该企业存档消息序号，该序号单调递增，拉取序号建议设置为上次拉取返回结果中最大序号。首次拉取时seq传0，sdk会返回有效期内最早的消息。
            //limit 表示本次拉取的最大消息条数，取值范围为1~1000
            //proxy与passwd为代理参数，如果运行sdk的环境不能直接访问外网，需要配置代理参数。sdk访问的域名是"https://qyapi.weixin.qq.com"。
            //建议先通过curl访问"https://qyapi.weixin.qq.com"，验证代理配置正确后，再传入sdk。
            //timeout 为拉取会话存档的超时时间，单位为秒，建议超时时间设置为5s。
            //sdkfileid 媒体文件id，从解密后的会话存档中得到
            //savefile 媒体文件保存路径
            //encrypt_key 拉取会话存档返回的encrypt_random_key，使用配置在企业微信管理台的rsa公钥对应的私钥解密后得到encrypt_key。
            //encrypt_chat_msg 拉取会话存档返回的encrypt_chat_msg
            if (args.Length < 2)
            {
                Console.WriteLine("./sdktools 1(chatmsg) 2(mediadata) 3(decryptdata)");
                Console.WriteLine("./sdktools 1 seq limit proxy passwd timeout");
                Console.WriteLine("./sdktools 2 fileid proxy passwd timeout savefile");
                Console.WriteLine("./sdktools 3 encrypt_key encrypt_chat_msg");
                return;
            }

            int ret = 0;
            //使用sdk前需要初始化，初始化成功后的sdk可以一直使用。
            //如需并发调用sdk，建议每个线程持有一个sdk实例。
            //初始化时请填入自己企业的corpid与secrectkey。
            IntPtr sdk = Finance.NewSdk();
            ret = Finance.Init(sdk, "wwd08c8e7c775ab44d", "zJ6k0naVVQ--gt9PUSSEvs03zW_nlDVmjLCTOTAfrew");
            if (ret != 0)
            {
                Finance.DestroySdk(sdk);
                Console.WriteLine("init sdk err ret " + ret);
                return;
            }

            if (args[0].Equals("1"))
            {
                //拉取会话存档
                int seq = int.Parse(args[1]);
                int limit = int.Parse(args[2]);
                string proxy = args[3];
                string passwd = args[4];
                int timeout = int.Parse(args[5]);

                //每次使用GetChatData拉取存档前需要调用NewSlice获取一个slice，在使用完slice中数据后，还需要调用FreeSlice释放。
                IntPtr slice = Finance.NewSlice();
                ret = Finance.GetChatData(sdk, seq, limit, proxy, passwd, timeout, slice);
                if (ret != 0)
                {
                    Console.WriteLine("getchatdata ret " + ret);
                    Finance.FreeSlice(slice);
                    return;
                }

                IntPtr cPtr = Finance.GetContentFromSlice(slice);
                string content = Marshal.PtrToStringAnsi(cPtr);
                Console.WriteLine("getchatdata :" + content);
                Finance.FreeSlice(slice);
            }
            else if (args[0].Equals("2"))
            {
                //拉取媒体文件
                string sdkfileid = args[1];
                string proxy = args[2];
                string passwd = args[3];
                int timeout = int.Parse(args[4]);
                string savefile = args[5];

                //媒体文件每次拉取的最大size为512k，因此超过512k的文件需要分片拉取。若该文件未拉取完整，sdk的IsMediaDataFinish接口会返回0，同时通过GetOutIndexBuf接口返回下次拉取需要传入GetMediaData的indexbuf。
                //indexbuf一般格式如右侧所示，”Range:bytes=524288-1048575“，表示这次拉取的是从524288到1048575的分片。单个文件首次拉取填写的indexbuf为空字符串，拉取后续分片时直接填入上次返回的indexbuf即可。
                string indexbuf = "";
                while (true)
                {
                    //每次使用GetMediaData拉取存档前需要调用NewMediaData获取一个media_data，在使用完media_data中数据后，还需要调用FreeMediaData释放。
                    IntPtr media_data = Finance.NewMediaData();
                    ret = Finance.GetMediaData(sdk, indexbuf, sdkfileid, proxy, passwd, timeout, media_data);
                    if (ret != 0)
                    {
                        Console.WriteLine("getmediadata ret:" + ret);
                        Finance.FreeMediaData(media_data);
                        return;
                    }
                    Console.WriteLine("getmediadata outindex len:{0}, data_len:{1}, is_finis:{2}", Finance.GetIndexLen(media_data), Finance.GetDataLen(media_data), Finance.IsMediaDataFinish(media_data));
                    try
                    {
                        //大于512k的文件会分片拉取，此处需要使用追加写，避免后面的分片覆盖之前的数据。
                        var dataPtr = Finance.GetData(media_data);
                        var dataLen = Finance.GetDataLen(media_data);
                        var bytes = new byte[dataLen];
                        Marshal.Copy(dataPtr, bytes, 0, dataLen);

                        FileStream fs = new FileStream(savefile, FileMode.Create);
                        fs.Write(bytes, 0, dataLen);
                        fs.Flush();
                        fs.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception Message :{0}, StackTrace: {1}", e.Message, e.StackTrace); ;
                    }

                    if (Finance.IsMediaDataFinish(media_data) == 1)
                    {
                        //已经拉取完成最后一个分片
                        Finance.FreeMediaData(media_data);
                        break;
                    }
                    else
                    {
                        //获取下次拉取需要使用的indexbuf
                        IntPtr indexbufPtr = Finance.GetOutIndexBuf(media_data);
                        indexbuf = Marshal.PtrToStringAnsi(indexbufPtr);
                        Finance.FreeMediaData(media_data);
                        break;
                    }
                }
            }
            else if (args[0].Equals("3"))
            {
                //解密会话存档内容
                //sdk不会要求用户传入rsa私钥，保证用户会话存档数据只有自己能够解密。
                //此处需要用户先用rsa私钥解密encrypt_random_key后，作为encrypt_key参数传入sdk来解密encrypt_chat_msg获取会话存档明文。
                string encrypt_key = args[1];
                string encrypt_chat_msg = args[2];

                //每次使用DecryptData解密会话存档前需要调用NewSlice获取一个slice，在使用完slice中数据后，还需要调用FreeSlice释放。
                IntPtr msg = Finance.NewSlice();
                ret = Finance.DecryptData(encrypt_key, encrypt_chat_msg, msg);
                if (ret != 0)
                {
                    Console.WriteLine("getchatdata ret " + ret);
                    Finance.FreeSlice(msg);
                    return;
                }

                IntPtr cPtr = Finance.GetContentFromSlice(msg);
                string content = Marshal.PtrToStringUTF8(cPtr);
                Console.WriteLine("decrypt ret:" + ret + " msg:" + content);
                Finance.FreeSlice(msg);
            }
            else
            {
                Console.WriteLine("wrong args " + args[0]);
            }

        }
    }
}
