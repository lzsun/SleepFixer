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

            foreach (SleepData data in SleepDataControl.jogs.Sleep)
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
                            + System.Environment.NewLine + "M: " + data.Mood.ToString()*/
                    });
                }
                
            }
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
