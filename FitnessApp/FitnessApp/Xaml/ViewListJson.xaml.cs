using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;

using FitnessApp.Portable;

namespace FitnessApp
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewListJson : ContentPage
	{
		public ViewListJson ()
		{
            List<Unit> units = SaveAndLoad.LoadUnits(this.GetType().GetTypeInfo().Assembly);
            List<SportType> sports = SaveAndLoad.LoadSportTypes(this.GetType().GetTypeInfo().Assembly);


            InitializeComponent();
            

            // TO DO : Binding Data units to ListView
            UnitList.ItemsSource = units;
            SportList.ItemsSource = sports;
        }

        private void AddSportClicked(object sender, EventArgs e)
        {
            //DisplayAlert("Ploup", "Ploup'z alert", "Quit!");
        }

        private void AddUnitClicked(object sender, EventArgs e)
        {
            if(UnitList.SelectedItem != null)
            {
                Application.Current.MainPage = new NavigationPage(new AddUnit(this, (Unit)UnitList.SelectedItem));
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(new AddUnit(this));
            }
        }
    }
}