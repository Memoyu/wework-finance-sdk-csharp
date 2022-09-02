using Newtonsoft.Json;

namespace Sample
{
    public class ChatLink : ChatBase
    {
        public ChatLinkDetail Link { get; set; }

        public class ChatLinkDetail
        {
            /// <summary>
            /// 链接url地址。String类型
            /// </summary>
            [JsonProperty("link_url")]
            public string LinkUrl { get; set; }

            /// <summary>
            /// 链接图片url。String类型
            /// </summary>
            [JsonProperty("image_url")]
            public string ImageUrl { get; set; }

            /// <summary>
            /// 消息标题。String类型
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// 消息描述。String类型
            /// </summary>
            public string Description { get; set; }
        }
    }
}
