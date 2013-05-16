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
          

        private void Display_Click(object sender, RoutedEventArgs e)
        {
            SleepDataControl.LoadJogs();
            radCalendar.AppointmentSource = new HistoryData();
        }

        private void Random_Click(object sender, RoutedEventArgs e)
        {
            
            SleepDataControl.Random();
        }
    }
}