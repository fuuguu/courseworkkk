using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace courseworkkk.Model
{
    public class Cars : INotifyPropertyChanged
    {
        [Key]
        public int id_Car { get; set; }
        public int id_Client { get; set; }
        private string year;
        public string Year
        {
            get { return year; }
            set
            {
                year = value;
                OnPropertyChanged("Year");
            }
        }
        private string brand;
        public string Brand
        {
            get { return brand; }
            set
            {
                brand = value;
                OnPropertyChanged("Brand");
            }
        }
        private string price;
        public string Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }
        private string power;
        public string Power
        {
            get { return power; }
            set
            {
                power = value;
                OnPropertyChanged("Power");
            }
        }

        private string vin;
        public string Vin
        {
            get { return vin; }
            set
            {
                vin = value;
                OnPropertyChanged("Vin");
            }
        }

        private string imageState;
        public string ImageState
        {
            get { return imageState; }
            set
            {
                imageState = value;
                OnPropertyChanged("ImageState");
            }
        }
        public void Delete(int id)
        {
            using (var connection = new SqliteConnection("Data Source=WEBWorkshop.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"Delete from Cars where id_Car={id}";
                command.ExecuteNonQuery();
            }
        }
        public void Update(int id)
        {
            using (var connection = new SqliteConnection("Data Source=WEBWorkshop.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"UPDATE Cars SET Year='{Year}', Brand='{Brand}',Price='{Price}',Power='{Power}',Vin='{Vin}',ImageState='{ImageState}' where id_Car={id}";
                command.ExecuteNonQuery();
            }
        }
        public void Insert()
        {
            using (var connection = new SqliteConnection("Data Source=WEBWorkshop.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"INSERT INTO Cars (Year,Brand,Price,Power,Vin,ImageState) VALUES ('{Year}', '{Brand}','{Price}','{Power}','{Vin}','{ImageState}')";
                command.ExecuteNonQuery();
            }
        }
        public static ObservableCollection<Cars> AllCars()
        {
            ObservableCollection<Cars> CarsList = new ObservableCollection<Cars>();
            using (var connection = new SqliteConnection("Data Source=WEBWorkshop.db"))
            {
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Cars";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())
                        {
                            Cars car = new Cars();
                            car.id_Car = reader.GetInt32(0);
                            car.Year = reader.GetString(1);
                            car.Brand = reader.GetString(2);
                            car.Price = reader.GetString(3);
                            car.Power = reader.GetString(4);
                            car.Vin = reader.GetString(5);
                            car.ImageState = reader.GetString(6);
                            CarsList.Add(car);
                        }
                    }
                }
            }
            return CarsList;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
