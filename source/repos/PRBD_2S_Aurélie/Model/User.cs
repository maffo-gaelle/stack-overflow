using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using PRBD_Framework;

namespace PRBD_2S_Aurélie
{
    public enum Role { Admin = 1, Member = 0}

    public class User : EntityBase<Model>
    {

        public int UserId { get; set; }

        [Required(ErrorMessage = "Le nom d'utilisateur est requis")]
        [MinLength(3, ErrorMessage = "Le nom d'utilisateur doit contenir au moins 3 caractères")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Un mot de passe est requis")]
        [MinLength(8, ErrorMessage = "Le nom d'utilisateur doit contenir au moins 8 caractères")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Un prénom est requis")]
        [MinLength(3, ErrorMessage = "Le prénom doit contenir au moins 3 caractères")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Un email est requis")]
        [RegularExpression("^[A-Za-z0-9](([_\\.\\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\\.\\-]?[a-zA-Z0-9]+)*)\\.([A-Za-z]{2,})$")]
        public string Email { get; set; }
        public Role Role { get; set; } = Role.Member;

        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public virtual ICollection<Vote> Votes { get; set; } = new HashSet<Vote>();

        protected User() { }

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
    }
}
       