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

using Microsoft.Devices.Sensors;
using Microsoft.Xna.Framework;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Phone.Scheduler;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Devices;
using System.Windows.Media.Imaging;


namespace SleepFixer
{
    public partial class AlarmPage : PhoneApplicationPage
    {
        
        private DateTime alarmTime;
        private TimeSpan sleepTime;
        private TimeSpan wakeupTime;
        public static Alarm alarm;


        private bool isSleepSet = false;
        private int state;

        private SoundEffectInstance alarmSound;


        Motion motion;
        DispatcherTimer timer = new DispatcherTimer();

        
        // Constructor
        public AlarmPage()
        {

            InitializeComponent();

            //alarmTime = new DateTime(2013, 5, 10, 9, 20, 3);

            alarmTimeText.Text = SettingsPage.is24Hr.Value ? alarmTime.ToString("HH:mm") : alarmTime.ToString("hh:mmtt");

            alarmSound = AlarmSound.Alarm.CreateInstance();
            alarmSound.IsLooped = true;

            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            //timer.Start();

        }


        /// <summary>
        /// Tick function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer_Tick(object sender, EventArgs e)
        {

            
            //Calculate the time, get the time


            if (alarmTime > DateTime.Now)
            {
                TimeSpan remain = alarmTime - DateTime.Now;
                remainText.Text = remain.ToString(@"hh\:mm\:ss");
            }
            else
                startAlarm();
            //Txb_timeremain.Text = string.Format("{0}:{1}:{2}",alarm.Hour-dt.Hour>0?alarm.Hour:dt.Hour,alarm.Minute-dt.Minute>0?alarm.Minute:dt.Minute,alarm.Second-dt.Second>0?alarm.Second:dt.Second);
            //Txb_timeremain.Text = string.Format("{0}:{1}:{2}", remain.Hours, remain.Minutes, remain.Seconds);
            
        }

        private void startAlarm()
        {
            state = 1;
            //Change the image
            (img.Content as Image).Source= new BitmapImage(new Uri("/Images/alarm.snooze.jpg",UriKind.Relative));

            if(SettingsPage.enableVibration.Value)
                VibrateController.Default.Start(TimeSpan.FromSeconds(.5));
            alarmSound.Play();
            //Bold Remain Time
            remainText.Foreground = new SolidColorBrush(Colors.Red);
            if (remainText.FontWeight != FontWeights.Bold)
                remainText.FontWeight = FontWeights.Bold;
            else
                remainText.FontWeight = FontWeights.Normal;

            backButton.Content = SettingsPage.holdToStop.Value ? "Hold to Stop Alarm" : "Stop Alarm";
        }

        private void setSleepTime()
        {
            isSleepSet = true;
            sleepTime = DateTime.Now.TimeOfDay;
            instructionText.Text = "Sleep Time Recorded: " + DateTime.Now.ToString("hh:mmtt")+ System.Environment.NewLine;
        }


        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //Start Alarm
            state = 0;
            isSleepSet = false;
            alarmTime = new DateTime(Convert.ToInt64(NavigationContext.QueryString["alarm"]));
            sleepTime = DateTime.Now.TimeOfDay;
            timer.Start();

            remainText.Text = (alarmTime - DateTime.Now).ToString(@"hh\:mm\:ss");
            alarmTimeText.Text = SettingsPage.is24Hr.Value ? alarmTime.ToString("HH:mm") : alarmTime.ToString("hh:mmtt");




            AlarmPage.alarm = new Alarm("alarm");
            AlarmPage.alarm.BeginTime = alarmTime;
            AlarmPage.alarm.ExpirationTime = DateTime.MaxValue;
            AlarmPage.alarm.RecurrenceType = RecurrenceInterval.None;
            AlarmPage.alarm.Sound = new Uri("Audio/alarm.wav", UriKind.Relative);
            AlarmPage.alarm.Content = "Alarm ringing!";
            

            //Motion
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
                if (e.Gravity.Z >= 0.9)
                {
                    if (isSleepSet == false)
                    {
                        setSleepTime();
                    }         
                }
            }
        }


        /*/// <summary>
        /// Snooze function
        /// Also need to stop alarm,
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Snooze(object sender, RoutedEventArgs e)
        {
            DateTime begin = Convert.ToDateTime(remainText.Text);
            Alarm a = new Alarm("snooze");
            a.BeginTime = begin;
            a.ExpirationTime = begin.AddMinutes(8.0);
            //a.Sound = 
            remainText.Text = (begin.AddMinutes(8.0)).ToString();
            //Txb_timedisplay.Text = (a.ExpirationTime.AddMinutes(8.0)).ToString();
        }


        /// <summary>
        /// Stop the alarm
        /// Go back to the homepage, not sure this code will work or not
        /// Following the format on windows phone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            //timer.Stop();
            NavigationService.Navigate(new Uri("blabla.xaml", UriKind.Relative));
        }*/

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            if(state == 0)
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            if (SettingsPage.holdToStop.Value == false)
            {
                if (state == 1)
                {
                    alarmSound.Stop();
                    wakeupTime = DateTime.Now.TimeOfDay;
                    window.IsOpen = true;
                }
                else if (state == 2)
                {
                    alarmSound.Stop();
                    wakeupTime = DateTime.Now.TimeOfDay;
                    window.IsOpen = true;
                }
            }
            

        }
        private void moodBack_Click(object sender, RoutedEventArgs e)
        {   
            //MessageBox.Show("Sleep: " + sleepTime.ToString(@"hh\:mm") + " Wakeup: " + wakeupTime.ToString(@"hh\:mm")+" Mood: "+Convert.ToInt32(moodSlider.Value));
            if (SleepDataControl.checkExist(DateTime.Now) == true)
            {
                SleepDataControl.Add(new SleepData(DateTime.Today, sleepTime, wakeupTime, Convert.ToInt32(moodSlider.Value), true));
            }
            else
            {
                SleepDataControl.Add(new SleepData(DateTime.Today, sleepTime, wakeupTime, Convert.ToInt32(moodSlider.Value), false));
            }
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {
            if (state == 0)
            {
                if (isSleepSet == false)
                {
                    setSleepTime();
                }
                
                
                //MessageBox.Show("Sleep Time:" + sleepTime.ToString());
            }
            else if (state == 1)
            {
                state = 2;
                //alarmTime = DateTime.Now.AddMinutes(SettingsPage.snoozeTime.Value);
                alarmTime = DateTime.Now.AddSeconds(SettingsPage.snoozeTime.Value);
                alarmSound.Stop();
                timeleftText.Text = "Snoozing: ";
                //remainText.Foreground = new SolidColorBrush(Colors.White);
                remainText.FontWeight = FontWeights.Normal;
                //MessageBox.Show("Snooze");
            }
        }

        private void moodSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(moodSlider != null)
                moodSlider.Value = Math.Round(moodSlider.Value);
        }

        private void back_Hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
            

            if (SettingsPage.holdToStop.Value == true)
            {
                if (state == 1)
                {
                    alarmSound.Stop();
                    wakeupTime = DateTime.Now.TimeOfDay;
                    window.IsOpen = true;
                }
                else if (state == 2)
                {
                    alarmSound.Stop();
                    wakeupTime = DateTime.Now.TimeOfDay;
                    window.IsOpen = true;
                }
            }
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