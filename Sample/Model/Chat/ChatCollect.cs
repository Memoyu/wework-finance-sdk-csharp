using Newtonsoft.Json;

namespace Sample
{
    public class ChatCollect : ChatBase
    {
        public ChatCollectDetail Vote { get; set; }

        public class ChatCollectDetail
        {
            /// <summary>
            /// 填表消息所在的群名称。String类型
            /// </summary>
            [JsonProperty("room_name")]
            public string RoomName { get; set; }

            /// <summary>
            /// 创建者在群中的名字。String类型
            /// </summary>
            public string Creator { get; set; }

            /// <summary>
            /// 创建的时间。String类型
            /// </summary>
            [JsonProperty("create_time")]
            public string CreateTime { get; set; }

            /// <summary>
            /// 表名。String类型
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// 表内容。json数组类型
            /// </summary>
            public string Details { get; set; }

            /// <summary>
            /// 表项id。Uint64类型
            /// </summary>
            public long Id { get; set; }

            /// <summary>
            /// 表项名称。String类型
            /// </summary>
            public string Ques { get; set; }

            /// <summary>
            /// 表项类型，有Text(文本),Number(数字),Date(日期),Time(时间)。String类型
            /// </summary>
            public string Type { get; set; }
        }
    }
}
