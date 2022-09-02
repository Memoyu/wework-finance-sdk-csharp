using Newtonsoft.Json;

namespace Sample
{
    public class ChatVideo : ChatBase
    {
        public ChatVideoDetail Video { get; set; }

        public class ChatVideoDetail
        {
            /// <summary>
            /// 资源的文件大小。Uint32类型
            /// </summary>
            [JsonProperty("file_size")]
            public int FileSize { get; set; }

            /// <summary>
            /// 视频播放长度。Uint32类型
            /// </summary>
            [JsonProperty("play_length")]
            public int PlayLength { get; set; }

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
        }
    }
}
