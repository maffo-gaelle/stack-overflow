using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRBD_Framework;

namespace PRBD_2S_Aurélie
{
    public class Tag : EntityBase<Model>
    {
        public int TagId { get; set; }
        public string TagName { get; set; }

        //Un Tag peut avoir plusieurs posts et plusieurs un post peut avoir plusieurs tags relation many to many
        public virtual ICollection<PostTag> PostTags { get; set; } = new HashSet<PostTag>();
         protected Tag() { }

    }
}
