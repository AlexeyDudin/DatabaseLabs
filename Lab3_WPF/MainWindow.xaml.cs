using DomainLab3;
using MahApps.Metro.Controls;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;

namespace Lab3_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, INotifyPropertyChanged
    {
        private ObservableCollection<Cource> courceList = new ObservableCollection<Cource>();
        private User user;

        public ObservableCollection<Cource> CourceList 
        { 
            get => courceList;
            set
            {
                courceList = value;
                OnPropertyChanged(nameof(CourceList));
            }
        }
        public User User 
        { 
            get => user;
            set
            {
                user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            User = GetUser();
        }

        private string GenerateNewUserName()
        {
            return DateTime.UtcNow.ToString();
        }

        private User GetUser()
        {
            string fileName = "user.id";
            if (File.Exists(fileName))
                return new User() { UserName = File.ReadAllText(fileName) };
            else
            {
                string uniqueValue = GenerateNewUserName();
                File.AppendAllText(fileName, uniqueValue);
                return new User() { UserName = uniqueValue };
            }
        }

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
