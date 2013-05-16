using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.Xml.Serialization;

namespace SleepFixer
{
    [XmlType("Sleep")]
    public class SleepData
    {

        public SleepData()
        {

        }

        public SleepData(DateTime Date, TimeSpan SleepTime, TimeSpan WakeupTime, int Mood, bool IsNap)
        {
            this.Date = Date;
            this.SleepTime = SleepTime;
            this.WakeupTime = WakeupTime;
            this.Mood = Mood;
            this.IsNap = IsNap;
        }


        [XmlIgnoreAttribute]
        public DateTime Date;
        
        [XmlAttribute("Date")]
        public String DateString
        {
            get { return Date.ToString("d"); }
            set { Date = DateTime.Parse(value); }
        }

        [XmlIgnoreAttribute]
        public TimeSpan SleepTime;
        [XmlIgnoreAttribute]
        public TimeSpan WakeupTime;


        [XmlAttribute("SleepTime")]
        public String SleeptimeString
        {
            get { return SleepTime.ToString(@"hh\:mm"); }
            set { SleepTime = TimeSpan.Parse(value); }
        }

        [XmlAttribute("WakeupTime")]
        public String WakeupTimeString
        {
            get { return WakeupTime.ToString(@"hh\:mm"); }
            set { WakeupTime = TimeSpan.Parse(value); }

        }

        [XmlAttribute("Mood")]
        public int Mood;

        [XmlAttribute("IsNap")]
        public bool IsNap;
        
    }
}
