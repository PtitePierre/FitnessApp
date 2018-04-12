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
            if (SportList.SelectedItem != null)
            {
                SportType sport = (SportType)SportList.SelectedItem;
                SportList.SelectedItem = null;
                Application.Current.MainPage = new NavigationPage(new AddSport(Application.Current.MainPage, sport));
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(new AddSport(Application.Current.MainPage));
            }
        }

        private void AddUnitClicked(object sender, EventArgs e)
        {
            if(UnitList.SelectedItem != null)
            {
                Unit unit = (Unit)UnitList.SelectedItem;
                UnitList.SelectedItem = null;
                Application.Current.MainPage = new NavigationPage(new AddUnit(Application.Current.MainPage, unit));
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(new AddUnit(Application.Current.MainPage));
            }
        }
    }
}