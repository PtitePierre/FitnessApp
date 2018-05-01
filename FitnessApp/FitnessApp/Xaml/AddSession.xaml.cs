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
		public AddSession ()
        {
            InitializeComponent();
            FillPickers();
        }

        private async void FillPickers()
        {
            List<Unit> units = await SaveAndLoad.LoadUnits();
            List<SportType> sports = await SaveAndLoad.LoadSports();

            pic_unit.ItemsSource = units;
            pic_sportType.ItemsSource = sports;

            pic_unit.SelectedItem = null;
            pic_sportType.SelectedItem = null;
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
        private async void Picker_SortUnits(object sender, EventArgs e)
        {
            if(pic_sportType.SelectedItem != null)
            {
                List<Unit> units = await SaveAndLoad.LoadUnits();
                SportType sport = (SportType)pic_sportType.SelectedItem;
                List<Unit> newUnits = new List<Unit>();

                foreach (Unit unit in units)
                {
                    if (sport.Units.Contains(unit.Id))
                    {
                        newUnits.Add(unit);
                    }
                }
                pic_unit.ItemsSource = newUnits;
            }
            else
            {
                List<Unit> units = await SaveAndLoad.LoadUnits();
                pic_unit.ItemsSource = units;
            }
        }
    }
}