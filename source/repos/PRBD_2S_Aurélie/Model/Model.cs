using PRBD_Framework;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace PRBD_2S_Aurélie
{
    public class Model : DbContext
    {
        public Model() : base("PRBD_2S_Aurélie")
        {
            Database.SetInitializer<Model>(
                new DropCreateDatabaseIfModelChanges<Model>()
                );
        } 
        public DbSet<Member> Members { get; set; }
        public DbSet<Message> Messages { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTag> PostTags { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Conventions.Remove<IncludeMetadataConvention>();

            modelBuilder.Properties<bool>().Configure(c => c.HasColumnType("bit"));
            //modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique(true);
            //modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique(true);
            modelBuilder.Entity<PostTag>().HasKey(posttag => new { posttag.PostId, posttag.TagId });
            modelBuilder.Entity<Vote>().HasKey(vote => new { vote.PostId, vote.UserId });

        }
        public Member CreateMember(string pseudo, string password, string profile = "", bool isAdmin = false)
        {
            var member = Members.Create();
            member.Pseudo = pseudo;
            member.Password = password;
            member.Profile = profile;
            member.IsAdmin = isAdmin;

            //on ajoute le member au DbSet pour qu'il soit pris en compte par EF
            Members.Add(member);
            return member;
        }

        public Message CreateMessage(Member author, Member recipient, string body, bool isPivate = false)
        {
            var message = Messages.Create();
            message.Body = body;
            message.IsPrivate = isPivate;
            //ici, on établit les relations dans le sens N-1
            message.Author = author;
            message.Recipient = recipient;
            //ici on établit les relations dans le sens 1-N
            author.MessagesSent.Add(message);
            recipient.MessagesReceived.Add(message);

            //on ajoute le member au DbSet pour qu'il soit pris en compte par EF
            Messages.Add(message);
            return message;
        }
    }
}
