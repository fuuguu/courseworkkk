using courseworkkk.Model;
using courseworkkk.View;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using XAct.Library.Settings;

namespace courseworkkk.ViewModel
{
    [Keyless]
    class SalesmanViewModel : INotifyPropertyChanged
    {
        private SalesmanView window;
        private Salesman selectedSalesman;
        ModelContext db = new ModelContext();
        public ObservableCollection<Salesman> SalesmanList { get; set; }
        public Salesman selectedSalesmen
        {
            get { return selectedSalesmen; }
            set
            {
                selectedSalesmen = value;
                OnPropertyChanged("SelectedExecutorProject");
            }
        }
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      Salesman salesMan = new Salesman();
                      salesMan.FirstName = window.FirstName.Text;
                      salesMan.SecondName = window.SecondName.Text;
                      salesMan.LastName = window.LastName.Text;
                      salesMan.Passport = window.Passport.Text;
                      salesMan.Address = window.Address.Text;
                      salesMan.Insert();
                      SalesmanList.Add(salesMan);
                  }));
            }
        }
        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new RelayCommand(obj =>
                  {
                      Salesman salesMan = obj as Salesman;
                      salesMan.Delete(salesMan.id_Salesman);
                      SalesmanList.Remove(salesMan);
                      if (salesMan == null) return;
                      db.Salesman.Remove(salesMan);
                      db.SaveChanges();
                  }));
            }
        }
        private RelayCommand updateCommand;
        public RelayCommand UpdateCommand
        {
            get
            {
                return updateCommand ??
                  (updateCommand = new RelayCommand(obj =>
                  {
                      Salesman executorProject = obj as Salesman;
                      executorProject.FirstName = window.FirstName.Text;
                      executorProject.SecondName = window.SecondName.Text;
                      executorProject.LastName = window.LastName.Text;
                      executorProject.Passport = window.Passport.Text;
                      executorProject.Address = window.Address.Text;
                      executorProject.Update(executorProject.id_Salesman);
                  }));
            }
        }
        public SalesmanViewModel(SalesmanView window)
        {
            this.window = window;
            SalesmanList = new ObservableCollection<Salesman>();
            using (var connection = new SqliteConnection("Data Source=WEBWorkshop.db"))
            {
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Salesman";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())
                        {
                            Salesman salesman = new Salesman();
                            salesman.FirstName = reader.GetValue(0).ToString();
                            salesman.SecondName = reader.GetValue(1).ToString();
                            salesman.LastName = reader.GetValue(2).ToString();
                            salesman.Passport = reader.GetValue(4).ToString();
                            salesman.Address = reader.GetValue(5).ToString();
                            SalesmanList.Add(salesman);
                        }
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
