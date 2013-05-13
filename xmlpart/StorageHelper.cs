using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;

namespace XMLTest
{
    public static class StorageHelper
    {
        private const string FILE_NAME = "SleepLog.xml";
       
        public static List<Sleep> LoadJogs()
        {
            List<Sleep> jogs = new List<Sleep>();
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
                XmlSerializer xs = new XmlSerializer(typeof(SleepFixer));
                jogs.AddRange(((SleepFixer)xs.Deserialize(reader)).Sleep);
                
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
            return jogs;
        }
        

        //List<Sleep> jogs
        public static void SaveJogs(SleepFixer sf)
        {
            TextWriter writer = null;
            try
            {
                IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForApplication();
                IsolatedStorageFileStream file = isoStorage.OpenFile(FILE_NAME, FileMode.Create);
                writer = new StreamWriter(file);
                //XmlSerializer xs = new XmlSerializer(typeof(List<Sleep>));
                XmlSerializer xs = new XmlSerializer(typeof(SleepFixer));
                //xs.Serialize(writer, jogs);
                xs.Serialize(writer, sf);
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



        //public static string GetAbsolutePath(string filename)
        //{
        //    IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();

        //    string absoulutePath = null;

        //    if (isoStore.FileExists(filename))
        //    {
        //        IsolatedStorageFileStream output = new IsolatedStorageFileStream(filename, FileMode.Open, isoStore);
        //        absoulutePath = output.Name;

        //        output.Close();
        //        output = null;
        //    }

        //    return absoulutePath;
        //}
        //public StorageHelper()
        //{
        //}
    }
}
