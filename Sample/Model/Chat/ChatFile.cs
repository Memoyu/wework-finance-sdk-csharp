using Newtonsoft.Json;

namespace Sample
{
    public class ChatFile : ChatBase
    {
        public ChatFileDetail File { get; set; }

        public class ChatFileDetail
        {
            /// <summary>
            /// 媒体资源的id信息。String类型
            /// </summary>
            [JsonProperty("sdkfileid")]
            public string SdkFileId { get; set; }

            /// <summary>
            /// 资源的md5值，供进行校验。String类型
            /// </summary>
            [JsonProperty("md5sum")]
            public string Md5Sum { get; set; }

            /// <summary>
            /// 文件名称。String类型
            /// </summary>
            [JsonProperty("filename")]
            public string FileName { get; set; }

            /// <summary>
            /// 文件类型后缀。String类型
            /// </summary>
            [JsonProperty("fileext")]
            public string FileExt { get; set; }

            /// <summary>
            /// 文件大小。Uint32类型
            /// </summary>
            [JsonProperty("filesize")]
            public int FileSize { get; set; }
        }
    }
}
