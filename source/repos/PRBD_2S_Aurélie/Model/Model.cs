using PRBD_Framework;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows.Navigation;

namespace PRBD_2S_Aurélie
{
    public class Model : DbContext
    {
        public Model() : base("PRBD_2S_Aurélie")
        {
            Console.WriteLine("cc");
            Database.SetInitializer<Model>(
                new DropCreateDatabaseIfModelChanges<Model>()
                );
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTag> PostTags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Properties<bool>().Configure(c => c.HasColumnType("bit"));
            modelBuilder.Entity<PostTag>().HasKey(posttag => new { posttag.PostId, posttag.TagId });
            modelBuilder.Entity<Vote>().HasKey(vote => new { vote.PostId, vote.UserId });
        }

        public Post CreatePost(string title, string body, User author, DateTime dateTime)
        {
            var post = Posts.Create();
            post.Title = title;
            post.Body = body;
            post.Author = author;
            post.Timestamp = dateTime;

            Posts.Add(post);

            return post;
        }

        public Post CreateAnswer(User user, Post parent, string body)
        {
            var post = Posts.Create();

            post.Author = user;
            post.Parent = parent;
            post.Body = body;
            //parent.Answers.Add(post);

            Posts.Add(post);

            return post;
        }
        public User CreateUser(string userName, string password, string fullName, string email, Role role = Role.Member)
        {
            var user = Users.Create();

            user.UserName = userName;
            user.Password = password;
            user.FullName = fullName;
            user.Email = email;
            user.Role = role;

            Users.Add(user);
            
            return user;
        }

        public Tag CreateTag(string tagName)
        {
            var tag = Tags.Create();
            tag.TagName = tagName;

            Tags.Add(tag);
            SaveChanges();

            return tag;
        }

        public Comment CreateComment(User user, Post post, string body)
        {
            var comment = Comments.Create();
            comment.Body = body;
            comment.post = post;
            comment.User = user;
            //ici on établit les relations dans le sens 1-N avec post et user
            //post.Comments.Add(comment);
            //user.Comments.Add(comment);

            Comments.Add(comment);
            SaveChanges();

            return comment;
        }

        public Vote CreateVote(User user, Post post, int updown)
        {
            var vote = Votes.Create();
            vote.UpDown = updown;
            //ici on établit les relations dans le sens 1-N avec post et user
            post.Votes.Add(vote);
            user.Votes.Add(vote);

            Votes.Add(vote);
            SaveChanges();

            return vote;
        }

        public PostTag CreatePostTag(Tag tag, Post post)
        {
            var posttag = PostTags.Create();
            posttag.Post = post;
            posttag.Tag = tag;
            //ici on établit les relations dans le sens 1-N avec post et tag
            //tag.PostTags.Add(posttag);
            //post.PostTags.Add(posttag);

            //PostTags.Add(posttag);
            //SaveChanges();

            return posttag;
        }

        public void ClearDatabase()
        {
            Users.RemoveRange(Users);
            Posts.RemoveRange(Posts);
            Comments.RemoveRange(Comments);
            Votes.RemoveRange(Votes);
            Tags.RemoveRange(Tags);
            PostTags.RemoveRange(PostTags);

            SaveChanges();
        }

        public void SeedData()
        {
            if (Users.Count() == 0)
            {
                Console.Write("Seeding members.............. ");
                
                var aurelie = CreateUser("aurelie", "Password1", "Maffo", "aurelie@test.com", Role.Admin);
                var bruno = CreateUser("bruno", "Password1", "Lacroix", "bruno@test.com", Role.Admin);
                SaveChanges();
                Console.WriteLine("Member initial ok");

                Console.WriteLine("Seeding posts...................");

                var post1 = CreatePost("Première question", "Le Body de la première question", aurelie, new DateTime(2020, 7, 3, 8, 34, 0));
                var post2 = CreatePost("Deuxième question", "Le Body de la Deuxième question", aurelie, new DateTime(2020, 7, 27, 8, 34, 0));
                var post3 = CreatePost("Troisième question", "Le Body de la troisieme question", bruno, new DateTime(2020, 8, 5, 8, 34, 0));

                SaveChanges();

                Console.Write("Seeding Tag.............. ");

                var tag1 = CreateTag("HTML");
                var tag2 = CreateTag("CSS");
                var tag3 = CreateTag("GIT");
                SaveChanges();

                Console.WriteLine("Seeding Vote.....................");
                var vote1 = CreateVote(aurelie, post3, -1);
                var vote2 = CreateVote(bruno, post3, -1);
                var vote3 = CreateVote(bruno, post1, 1);

                Console.WriteLine("Seeding answers...................");
                var answer1 = CreateAnswer(bruno, post1, "bruno répond à la première question");
                var answer2 = CreateAnswer(aurelie, post3, "aurelie répond à la troisième question");
                var answer3 = CreateAnswer(aurelie, post3, "aurelie répond à nouveau à la troisième question");

                SaveChanges();

                Console.WriteLine("Seeding comments ..........................");
                var comment1 = CreateComment(aurelie, post1, "aurelie commente la première question");
                var comment2 = CreateComment(bruno, post1, "bruno commente la première question");
                var comment3 = CreateComment(aurelie, answer1, "aurelie commente une réponse");
            }
        }
    }
}
