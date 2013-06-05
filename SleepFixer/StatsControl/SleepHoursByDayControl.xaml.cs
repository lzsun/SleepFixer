using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Telerik.Windows.Controls;
using Telerik.Charting;

namespace SleepFixer
{
    public partial class SleepHoursByDayControl : UserControl
    {
        private int scale =0;
        private DateTime datePick;
        
        public SleepHoursByDayControl()
        {
            InitializeComponent();
            datePick = DateTime.Today;
            int week = Convert.ToInt32(Math.Ceiling((double)datePick.DayOfYear / 7));
            Text_Date.Text = datePick.Year.ToString() + " W" + week.ToString();

            Calc();
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            if (scale == 0)
            {
                datePick = datePick.AddDays(-7);
                int week = Convert.ToInt32(Math.Ceiling((double)datePick.DayOfYear / 7));
                Text_Date.Text = datePick.Year.ToString() + " W" + week.ToString();
            }
            else if (scale == 1)
            {
                datePick = datePick.AddMonths(-1);
                Text_Date.Text = datePick.ToString("MMM yyyy");
            }
            else if (scale == 2)
            {
                datePick = datePick.AddYears(-1);
                Text_Date.Text = datePick.ToString("yyyy");
            }
            Calc();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (scale == 0)
            {
                datePick = datePick.AddDays(7);
                int week = Convert.ToInt32(Math.Ceiling((double)datePick.DayOfYear / 7));
                Text_Date.Text = datePick.Year.ToString() + " W" + week.ToString();
            }
            else if (scale == 1)
            {
                datePick = datePick.AddMonths(1);
                Text_Date.Text = datePick.ToString("MMM yyyy");
            }
            else if (scale == 2)
            {
                datePick = datePick.AddYears(1);
                Text_Date.Text = datePick.ToString("yyyy");
            }
            Calc();
        }

        private void Week_Click(object sender, RoutedEventArgs e)
        {
            scale = 0;
            int week = Convert.ToInt32(Math.Ceiling((double)datePick.DayOfYear / 7));
            Text_Date.Text = datePick.Year.ToString() + " W" + week.ToString();
            Calc();
        }

        private void Month_Click(object sender, RoutedEventArgs e)
        {
            scale = 1;
            Text_Date.Text = datePick.ToString("MMM yyyy");
            Calc();
        }

        private void Year_Click(object sender, RoutedEventArgs e)
        {
            scale = 2;
            Text_Date.Text = datePick.ToString("yyyy");
            Calc();
        }

        private void Calc()
        {
            LinkedList<SleepData> calcData = new LinkedList<SleepData>();
            double[] hourData = { 0, 0, 0, 0, 0 ,0, 0};
            int[] count = { 0, 0, 0, 0, 0, 0, 0 };
            double[] result = { 0, 0, 0, 0, 0, 0, 0 };
            string[] dayofWeek = { "Sun","Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };

            DateTime start = datePick;
            DateTime end = datePick;

            if (scale == 0)
            {
                start = datePick.AddDays(-1 * Convert.ToInt32(datePick.DayOfWeek));
                end = start.AddDays(6);
            }
            else if (scale == 1)
            {
                start = datePick.AddDays(-1 * (datePick.Day - 1));
                end = start.AddMonths(1).AddDays(-1);
            }
            else if (scale == 2)
            {
                start = datePick.AddDays(-1 * (datePick.DayOfYear - 1));
                end = start.AddYears(1).AddDays(-1);
            }

            foreach (SleepData data in SleepDataControl.jogs.Sleep)
            {
                if (data.Date >= start && data.Date <= end)
                    calcData.AddLast(data);
            }


            foreach (SleepData data in calcData)
            {
                if (data.IsNap == false)
                {
                    TimeSpan sleepHours = data.WakeupTime >= data.SleepTime ? data.WakeupTime - data.SleepTime : (data.WakeupTime - data.SleepTime).Add(new TimeSpan(24, 0, 0));
                    hourData[Convert.ToInt32(data.Date.DayOfWeek)] += sleepHours.TotalHours;
                    count[Convert.ToInt32(data.Date.DayOfWeek)]++;
                }
                else
                {
                    TimeSpan sleepHours = data.WakeupTime >= data.SleepTime ? data.WakeupTime - data.SleepTime : (data.WakeupTime - data.SleepTime).Add(new TimeSpan(24, 0, 0));
                    hourData[Convert.ToInt32(data.Date.DayOfWeek)] += sleepHours.TotalHours;
                }
            }

            (radChart.Series[0] as BarSeries).DataPoints.Clear();

            for (int i = 0; i < 7; i++)
            {
                if (count[i] == 0)
                {
                    result[i] = 0;
                }
                else
                {
                    result[i] = hourData[i] / count[i];
                }
                CategoricalDataPoint dp = new CategoricalDataPoint();
                dp.Value = result[i];     
                dp.Category = dayofWeek[i];
                if (result[i] == 0)
                {
                    dp.Label = "N/A";
                }
                else
                {
                    dp.Label = Math.Round(result[i], 2).ToString();
                }
                (radChart.Series[0] as BarSeries).DataPoints.Add(dp);
            }

        }
    }
}
