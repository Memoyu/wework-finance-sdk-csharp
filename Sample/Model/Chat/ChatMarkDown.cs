namespace Sample
{
    public class ChatMarkDown : ChatBase
    {
        public ChatMarkDownDetail Info { get; set; }

        public class ChatMarkDownDetail
        {
            /// <summary>
            /// markdown消息内容，目前为机器人发出的消息
            /// </summary>
            public string Content { get; set; }
        }
    }
}
