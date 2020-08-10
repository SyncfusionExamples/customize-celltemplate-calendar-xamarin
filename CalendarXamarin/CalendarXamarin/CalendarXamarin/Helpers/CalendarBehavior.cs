using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CalendarXamarin
{
    internal class CalendarBehavior : Behavior<ContentPage>
    {
        SfCalendar calendar;
        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            calendar = bindable.FindByName<SfCalendar>("calendar");
            calendar.OnMonthCellLoaded += Calendar_OnMonthCellLoaded;
        }
        private void Calendar_OnMonthCellLoaded(object sender, MonthCellLoadedEventArgs e)
        {
            CalendarPageModel cellBindingContext = new CalendarPageModel(e.IsCurrentMonth, e.Date);
            e.CellBindingContext = cellBindingContext;
        }
        protected override void OnDetachingFrom(ContentPage bindable)
        {
            base.OnDetachingFrom(bindable);
            calendar.OnMonthCellLoaded -= Calendar_OnMonthCellLoaded;
            calendar = null;
        }
    }
}
