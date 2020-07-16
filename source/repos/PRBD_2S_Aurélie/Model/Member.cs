using PRBD_Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRBD_2S_Aurélie
{
    public class Member : EntityBase<Model>
    {
        [Key]
        public string Pseudo { get; set; }
        public string Password { get; set; }
        public string Profile { get; set; }
        public bool IsAdmin { get; set; }
        public string PicturePath { get; set; }

        public virtual ICollection<Member> Followees { get; set; } = new HashSet<Member>();
        public virtual ICollection<Member> Followers { get; set; } = new HashSet<Member>();

        [InverseProperty(nameof(Message.Author))]
        public virtual ICollection<Message> MessagesSent { get; set; } = new HashSet<Message>();
        
        [InverseProperty(nameof(Message.Recipient))]
        public virtual ICollection<Message> MessagesReceived { get; set; } = new HashSet<Message>();

        protected Member() { }

        public void Follow(Member member)
        {
            if(!Followees.Contains(member)) {
                Followees.Add(member);
                member.Followers.Add(this);
            }
        }

        public void Unfollow(Member member)
        {
            Followees.Remove(member);
            member.Followers.Remove(this);
        }

        public Message Send(Member recipient, string body, bool isPrivate = false)
        {
            return Model.CreateMessage(this, recipient, body, isPrivate);
        }

        public void Delete()
        {
            //defait les relations de suivi
            foreach(var followee in Followees)
            {
                followee.Followers.Remove(this);
            }
            Followees.Clear();
            foreach(var follower in Followers)
            {
                follower.Followees.Remove(this);
            }
            Followers.Clear();

            //supprime les messages envoyés ou reçus
            Model.Messages.RemoveRange(MessagesSent);
            MessagesSent.Clear();
            Model.Messages.RemoveRange(MessagesReceived);
            MessagesReceived.Clear();

            //supprime le membre lui-meme
            Model.Members.Remove(this);
        }
        public override string ToString()
        {
            return $"<Member: Pseudo={Pseudo}, " +
                $"#followees={Followees.Count}, " +
                $"#followers={Followers.Count}" +
                $"sent={MessagesSent.Count}, " +
                $"#received={MessagesReceived.Count}>";
        }
    }
}
