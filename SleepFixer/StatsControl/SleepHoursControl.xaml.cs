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
    public partial class SleepHoursControl : UserControl
    {
        private int scale = 0;
        
        public SleepHoursControl()
        {
            InitializeComponent();
            update();
            if ((chart.Series[0] as BarSeries).DataPoints.Count != 0)
            {
                chart.Zoom = new Size(Math.Ceiling((double)(chart.Series[0] as BarSeries).DataPoints.Count / 7), 1);
            }
            chart.PanOffset = new Point(0, 0);
        }

        private sealed class ReverseComparer<T> : IComparer<T>
        {
            private readonly IComparer<T> inner;
            public ReverseComparer() : this(null) { }
            public ReverseComparer(IComparer<T> inner)
            {
                this.inner = inner ?? Comparer<T>.Default;
            }
            int IComparer<T>.Compare(T x, T y) { return inner.Compare(y, x); }
        }
        
        public void update()
        {
            SortedDictionary<DateTime, double> data_map = new SortedDictionary<DateTime, double>(new ReverseComparer<DateTime>());
            
            foreach (SleepData data in SleepDataControl.jogs.Sleep)
            {
                TimeSpan sleepHours = data.WakeupTime >= data.SleepTime ? data.WakeupTime - data.SleepTime : (data.WakeupTime - data.SleepTime).Add(new TimeSpan(24, 0, 0));
                if (data_map.ContainsKey(data.Date))
                {
                    data_map[data.Date] = data_map[data.Date] + sleepHours.TotalHours;
                }
                else
                {
                    data_map.Add(data.Date, sleepHours.TotalHours);
                }                
            }


            (chart.Series[0] as BarSeries).DataPoints.Clear();

            if (scale == 0)
            {
                foreach (KeyValuePair<DateTime, double> data in data_map)
                {
                    CategoricalDataPoint dp = new CategoricalDataPoint();
                    dp.Value = data.Value;
                    dp.Category = data.Key.ToString("MMM-dd-yy");
                    if (data.Value == 0)
                    {
                        dp.Label = "N/A";
                    }
                    else
                    {
                        dp.Label = Math.Round(data.Value, 2).ToString();
                    }
                    (chart.Series[0] as BarSeries).DataPoints.Add(dp);
                }
            }
            else if (scale == 1)
            {
                DateTime start = data_map.Keys.First().AddDays(Convert.ToInt32(6-data_map.Keys.First().DayOfWeek));
                for (DateTime date = start; date >= data_map.Keys.Last(); date = date.AddDays(-7))
                {
                    double sleepHours = 0;
                    int count = 0;
                    for (int i = 0; i < 7; i++)
                    {
                        if(data_map.ContainsKey(date.AddDays(-1 *i)))
                        {
                            sleepHours += data_map[date.AddDays(-1 * i)];
                            count++;
                        }
                    }

                    CategoricalDataPoint dp = new CategoricalDataPoint();
                    dp.Value = sleepHours / count;
                    dp.Category = "W"+ (Math.Ceiling((double)date.DayOfYear/7)).ToString()+date.ToString("-yy");
                    if (dp.Value == 0)
                    {
                        dp.Label = "N/A";
                    }
                    else
                    {
                        dp.Label = Math.Round(sleepHours / count, 2).ToString();
                    }
                    (chart.Series[0] as BarSeries).DataPoints.Add(dp);
                }
            }
            else if (scale == 2)
            {
                DateTime start = data_map.Keys.First().AddMonths(1).AddDays(-1 *data_map.Keys.First().Day);
                for (DateTime date = start; date >= data_map.Keys.Last(); date = date.AddDays(-1 * date.Day))
                {
                    double sleepHours = 0;
                    int count = 0;
                    for (int i = 0; i < date.Day; i++)
                    {
                        if (data_map.ContainsKey(date.AddDays(-1 * i)))
                        {
                            sleepHours += data_map[date.AddDays(-1 * i)];
                            count++;
                        }
                    }

                    CategoricalDataPoint dp = new CategoricalDataPoint();
                    dp.Value = sleepHours / count;
                    dp.Category = date.ToString("MMM-yy");
                    if (dp.Value == 0)
                    {
                        dp.Label = "N/A";
                    }
                    else
                    {
                        dp.Label = Math.Round(sleepHours / count, 2).ToString();
                    }
                    (chart.Series[0] as BarSeries).DataPoints.Add(dp);
                }
            }

            
        }

        private void Daily_Click(object sender, RoutedEventArgs e)
        {      
            scale = 0;
            update();
            if ((chart.Series[0] as BarSeries).DataPoints.Count != 0)
            {
                chart.Zoom = new Size(Math.Ceiling((double)(chart.Series[0] as BarSeries).DataPoints.Count / 7), 1);
            }
            chart.PanOffset = new Point(0, 0);
        }

        private void Weekly_Click(object sender, RoutedEventArgs e)
        {
            scale = 1;
            update();
            if ((chart.Series[0] as BarSeries).DataPoints.Count != 0)
            {
                chart.Zoom = new Size(Math.Ceiling((double)(chart.Series[0] as BarSeries).DataPoints.Count / 7), 1);
            }
            chart.PanOffset = new Point(0, 0);
        }

        private void Monthly_Click(object sender, RoutedEventArgs e)
        {
            scale = 2;
            update();
            if((chart.Series[0] as BarSeries).DataPoints.Count !=0)
            {
                chart.Zoom = new Size(Math.Ceiling((double)(chart.Series[0] as BarSeries).DataPoints.Count / 7), 1);
            }
            chart.PanOffset = new Point(0, 0);
        }
    }
}
