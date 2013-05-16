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

        //Generate Random Data
        public static void Random()
        {
            DateTime wake = new DateTime(2013, 3, 10, 6, 0, 0);
            DateTime sleep = new DateTime(2013, 3, 10, 22, 0, 0);
            Random r = new Random();
            jogs = new SleepDataRoot();
            for (int i = 0; i < 60; i++)
            {
                SleepData s = new SleepData(
                    wake.AddDays(i),         
                    sleep.AddMinutes(r.Next(0, 240)).TimeOfDay,
                    wake.AddMinutes(r.Next(0, 240)).TimeOfDay,
                    r.Next(1,5),
                    false);
                jogs.Sleep.Add(s);
            }
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


       
    }
}
