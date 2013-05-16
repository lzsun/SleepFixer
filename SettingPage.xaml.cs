/* 
    Copyright (c) 2012 Microsoft Corporation.  All rights reserved.
    Use of this sample source code is subject to the terms of the Microsoft license 
    agreement under which you licensed this sample source code and is provided AS-IS.
    If you did not accept the terms of the license agreement, you are not authorized 
    to use this sample source code.  For the terms of the license, please see the 
    license agreement between you and Microsoft.
  
    To see all Code Samples for Windows Phone, visit http://go.microsoft.com/fwlink/?LinkID=219604 
  
*/
using System;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;
using Microsoft.Devices;
using Microsoft.Phone.Controls;
using Microsoft.Xna.Framework.Audio;
using Windows.Phone.Speech.Recognition;

namespace AlarmClockWithVoice
{
    public partial class Settings : PhoneApplicationPage
    {
        public static readonly StoredItem<bool> canAutoLock = new StoredItem<bool>("canAutoLock", true);
        public static readonly StoredItem<bool> showSeconds = new StoredItem<bool>("showSeconds", true);
        public static readonly StoredItem<bool> is24Hr = new StoredItem<bool>("is24Hr", true);
        public static readonly StoredItem<bool> snoozetime = new StoredItem<bool>("snoozetime", true);
        public static readonly StoredItem<bool> alarmstopstyle = new StoredItem<bool>("alarmstopstyle", true);
       public static readonly StoredItem<int> snoozetimerecorder = new StoredItem<int>("snoozetimerecorder", 0);
        
        
        
        
        public static readonly StoredItem<bool> enableVibration = new StoredItem<bool>("enableVibration", true);
        public static readonly StoredItem<bool> enableSpeech = new StoredItem<bool>("enableSpeech", true);
        public static readonly StoredItem<bool> alarmSet = new StoredItem<bool>("alarmSet", true);
        public static readonly StoredItem<TimeSpan> alarmTime = new StoredItem<TimeSpan>("alarmTime", TimeSpan.Zero);
        public static readonly StoredItem<string> voicePwd = new StoredItem<string>("voicePwd", "");

        DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };

  
        public Settings()
        {
            InitializeComponent();

            this.timer.Tick += Timer_Tick;

            // Initialize the alarm sound.
            
        }

        // When the settings page is no longer the active page, store the settings variables according to the ToggleSwitches.
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            // Stop the sound and vibration.
            this.timer.Stop();
            //this.alarmSound.Stop();



            Settings.is24Hr.Value = this.hourFmtToggleSwitch.IsChecked.Value;
            // Settings.showSeconds.Value = this.secFmtToggleSwitch.IsChecked.Value;
            Settings.enableVibration.Value = this.vibrationToggleSwitch.IsChecked.Value;
            //Settings.snoozetime.Value = this.snoozetimeToggleSwitch.IsChecked.Value;
            Settings.alarmstopstyle.Value = this.alarmstopstyleToggleSwitch.IsChecked.Value;
            Settings.snoozetimerecorder.Value = Convert.ToInt32(this.snoozeslider.Value);



