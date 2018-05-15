using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FitnessApp.Portable;
using System.Globalization;

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
    }
}