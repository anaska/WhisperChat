using System;

namespace Chat
{
    [Serializable]
    public class MessageContainer
    {
        public IClient From { get; set; }
        public IClient To { get; set; }

        public DateTime TimeStamp { get; set; }
        public string Message { get; set; }

    }
}
