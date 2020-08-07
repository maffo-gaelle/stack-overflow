using System;
using System.ComponentModel.DataAnnotations;
using PRBD_Framework;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace PRBD_2S_Aurélie
{
    public class Comment : EntityBase<Model>
    {
        //relation one avec post et user (c'est une relation one-to-many)
        [Key]
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int PostId { get; set; }
        public virtual Post post { get; set; }
        [Required(ErrorMessage = "Un texte est requis")]
        public string Body { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;

        protected Comment() { }

    }
    
}
