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

namespace SleepFixer
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        //public static readonly StoredItem<bool> canAutoLock = new StoredItem<bool>("canAutoLock", true);
        //public static readonly StoredItem<bool> showSeconds = new StoredItem<bool>("showSeconds", true);
        public static readonly StoredItem<bool> is24Hr = new StoredItem<bool>("is24Hr", true);
        //public static readonly StoredItem<bool> snoozetime = new StoredItem<bool>("snoozetime", true);
        public static readonly StoredItem<bool> holdToStop = new StoredItem<bool>("holdToStop", true);
        public static readonly StoredItem<int> snoozeTime = new StoredItem<int>("snoozeTime", 5);
        public static readonly StoredItem<bool> enableVibration = new StoredItem<bool>("enableVibration", true);
        public static readonly StoredItem<double> sleepHour = new StoredItem<double>("sleepHour", 8);
        
        
        
        
        
        //public static readonly StoredItem<bool> enableSpeech = new StoredItem<bool>("enableSpeech", true);
        //public static readonly StoredItem<bool> alarmSet = new StoredItem<bool>("alarmSet", true);
        //public static readonly StoredItem<TimeSpan> alarmTime = new StoredItem<TimeSpan>("alarmTime", TimeSpan.Zero);
        //public static readonly StoredItem<string> voicePwd = new StoredItem<string>("voicePwd", "");


  
        public SettingsPage()
        {
            InitializeComponent();


            // Initialize the alarm sound.
            
        }

        // When the settings page is no longer the active page, store the settings variables according to the ToggleSwitches.
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            // Stop the sound and vibration.




            SettingsPage.is24Hr.Value = this.hourFmtToggleSwitch.IsChecked.Value;
            // Settings.showSeconds.Value = this.secFmtToggleSwitch.IsChecked.Value;
            SettingsPage.enableVibration.Value = this.vibrationToggleSwitch.IsChecked.Value;
            //Settings.snoozetime.Value = this.snoozetimeToggleSwitch.IsChecked.Value;
            //SettingsPage.alarmstopstyle.Value = this.alarmstopstyleToggleSwitch.IsChecked.Value;
            SettingsPage.snoozeTime.Value = Convert.ToInt32(this.snoozeslider.Value);
            SettingsPage.holdToStop.Value = this.holdToggleSwitch.IsChecked.Value;
            SettingsPage.sleepHour.Value = Convert.ToDouble(this.sleepSlider.Value);



            //  Settings.enableSpeech.Value = this.speechToggleSwitch.IsChecked.Value;
        }

        // When the settings page becomes the active page, initialize the ToggleSwitch status according to the settings.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            base.OnNavigatedTo(e);

            this.hourFmtToggleSwitch.IsChecked = SettingsPage.is24Hr.Value;
            //this.snoozetimeToggleSwitch.IsChecked = Settings.snoozetime.Value;
            this.vibrationToggleSwitch.IsChecked = SettingsPage.enableVibration.Value;
            //this.alarmstopstyleToggleSwitch.IsChecked = SettingsPage.alarmstopstyle.Value;
            this.holdToggleSwitch.IsChecked = SettingsPage.holdToStop.Value;
            this.snoozeslider.Value = SettingsPage.snoozeTime.Value;
            this.sleepSlider.Value = SettingsPage.sleepHour.Value;
           

            this.hourFmtToggleSwitch.Content = SettingsPage.is24Hr.Value ?
                "24 Hour Clock: ON" : "24 Hour Clock: OFF";
            //this.snoozetimeToggleSwitch.Content = Settings.snoozetime.Value ?
            //"Modify snooze time: ON" : "Modify snooze time: OFF";
            this.vibrationToggleSwitch.Content = SettingsPage.enableVibration.Value ?
                "Enable Vibration: ON" : "Enable Vibration: OFF";
            this.holdToggleSwitch.Content = SettingsPage.holdToStop.Value ?
                "Alarm Stop Trigger: Hold " : "Alarm Stop Trigger: Press";
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

        
        private void Hold_Uncheck(object sender, RoutedEventArgs e)
        {
            ToggleSwitch senderToggleSwitch = sender as ToggleSwitch;
            senderToggleSwitch.Content = "Alarm Stop Trigger: Press";
        }

        private void Hold_Check(object sender, RoutedEventArgs e)
        {
            ToggleSwitch senderToggleSwitch = sender as ToggleSwitch;
            senderToggleSwitch.Content = "Alarm Stop Trigger: Hold";
        }

        private void Snooze_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (snoozetext != null)
            {
                snoozeslider.Value = Math.Round(snoozeslider.Value);
                snoozetext.Text = "Snooze Time: " + snoozeslider.Value.ToString()+ " mins"; 
            }
            
            
        }

        private void Random_Click(object sender, RoutedEventArgs e)
        {
            SleepDataControl.Random();
        }

        private void sleepSlider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sleepText != null)
            {
                sleepSlider.Value = Math.Round(sleepSlider.Value*2)/2;
                sleepText.Text = "Desired Sleep Hours: " + sleepSlider.Value.ToString() + " hours";
            }
        }
       
    }
}
