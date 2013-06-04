using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SleepFixer.Resources;
using System.Text;
using Telerik.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls.Calendar;
using System.Windows.Media.Imaging;



namespace SleepFixer
{
    public partial class HistoryPage : PhoneApplicationPage
    {
        /// <summary>
        /// note: have to instance sleep before use sleepfixer
        /// </summary>
        //public SleepDataRoot sf = new SleepDataRoot{Sleep=new List<SleepData>()};
        // Constructor


        public HistoryPage()
        {            
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //SleepDataControl.LoadJogs();
            radCalendar.AppointmentSource = new HistoryData();
            DataTemplateSelector dts = radCalendar.ItemTemplateSelector;
            radCalendar.ItemTemplateSelector = null;
            radCalendar.ItemTemplateSelector = dts;
        }

        private void Random_Click(object sender, RoutedEventArgs e)
        {
            
            SleepDataControl.Random();

            //SleepDataControl.LoadJogs();
            radCalendar.AppointmentSource = new HistoryData();
            DataTemplateSelector dts = radCalendar.ItemTemplateSelector;
            radCalendar.ItemTemplateSelector = null;
            radCalendar.ItemTemplateSelector = dts;
        }


        private void radCalendar_DisplayDateChanged(object sender, ValueChangedEventArgs<object> e)
        {
            DataTemplateSelector dts = radCalendar.ItemTemplateSelector;
            radCalendar.ItemTemplateSelector = null;
            radCalendar.ItemTemplateSelector = dts;
        }


        private void radCalendar_ItemTap(object sender, CalendarItemTapEventArgs e)
        {
            if (e.Item.Appointments != null && e.Item.Appointments.Count() > 0)
            {
                SleepData data = (e.Item.Appointments.Cast<SampleAppointment>().ToArray())[0].Data;
                TimeSpan sleepHours = data.WakeupTime >= data.SleepTime ? data.WakeupTime - data.SleepTime : (data.WakeupTime - data.SleepTime).Add(new TimeSpan(24, 0, 0));
                /*detail.Text = "S: " + (SettingsPage.is24Hr.Value ? data.SleeptimeString : DateTime.Today.Add(data.SleepTime).ToString("hh:mmt"))
                            + System.Environment.NewLine + "W: " + (SettingsPage.is24Hr.Value ? data.WakeupTimeString : DateTime.Today.Add(data.WakeupTime).ToString("hh:mmt"))
                            + System.Environment.NewLine + sleepHours.ToString(@"h\hmm\m")
                            + System.Environment.NewLine + "M: " + data.Mood.ToString();*/
                DetailSleepTime.Text = (SettingsPage.is24Hr.Value ? data.SleeptimeString : DateTime.Today.Add(data.SleepTime).ToString("hh:mmtt"));
                DetailWakeupTime.Text = (SettingsPage.is24Hr.Value ? data.WakeupTimeString : DateTime.Today.Add(data.WakeupTime).ToString("hh:mmtt"));
                DetailSleepHours.Text = sleepHours.ToString(@"h\hmm\m");
                DetailMood.Source = new BitmapImage(new Uri("/Images/mood."+data.Mood.ToString()+".png", UriKind.Relative));
                window.IsOpen = true;
            }
                
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            window.IsOpen = false;
        }

    }

    public class MoodSelector : DataTemplateSelector
    {
        public DataTemplate MoodTemplate1
        {
            get;
            set;
        }

        public DataTemplate MoodTemplate2
        {
            get;
            set;
        }

        public DataTemplate MoodTemplate3
        {
            get;
            set;
        }

        public DataTemplate MoodTemplate4
        {
            get;
            set;
        }

        public DataTemplate MoodTemplate5
        {
            get;
            set;
        }

        public DataTemplate MoodTemplate
        {
            get;
            set;
        }



        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            CalendarButtonContentInfo info = item as CalendarButtonContentInfo;
            CalendarButton button = container as CalendarButton;
            if (!button.IsFromCurrentView) return MoodTemplate;
            if (info.Date == null) return null;
            if (button.Appointments != null && button.Appointments.Count() > 0 && (button.Appointments.Cast<SampleAppointment>().ToArray())[0].Mood == "1")
                return MoodTemplate1;
            else if (button.Appointments != null && button.Appointments.Count() > 0 && (button.Appointments.Cast<SampleAppointment>().ToArray())[0].Mood == "2")
                return MoodTemplate2;
            else if (button.Appointments != null && button.Appointments.Count() > 0 && (button.Appointments.Cast<SampleAppointment>().ToArray())[0].Mood == "3")
                return MoodTemplate3;
            else if (button.Appointments != null && button.Appointments.Count() > 0 && (button.Appointments.Cast<SampleAppointment>().ToArray())[0].Mood == "4")
                return MoodTemplate4;
            else if (button.Appointments != null && button.Appointments.Count() > 0 && (button.Appointments.Cast<SampleAppointment>().ToArray())[0].Mood == "5")
                return MoodTemplate5;
            else
                return MoodTemplate;
        }
    }
}