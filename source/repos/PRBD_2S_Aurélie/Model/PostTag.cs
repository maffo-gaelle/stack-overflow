using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRBD_Framework;

namespace PRBD_2S_Aurélie
{
    public class PostTag : EntityBase<Model>
    {
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
        public virtual Post Post { get; set; }
        public int PostId { get; set; }

        protected PostTag () { }
    }
}
