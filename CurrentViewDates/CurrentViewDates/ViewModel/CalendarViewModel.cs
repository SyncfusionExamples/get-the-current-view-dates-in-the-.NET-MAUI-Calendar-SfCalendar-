using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrentViewDates
{
    public class CalendarViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<DateTime> visibleDates = new List<DateTime>();

        public List<DateTime> VisibleDates
        {
            get
            {
                return visibleDates;
            }
            set
            {
                this.visibleDates = value;
                this.OnPropertyChanged(nameof(VisibleDates));
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
