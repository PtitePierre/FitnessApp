using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FitnessApp.Portable;
using System.Globalization;
using Microcharts;
using SkiaSharp;
using Microcharts.Forms;
using Entry = Microcharts.Entry;

namespace FitnessApp
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewWeek : ContentPage
	{
        private DateTime day;
        private static Calendar cal = CultureInfo.InvariantCulture.Calendar;
        public ViewWeek ()
		{
			InitializeComponent ();

            day = DateTime.Today;
            FillList(day);
		}

        /// <summary>
        /// sorts the sessions by date
        /// keep only the sessions scheduled on the same week as day
        /// </summary>
        /// <param name="day">one day of the wanted week</param>
        private async void FillList(DateTime day)
        {
            lab_week.Text = "Sessions of the Week #" + GetIso8601WeekOfYear(day);
            List<Session> sessions = await SaveAndLoad.LoadSessions();

            List<Session> weekSessions = new List<Session>();

            foreach(Session s in sessions)
            {
                if (GetIso8601WeekOfYear(s.SDate) == GetIso8601WeekOfYear(day))
                    weekSessions.Add(s);
            }

            lis_sessions.ItemsSource = weekSessions;
            FillChart(weekSessions, day);
        }

        private void FillChart(List<Session> sessions, DateTime day)
        {
            Dictionary<string, float> week = new Dictionary<string, float>();
            foreach(DateTime dt in GetWeek(day))
            {
                week.Add(dt.ToShortDateString(), 0);
            }
            
            foreach (Session s in sessions)
            {
                float coef = s.GetUnitCoef();
                if (week.ContainsKey(s.SDate.ToShortDateString()))
                    week[s.SDate.ToShortDateString()] += s.Quantity*coef;
                else
                    week.Add(s.SDate.ToShortDateString(), s.Quantity * coef);
            }
            // set chart entries
            List<Entry> entries = new List<Entry>();
            int ind = 0;
            foreach (var d in week)
            {
                ind++;
                // set a new color
                int red = (int)(127 - (d.Value % 7 + 1) * 18);
                int green = (int)(((d.Value + ind) % 7 + 1) * 30);
                int blue = (int)(127 + (d.Value % 7 + 1) * 18);

                string hex = String.Format("#{0:X2}{1:X2}{2:X2}", red, green, blue);

                // add the pair sport name and its number of occurences
                entries.Add(
                    new Entry(d.Value)
                    {
                        Label = d.Key,
                        ValueLabel = d.Value.ToString(),
                        Color = SKColor.Parse(hex)
                    });
            }

            cha_quantity.Chart = new LineChart() { Entries = entries };
        }

        private void Button_NextWeek(object sender, EventArgs e)
        {
            DependencyService.Get<IMessage>().shorttime("Next Week");
            day = day.AddDays(7);
            FillList(day);
        }

        private void Button_PrevWeek(object sender, EventArgs e)
        {
            DependencyService.Get<IMessage>().shorttime("Prev Week");
            day = day.AddDays(-7);
            FillList(day);
        }

        public static int GetIso8601WeekOfYear(DateTime time)
        {
            DayOfWeek dow = cal.GetDayOfWeek(time);
            if (dow >= DayOfWeek.Monday && dow <= DayOfWeek.Wednesday)
                time = time.AddDays(3);

            // Return the week of our adjusted day
            return cal.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public static List<DateTime> GetWeek(DateTime time)
        {
            DateTime monday, day;
            List<DateTime> week = new List<DateTime>();

            DayOfWeek dow = cal.GetDayOfWeek(time);
            switch (dow)
            {
                case DayOfWeek.Monday:monday = time;
                    break;
                case DayOfWeek.Tuesday:monday = time.AddDays(-1);
                    break;
                case DayOfWeek.Wednesday:
                    monday = time.AddDays(-2);
                    break;
                case DayOfWeek.Thursday:
                    monday = time.AddDays(-3);
                    break;
                case DayOfWeek.Friday:
                    monday = time.AddDays(-4);
                    break;
                case DayOfWeek.Saturday:
                    monday = time.AddDays(-5);
                    break;
                default:
                    monday = time.AddDays(-6);
                    break;
            }
            day = monday;

            do
            {
                week.Add(day);
                day = day.AddDays(1);
            } while (cal.GetDayOfWeek(day) != DayOfWeek.Monday);

            return week;
        }
    }
}