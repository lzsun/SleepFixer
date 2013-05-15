using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.Xml.Serialization;

namespace SleepFixer
{
    [XmlType("Sleep")]
    public class Sleep
    {
        private String date;
        private TimeSpan sleeptime;
        private TimeSpan wakeuptime;
        private int mood;
        private bool isNap;

        [XmlAttribute("Date")]
        public String Date
        {
            get { return date; }
            set { date = value; }
        }

        //[XmlAttribute("SleepTime")]
        //public string Sleeptime
        //{
        //    get { return sleeptime; }
        //    set { sleeptime = value; }
        //}


        [XmlAttribute("SleepTimeString")]
        public String SleeptimeString
        {
            get { return sleeptime.ToString(@"hh\:mm"); }
            set { sleeptime = TimeSpan.Parse(value); }
        }

        /*[XmlAttribute("WakeupTime")]
        public TimeSpan Wakeuptime
        {
            get { return wakeuptime; }
            set { wakeuptime = value; }
        }*/

        [XmlAttribute("WakeupTimeString")]
        public String WakeupTimeString
        {
            get { return wakeuptime.ToString(@"hh\:mm"); }
            set { wakeuptime = TimeSpan.Parse(value); }
            

        }


        [XmlAttribute("Mood")]
        public int Mood
        {
            get { return mood; }
            set { mood = value; }
        }

        [XmlAttribute("IsNap")]
        public bool IsNap
        {
            get { return isNap; }
            set { isNap = value; }
        }

    }
}
