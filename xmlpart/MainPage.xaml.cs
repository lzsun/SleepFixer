using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using XMLTest.Resources;
using System.Text;



namespace XMLTest
{
    public partial class MainPage : PhoneApplicationPage
    {
        /// <summary>
        /// note: have to instance sleep before use sleepfixer
        /// </summary>
        public SleepFixer sf = new SleepFixer{Sleep=new List<Sleep>()};
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            sf.Sleep.AddRange(Generator());
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }     
          
        // <summary>
        /// 添加记录，要不要写重载呢？
        /// </summary>
        /// <param name="s"></param>
        public void AddLog(Sleep s)
        {
            //这logs应该建成全局对象
            List<Sleep> logs = StorageHelper.LoadJogs();
            logs.Add(s);
            StorageHelper.SaveJogs(sf);

        }


       
        public void Delete()
        {

        }

        public static List<Sleep> Generator()
        {
            DateTime wake = new DateTime(2013, 5, 10, 5, 0, 0);
            DateTime sleep = new DateTime(2013, 5, 10, 20, 0, 0);
            Random r = new Random();
            List<Sleep> list = new List<Sleep>();
            for (int i = 0; i < 60; i++)
            {
                Sleep s = new Sleep();
                s.Date = DateTime.Now.AddDays(i).ToString("d");
                //s.Wakeuptime = wake.AddMinutes(r.Next(0, 360)).ToString(@"HH:mm");
                //TimeofDay returns a timespan value
                s.WakeupTimeString = wake.AddMinutes(r.Next(0, 360)).TimeOfDay.ToString();
                s.SleeptimeString = sleep.AddMinutes(r.Next(0, 360)).TimeOfDay.ToString();
                s.Mood = r.Next(1, 5);
                s.IsNap = false;

                list.Add(s);
            }

            return list;
        }

        private void XMLTest(object sender, RoutedEventArgs e)
        {
            StorageHelper.SaveJogs(sf);
            //AddLog(List[59]);
            //string s = StorageHelper.GetAbsolutePath("SleepLog.xml");
           List<Sleep> temp = StorageHelper.LoadJogs();
        }
        
       
    }
}