using PRBD_Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;


using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations.Schema;
using System;


namespace PRBD_2S_Aurélie
{
    public enum Role { Admin = 1, Member = 0}

    public class User : EntityBase<Model>
    {

        public int UserId { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }
        public Role Role { get; set; } = Role.Member;

        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public virtual ICollection<Vote> Votes { get; set; } = new HashSet<Vote>();

        public User() { }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var context = validationContext.GetService(typeof(DbContext)) as Model;
            var userByUserName = (from user in context.Users
                            where user.UserName == UserName
                            select user
                            ).FirstOrDefault();

            var userByEmail = (from user in context.Users
                               where user.Email == Email
                               select user
                               ).FirstOrDefault();

            if (UserName == null)
                yield return new ValidationResult("Le nom d'utilisateur est requis", new[] { nameof(UserName) });
            if (userByUserName != null)
                 yield return new ValidationResult("Cet utiisateur existe déjà", new[] { nameof(UserName) });
            if (Email == null)
                yield return new ValidationResult("L'email est requis", new[] { nameof(Email) });
            if (userByEmail != null)
                yield return new ValidationResult("Cet email existe déjà", new[] { nameof(Email) });

        }

        public override string ToString()
        {
            return UserName + " " + FullName;
        }
    }
}
       