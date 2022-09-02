using Newtonsoft.Json;
using System.Collections.Generic;

namespace Sample
{
    public class ChatNews : ChatBase
    {
        public ChatNewsDetail Info { get; set; }

        public class ChatNewsDetail
        {
            /// <summary>
            /// 图文消息数组，每个item结构包含title、description、url、picurl等结构
            /// </summary>
            public List<ChatNewsDetailItem> Item { get; set; }
        }

        public class ChatNewsDetailItem
        {
            /// <summary>
            /// 图文消息标题。String类型
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// 图文消息描述。String类型
            /// </summary>
            public string Description { get; set; }

            /// <summary>
            /// 图文消息点击跳转地址。String类型
            /// </summary>
            public string Url { get; set; }

            /// <summary>
            /// 图文消息配图的url。String类型
            /// </summary>
            [JsonProperty("picurl")]
            public string PicUrl { get; set; }
        }
    }
}
