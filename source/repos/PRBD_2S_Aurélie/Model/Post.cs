using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using PRBD_Framework;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRBD_2S_Aurélie

{
    public class Post : EntityBase<Model>
    {
        public int PostId { get; set; }
        public virtual User Author { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public virtual Post AcceptedAnswer { get; set; }
        public virtual Post Parent { get; set; }//relation de post vers post: one to many
        //Posttags recupère tous les tags du post et c'est une relation one-to-many avec posttag et posttag a une relation one-to-many avec tag
        public virtual ICollection<PostTag> PostTags { get; set; } = new HashSet<PostTag>();
        //le lien qu'il y'a entre post et tag est posttag. Dans ma collection posttag, je seclectionne tous les postag liés à mon post et dans mon IEnumurale tag, je recupère tous les tags de ma collection posttag
        //puisque ici, la seule relation qu'il y'a entre post et tag est posttag et que un tag est constitué des post et d'un tag,
        //dans un IEnumerable, je select tous les tags de la collection posttags ci-haut. et je fait juste un get parce que je ne modifie rien
        [NotMapped]//je ne veux pas cet enregistrement dans la base de données
        public IEnumerable<Tag> Tags { get => PostTags.Select(posttag => posttag.Tag); }
        //relation one-to-many avec Comment
        //[InverseProperty(nameof(Comment.post))]
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        //relation one-to-many avec vote
        public virtual ICollection<Vote> Votes { get; set; } = new HashSet<Vote>();
        //relation one-to-many avec post(parent) et post(answers)
        public virtual ICollection<Post> Answers { get; set; } = new HashSet<Post>();
        protected Post() { }

        //nombre de reponses
        public int NbAnswers
        {
            get
            {
                return (from answer in Answers
                        select answer).Count();
            }

        }

        //le score de chaque vote en faisant la somme des differents score de ma liste
        public int Score
        {
            get
            {
                return Votes.Sum(vote => vote.UpDown);
            }
        }

        //le nombre de commentaires de chaque post
        public int NbComments
        {
            get
            {
                return (from comment in Comments select comment).Count();
            }
        }
    }
}
