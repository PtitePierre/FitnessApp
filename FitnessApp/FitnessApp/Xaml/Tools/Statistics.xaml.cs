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
        }

        public void SetChartDayRepart()
        {
            Dictionary<string, float> repart = new Dictionary<string, float>();
            for(int i=0; i<6; i++)
            {
                string label = (i * 4).ToString() + ":00-" + ((i + 1) * 4).ToString() + ":00";
                repart.Add(label, 0);
                foreach (Session s in sessions)
                {
                    if(s.SDate.TimeOfDay< TimeSpan.FromHours((i + 1) * 4))
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
            List<string> w = new List<string>();
            
            DayOfWeek dow = cal.GetDayOfWeek(day);
            do
            {
                w.Add(cal.GetDayOfWeek(day).ToString());
                day = day.AddDays(1);
            } while (cal.GetDayOfWeek(day) != dow);

            Dictionary<string, float> week = new Dictionary<string, float>();
            try
            {
                foreach (string d in w)
                {
                    float activity = 0;
                    foreach (Session s in sessions)
                    {
                        if (cal.GetDayOfWeek(s.SDate).ToString() == d)
                        {
                            activity += s.Quantity * s.GetUnitCoef();
                        }
                    }
                    week.Add(d, activity);
                }

                List<Entry> entries = CreateEntries(week);
                cha_weekrepart.Chart = new LineChart() { Entries = entries };
            }
            catch(Exception e)
            {
                DependencyService.Get<IMessage>().longtime("ERR: " + e.Message);
            }
        }

        public List<Entry> CreateEntries(Dictionary<string, float> src)
        {
            List<Entry> entries = new List<Entry>();
            foreach (var time in src)
            {
                // set a new color
                string hex;

                int red = (int)(255 - (time.Value % 7 + 1) * 30);
                int green = (int)((time.Value % 7 + 1) * 30);
                int blue = (int)(255 - (time.Value % 7 + 1) * 30);

                hex = String.Format("#{0:X2}{1:X2}{2:X2}", red, green, blue);

                // add the pair sport name and its number of occurences
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
    }
}