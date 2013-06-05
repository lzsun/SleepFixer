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
    public partial class SleepScoreControl : UserControl
    {
        private DateTime datePick;
        private List<String> scores = new List<string> { "-", "E", "D", "C", "B", "A" };
        private List<String> comments = new List<string> {
            "Insufficient Data",
            "You need to improve your sleep right now!",
            "You don't sleep very well.",
            "Your sleep is OK.",
            "Your sleep is pretty good.",
            "You sleep very well."};
        
        public SleepScoreControl()
        {
            InitializeComponent();
            datePick = DateTime.Today;
            update();
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            datePick = datePick.AddMonths(-1);
            update();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            datePick = datePick.AddMonths(1);
            update();
        }

        private void update()
        {
            Text_Date.Text = datePick.ToString("MMM yyyy");
            double score = SleepScore.CalcScore(datePick, datePick.AddMonths(1).AddDays(-1));
            if (score == -1)
            {
                Text_Score.Text = scores[0];
                Text_Comment.Text = comments[0];
                Text_AvgHoursScore.Text = scores[0];
                Text_VarHoursScore.Text = scores[0];
                Text_AvgBedScore.Text = scores[0];
                Text_VarBedScore.Text = scores[0];
                Text_AvgMoodScore.Text = scores[0];
                Text_AvgHours.Text = scores[0];
                Text_VarHours.Text = scores[0];
                Text_AvgBed.Text = scores[0];
                Text_VarBed.Text = scores[0];
                Text_AvgMood.Text = scores[0];
            }
            else
            {
                Text_Score.Text = scores[Convert.ToInt32(Math.Ceiling(score * 5))];
                Text_Comment.Text = comments[Convert.ToInt32(Math.Ceiling(score * 5))];
                Text_AvgHoursScore.Text = scores[Convert.ToInt32(Math.Ceiling(SleepScore.AvgHoursScore * 5))];
                Text_VarHoursScore.Text = scores[Convert.ToInt32(Math.Ceiling(SleepScore.VarHoursScore * 5))];
                Text_AvgBedScore.Text = scores[Convert.ToInt32(Math.Ceiling(SleepScore.AvgBedTimeScore * 5))];
                Text_VarBedScore.Text = scores[Convert.ToInt32(Math.Ceiling(SleepScore.VarBedTimeScore * 5))];
                Text_AvgMoodScore.Text = scores[Convert.ToInt32(Math.Ceiling(SleepScore.AvgMoodScore * 5))];
                Text_AvgHours.Text = Math.Round(SleepScore.AvgHours, 2).ToString();
                Text_VarHours.Text = Math.Round(SleepScore.VarHours, 2).ToString();
                Text_AvgBed.Text = DateTime.Today.AddHours(Math.Round(SleepScore.AvgBedTime, 2)).ToString("HH:mm");
                Text_VarBed.Text = Math.Round(SleepScore.VarBedTime, 2).ToString();
                Text_AvgMood.Text = Math.Round(SleepScore.AvgMood, 2).ToString();
            }
        }
    }
}
