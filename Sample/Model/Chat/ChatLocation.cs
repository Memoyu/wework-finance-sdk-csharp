namespace Sample
{
    public class ChatLocation : ChatBase
    {
        public ChatLocationDetail Location { get; set; }

        public class ChatLocationDetail
        {
            /// <summary>
            /// 经度，单位double
            /// </summary>
            public double Longitude { get; set; }

            /// <summary>
            /// 纬度，单位double
            /// </summary>
            public double Latitude { get; set; }

            /// <summary>
            /// 地址信息。String类型
            /// </summary>
            public string Address { get; set; }

            /// <summary>
            /// 位置信息的title。String类型
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// 缩放比例。Uint32类型
            /// </summary>
            public int Zoom { get; set; }
        }
    }
}
