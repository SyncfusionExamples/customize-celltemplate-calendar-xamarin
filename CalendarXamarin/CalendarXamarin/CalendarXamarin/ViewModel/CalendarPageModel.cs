using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using System.ComponentModel;
using System.Windows.Input;

namespace CalendarXamarin
{
    public class CalendarPageModel : INotifyPropertyChanged
    {
        Random random = new Random();
        public bool IsCurrentMonth { get; set; }
        public string Title { get; set; }
        public string Temperature { get; set; }
        public string Wind { get; set; }
        public string Humidity { get; set; }
        public DateTime Date { get; set; }

        private double celcius;

        public double Celcius
        {
            get
            {
                return celcius;
            }

            set
            {
                celcius = value; OnPropertyChanged("Celcius");
            }
        }

        private string image;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Image
        {
            get
            {
                return image;
            }

            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }

        public CalendarPageModel(bool iscurrentmonth, DateTime date)
        {
            IsCurrentMonth = iscurrentmonth;
            this.Date = date;
            this.Title = " ";
            this.Temperature = " ";
            this.Wind = " ";
            this.Humidity = " ";
            if (IsCurrentMonth)
            {
                UpdateCelcius();

            }
        }

        private void UpdateCelcius()
        {
            if (this.Date.Day % 5 == 0)
            {
                this.Celcius = 50;
            }
            else if (this.Date.Day % 3 == 0)
            {
                this.Celcius = 80;
            }
            else
            {
                this.Celcius = 120;
            }

            if (Celcius > 10 && Celcius < 60)
            {
                this.Image = "sunrain.png";
            }
            else if (Celcius > 60 && Celcius < 100)
            {
                this.Image = "sun1.png";
            }
            else
            {
                this.Image = "sun.png";
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