            //  Settings.enableSpeech.Value = this.speechToggleSwitch.IsChecked.Value;
        }

        // When the settings page becomes the active page, initialize the ToggleSwitch status according to the settings.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            base.OnNavigatedTo(e);

            this.hourFmtToggleSwitch.IsChecked = Settings.is24Hr.Value;
            //this.snoozetimeToggleSwitch.IsChecked = Settings.snoozetime.Value;
            this.vibrationToggleSwitch.IsChecked = Settings.enableVibration.Value;
            this.alarmstopstyleToggleSwitch.IsChecked = Settings.alarmstopstyle.Value;
            this.snoozeslider.Value = Settings.snoozetimerecorder.Value;
           

            this.hourFmtToggleSwitch.Content = Settings.is24Hr.Value ?
                "24 Hour Clock: ON" : "24 Hour Clock: OFF";
            //this.snoozetimeToggleSwitch.Content = Settings.snoozetime.Value ?
            //"Modify snooze time: ON" : "Modify snooze time: OFF";
            this.vibrationToggleSwitch.Content = Settings.enableVibration.Value ?
                "Enable Vibration: ON" : "Enable Vibration: OFF";
            this.alarmstopstyleToggleSwitch.Content = Settings.alarmstopstyle.Value ?
                "Hold to stop: ON" : "Hold to stop: OFF";
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Vibrate for half of a second.
            VibrateController.Default.Start(TimeSpan.FromSeconds(.5));
        }

        private void ToggleSwitch_UnChecked(object sender, RoutedEventArgs e)
        {
            ToggleSwitch senderToggleSwitch = sender as ToggleSwitch;
            string toggleSwitchString = senderToggleSwitch.Content as string;


            senderToggleSwitch.Content = toggleSwitchString.Substring(0, toggleSwitchString.Length - "ON".Length) + "OFF";
        }

        private void ToggleSwitch_Checked(object sender, RoutedEventArgs e)
        {
            ToggleSwitch senderToggleSwitch = sender as ToggleSwitch;
            string toggleSwitchString = senderToggleSwitch.Content as string;
            senderToggleSwitch.Content = toggleSwitchString.Substring(0, toggleSwitchString.Length - "OFF".Length) + "ON";
        }

        
        private void Holder(object sender, RoutedEventArgs e)
        {
            ToggleSwitch senderToggleSwitch = sender as ToggleSwitch;
            string toggleSwitchString = senderToggleSwitch.Content as string;
            senderToggleSwitch.Content = "Hold to stop";
        }

        private void Simple(object sender, RoutedEventArgs e)
        {
            ToggleSwitch senderToggleSwitch = sender as ToggleSwitch;
            string toggleSwitchString = senderToggleSwitch.Content as string;
            senderToggleSwitch.Content = "Simple stop";
        }

        private void Snooze(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //string b = Convert.ToInt32(e.OldValue).ToString();

            
            string s=Convert.ToInt32(e.NewValue).ToString();
            if (snoozetext != null)
            {
                snoozeslider.Value = Math.Round(snoozeslider.Value);
                snoozetext.Text = string.Format("Set your snooze time as: {0} min", s);
            }
            
            
        }

        //private void Displayslider(object sender, RoutedEventArgs e)
        //{
        //    snoozeSet.Value = 0;
        //    snoozeSetNum.Text = "0";
        //    snoozeSet.Visibility = Visibility.Visible;
        //    snoozeSetNum.Visibility = Visibility.Visible;
        //    sure.Visibility = Visibility.Visible;
        //}

        //private void Hideslider(object sender, RoutedEventArgs e)
        //{
            
        //    snoozeSet.Visibility = Visibility.Collapsed;
        //    snoozeSetNum.Visibility = Visibility.Collapsed;
        //    sure.Visibility = Visibility.Collapsed;
        //}

        //private void ValidateNum(object sender, System.Windows.Controls.TextChangedEventArgs e)
        //{
        //    int temp;
        //    if (int.TryParse(snoozeSetNum.Text, out temp))
        //    {
        //        if (temp >= 0 && temp <= 20)
        //        {
        //            snoozeSetNum.Text = temp.ToString();
        //        }
        //        else
        //        {
        //            //snoozeSet.Visibility = Visibility.Collapsed;
        //            MessageBox.Show("Use a num between 0 and 20");
        //        }
        //    }
        //    else
        //    {
        //        if (snoozeSetNum.Text != string.Empty)
        //        {
        //            MessageBox.Show("Please use a validated number");
        //            snoozeSetNum.Text = temp.ToString();
        //        }
        //    }
        //}

        //private void Confirm(object sender, RoutedEventArgs e)
        //{
        //    snoozeSet.Visibility = Visibility.Collapsed;
        //    snoozeSetNum.Visibility = Visibility.Collapsed;
        //    sure.Visibility = Visibility.Collapsed;
        //    //store the num*******************
        //    Settings.snoozetimerecorder.Value=Convert.ToInt32(snoozeSetNum.Text);
        //    snoozetimeToggleSwitch.IsChecked = true;
        //}

        //private void Hide(object sender, RoutedEventArgs e)
        //{
        //    snoozeSet.Visibility = Visibility.Collapsed;
        //}

       
    }
}
