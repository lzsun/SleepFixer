using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

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
                    TimeSpan sleepTime = data.WakeupTime >= data.SleepTime ? data.WakeupTime - data.SleepTime : (data.WakeupTime - data.SleepTime).Add(new TimeSpan(24, 0, 0));
                    this.AllAppointments.Add(new SampleAppointment()
                    {
                        StartDate = data.Date,
                        EndDate = data.Date.AddHours(1),
                        Subject = "S: " + data.SleeptimeString
                            + System.Environment.NewLine + "W: " + data.WakeupTimeString
                            + System.Environment.NewLine + sleepTime.ToString(@"h\hmm\m")
                            + System.Environment.NewLine + "M: " + data.Mood.ToString()
                    });
                }
                
            }
            /*this.AllAppointments.Add(new SampleAppointment()
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(1),
                Subject = "Today Appointment 1",
                Details = "Some Long Details are coming here",
                Location = "My Home Town"
            });

            this.AllAppointments.Add(new SampleAppointment()
            {
                StartDate = DateTime.Now.AddMinutes(30),
                EndDate = DateTime.Now.AddHours(1),
                Subject = "Today Appointment 2",

                Details = "Some Long Details are coming here",
                Location = "Paris"
            });

            this.AllAppointments.Add(new SampleAppointment()
            {
                StartDate = DateTime.Now.AddMinutes(30),
                EndDate = DateTime.Now.AddHours(1),
                Subject = "Today Appointment 3",
                Details = "Some Long Details are coming here",
                Location = "London"

            });

            this.AllAppointments.Add(new SampleAppointment()
            {
                StartDate = DateTime.Now.AddHours(2),
                EndDate = DateTime.Now.AddHours(3),
                Subject = "Today Appointment 4",

                Details = "Some Long Details are coming here",
                Location = "Berlin"
            });

            this.AllAppointments.Add(new SampleAppointment()
            {
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(1).AddHours(1),
                Subject = "Check Game scores",

                Details = "Some Long Details are coming here",
                Location = "Redmond"
            });
            this.AllAppointments.Add(new SampleAppointment()
            {
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(3),
                Subject = "Long run",

                Details = "Some Long Details are coming here",
                Location = "Seattle"
            });
            this.AllAppointments.Add(new SampleAppointment()
            {
                StartDate = DateTime.Now.Date.AddDays(6),
                EndDate = DateTime.Now.Date.AddDays(7),
                Subject = "Long run",

                Details = "Some Long Details are coming here",
                Location = "Honolulu"
            });

            this.AllAppointments.Add(new SampleAppointment()
            {
                StartDate = DateTime.Now.AddDays(4),
                EndDate = DateTime.Now.AddDays(4).AddHours(1),
                Subject = "Go to cinema",

                Details = "Some Long Details are coming here",
                Location = "Lihue"
            });

            this.AllAppointments.Add(new SampleAppointment()
            {
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now.AddDays(-1).AddHours(1),
                Subject = "Wash the car",

                Details = "Some Long Details are coming here",
                Location = "Rome"
            });

            this.AllAppointments.Add(new SampleAppointment()
            {
                StartDate = DateTime.Now.AddDays(7),
                EndDate = DateTime.Now.AddDays(7).AddHours(1),
                Subject = "Sail the boat",

                Details = "Some Long Details are coming here",
                Location = "Vienna"
            });

            this.AllAppointments.Add(new SampleAppointment()
            {
                StartDate = DateTime.Now.AddDays(7),
                EndDate = DateTime.Now.AddDays(7).AddHours(1),
                Subject = "Feed the fish",

                Details = "Some Long Details are coming here",
                Location = "Sofia"
            });

            this.AllAppointments.Add(new SampleAppointment()
            {
                StartDate = DateTime.Now.AddDays(8),
                EndDate = DateTime.Now.AddDays(8).AddHours(1),
                Subject = "Go to ski",

                Details = "Some Long Details are coming here",
                Location = "Zermatt"
            });

            this.AllAppointments.Add(new SampleAppointment()
            {
                StartDate = DateTime.Now.AddMonths(1),
                EndDate = DateTime.Now.AddMonths(1).AddHours(12),
                Subject = "Have fun with the kids",

                Details = "Some Long Details are coming here",
                Location = "Skiathos"
            });

            this.AllAppointments.Add(new SampleAppointment()
            {
                StartDate = DateTime.Now.AddMonths(1),
                EndDate = DateTime.Now.AddMonths(1).AddHours(12),
                Subject = "Go to MIX",

                Details = "Some Long Details are coming here",
                Location = "Las Vegas"
            });

            this.AllAppointments.Add(new SampleAppointment()
            {
                StartDate = DateTime.Now.AddMonths(1),
                EndDate = DateTime.Now.AddMonths(1).AddHours(12),
                Subject = "Visit Kauai",
                Details = "Some Long Details are coming here",
                Location = "Lihue"
            });*/
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

		/// <summary>
		/// Gets the details of the appointment.
		/// </summary>
		public string Details
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the location of the appointment.
		/// </summary>
		public string Location
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
