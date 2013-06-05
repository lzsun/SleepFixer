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
using Telerik.Windows.Controls;

namespace SleepFixer
{
    public partial class SleepTimeControl : UserControl
    {
        public SleepTimeControl()
        {
            InitializeComponent();
            update();
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

        private void update()
        {
            SortedDictionary<DateTime, TimeSpan> sleep_map = new SortedDictionary<DateTime, TimeSpan>(new ReverseComparer<DateTime>());
            SortedDictionary<DateTime, TimeSpan> wake_map = new SortedDictionary<DateTime, TimeSpan>(new ReverseComparer<DateTime>());

            foreach (SleepData data in SleepDataControl.jogs.Sleep)
            {
                if(data.IsNap == false && sleep_map.ContainsKey(data.Date) == false)
                {
                    sleep_map[data.Date] = data.SleepTime;
                }
                if (data.IsNap == false && wake_map.ContainsKey(data.Date) == false)
                {
                    wake_map[data.Date] = data.WakeupTime;
                }
            }


            (radChart.Series[0] as LineSeries).DataPoints.Clear();

            foreach (KeyValuePair<DateTime, TimeSpan> data in sleep_map)
            {
                CategoricalDataPoint dp = new CategoricalDataPoint();
                dp.Value = data.Value.TotalHours > 12 ? data.Value.TotalHours - 24 : data.Value.TotalHours;
                dp.Category = data.Key;
                dp.Label = data.Value.ToString(@"hh\:mm");
                (radChart.Series[0] as LineSeries).DataPoints.Add(dp);
            }

            (radChart.Series[1] as LineSeries).DataPoints.Clear();
            foreach (KeyValuePair<DateTime, TimeSpan> data in wake_map)
            {
                CategoricalDataPoint dp = new CategoricalDataPoint();
                dp.Value = data.Value.TotalHours > 12 ? data.Value.TotalHours - 24 : data.Value.TotalHours;
                dp.Category = data.Key;
                dp.Label = data.Value.ToString(@"hh\:mm");
                (radChart.Series[1] as LineSeries).DataPoints.Add(dp);
            }

        }

        private void ChartTrackBallBehavior_TrackInfoUpdated(object sender, TrackBallInfoEventArgs e)
        {
            CategoricalDataPoint dataPoint = e.Context.ClosestDataPoint.DataPoint as CategoricalDataPoint;
            DateTime date = (DateTime)dataPoint.Category;
            e.Header = date.ToString("MMM-dd-yy");
            foreach (DataPointInfo info in e.Context.DataPointInfos)
            {
                dataPoint = info.DataPoint as CategoricalDataPoint;
                //date = (DateTime)dataPoint.Category;
                if (info.Series == radChart.Series[0])
                {
                    info.DisplayHeader = "Go-to-bed Time: ";
                }
                else if (info.Series == radChart.Series[1])
                {
                    info.DisplayHeader = "Wake-up Time: ";
                }
                info.DisplayContent = dataPoint.Label;
            }
        }
    }

    class TimeDataPoint
    {
        public DateTime time { get; set; }
        public DateTime date { get; set; }
    }
}


