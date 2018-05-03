using FitnessApp.Portable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using System.Reflection;

namespace FitnessApp
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddSession : ContentPage
	{
        private static bool done = false;
        public AddSession ()
        {
            InitializeComponent();
            FillPickers();
        }

        private async void FillPickers()
        {
            List<SportType> sports = await SaveAndLoad.LoadSports();
            if (sports != null)
            {
                pic_sportType.ItemsSource = sports.OrderBy(s => s.Name).ToList();
                pic_sportType.SelectedItem = null;
            }

            List<Unit> units = await SaveAndLoad.LoadUnits();
            if(units != null)
            {
                pic_unit.ItemsSource = units;
                pic_unit.SelectedItem = null;
            }
        }

        /// <summary>
        /// check the inputs
        /// create and save a new session if all the information are correct
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Submit(object sender, EventArgs e)
        {
            Session newSession = new Session();

            if(in_quantity.Text != null && pic_sportType.SelectedItem != null && pic_unit.SelectedItem != null)
            {
                newSession.Quantity = float.Parse(in_quantity.Text);
                newSession.SType = (SportType)pic_sportType.SelectedItem;
                newSession.SUnit = (Unit)pic_unit.SelectedItem;
                newSession.SDate = pic_date.Date + pic_time.Time;
                newSession.Done = done;

                SaveAndLoad.SaveSession(newSession);

                FillPickers();
                in_quantity.Text = null;

                DependencyService.Get<IMessage>().longtime("New session added");
            }
            else
            {
                DependencyService.Get<IMessage>().longtime("Incomplete information");
            }
        }

        /// <summary>
        /// Displays the units that are linked to the selected sport type in pic_unit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Picker_SelectSport(object sender, EventArgs e)
        {
            try
            {
                if (pic_sportType.SelectedItem != null)
                {
                    SportType sport = (SportType)pic_sportType.SelectedItem;

                    List<Unit> units = await SaveAndLoad.LoadUnits();
                    if (units != null)
                    {
                        List<Unit> newUnits = new List<Unit>();

                        foreach (Unit unit in units)
                        {
                            if (sport.Units.Contains(unit.Id))
                            {
                                newUnits.Add(unit);
                            }
                        }

                        if (newUnits.Count != 0)
                            pic_unit.ItemsSource = newUnits;
                    }

                    List<Session> sessions = await SaveAndLoad.LoadSessions();
                    if (sessions != null)
                    {
                        List<Session> newSessions = new List<Session>();
                        foreach (Session session in sessions)
                        {
                            if (session.SType == sport)
                            {
                                newSessions.Add(session);
                            }
                        }
                        if (newSessions.Count != 0)
                        {
                            lis_sessions.ItemsSource = newSessions;
                        }
                    }

                }
                else
                {
                    List<Unit> units = await SaveAndLoad.LoadUnits();
                    if (units != null)
                        pic_unit.ItemsSource = units;

                    List<Session> sessions = await SaveAndLoad.LoadSessions();
                    if (sessions != null)
                        lis_sessions.ItemsSource = sessions;
                }
            }
            catch(Exception ex)
            {
                DependencyService.Get<IMessage>().longtime("ERROR: "+ ex.Message);
            }
        }

        private void Button_Done(object sender, EventArgs e)
        {
            if(!done)
            {
                btn_done.Text = "Done";
                btn_done.BackgroundColor = Color.FromHex("#03AB17");
                done = true;
            }
            else
            {
                btn_done.Text = "To do";
                btn_done.BackgroundColor = Color.FromHex("#131313");
                done = false;
            }
        }
    }
}