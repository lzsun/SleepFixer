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

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            SecondHand.Begin();
            LongHand.Begin();
            HourHand.Begin();

            int second = DateTime.Now.Second;
            SecondHand.Seek(new TimeSpan(0, 0, second));

            int minutes = DateTime.Now.Minute;
            LongHand.Seek(new TimeSpan(0, minutes, second));
         
            int hour = DateTime.Now.Hour;
            HourHand.Seek(new TimeSpan(hour, minutes, second));

        }


        void timer_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;

            txtDigitalClock.Text = dt.ToString("MM/dd HH:mm:ss");
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