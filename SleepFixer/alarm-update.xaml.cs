using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Part.Resources;

using Microsoft.Devices.Sensors;
using Microsoft.Xna.Framework;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Phone.Scheduler;


namespace Part
{
    public partial class MainPage : PhoneApplicationPage
    {

        Motion motion;
        DispatcherTimer timer = new DispatcherTimer();
        // Constructor
        public MainPage()
        {

            InitializeComponent();

            //DateTime alarm = DateTime.Now;

            //string t = DateTime.Now.ToString("t");
            DateTime t = new DateTime(2013, 5, 9, 9, 20, 3);
            Txb_timedisplay.Text = t.ToString();



            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();

            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            
        }

        /// <summary>
        /// Tick function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer_Tick(object sender, EventArgs e)
        {
            DateTime dt = Convert.ToDateTime(Txb_timedisplay.Text);
           // DateTime alarm = Convert.ToDateTime(Txb_timeremain.Text);
           

            //Calculate the time, get the time
            TimeSpan remain = DateTime.Now- dt;

            //Txb_timeremain.Text = string.Format("{0}:{1}:{2}",alarm.Hour-dt.Hour>0?alarm.Hour:dt.Hour,alarm.Minute-dt.Minute>0?alarm.Minute:dt.Minute,alarm.Second-dt.Second>0?alarm.Second:dt.Second);
            //Txb_timeremain.Text = string.Format("{0}:{1}:{2}", remain.Hours, remain.Minutes, remain.Seconds);
            Txb_timeremain.Text = Convert.ToString(remain).Substring(1,7);
        }


        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Check to see whether the Motion API is supported on the device.
            if (!Motion.IsSupported)
            {
                MessageBox.Show("the Motion API is not supported on this device.");
                return;
            }

            // If the Motion object is null, initialize it and add a CurrentValueChanged
            // event handler.
            if (motion == null)
            {
                motion = new Motion();
                motion.TimeBetweenUpdates = TimeSpan.FromMilliseconds(20);
                motion.CurrentValueChanged +=
                    new EventHandler<SensorReadingEventArgs<MotionReading>>(motion_CurrentValueChanged);
            }

            // Try to start the Motion API.
            try
            {
                motion.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("unable to start the Motion API.");
            }
        }

        void motion_CurrentValueChanged(object sender, SensorReadingEventArgs<MotionReading> e)
        {
            // This event arrives on a background thread. Use BeginInvoke to call
            // CurrentValueChanged on the UI thread.
            Dispatcher.BeginInvoke(() => CurrentValueChanged(e.SensorReading));
        }


        /// <summary>
        /// Use e to detect if the phone is reversed.
        /// if e >0.9 then it is considered as sleep
        /// </summary>
        /// <param name="e"></param>
        private void CurrentValueChanged(MotionReading e)
        {
            // Check to see if the Motion data is valid.
            if (motion.IsDataValid)
            {

            }
        }


        /// <summary>
        /// Snooze function
        /// Also need to stop alarm,
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Snooze(object sender, RoutedEventArgs e)
        {
            DateTime begin = Convert.ToDateTime(Txb_timeremain.Text);
            Alarm a = new Alarm("snooze");
            a.BeginTime = begin;
            a.ExpirationTime = begin.AddMinutes(8.0);
            //a.Sound = 
            Txb_timeremain.Text = (begin.AddMinutes(8.0)).ToString();
            Txb_timedisplay.Text = (a.ExpirationTime.AddMinutes(8.0)).ToString();
        }


        /// <summary>
        /// Stop the alarm
        /// Go back to the homepage, not sure this code will work or not
        /// Following the format on windows phone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Stop(object sender, RoutedEventArgs e)
        {
            //timer.Stop();
            NavigationService.Navigate(new Uri("blabla.xaml", UriKind.Relative));
        }







        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}