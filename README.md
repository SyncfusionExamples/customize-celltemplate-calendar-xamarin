# How to customize the month cell of Calendar with custom object in Xamarin.Forms (SfCalendar)

You can customize the month cell of [Calendar](https://help.syncfusion.com/xamarin/calendar/getting-started) control with custom object by customizing the

[CellTemplate](https://help.syncfusion.com/cr/xamarin/Syncfusion.SfCalendar.XForms~Syncfusion.SfCalendar.XForms.MonthViewSettings~CellTemplate.html) property in [MonthViewSettings](https://help.syncfusion.com/cr/xamarin/Syncfusion.SfCalendar.XForms~Syncfusion.SfCalendar.XForms.MonthViewSettings.html).

You can also refer the following article.

https://www.syncfusion.com/kb/8588/how-to-customize-the-month-cell-of-calendar-with-custom-object-in-xamarin-forms-sfcalendar

**C#**

**CalendarPageModel** defined as custom object to bind in **CellTemplate**

``` c#
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
```

**XAML**

**CellTamplate** customized with CalendarPageModel custom object property by binding.

``` xml
<ContentPage.Resources>
        <ResourceDictionary>
            <DataTemplate x:Key="weathertemplate">
                <Grid BackgroundColor="White" 
                                  HorizontalOptions="Center">
                    <Grid.RowDefinitions>
                        <RowDefinition Height="*" />
                        <RowDefinition Height="2*" />
                    </Grid.RowDefinitions>
                    <Label Text="{Binding Date.Day}" 
                                       HorizontalTextAlignment="Center"
                                       IsVisible="{Binding IsCurrentMonth}" 
                                       VerticalTextAlignment="Center" 
                                       VerticalOptions="FillAndExpand" 
                                       HorizontalOptions="FillAndExpand" 
                                       TextColor="Black" 
                                       FontAttributes="None" 
                                       FontSize="10" />
                    <Grid Grid.Row="1" 
                                      IsVisible="{Binding IsCurrentMonth}">
                        <Grid.RowDefinitions>
                            <RowDefinition Height="2*" />
                            <RowDefinition Height="*" />
                        </Grid.RowDefinitions>
                        <Grid Grid.Row="1" 
                                          HorizontalOptions="Center" 
                                          VerticalOptions="Center">
                            <Grid.ColumnDefinitions>
                                <ColumnDefinition Width="*" />
                                <ColumnDefinition Width="*" />
                            </Grid.ColumnDefinitions>
                            <Label Text="{Binding Celcius}" 
                                               HorizontalTextAlignment="End" 
                                               VerticalTextAlignment="Center"
                                               VerticalOptions="FillAndExpand" 
                                               HorizontalOptions="FillAndExpand" 
                                               TextColor="Black" 
                                               FontAttributes="None" 
                                               FontSize="8" 
                                               WidthRequest="20" 
                                               HeightRequest="20" />
                            <Label Grid.Column="1" 
                                               Text="C" 
                                               FontSize="8" />
                        </Grid>
                        <Image Source="{Binding Image}" 
                                           WidthRequest="20" 
                                           HeightRequest="20" 
                                           HorizontalOptions="Center" 
                                           VerticalOptions="Center" />
                    </Grid>
                </Grid>
            </DataTemplate>
        </ResourceDictionary>
    </ContentPage.Resources>
    <ContentPage.Content>
        <Syncfusion:SfCalendar x:Name="calendar" >
            <syncfusion:SfCalendar.MonthViewSettings>
                <Syncfusion:MonthViewSettings DateSelectionColor="#dddddd" CellTemplate="{StaticResource weathertemplate}"/>
            </syncfusion:SfCalendar.MonthViewSettings>
        </Syncfusion:SfCalendar>
    </ContentPage.Content>
    <ContentPage.Behaviors>
        <local:CalendarBehavior/>
    </ContentPage.Behaviors>
</ContentPage>
```

**C#**

Using [CellBindingContext](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfCalendar.XForms~Syncfusion.SfCalendar.XForms.MonthCell~CellBindingContext.html) property in the argument of [OnMonthCellLoaded](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfCalendar.XForms~Syncfusion.SfCalendar.XForms.SfCalendar~OnMonthCellLoaded_EV.html) event to pass custom object on **OnMonthCellLoaded** event.

``` c#
private void Calendar_OnMonthCellLoaded(object sender, MonthCellLoadedEventArgs e)
{
    CalendarPageModel cellBindingContext = new CalendarPageModel(e.IsCurrentMonth, e.Date);
    e.CellBindingContext = cellBindingContext;
}
```

**Output**

![MonthCellCustomization](https://github.com/SyncfusionExamples/customize-celltemplate-calendar-xamarin/blob/master/ScreenShot/MonthCellCustomization.gif)
