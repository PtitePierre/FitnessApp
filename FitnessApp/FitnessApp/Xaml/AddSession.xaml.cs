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
            InitializeComponent ();
            FillPickers();
        }

        private async void FillPickers()
        {
            List<Unit> units = await SaveAndLoad.LoadUnits();
            List<SportType> sports = await SaveAndLoad.LoadSports();

            pic_unit.ItemsSource = units;
            pic_sportType.ItemsSource = sports;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Session newSession = new Session();

            if(in_quantity.Text != null && pic_sportType.SelectedItem != null && pic_unit.SelectedItem != null)
            {
                newSession.Quantity = Int32.Parse(in_quantity.Text);
                newSession.SType = (SportType)pic_sportType.SelectedItem;
                newSession.SUnit = (Unit)pic_unit.SelectedItem;
                newSession.SDate = pic_date.Date + pic_time.Time;

                SaveAndLoad.SaveSession(newSession);

                pic_unit.SelectedItem = null;
                pic_sportType.SelectedItem = null;
                in_quantity.Text = null;

                DependencyService.Get<IMessage>().longtime("New session added");
            }
            else
            {
                DependencyService.Get<IMessage>().longtime("Incomplete information");
            }            
        }
    }
}