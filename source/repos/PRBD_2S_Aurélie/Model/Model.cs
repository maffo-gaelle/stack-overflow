﻿using PRBD_Framework;
using System;
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

        public User CreateUser(int userId, string userName, string password, string fullName, string email, Role role)
        {
            var user = Users.Create();
            user.UserId = userId;
            user.UserName = userName;
            user.Password = password;
            user.FullName = fullName;
            user.Email = email;
            user.Role = role;

            Users.Add(user);
            return user;
        }

        public Post CreatePost(int postId, int authorId, Post parent, User author, string title, string body, DateTime timestamp, int acceptedAnswerId, int parentId)
        {
            var post = Posts.Create();
            post.PostId = postId;
            post.AuthorId = authorId;
            post.Title = title;
            post.Body = body;
            post.Timestamp = timestamp;
            post.AcceptedAnswerId = acceptedAnswerId;
            post.ParentId = parentId;
            //ici on établit les relations dans le sens 1-N avec post et user
            author.Posts.Add(post);
            //la relation 1-N avec post pour les reponses?
            if(title == null && parent != null)
            {
                parent.Answers.Add(post);
            }
            //la relation 1-1 avec post pour accptedAnswerId?

            Posts.Add(post);
            return post;

        }

        public Tag CreateTag(int tagId, string tagName)
        {
            var tag = Tags.Create();
            tag.TagId = tagId;
            tag.TagName = tagName;

            Tags.Add(tag);
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
            return comment;
        }

        public Vote CreateVote(int userId, User user, Post post, int postId, int updown)
        {
            var vote = Votes.Create();
            vote.UserId = userId;
            vote.PostId = postId;
            vote.UpDown = updown;
            //ici on établit les relations dans le sens 1-N avec post et user
            post.Votes.Add(vote);
            user.Votes.Add(vote);

            Votes.Add(vote);
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
            return posttag;
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
    }
}
