using MySql.Data.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Policy;
using PRBD_Framework;
using MySqlX.XDevAPI.Relational;

namespace PRBD_2S_Aurélie
{
    public enum Role {
        Member = 1,
        Admin = 2
    }

    public enum DbType { MsSQL, MySQL }

    public enum EFDatabaseInitMode { CreateIfNotExists, DropCreateIfChanges, DropCreateAlways }

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MySqlModel : Model
    {
        public MySqlModel(EFDatabaseInitMode initMode) : base('name=library-mysql')
        {
            switch (initMode)
            {
                case EFDatabaseInitMode.CreateIfNotExists:
                    Database.SetInitializer<MySqlModel>(new CreateDatabaseIfNotExists<MySqlModel>());
                    break;
                case EFDatabaseInitMode.DropCreateIfChanges:
                    Database.SetInitializer<MySqlModel>(new DropCreateDatabaseIfModelChanges<MySqlModel>());
                    break;
                case EFDatabaseInitMode.DropCreateAlways:
                    Database.SetInitializer<MySqlModel>(new DropCreateDatabaseAlways<MySqlModel>());
                    break;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Properties<bool>().Configure(c => c.HasColumnType("bit"));
        }

        public override void Reseed(string tableName)
        {
            Database.ExecuteSqlCommand($"ALTER TABLE {tableName} AUTO_INCREMENT=1");
        }

    }

    public class MsSqlModel : Model
    {
        public MsSqlModel(EFDatabaseInitMode initMode) : base('name=library-mysql')
        {
            switch (initMode)
            {
                case EFDatabaseInitMode.CreateIfNotExists:
                    Database.SetInitializer<MsSqlModel>(new CreateDatabaseIfNotExists<MsSqlModel>());
                    break;
                case EFDatabaseInitMode.DropCreateIfChanges:
                    Database.SetInitializer<MsSqlModel>(new DropCreateDatabaseIfModelChanges<MsSqlModel>());
                    break;
                case EFDatabaseInitMode.DropCreateAlways:
                    Database.SetInitializer<MsSqlModel>(new DropCreateDatabaseAlways<MsSqlModel>());
                    break;
            }
        }

        public override void Reseed(string tableName)
        {
            Database.ExecuteSqlCommand($"DBCC CHECKIDENT(' {tableName}', RESEED, 0)");
        }

    }

    public abstract class Model : DbContext {

        public Model(string name) : base(name) { }
        public Model() : base("PRBD_2S_Aurélie") {
            // la base de données est supprimée et recréée quand le modèle change
            Database.SetInitializer<Model>(new DropCreateDatabaseIfModelChanges<Model>());
        }

        public static Model CreateModel(DbType type, EFDatabaseInitMode initMode = EFDatabaseInitMode.DropCreateIfChanges)
        {
            Console.WriteLine($"Création du model pour {type}\n");

            switch (type)
            {
                case DbType.MsSQL:
                    return new MsSqlModel(initMode);
                case DbType.MySQL:
                    return new MySqlModel(initMode);
                default:
                    throw new ApplicationException("Base de données indéfinie");

            }
        }

        public abstract void Reseed(string tableName);

        public DbSet<Member> Members { get; set; }

        public Member CreateMember(string pseudo, string password, Role role = Role.Member) {
            var member = Members.Create();
            member.Pseudo = pseudo;
            member.Password = password;
            member.Role = role;
            Members.Add(member);
            return member;
        }

        public void SeedData() {
            if (Members.Count() == 0) {
                Console.Write("Seeding members... ");
                var admin = CreateMember("admin", "admin", Role.Admin);
                var ben = CreateMember("ben", "ben");
                var bruno = CreateMember("bruno", "bruno");
                SaveChanges();
                Console.WriteLine("ok");
            }
        }
    }
}