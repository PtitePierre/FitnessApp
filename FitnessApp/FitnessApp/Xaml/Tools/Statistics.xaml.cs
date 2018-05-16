using FitnessApp.Portable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microcharts;
using SkiaSharp;
using Microcharts.Forms;
using Entry = Microcharts.Entry;
using System.Globalization;

namespace FitnessApp
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Statistics : ContentPage
    {
        private List<Session> sessions;

        public Statistics ()
		{
			InitializeComponent ();
            LoadLists();
        }

        public async void LoadLists()
        {
            sessions = await SaveAndLoad.LoadSessions();
            SetChartDayRepart();
            SetChartWeekRepart();
            SetChartFavSport();
        }

        public void SetChartDayRepart()
        {
            Dictionary<string, float> repart = new Dictionary<string, float>();
            for(int i=0; i<6; i++)
            {
                int min = (i * 4);
                int max = ((i + 1) * 4);
                string label = min.ToString() + ":00-" + max.ToString() + ":00";
                repart.Add(label, 0);
                foreach (Session s in sessions)
                {
                    if(s.SDate.TimeOfDay >= TimeSpan.FromHours(min) && s.SDate.TimeOfDay < TimeSpan.FromHours(max))
                    {
                        repart[label] += s.Quantity * s.GetUnitCoef();
                    }
                }
            }

            List<Entry> entries = CreateEntries(repart);
            cha_dayrepart.Chart = new LineChart() { Entries = entries };
        }

        public void SetChartWeekRepart()
        {
            Calendar cal = CultureInfo.InvariantCulture.Calendar;
            DateTime day = DateTime.Now;
            List<DayOfWeek> w = new List<DayOfWeek>();
            
            DayOfWeek dow = cal.GetDayOfWeek(day);
            do
            {
                w.Add(cal.GetDayOfWeek(day));
                day = day.AddDays(1);
            } while (cal.GetDayOfWeek(day) != dow);

            Dictionary<DayOfWeek, float> week = new Dictionary<DayOfWeek, float>();
            try
            {
                foreach (DayOfWeek d in w)
                {
                    float activity = 0;
                    foreach (Session s in sessions)
                    {
                        if (cal.GetDayOfWeek(s.SDate) == d)
                        {
                            activity += s.Quantity * s.GetUnitCoef();
                        }
                    }
                    week.Add(d, activity);
                }

                List<Entry> entries = CreateWEntries(week);
                cha_weekrepart.Chart = new DonutChart() { Entries = entries };
            }
            catch(Exception e)
            {
                DependencyService.Get<IMessage>().longtime("ERR: " + e.Message);
            }
        }

        public void SetChartFavSport()
        {
            Dictionary<string, float> repart = new Dictionary<string, float>();

            foreach (Session session in sessions)
            {
                if (repart.ContainsKey(session.GetSportType().Name))
                    repart[session.GetSportType().Name] += session.Quantity * session.GetUnitCoef();
                else
                    repart.Add(session.GetSportType().Name, session.Quantity * session.GetUnitCoef());
            }

            List<Entry> entries = CreateEntries(repart);
            cha_favsport.Chart = new RadialGaugeChart() { Entries = entries };

        }

        public List<Entry> CreateEntries(Dictionary<string, float> src)
        {
            List<Entry> entries = new List<Entry>();
            int ind = 0;
            foreach (var time in src)
            {
                ind++;
                // set a new color
                int red = (int)(127 - (time.Value % 7 + 1) * 18);
                int green = (int) (((time.Value + ind) % 7 + 1) * 30);
                int blue = (int)(127 + (time.Value % 7 + 1) * 18);

                string hex = String.Format("#{0:X2}{1:X2}{2:X2}", red, green, blue);
                
                entries.Add(
                    new Entry(time.Value)
                    {
                        Label = time.Key,
                        ValueLabel = time.Value.ToString(),
                        Color = SKColor.Parse(hex)
                    });
            }
            return entries;
        }

        public List<Entry> CreateWEntries(Dictionary<DayOfWeek, float> src)
        {
            List<Entry> entries = new List<Entry>();
            foreach (var time in src)
            {
                // set a new color
                int key = (int)time.Key;

                int red = (int)(127 - (key % 7) * 18);
                int green = (int)((key % 7) * 30);
                int blue = (int)(127 + (key % 7) * 18);

                string hex = String.Format("#{0:X2}{1:X2}{2:X2}", red, green, blue);

                entries.Add(
                    new Entry(time.Value)
                    {
                        Label = time.Key.ToString(),
                        ValueLabel = time.Value.ToString(),
                        Color = SKColor.Parse(hex)
                    });
            }
            return entries;
        }
    }
}