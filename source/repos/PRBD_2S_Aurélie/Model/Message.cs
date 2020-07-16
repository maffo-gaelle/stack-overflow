using PRBD_Framework;
using System;

namespace PRBD_2S_Aurélie
{
    public class Message : EntityBase<Model>
    {
        public int MessageId { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public string Body { get; set; }
        public bool IsPrivate { get; set; }

        public virtual Member Author { get; set; }

        public virtual Member Recipient { get; set; }

        protected Message() { }
    }
}
