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


            this.radChart1.Series[0].ItemsSource = new double[] { 7.5, 8, 8.5, 7.2, 6.5, 9 ,8};
        }

        
    }
}