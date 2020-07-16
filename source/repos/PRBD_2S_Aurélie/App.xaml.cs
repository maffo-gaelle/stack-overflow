using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PRBD_2S_Aurélie
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            TestingEntityFramework();
        }

        private void TestingEntityFramework()
        {
            //création du modèle
            var model = new Model();

            //Activation du log dans la console
            model.Database.Log = Console.Write;

            //affichage du nombre d'instance de l'entité 'Member'
            Console.WriteLine(model.Members.Count());

            //afficher le pseudo de tous les membres
            foreach (var m in model.Members)
            {
                Console.WriteLine(m.Pseudo);
            }

            //affichage du pseudo de tous les membres triés de manière décroissante
            foreach (var m in model.Members.OrderByDescending(m => m.Pseudo))
            {
                Console.WriteLine(m.Pseudo);
            }

            //Vérifie s'il existe un membre donc la clée est "test".si oui, l'efface
            var member = model.Members.Find("test");
            if (member != null)
            {
                member.Delete();
                model.SaveChanges();
            }

            //Créer un nouveau membre "test" et l'inserer dans la collection
            member = model.CreateMember("test", "test");
            model.SaveChanges();

            //Avec linq, sélectionne les membres dont le pseudo ou le profil contient 'a'
            var q1 = from m in model.Members
                     where m.Pseudo.Contains("a") || m.Profile.Contains("a")
                     select m;
            foreach (var m in q1)
            {
                Console.WriteLine(m.Pseudo);
            }

            //Selectionne certaines proprietés des membres
            var q2 = from m in model.Members
                     select new
                     {
                         m.Pseudo,
                         profile = m.Profile ?? "<NULL>"
                     };
            foreach (var m in q2)
            {
                Console.WriteLine(m);
            }

            //Selectionne les membres ayant envoyés ou reçcu au moins un message
            var q3 = from m in model.Members
                     let r = m.MessagesReceived.Count
                     let s = m.MessagesSent.Count
                     where
                     r + s > 0
                     select new { m.Pseudo, Received = r, Sent = s };
            foreach (var m in q3)
            {
                Console.WriteLine(m);
            }
        }
    }
}
