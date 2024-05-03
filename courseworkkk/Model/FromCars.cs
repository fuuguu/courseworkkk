using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace courseworkkk.Model
{
    internal class FromCars : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int id = int.Parse(value.ToString());
            using (ModelContext db = new ModelContext())
            {
                return db.Cars.Where(p => p.id_Car == id).Select(p => p.Year + "," + p.Brand + "," + p.Price + "," + p.Power + "," + p.Vin).FirstOrDefault();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}