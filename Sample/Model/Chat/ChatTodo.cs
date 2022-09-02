namespace Sample
{
    public class ChatTodo : ChatBase
    {
        public ChatTodoDetail Todo { get; set; }

        public class ChatTodoDetail
        {
            /// <summary>
            /// 待办的来源文本。String类型
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// 待办的具体内容。String类型
            /// </summary>
            public string Content { get; set; }
        }
    }
}
