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
    public class Tag : EntityBase<Model>
    {
        public int TagId { get; set; }
        [Required(ErrorMessage = "Un nom de tag est requis")]
        public string TagName { get; set; }

        //Un Tag peut avoir PostTags: relation many to many; un tag n'est pas directement lié à un post selon le diagramme de classe
        public virtual ICollection<PostTag> PostTags { get; set; } = new HashSet<PostTag>();
        protected Tag() { }

        //Une TagName est requis et doit être unique dans la base de données
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var context = validationContext.GetService(typeof(DbContext)) as Model;
            var tagByTagName = (from tag in context.Tags
                                where tag.TagName == TagName
                                select tag
                            ).FirstOrDefault();


            if (TagName == null)
                yield return new ValidationResult("Le nom de tag est requis", new[] { nameof(TagName) });
            if (tagByTagName != null)
                yield return new ValidationResult("Ce tag existe déjà", new[] { nameof(TagName) });
        }

        public override string ToString()
        {
            return $"TagName: {TagName}; TagID: {TagId}";
        }
    }
}
