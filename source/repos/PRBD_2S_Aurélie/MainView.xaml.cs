using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;

namespace PRBD_2S_Aurélie
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainView : Window, INotifyPropertyChanged
    {
        public string Filter { get; set; }
        public ICommand ApplyFilter { get; set; }
        public ObservableCollection<Member> Members { get; set; }

        public  MainView() 
        {
            DataContext = this;
            var model = new Model();
            Members = new ObservableCollection<Member>(model.Members);
            ApplyFilter = new ApplyFilterCommand(ApplyFilterAction);

            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void ApplyFilterAction()
        {
            Console.WriteLine("search clicked! " + Filter);
            var model = new Model();
            var query = from m in model.Members
                        where m.Pseudo.Contains(Filter) || m.Profile.Contains(Filter)
                        select m;
            Members = new ObservableCollection<Member>(query);
            Console.WriteLine($"{query.Count()} members found");
        }
       
    }

    public class ApplyFilterCommand : ICommand
    {
        private Action action;

        public event EventHandler CanExecuteChanged;

        public ApplyFilterCommand(Action action)
        {
            this.action = action;
        }
        public bool CanExecute(Object parameter)
        {
            return true;
        }

        public void Execute(Object parameter)
        {
            action();
        }
    }
}
