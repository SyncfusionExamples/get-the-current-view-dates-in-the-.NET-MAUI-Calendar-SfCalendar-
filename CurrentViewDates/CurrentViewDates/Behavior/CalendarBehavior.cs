using Syncfusion.Maui.Calendar;
using System.ComponentModel;

namespace CurrentViewDates
{
    public class CalendarBehavior : Behavior<ContentPage>, INotifyPropertyChanged
    {
        private SfCalendar calendar;
        private Button displayDatesButton;
        private CalendarViewModel viewModel;

        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            this.calendar = bindable.FindByName<SfCalendar>("calendar");
            this.calendar.ViewChanged += Calendar_ViewChanged;
            this.displayDatesButton = bindable.FindByName<Button>("displayDatesButton");
            this.displayDatesButton.Clicked += DisplayDatesButton_Clicked;
        }

        private void Calendar_ViewChanged(object sender, CalendarViewChangedEventArgs e)
        {
            if (viewModel == null)
            {
                viewModel = new CalendarViewModel();
            }

            if (e.NewView == CalendarView.Month)
            {
                viewModel.VisibleDates.Clear();
                foreach (var item in e.NewVisibleDates)
                {
                    if (item.ToString("MMMM") == this.calendar.DisplayDate.ToString("MMMM"))
                    {
                        viewModel.VisibleDates.Add(item);
                    }

                }
            }
        }

        private void DisplayDatesButton_Clicked(object sender, EventArgs e)
        {
            string dateList = string.Empty;
            foreach (var item in viewModel.VisibleDates)
            {
                dateList += item.ToString("dddd, dd MMMM yyyy") + "\n";
            }

            App.Current.MainPage.DisplayAlert(this.calendar.DisplayDate.ToString("MMMM") + " month's dates", dateList, "ok");
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            base.OnDetachingFrom(bindable);
            if (this.calendar != null)
            {
                this.calendar.ViewChanged -= Calendar_ViewChanged;
            }

            if (this.displayDatesButton != null)
            {
                this.displayDatesButton.Clicked -= DisplayDatesButton_Clicked;
            }

            this.calendar = null;
            this.displayDatesButton = null;
            this.viewModel = null;
        }
    }
}
