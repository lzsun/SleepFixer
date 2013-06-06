using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepFixer
{
    public static class SleepScore
    {
        public static double BedTimeBaseline = -1;
        
        public static double AvgHours = 1;
        public static double AvgHoursScore = 1;
        public static double VarHours = 1;
        public static double VarHoursScore = 1;
        public static double AvgBedTime = 1;
        public static double AvgBedTimeScore = 1;
        public static double VarBedTime = 1;
        public static double VarBedTimeScore = 1;
        public static double AvgMood = 1;
        public static double AvgMoodScore = 1;
        public static double Score = 1;

        public static double CalcScore(DateTime start, DateTime end)
        {
            //SleepDataControl.LoadJogs();

            LinkedList<SleepData> calcData = new LinkedList<SleepData>();

            foreach (SleepData data in SleepDataControl.jogs.Sleep)
            {
                if(data.Date >= start && data.Date <= end)
                    calcData.AddLast(data);
            }

            int dateCount = 0;
            foreach (SleepData data in calcData)
            {
                if(data.IsNap == false)
                    dateCount ++;
            }
            if(dateCount < 7)
            {
                Score = -1;
            }
            else
            {
                CalcAvgHours(calcData);
                CalcVarHours(calcData);
                CalcAvgBedTime(calcData);
                CalcVarBedTime(calcData);
                CalcAvgMood(calcData);
                Score = AvgHoursScore * 0.3 + VarHoursScore * 0.25 + AvgBedTimeScore * 0.2 + VarBedTimeScore * 0.15 + AvgMoodScore * 0.1;
            }
            
            return Score;
        }

        private static void CalcAvgHours(LinkedList<SleepData> CalcData)
        {
            AvgHours = 0;
            int count = 0 ;
            foreach (SleepData data in CalcData)
            {
                TimeSpan sleepHours = data.WakeupTime >= data.SleepTime ? data.WakeupTime - data.SleepTime : (data.WakeupTime - data.SleepTime).Add(new TimeSpan(24, 0, 0));
                AvgHours += sleepHours.TotalHours;
                if (data.IsNap == false)
                    count++;
            }
            AvgHours /= count;
            AvgHoursScore = SleepScore.ConvertScore(AvgHours, 5, 9);
        }

        private static void CalcVarHours(LinkedList<SleepData> CalcData)
        {
            Dictionary<DateTime, double> data_map = new Dictionary<DateTime, double>();
            foreach (SleepData data in CalcData)
            {
                TimeSpan sleepHours = data.WakeupTime >= data.SleepTime ? data.WakeupTime - data.SleepTime : (data.WakeupTime - data.SleepTime).Add(new TimeSpan(24, 0, 0));
                if(data_map.ContainsKey(data.Date))
                {
                    data_map[data.Date] = data_map[data.Date] + sleepHours.TotalHours;
                }
                else
                {
                    data_map.Add(data.Date, sleepHours.TotalHours);
                }                
            }
            

            VarHours = SleepScore.CalculateStdDev(new List<double>(data_map.Values));
            VarHoursScore = 1 - SleepScore.ConvertScore(VarHours,0,2);
        }

        private static void CalcAvgBedTime(LinkedList<SleepData> CalcData)
        {
            AvgBedTime = 0;
            int count = 0;
            foreach (SleepData data in CalcData)
            {
                if (data.IsNap == false)
                {
                    AvgBedTime += data.SleepTime.Hours <= 12 ? data.SleepTime.Hours : (data.SleepTime.Hours - 24);
                    count++;
                }  
            }
            AvgBedTime /= count;
            AvgBedTimeScore = 1 - SleepScore.ConvertScore(AvgBedTime, -2, 3);
        }

        private static void CalcVarBedTime(LinkedList<SleepData> CalcData)
        {
            List<double> data_map = new List<double>();
            
            foreach (SleepData data in CalcData)
            {
                if (data.IsNap == false)
                {
                    data_map.Add(data.SleepTime.Hours <= 12 ? data.SleepTime.Hours : (data.SleepTime.Hours - 24));
                }
            }
            
            VarBedTime = SleepScore.CalculateStdDev(data_map);
            VarBedTimeScore = 1 - SleepScore.ConvertScore(VarBedTime, 0, 2.5);
        }

        private static void CalcAvgMood(LinkedList<SleepData> CalcData)
        {
            AvgMood = 0;
            int count = 0 ;
            foreach (SleepData data in CalcData)
            {
                if (data.IsNap == false)
                {
                    count ++;
                    AvgMood += data.Mood;
                }
                    
            }
            AvgMood /= count;
            AvgMoodScore = SleepScore.ConvertScore(AvgMood, 1, 5);
        }

        private static double CalculateStdDev(IEnumerable<double> values)
        {
            double ret = 0;
            if (values.Count() > 0)
            {
                //Compute the Average      
                double avg = values.Average();
                //Perform the Sum of (value-avg)_2_2      
                double sum = values.Sum(d => Math.Pow(d - avg, 2));
                //Put it all together      
                //ret = Math.Sqrt((sum) / (values.Count() - 1));
                ret = Math.Sqrt((sum) / values.Count());
            }
            return ret;
        }

        private static double ConvertScore(double value, double min, double max)
        {
            double result = (value - min) / (max - min);
            if (result <0)
                result = 0;
            if(result >1)
                result = 1;
            return result;
        }
    }
}
