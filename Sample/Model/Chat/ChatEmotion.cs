using Newtonsoft.Json;

namespace Sample
{
    public class ChatEmotion : ChatBase
    {
        public ChatEmotionDetail Emotion { get; set; }

        public class ChatEmotionDetail
        {
            /// <summary>
            /// 表情类型，png或者gif.1表示gif 2表示png。Uint32类型
            /// </summary>
            public int Type { get; set; }

            /// <summary>
            /// 表情图片宽度。Uint32类型
            /// </summary>
            public int Width { get; set; }

            /// <summary>
            /// 表情图片高度。Uint32类型
            /// </summary>
            public int Height { get; set; }

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
            /// 资源的文件大小。Uint32类型
            /// </summary>
            [JsonProperty("imagesize")]
            public int ImageSize { get; set; }
        }
    }
}
