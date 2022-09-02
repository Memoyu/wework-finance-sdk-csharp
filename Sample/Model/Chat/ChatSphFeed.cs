using Newtonsoft.Json;

namespace Sample
{
    public class ChatSphFeed : ChatBase
    {
        [JsonProperty("sphfeed")]
        public ChatSphFeedDetail SphFeed { get; set; }

        public class ChatSphFeedDetail
        {
            /// <summary>
            /// 视频号消息类型。2 图片、4 视频、9 直播。Uint32类型
            /// </summary>
            [JsonProperty("feed_type")]
            public int FeedType { get; set; }

            /// <summary>
            /// 视频号账号名称。String类型
            /// </summary>
            [JsonProperty("sph_name")]
            public string SphName { get; set; }

            /// <summary>
            /// 视频号消息描述。String类型
            /// </summary>
            [JsonProperty("feed_desc")]
            public string FeedDesc { get; set; }
        }
    }
}
