using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;

namespace SleepFixer
{
    
    public static class SleepDataControl
    {
        private const string FILE_NAME = "SleepLog.xml";

        public static SleepDataRoot jogs = new SleepDataRoot();

        static SleepDataControl()
        {
            LoadJogs();
        }
       
        public static void LoadJogs()
        {
            TextReader reader = null;
            try
            {
                IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForApplication();
                IsolatedStorageFileStream file = isoStorage.OpenFile(FILE_NAME, FileMode.OpenOrCreate);
                reader = new StreamReader(file);


                //这里有点问题，要不要改下xml结构呢。现在是sleepfixer套了一层，如果不套可以这么直接读
                //用不用xmlhelper没什么区别
                //XmlSerializer xs = new XmlSerializer(typeof(List<Sleep>));
                //jogs.AddRange(((List<Sleep>)xs.Deserialize(reader)));
                XmlSerializer xs = new XmlSerializer(typeof(SleepDataRoot));
                jogs = new SleepDataRoot();
                jogs.Sleep.AddRange(((SleepDataRoot)xs.Deserialize(reader)).Sleep);
                
                reader.Close();
            }
            catch
            {

            }
            finally
            {
                if (reader != null)
                    reader.Dispose();
            }
        }
        

        //List<Sleep> jogs
        public static void SaveJogs()
        {
            TextWriter writer = null;
            try
            {
                IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForApplication();
                IsolatedStorageFileStream file = isoStorage.OpenFile(FILE_NAME, FileMode.Create);
                writer = new StreamWriter(file);
                //XmlSerializer xs = new XmlSerializer(typeof(List<Sleep>));
                XmlSerializer xs = new XmlSerializer(typeof(SleepDataRoot));
                //xs.Serialize(writer, jogs);
                xs.Serialize(writer, jogs);
                writer.Close();
               
                
            }
            catch
            {
            }
            finally
            {
                if (writer != null)
                    writer.Dispose();
            }
            
        }

        public static void Add(SleepData data)
        {
            jogs.Sleep.Add(data);
            SaveJogs();
        }

        public static Boolean checkExist(DateTime date)
        {
            foreach (SleepData data in jogs.Sleep)
            {
                if(data.Date.ToString("d") == date.ToString("d"))
                    return true;
            }
            return false;
        }

        //Generate Random Data
        public static void Random()
        {
            jogs = new SleepDataRoot();

            double[] mood1 = { 0.25, 0.55, 0.75, 0.95, 1 };
            random(new DateTime(2013, 1, 1, 0, 0, 0),
                new DateTime(2013, 1, 31, 0, 0, 0),
                23, 5,
                5.5, 3,
                mood1, false);

            double[] mood2 = { 0.2, 0.45, 0.7, 0.85, 1 };
            random(new DateTime(2013, 2, 1, 0, 0, 0),
                new DateTime(2013, 2, 28, 0, 0, 0),
                22, 4,
                6.5, 3,
                mood2, false);

            double[] mood3 = { 0.15, 0.35, 0.55, 0.85, 1 };
            random(new DateTime(2013, 3, 1, 0, 0, 0),
                new DateTime(2013, 3, 31, 0, 0, 0),
                22.5, 2.5,
                7, 2,
                mood3, false);

            double[] mood4 = { 0.1, 0.2, 0.4, 0.7, 1 };
            random(new DateTime(2013, 4, 1, 0, 0, 0),
                new DateTime(2013, 4, 30, 0, 0, 0),
                22, 2,
                7.5, 1.5,
                mood4, false);

            double[] mood5 = { 0.05, 0.15, 0.25, 0.5, 1 };
            random(new DateTime(2013, 5, 1, 0, 0, 0),
                new DateTime(2013, 5, 31, 0, 0, 0),
                22.5, 1.5,
                7, 1.25,
                mood5, false);

            random(new DateTime(2013, 5, 1, 0, 0, 0),
                new DateTime(2013, 5, 31, 0, 0, 0),
                13, 0.5,
                0.75, 0.5,
                mood5, true);

            SaveJogs();
        }

        private static void random(DateTime start, DateTime end, double time, double timeRange, double hour, double hourRange, double[] moodRange, bool isNap)
        {
            Random r = new Random();
            for (DateTime i = start; i <= end; i = i.AddDays(1))
            {
                double mood = r.NextDouble();
                for (int j = 0; j < moodRange.Length; j++)
                {
                    if (mood < moodRange[j])
                        mood = j + 1;
                }
                TimeSpan sleepTime = DateTime.Today.AddHours(time + r.NextDouble() * timeRange).TimeOfDay;
                SleepData s = new SleepData(
                    i,
                    sleepTime,
                    DateTime.Today.Add(sleepTime).AddHours(hour + r.NextDouble() * hourRange).TimeOfDay,
                    Convert.ToInt32(mood),
                    isNap);
                jogs.Sleep.Add(s);
            }

        }
       
    }
}
