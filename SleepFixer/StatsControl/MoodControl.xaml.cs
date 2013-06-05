using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Telerik.Charting;

namespace SleepFixer
{
    public partial class MoodControl : UserControl
    {
        private int scale =0;
        private DateTime datePick;
        
        public MoodControl()
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
            int[] moodData = {0,0,0,0,0};

            DateTime start = datePick;
            DateTime end = datePick;

            if (scale == 0)
            {
                start = datePick.AddDays(-1 * Convert.ToInt32(datePick.DayOfWeek));
                end = start.AddDays(6);
            }
            else if (scale == 1)
            {
                start = datePick.AddDays(-1 * (datePick.Day-1));
                end = start.AddMonths(1).AddDays(-1);
            }
            else if (scale == 2)
            {
                start = datePick.AddDays(-1 * (datePick.DayOfYear-1));
                end = start.AddYears(1).AddDays(-1);
            }

            foreach (SleepData data in SleepDataControl.jogs.Sleep)
            {
                if (data.Date >= start && data.Date <= end)
                    calcData.AddLast(data);
            }

            int count = 0;

            foreach (SleepData data in calcData)
            {
                if (data.IsNap == false)
                {
                    moodData[data.Mood - 1]++;
                    count++;
                }
            }
            /*if (count != 0)
            {
                for (int i = 0; i < moodData.Length; i++)
                {
                    moodData[i] /= count;
                }
            }*/
            if (count == 0)
            {
                radPieChart.Series[0].DataPoints.Clear();
            }
            else
            {
                radPieChart.Series[0].DataPoints.Clear();
                for (int i = 0; i < moodData.Length; i++)
                {
                    PieDataPoint dp = new PieDataPoint();
                    dp.Value = moodData[i];
                    double percent = ((double)moodData[i])/count * 100;
                    dp.Label = Math.Round(percent,1).ToString()+"%";
                    radPieChart.Series[0].DataPoints.Add(dp);
                }
            }
            
        }
    }
}
