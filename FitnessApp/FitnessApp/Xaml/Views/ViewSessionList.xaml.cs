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
                    if (sports.ContainsKey(session.SType.Name))
                    {
                        sports[session.SType.Name]++;
                    }
                    else
                    {
                        sports.Add(session.SType.Name, 1);
                        //sports[session.SType.Name] = 1;
                    }
                }

                int num = 0;
                // set chart entries
                foreach (var sport in sports)
                {
                    // set a new color
                    string hex;

                    int red = (int)(255 - ((num + sport.Value) % 6) * 50);
                    int green = (int)(255 - (num % 6) * 50);
                    int blue = (int)(255 - (sport.Value%6) * 50);

                    hex = String.Format("#{0:X2}{1:X2}{2:X2}", red, green, blue);
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