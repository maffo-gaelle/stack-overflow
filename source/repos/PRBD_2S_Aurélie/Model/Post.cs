using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using PRBD_Framework;
using System.Data.Entity;

namespace PRBD_2S_Aurélie

{
    public class Post : EntityBase<Model>
    {
        public int PostId { get; set; }
        public virtual User Author { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public int? AcceptedAnswerId { get; set; }//relation de post vers post: one to one
        public virtual Post AcceptedAnswer { get; set; }
        public int? ParentId { get; set; }

        public virtual Post Parent { get; set; }//relation de post vers post: one to many

        //Un Tag peut avoir plusieurs posts et un post peut avoir plusieurs tags relation many to many avec ces deux relation, EF va créer une table de jointure
        //et on pourra à partir de la configuration modifier le nom des colonnes de la table de jointure donc je dirai que un dbset de postTag ne me servida plus
        public virtual ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();
        public virtual ICollection<PostTag> Posttags { get; set; } = new HashSet<PostTag>();

        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public virtual ICollection<Vote> Votes { get; set; } = new HashSet<Vote>();
        //relation one-to-many avec post(parent) et post(answers)
        public virtual ICollection<Post> Answers { get; set; } = new HashSet<Post>();
        protected Post() { }

    }
}
