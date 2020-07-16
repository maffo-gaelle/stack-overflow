using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRBD_Framework;

namespace PRBD_2S_Aurélie
{
    public class Vote : EntityBase<Model>
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public int UpDown { get; set; }

        protected Vote() { }
    }
}
