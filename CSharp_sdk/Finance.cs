using System;
using System.Runtime.InteropServices;

namespace CSharp_sdk
{
    /*
   返回码	错误说明
   10000	参数错误，请求参数错误
   10001	网络错误，网络请求错误
   10002	数据解析失败
   10003	系统失败
   10004	密钥错误导致加密失败
   10005	fileid错误
   10006	解密失败
   10007 找不到消息加密版本的私钥，需要重新传入私钥对
   10008 解析encrypt_key出错
   10009 ip非法
   10010 数据过期
    */

    /* sdk返回数据
    typedef struct Slice_t {
        char* buf;
        int len;
    } Slice_t;

    typedef struct MediaData {
        char* outindexbuf;
        int out_len;
        char* data;    
        int data_len;
        int is_finish;
    } MediaData_t;
    */
    public class Finance
    {
        private const string DllName = "Lib/WeWorkFinanceSdk.dll";

        [DllImport(DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr NewSdk();

        /// <summary>
        /// 初始化函数 Return值=0表示该API调用成功
        /// </summary>
        /// <param name="sdk">NewSdk返回的sdk指针</param>
        /// <param name="corpId">调用企业的企业id，例如：wwd08c8exxxx5ab44d，可以在企业微信管理端--我的企业--企业信息查看</param>
        /// <param name="secret">聊天内容存档的Secret，可以在企业微信管理端--管理工具--聊天内容存档查看</param>
        /// <returns> 返回是否调用成功  0：成功   !=0： 失败</returns>
        [DllImport(DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Init(IntPtr sdk, string corpId, string secret);


        /// <summary>
        /// 拉取聊天记录函数 Return值=0表示该API调用成功
        /// </summary>
        /// <param name="sdk">NewSdk返回的sdk指针</param>
        /// <param name="seq">从指定的seq开始拉取消息，注意的是返回的消息从seq+1开始返回，seq为之前接口返回的最大seq值。首次使用请使用seq:0</param>
        /// <param name="limit"> 一次拉取的消息条数，最大值1000条，超过1000条会返回错误</param>
        /// <param name="proxy">使用代理的请求，需要传入代理的链接。如：socks5://10.0.0.1:8081 或者 http://10.0.0.1:8081</param>
        /// <param name="passwd">代理账号密码，需要传入代理的账号密码。如 user_name:passwd_123</param>
        /// <param name="timeout">超时时间，单位秒</param>
        /// <param name="chatData">返回本次拉取消息的数据，slice结构体.内容包括errcode/errmsg，以及每条消息内容。</param>
        /// <returns>返回是否调用成功  0：成功   !=0： 失败</returns>
        [DllImport(DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetChatData(IntPtr sdk, long seq, long limit, string proxy, string passwd, long timeout, IntPtr chatData);

        /// <summary>
        /// 拉取媒体消息函数
        /// </summary>
        /// <param name="sdk">NewSdk返回的sdk指针</param>
        /// <param name="indexbuf">媒体消息分片拉取，需要填入每次拉取的索引信息。首次不需要填写，默认拉取512k，后续每次调用只需要将上次调用返回的outindexbuf填入即可。</param>
        /// <param name="sdkField">从GetChatData返回的聊天消息中，媒体消息包括的sdkfileid</param>
        /// <param name="proxy">使用代理的请求，需要传入代理的链接。如：socks5://10.0.0.1:8081 或者 http://10.0.0.1:8081</param>
        /// <param name="passwd">代理账号密码，需要传入代理的账号密码。如 user_name:passwd_123</param>
        /// <param name="timeout">超时时间，单位秒</param>
        /// <param name="mediaData">返回本次拉取的媒体数据.MediaData结构体.内容包括data(数据内容)/outindexbuf(下次索引)/is_finish(拉取完成标记)</param>
        /// <returns>返回是否调用成功  0：成功   !=0： 失败</returns>
        [DllImport(DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetMediaData(IntPtr sdk, string indexbuf, string sdkField, string proxy, string passwd, long timeout, IntPtr mediaData);

        /// <summary>
        /// 解析密文.企业微信自有解密内容
        /// </summary>
        /// <param name="encrypt_key">getchatdata返回的encrypt_random_key,使用企业自持对应版本秘钥RSA解密后的内容</param>
        /// <param name="encrypt_msg">getchatdata返回的encrypt_chat_msg</param>
        /// <param name="msg">解密的消息明文</param>
        /// <returns>返回是否调用成功  0：成功   !=0： 失败</returns>
        [DllImport(DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern int DecryptData(string encrypt_key, string encrypt_msg, IntPtr msg);

        /// <summary>
        /// 释放sdk，和NewSdk成对使用
        /// </summary>
        /// <param name="sdk">sdk</param>
        [DllImport(DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DestroySdk(IntPtr sdk);

        [DllImport(DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr NewSlice();

        /// <summary>
        /// 释放slice，和NewSlice成对使用
        /// </summary>
        /// <param name="slice">slice</param>
        [DllImport(DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern void FreeSlice(IntPtr slice);

        /// <summary>
        /// 获取slice内容
        /// </summary>
        /// <param name="slice">slice</param>
        /// <returns>内容</returns>
        [DllImport(DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetContentFromSlice(IntPtr slice);

        /// <summary>
        /// 获取slice内容长度
        /// </summary>
        /// <param name="slice">slice</param>
        /// <returns>内容</returns>
        [DllImport(DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetSliceLen(IntPtr slice);

        [DllImport(DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr NewMediaData();

        [DllImport(DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern void FreeMediaData(IntPtr mediaData);

        /// <summary>
        /// 获取mediadata分片标识
        /// </summary>
        /// <param name="mediaData">mediaData</param>
        /// <returns></returns>
        [DllImport(DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetOutIndexBuf(IntPtr mediaData);

        [DllImport(DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetData(IntPtr mediaData);

        [DllImport(DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetIndexLen(IntPtr mediaData);

        [DllImport(DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetDataLen(IntPtr mediaData);

        /// <summary>
        /// 判断mediadata是否结束
        /// </summary>
        /// <param name="mediaData">mediaData</param>
        /// <returns> 1完成、0未完成</returns>
        [DllImport(DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern int IsMediaDataFinish(IntPtr mediaData);
    }
}
