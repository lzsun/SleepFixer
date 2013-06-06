using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Calendar;

namespace SleepFixer
{
    class HistoryData : AppointmentSource
    {
        public HistoryData()
		{

		}

		public override void FetchData(DateTime startDate, DateTime endDate)
		{

			this.AllAppointments.Clear();

            Dictionary<DateTime, List<SleepData>> data_map = new Dictionary<DateTime, List<SleepData>>();
            foreach (SleepData data in SleepDataControl.jogs.Sleep)
            {
                if(data_map.ContainsKey(data.Date))
                {
                    data_map[data.Date].Add(data);
                }
                else
                {
                    data_map.Add(data.Date,new List<SleepData>());
                    data_map[data.Date].Add(data);
                }
                
            }

            foreach (KeyValuePair<DateTime, List<SleepData>> list in data_map)
            {
                TimeSpan totalHours = new TimeSpan();
                TimeSpan napHours = new TimeSpan();
                SleepData regularSleep = new SleepData();
                foreach (SleepData data in list.Value)
                {
                    TimeSpan sleepHours = data.WakeupTime >= data.SleepTime ? data.WakeupTime - data.SleepTime : (data.WakeupTime - data.SleepTime).Add(new TimeSpan(24, 0, 0));
                    totalHours += sleepHours;
                    this.AllAppointments.Add(new SampleAppointment()
                    {
                        StartDate = data.Date,
                        EndDate = data.Date.AddHours(1),
                        //Mood = data.Mood.ToString(),
                        //Subject = sleepHours.ToString(@"h\hmm\m"),
                        Data = data
                    });
                    if (data.IsNap == false)
                    {
                        regularSleep = data;
                    }
                    else
                    {
                        napHours += sleepHours;
                    }
                }
                this.AllAppointments.Insert(0,new SampleAppointment()
                {
                    StartDate = list.Key,
                    EndDate = list.Key.AddHours(1),
                    Mood = regularSleep.Mood.ToString(),
                    Subject = totalHours.ToString(@"h\hmm\m"),
                    NapHours = napHours,
                    Data = regularSleep
                });                   
            }

            /*foreach (SleepData data in SleepDataControl.jogs.Sleep)
            {
                if (data.IsNap == false)
                {
                    TimeSpan sleepHours = data.WakeupTime >= data.SleepTime ? data.WakeupTime - data.SleepTime : (data.WakeupTime - data.SleepTime).Add(new TimeSpan(24, 0, 0));
                    this.AllAppointments.Add(new SampleAppointment()
                    {
                        StartDate = data.Date,
                        EndDate = data.Date.AddHours(1),
                        Mood = data.Mood.ToString(),
                        Subject = sleepHours.ToString(@"h\hmm\m"),
                        Data = data
                        /*Subject = "S: " + (SettingsPage.is24Hr.Value ? data.SleeptimeString :DateTime.Today.Add(data.SleepTime).ToString("hh:mmt"))
                            + System.Environment.NewLine + "W: " + (SettingsPage.is24Hr.Value ? data.WakeupTimeString : DateTime.Today.Add(data.WakeupTime).ToString("hh:mmt"))
                            + System.Environment.NewLine + sleepHours.ToString(@"h\hmm\m")
                            + System.Environment.NewLine + "M: " + data.Mood.ToString()
                    });
                }
                
            }*/
			this.OnDataLoaded();
		}
	}

	public class SampleAppointment : IAppointment
	{
		/// <summary>
		/// Gets the subject of the appointment.
		/// </summary>
		public string Subject
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the start date and time of the appointment.
		/// </summary>
		public DateTime StartDate
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the end date and time of the appointment.
		/// </summary>
		public DateTime EndDate
		{
			get;
			set;
		}

		public string Mood
		{
			get;
			set;
		}

        public SleepData Data
        {
            get;
            set;
        }

        public TimeSpan NapHours
        {
            get;
            set;
        }


		/// <summary>
		/// Gets a string representation of the appointment
		/// </summary>
		/// <returns>A string representation of the appointment</returns>
		public override string ToString()
		{
			return this.Subject;
		}
	}

    

}
