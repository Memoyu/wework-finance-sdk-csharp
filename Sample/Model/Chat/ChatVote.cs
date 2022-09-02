using Newtonsoft.Json;
using System.Collections.Generic;

namespace Sample
{
    public class ChatVote : ChatBase
    {
        public ChatVoteDetail Vote { get; set; }

        public class ChatVoteDetail
        {
            /// <summary>
            /// 投票主题。String类型
            /// </summary>
            [JsonProperty("votetitle")]
            public string VoteTitle { get; set; }

            /// <summary>
            /// 投票选项，可能多个内容。String数组
            /// </summary>
            [JsonProperty("voteitem")]
            public List<string> VoteItem { get; set; }

            /// <summary>
            /// 投票类型.101发起投票、102参与投票。Uint32类型
            /// </summary>
            [JsonProperty("votetype")]
            public int VoteType { get; set; }

            /// <summary>
            /// 投票id，方便将参与投票消息与发起投票消息进行前后对照。String类型
            /// </summary>
            [JsonProperty("voteid")]
            public string VoteId { get; set; }
        }
    }
}
