using Newtonsoft.Json;

namespace Sample
{
    public class ChatWeApp : ChatBase
    {
        public ChatWeAppDetail WeApp { get; set; }

        public class ChatWeAppDetail
        {
            /// <summary>
            /// 用户名称。String类型
            /// </summary>
            [JsonProperty("username")]
            public string UserName { get; set; }

            /// <summary>
            /// 小程序名称。String类型
            /// </summary>
            [JsonProperty("displayname")]
            public string DisplayName { get; set; }

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
