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
        private static string[] wunits = { "kg", "g", "lbs" };
        private Session session, oldS;
        private Page Page;
        private bool edit;

        public AddSession (Page page, Session old = null)
        {
            Page = page;
            InitializeComponent();
            FillPickers(old);
            pic_weightUnit.ItemsSource = wunits;

            //CheckOld(old);
        }

        private async void FillPickers(Session old)
        {
            List<Unit> units = await SaveAndLoad.LoadUnits();
            pic_unit.ItemsSource = units;

            List<SportType> sports = await SaveAndLoad.LoadSports();
            pic_sportType.ItemsSource = sports;

            CheckOld(old);
        }

        private void CheckOld(Session old)
        {
            if (old != null)
            {
                edit = true;
                oldS = old;
                pic_sportType.SelectedItem = old.SType;
                in_quantity.Text = old.Quantity.ToString();
                pic_date.Date = old.SDate;
                pic_time.Time = old.SDate.TimeOfDay;
                pic_unit.SelectedItem = old.SUnit;
                done = old.Done;

                this.session = old;
                this.Title = "Edit a Session";
            }
            else
            {
                pic_unit.SelectedItem = null;
                pic_sportType.SelectedItem = null;

                this.session = new Session();
                edit = false;
                this.Title = "Create a Session";
            }
            CheckDone(done);
        }

        /// <summary>
        /// check the inputs
        /// create and save a new session if all the information are correct
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Submit(object sender, EventArgs e)
        {
            if(in_quantity.Text != null && pic_sportType.SelectedItem != null && pic_unit.SelectedItem != null)
            {
                session.Quantity = float.Parse(in_quantity.Text);
                session.SType = (SportType)pic_sportType.SelectedItem;
                session.SUnit = (Unit)pic_unit.SelectedItem;
                session.SDate = pic_date.Date + pic_time.Time;
                session.Done = done;

                if (edit)
                    SaveAndLoad.UpDateSession(oldS, session);
                else
                    SaveAndLoad.SaveSession(session);

                Application.Current.MainPage = Page;
            }
            else
            {
                DependencyService.Get<IMessage>().longtime("Incomplete information");
            }
        }

        private void Button_Cancel(object sender, EventArgs e)
        {
            Application.Current.MainPage = Page;
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
                                newUnits.Add(unit);
                        }

                        if (newUnits.Count != 0)
                            pic_unit.ItemsSource = newUnits;
                    }

                    List<Session> sessions = await SaveAndLoad.LoadSessions();
                    if (sessions != null)
                    {
                        List<Session> newSessions = new List<Session>();
                        foreach (Session ss in sessions)
                        {
                            if (ss.SType.Id == sport.Id)
                                newSessions.Add(ss);
                        }
                        if (newSessions != null)
                        {
                            lis_sessions.ItemsSource = newSessions;
                            lis_sessions.ItemSelected += ListSession_ItemSelec;
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
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().longtime("ERROR: " + ex.Message);
            }
        }

        private void Button_Done(object sender, EventArgs e)
        {
            CheckDone(done);
        }

        private void CheckDone(bool done)
        {
            if (!done)
            {
                btn_done.Text = "Done";
                btn_done.BackgroundColor = Color.FromHex("#03AB17");
                done = true;
            }
            else
            {
                btn_done.Text = "To do";
                btn_done.BackgroundColor = Color.FromHex("#AB1717");
                done = false;
            }
        }

        private void ListSession_ItemSelec(object sender, EventArgs e)
        {
            Session old = (Session) lis_sessions.SelectedItem;
            pic_sportType.SelectedItem = old.SType;
            in_quantity.Text = old.Quantity.ToString();
            pic_date.Date = old.SDate;
            pic_time.Time = old.SDate.TimeOfDay;
            pic_unit.SelectedItem = old.SUnit;
        }
    }
}