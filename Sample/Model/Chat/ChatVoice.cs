using Newtonsoft.Json;

namespace Sample
{
    public class ChatVoice : ChatBase
    {
        public ChatVoiceDetail Voice { get; set; }

        public class ChatVoiceDetail
        {
            /// <summary>
            /// 语音消息大小。Uint32类型
            /// </summary>
            [JsonProperty("voice_size")]
            public int VoiceSize { get; set; }

            /// <summary>
            /// 播放长度。Uint32类型
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
