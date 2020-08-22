using PRBD_2S_Aurélie.Properties;
using PRBD_Framework;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace PRBD_2S_Aurélie
{
    public enum AppMessages
    {
        MSG_DETAILS_POST,
        MSG_NEW_TAG,
        MSG_CLOSE_TAB,
        MSG_NEW_QUESTION,
        MSG_QUESTION_CHANGED,
        MSG_AUTHOR_POST,
        MSG_RESPONSE_ADDED,
        MSG_ANSWERS_POST,
        MSG_UPDATE_QUESTION,
        MSG_DELETE_QUESTION,
        MSG_QUESTION_DELETED,
        MSG_ANSWER_DELETE,
        MSG_ANSWER_DELETED,
        MSG_ANSWER_ADDED,
        MSG_DISPLAY_POSTOFTAG,
        MSG_POSTTAG_DELETED,
        MSG_POSTTAG_ADDED,
        MSG_VOTE_CHANGED,
        MSG_TAG_DELETED,
        MSG_TAG_UPDATED,
        MSG_NOT_CURRENT
    }

    public partial class App : ApplicationBase 
    { 
        public static User CurrentUser { get; set; }
        //public static Model Model { get; }
        public static Model Model { get; set; }
        public static bool tagModified = false;
        public App()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Culture);
            Model = new Model();
            Model.SeedData();
        }

        
    }
}
