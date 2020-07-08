using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PRBD_Framework;

namespace PRBD_2S_Aurélie 
{
    public class Member : EntityBase<Model> {
        [Key]
        public string Pseudo { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        protected Member() { }

        public override string ToString() {
            return $"<Member: Pseudo={Pseudo}, Role={Role.ToString()}>";
        }
    }
}