using Newtonsoft.Json;

namespace Sample
{
    public class ChatImage : ChatBase
    {
        public ChatImageDetail Image { get; set; }

        public class ChatImageDetail
        {
            /// <summary>
            /// 图片资源的md5值，供进行校验。String类型
            /// </summary>
            [JsonProperty("md5sum")]
            public string Md5Sum { get; set; }

            /// <summary>
            /// 媒体资源的id信息。String类型
            /// </summary>
            [JsonProperty("sdkfileid")]
            public string SdkFileId { get; set; }

            /// <summary>
            /// 图片资源的文件大小。Uint32类型
            /// </summary>
            [JsonProperty("filesize")]
            public int FileSize { get; set; }
        }
    }
}
