using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.Xml.Serialization;



namespace XMLTest
{
    /// <summary>
    /// Using XML serization
    /// <SleepFixer>
    ///<Sleep Date="2011-4-20" SleepTime="1:00pm" WakeupTime="2:00pm"  Mood="5" IsNap="true"></Sleep>
    ///</SleepFxier>
    /// </summary>

    [XmlType("SleepFixer")]
    public class SleepFixer
    {
        
        [XmlElement]
        //[XmlArrayItem("Sleep")]
        public List<Sleep> Sleep{get;set;}
    }

    

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
            //Sleep s1=new Sleep{Date=Convert.ToDateTime("2012-6-21"),Sleeptime=Convert.ToDateTime("1:00pm"),Wakeuptime=Convert.ToDateTime("2:00pm"), Mood=5, IsNap=true};
            //Sleep s2 = new Sleep { Date = Convert.ToDateTime("2012-5-21"), Sleeptime = Convert.ToDateTime("3:00pm"), Wakeuptime = Convert.ToDateTime("5:00pm"), Mood = 4, IsNap = false };

            

           
            //List<Sleep> List=new List<Sleep>();
            //List.AddRange(Generator());
            //List.Add(s1);
            //List.Add(s2);
            // 说明：下面二行代码的输出结果是一样的。

          //  SleepFixer sleepxml = new SleepFixer {Sleep=List};

          //  string xml = XmlHelper.XmlSerialize(sleepxml, Encoding.UTF8);
            //Console.WriteLine(xml);
            //Console.ReadKey();
            // 序列化的结果，反序列化一定能读取，所以就不再测试反序列化了。


        }

        /// <summary>
        /// 添加记录，要不要写重载呢？
        /// </summary>
        /// <param name="s"></param>
        //public void  AddLog(Sleep s)
        //{
        //    //这logs应该建成全局对象，同时加一个refresh函数
        //   List<Sleep> logs= StorageHelper.LoadJogs();
        //   logs.Add(s);
        //   StorageHelper.SaveJogs(logs);
            
        //}


        ///// <summary>
        ///// 删除的逻辑，根据什么来删呢
        ///// </summary>
        //public void Delete()
        //{ 
        
        //}

        //public static List<Sleep> Generator()
        //{
        //    DateTime wake=new DateTime(2013,5,10,5,0,0);
        //    DateTime sleep=new DateTime(2013,5,10,20,0,0);
        //    Random r = new Random();
        //    List<Sleep> list = new List<Sleep>();
        //    for (int i = 0; i < 60; i++)
        //    {
        //        Sleep s = new Sleep();
        //        s.Date = DateTime.Now.AddDays(i).ToString("d");
        //        //s.Wakeuptime = wake.AddMinutes(r.Next(0, 360)).ToString(@"HH:mm");
        //        //TimeofDay returns a timespan value
        //        s.wakeuptime = wake.AddMinutes(r.Next(0, 360)).TimeOfDay;
        //        s.sleeptime= sleep.AddMinutes(r.Next(0, 360)).TimeOfDay;
        //        s.Mood = r.Next(1, 5);
        //        s.IsNap = false;

        //        list.Add(s);
        //    }

        //    return list;
        //}
//    }
//}
