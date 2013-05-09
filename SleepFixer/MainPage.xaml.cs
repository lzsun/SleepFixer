using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Sleep_Fixer.Resources;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Input;

namespace Sleep_Fixer
{
    public partial class MainPage : PhoneApplicationPage
    {

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            PhoneApplicationService phoneAppService = PhoneApplicationService.Current;
            phoneAppService.UserIdleDetectionMode = IdleDetectionMode.Disabled;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }



        private void timer_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            TimeSpan currentTime = now.TimeOfDay;

            ((CompositeTransform)hourHand.RenderTransform).Rotation = (currentTime.Hours % 12) * 30 + currentTime.Minutes * 0.5 - 90;
            ((CompositeTransform)minuteHand.RenderTransform).Rotation = currentTime.Minutes * 6 - 90;
            ((CompositeTransform)secondHand.RenderTransform).Rotation = currentTime.Seconds * 6 - 90;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Convert the click position to the ordinary de cartesan coordinate, 
            // so that we can easily calculate the tangent value, hence the angle value.
            Point position = e.GetPosition(this.clockFaceImage);
            position.X -= this.clockFaceImage.Width / 2;
            position.Y -= this.clockFaceImage.Height / 2;

            // Get the angle between the current tap position and 12 o'clock. 
            // Multiplying by 180 / Math.PI is to convert the arctan result from radius to degree.
            double tappedAngle = 180 - Math.Atan2(position.X, position.Y) * 180 / Math.PI;
            tappedAngle = Math.Round(tappedAngle / 6) * 6;
            Time.Text = tappedAngle.ToString();

            /*this.angleToGo = tappedAngle;

            if (this.clockHandState == ClockHandState.HourHandSelected)
            {
                // Since we now know the tapped angle, 
                // we need to deduce the current clock hand angle from the tapped angle,
                // so we can get the actual amount of angles that we need to go.
                this.UpdateAngleToGo(this.hourHandRotation.Rotation);

                // Round the angle value to a time of 30 degrees, 
                // because the min adjust value for the hour hand is 1 hour, which corresponds to 30 degrees.
                this.angleToGo = Math.Round(this.angleToGo / 30) * 30;

                this.clockHandDispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            }
            else if (this.clockHandState == ClockHandState.MinuteHandSelected)
            {
                this.UpdateAngleToGo(this.minuteHandRotation.Rotation);

                // Round the angle value to to a time of 6 degrees, 
                // because the min adjust value for the minute hand is 1 minute, which corresponds to 6 degrees
                this.angleToGo = Math.Round(this.angleToGo / 6) * 6;

                this.clockHandDispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            }*/
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