using courseworkkk.Model;
using courseworkkk.View;
using Microsoft.Data.Sqlite;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Xml;
using System.IO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using XAct.Users;

namespace courseworkkk.ViewModel
{
    class CarsViewModel : INotifyPropertyChanged
    {
        [Key]
        private CarsView window;
        private Cars selectedCar;
        ModelContext db = new ModelContext();
        private string ImageFileName { get; set; }
        public ObservableCollection<Cars> CarsList { get; set; }
        public ObservableCollection<Client> ClientsList { get; set; }
        public ObservableCollection<Salesman> SalesmanList { get; set; }
        public Cars SelectedCar
        {
            get { return selectedCar; }
            set
            {
                selectedCar = value;
                OnPropertyChanged("SelectedCar");
            }
        }
        public CarsViewModel(CarsView window)
        {
            this.window = window;
            CarsList = new ObservableCollection<Cars>();
            db.Database.EnsureCreated();
            db.Cars.Load();
            db.Client.Load();
            db.Salesman.Load();
            ClientsList = db.Client.Local.ToObservableCollection();
            CarsList = db.Cars.Local.ToObservableCollection();
            SalesmanList = db.Salesman.Local.ToObservableCollection();
        }
        private RelayCommand loadCommand;
        public RelayCommand LoadCommand
        {
            get
            {
                return loadCommand ??
                  (loadCommand = new RelayCommand(obj =>
                  {
                      OpenFileDialog ofd = new OpenFileDialog();
                      ofd.Filter = "*.jpg|*.jpg|*.bmp|*.bmp";
                      if (ofd.ShowDialog() == true)
                      {
                          BitmapImage myBitmapImage = new BitmapImage();
                          ImageFileName = ofd.FileName;
                          myBitmapImage.BeginInit();
                          myBitmapImage.UriSource = new Uri(ofd.FileName);
                          myBitmapImage.DecodePixelWidth = 200;
                          myBitmapImage.EndInit();
                          window.StateImage.Source = myBitmapImage;
                      }
                  }));
            }
        }
        private RelayCommand insertCommand;
        public RelayCommand InsertCommand
        {
            get
            {
                return insertCommand ??
                  (insertCommand = new RelayCommand(obj =>
                  {
                      Cars car = new Cars();
                      car.id_Client = (window.cbClient.SelectedItem as Client).id_Client;
                      car.Year = window.year.Text;
                      car.Brand = window.brand.Text;
                      car.Price = window.price.Text;
                      car.Power = window.power.Text;
                      car.Vin = window.vin.Text;
                      car.ImageState = Convert.ToBase64String(File.ReadAllBytes(ImageFileName));
                      db.Cars.Add(car);
                      db.SaveChanges();
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
                      Cars car = obj as Cars;
                      CarsList.Remove(car);
                      if (car == null) return;
                      db.Cars.Remove(car);
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

                      Cars project = selectedCar as Cars;
                      if (project == null) return;
                      project.Year = window.year.Text;
                      project.Brand = window.brand.Text;
                      project.Price = window.price.Text;
                      project.Power = window.power.Text;
                      project.Vin = window.vin.Text;
                      project.ImageState = Convert.ToBase64String(File.ReadAllBytes(ImageFileName));
                      db.Entry(project).State = EntityState.Modified;
                      db.SaveChanges();
                  }));
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