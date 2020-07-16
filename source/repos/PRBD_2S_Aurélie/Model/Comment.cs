using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRBD_Framework;


namespace PRBD_2S_Aurélie
{
    public class Comment : EntityBase<Model>
    {
        [Key]
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        //Pour une one-to-many faire par exemple un user et un userId int comme ici; many on met un user et one on met une liste de comment
        public int PostId { get; set; }
        public virtual Post post { get; set; }
        public string Body { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;

        protected Comment() { }

        internal static void RemoveRange(DbSet<Comment> comments)
        {
            throw new NotImplementedException();
        }
    }
    
}
