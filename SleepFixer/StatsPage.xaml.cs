using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace SleepFixer
{
    public partial class StatsPage : PhoneApplicationPage
    {
        
        
        public StatsPage()
        {
            InitializeComponent();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            sleepHoursByDayControl.Calc();
            moodControl.Calc();
            sleepHoursControl.update();
            sleepScoreControl.update();
            sleepTimeControl.update();
        }
        
    }
}