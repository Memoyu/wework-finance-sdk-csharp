using Newtonsoft.Json;
using System.Collections.Generic;

namespace Sample
{
    public class ChatChatRecord : ChatBase
    {
        [JsonProperty("chatrecord")]
        public ChatChatRecordDetail ChatRecord { get; set; }

        public class ChatChatRecordDetail
        {
            /// <summary>
            /// 聊天记录标题。String类型
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// 消息记录内的消息内容，批量数据
            /// </summary>
            public List<ChatChatRecordItem> Item { get; set; }
        }

        public class ChatChatRecordItem
        {
            /// <summary>
            /// 每条聊天记录的具体消息类型：ChatRecordText/ ChatRecordFile/ ChatRecordImage/ ChatRecordVideo/ ChatRecordLink/ ChatRecordLocation/ ChatRecordMixed ….
            /// </summary>
            public string Type { get; set; }

            /// <summary>
            /// 消息时间，utc时间，单位秒。
            /// </summary>
            [JsonProperty("msgtime")]
            public long MsgTime { get; set; }

            /// <summary>
            /// 消息内容。Json串，内容为对应类型的json。String类型
            /// </summary>
            public string Content { get; set; }

            /// <summary>
            /// 是否来自群会话。Bool类型
            /// </summary>
            [JsonProperty("from_chatroom")]
            public bool FromChatRoom { get; set; }
        }
    }
}
