using PRBD_Framework;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PRBD_2S_Aurélie
{
    public class PostTag : EntityBase<Model>
    {
        //relation one-to-many avec post et tag: un post peut avoir plusieurs posttag et un tag peut avoir plusieurs posttag
        //mais un posttag appartient juste à un tag et un post. Donc ici on a la relation one
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
        public virtual Post Post { get; set; }
        public int PostId { get; set; }

        protected PostTag () { }

        public override string ToString()
        {
            return " tag: " + Tag.TagName + " post: " + Post.Title;
        }
    }
}
