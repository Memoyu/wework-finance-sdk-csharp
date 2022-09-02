namespace Sample
{
    public class ChatText : ChatBase
    {
        public ChatTextDetail Text { get; set; }

        public class ChatTextDetail
        {
            /// <summary>
            /// 消息内容。String类型
            /// </summary>
            public string Content { get; set; }
        }
    }
}
