using System;
using System.Linq;
using System.Windows.Interop;

namespace PRBD_2S_Aurélie
{
    class Program
    {
        static void TestingEntityFramework()
        {
            using (var model = new Model())
            {

                foreach (var member in model.Members)
                {
                    member.Delete();
                }
                model.SaveChanges();

                //création de quelques membres
                var ben = model.CreateMember("ben", "ben");
                var bruno = model.CreateMember("bruno", "bruno");
                var admin = model.CreateMember("admin", "admin", isAdmin: true);
                admin.Pseudo = "admin"; admin.Password = "admin";

                //création de quelques relations entre les membres
                ben.Follow(admin);
                ben.Follow(bruno);
                bruno.Follow(admin);

                //Avant la création des messages
                Console.WriteLine($"\nBefore creating messages:\n{ben}\n{bruno}\n{admin}");

                //Envoi de quelques messages
                var msg1 = ben.Send(bruno, "Hello de =ben");
                var msg2 = ben.Send(admin, "heloo de ben");
                var msg3 = bruno.Send(admin, "hello de bruno");

                //état des membres avant la sauvegarde
                Console.WriteLine($"\nBefore SaveChanges():\n{ben}\n{bruno}\n{admin}");

                model.SaveChanges();

                //état des membres après la sauvegarde
                Console.WriteLine($"\nAfter SaveChanges():\n{ben}\n{bruno}\n{admin}");

                //Console.WriteLine(ben.GetType().Name);


            }
        }

        private static void Query1(Model model)
        {
            var q1 = from msg in model.Messages
                     where msg.Author.Followers.Count >= 1
                     select msg;
            foreach (var m in q1)
                Console.WriteLine($"Message #{m.MessageId} emitted by {m.Author.Pseudo} who has {m.Author.Followers.Count} follower(s)");
        }

        private static void Query2(Model model)
        {
            var q2 = from mbr in model.Members
                     group mbr by mbr.Pseudo.Substring(0, 1) into g1
                     orderby g1.Key descending
                     select g1;
            foreach (var g in q2)
                Console.WriteLine(g.Key.ToUpper() + "has" + g.Count() + " member(s) : " +
                    string.Join(", ", from m in g select m.Pseudo));
        }

        private static void Query3(Model model)
        {
            var q3 = from msg in model.Messages
                     group msg by msg.Author.Pseudo.Substring(0, 1) into g1
                     orderby g1.Key ascending
                     select g1;
            foreach (var g in q3)
                Console.WriteLine(g.Key + " - " + g.Count());
        }

        private static void Query4(Model model)
        {
            var q4 = from msg in model.Messages
                     let follows = msg.Author.Followees.Contains(msg.Recipient)
                     let followed = msg.Author.Followers.Contains(msg.Recipient)
                     where follows || followed
                     select new { msg, follows, followed };
            foreach (var r in q4)
            {
                var rel = r.followed && r.follows ? "is mutual friend with" : r.followed ? "is followed by" : "follows";
                Console.WriteLine($"Message #{r.msg.MessageId}: author:{r.msg.Author.Pseudo} {rel} recipient={r.msg.Recipient.Pseudo}");
            }
        }

        private static void Query5(Model model)
        {
            var q5 = from m1 in model.Members
                     from m2 in model.Members
                     where m1.Pseudo.CompareTo(m2.Pseudo) < 0
                     where m1.Followees.Contains(m2) && m1.Followers.Contains(m2)
                     select new { membre1 = m1.Pseudo, membre2 = m2.Pseudo };
            foreach (var mbr in q5)
                Console.WriteLine(mbr);
        }

        private static void Query6(Model model)
        {
            var q6 = from msg in model.Messages
                     let month = (msg.DateTime.Year * 100 + msg.DateTime.Month).ToString().Insert(4, "-")
                     group msg by month into g1
                     select g1;
            foreach (var g in q6)
            {
                Console.WriteLine(g.Key);
                foreach (var m in g)
                    Console.WriteLine($"    {m.MessageId}  -  {m.Author.Pseudo}  -  {m.Recipient.Pseudo}  -  {m.Body}");

            }
        }

        private static void ExercicesLinq()
        {
            Console.WriteLine("\n\nExercices LINQ\n==================");
            using (var model = new Model())
            {
                model.Members.Count();
                Console.WriteLine("\nQurery1\n---------");
                Query1(model);
                Console.WriteLine("\nQurery2\n---------");
                Query2(model);
                Console.WriteLine("\nQurery3\n---------");
                Query3(model);
                Console.WriteLine("\nQurery4\n---------");
                Query4(model);
                Console.WriteLine("\nQurery5\n---------");
                Query5(model);
                Console.WriteLine("\nQurery6\n---------");
                Query6(model);
            }
        }
        static void Main(string[] args)
        {
            //TestingEntityFramework();
            ExercicesLinq();
        }

    }
}
