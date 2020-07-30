using PRBD_Framework;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

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

            modelBuilder.Properties<bool>().Configure(c => c.HasColumnType("bit"));
            modelBuilder.Entity<PostTag>().HasKey(posttag => new { posttag.PostId, posttag.TagId });
            modelBuilder.Entity<Vote>().HasKey(vote => new { vote.PostId, vote.UserId });
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

        public Comment CreateComment(int commentId, int userId, User user, int postId, Post post, string body, DateTime timestamp)
        {
            var comment = Comments.Create();
            comment.CommentId = commentId;
            comment.UserId = userId;
            comment.PostId = postId;
            comment.Body = body;
            comment.Timestamp = timestamp;
            //ici on établit les relations dans le sens 1-N avec post et user
            post.Comments.Add(comment);
            user.Comments.Add(comment);

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

        public PostTag CreatePostTag(int tagId, Tag tag, Post post, int postId)
        {
            var posttag = PostTags.Create();
            posttag.TagId = tagId;
            posttag.PostId = postId;
            //ici on établit les relations dans le sens 1-N avec post et tag
            tag.PostTags.Add(posttag);
            post.PostTags.Add(posttag);

            PostTags.Add(posttag);
            SaveChanges();

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

        //public void SeedData()
        //{
        //    if (Users.Count() == 0)
        //    {
        //        Console.Write("Seeding members.............. ");
        //        var admin = CreateUser("admin", "Password1", "administrateur", "admin@test.com", Role.Admin);
        //        var ben = CreateUser("ben", "Password1", "Penelle", "ben@yahoo.com", Role.Member);
        //        var bruno = CreateUser("bruno", "Password1", "bruno", "ben@test.com", Role.Admin);               
        //        SaveChanges();
        //        Console.WriteLine("Member initial ok");

        //        Console.WriteLine("Seeding posts...................")

        //    }
        //}
    }
}
