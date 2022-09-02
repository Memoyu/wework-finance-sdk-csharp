namespace Sample
{
    /// <summary>
    /// 注：切换企业日志不是真正的消息，与上述消息结构不完全相同。部分公共字段没有值
    /// </summary>
    public class ChatSwitch : ChatBase
    {
        /// <summary>
        /// 消息发送时间戳，utc时间，ms单位。
        /// </summary>
        public long Time { get; set; }

        /// <summary>
        /// 具体为切换企业的成员的userid。String类型
        /// </summary>
        public string User { get; set; }
    }
}
