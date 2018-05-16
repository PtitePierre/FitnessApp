using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using FitnessApp.Portable;
using System.Reflection;
using Microcharts;
using SkiaSharp;
using Microcharts.Forms;
using Entry = Microcharts.Entry;

namespace FitnessApp
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewSessionList : ContentPage
	{
        private List<Session> sessions;

        public ViewSessionList ()
        {
            InitializeComponent ();
            FillListView();
        }

        /// <summary>
        /// 
        /// </summary>
        private async void FillListView()
        {
            sessions = await SaveAndLoad.LoadSessions();

            if(sessions == null)
                DependencyService.Get<IMessage>().longtime("list of sessions is null");

            lis_sessions.ItemsSource = sessions;
            lis_sessions.ItemSelected += ListItem_Selected;
            lis_sessions.SelectedItem = null;

            SetChart();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListItem_Selected(object sender, EventArgs e)
        {
            try
            {
                Session session = (Session)lis_sessions.SelectedItem;
                //lis_sessions.SelectedItem = null;
                Application.Current.MainPage = new NavigationPage(new AddSession(Application.Current.MainPage, session));
            }
            catch(Exception ex)
            {
                DependencyService.Get<IMessage>().longtime(ex.Message);
            }
        }

        private void Button_NewSession(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new AddSession(Application.Current.MainPage));
        }

        private void SetChart()
        {
            List<Entry> entries = new List<Entry>();
            Dictionary<string, int> sports = new Dictionary<string, int>();

            try
            {

                foreach (Session session in sessions)
                {
                    if (sports.ContainsKey(session.GetSportType().Name))
                        sports[session.GetSportType().Name]++;
                    else
                        sports.Add(session.GetSportType().Name, 1);
                }

                int num = 0;
                // set chart entries
                foreach (var sport in sports)
                {
                    // set a new color
                    int red = (int)(127 - (sport.Value % 7 + 1) * 18);
                    int green = (int)(((sport.Value + num) % 7 + 1) * 30);
                    int blue = (int)(127 + (sport.Value % 7 + 1) * 18);

                    string hex = String.Format("#{0:X2}{1:X2}{2:X2}", red, green, blue);
                    num++;

                    // add the pair sport name and its number of occurences
                    entries.Add(
                        new Entry(sport.Value)
                        {
                            Label = sport.Key,
                            ValueLabel = sport.Value.ToString(),
                            Color = SKColor.Parse(hex)
                        });
                }

                cha_sports.Chart = new BarChart() { Entries = entries };

            }
            catch (Exception e)
            {
                DependencyService.Get<IMessage>().longtime(e.Message);
            }
        }
	}
}