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
            InitializeComponent();
            FillListViews();
        }

        private async void FillListViews()
        {
            List<Unit> units = await SaveAndLoad.LoadUnits();
            List<SportType> sports = await SaveAndLoad.LoadSports();
            
            // TO DO : Binding Data units to ListView
            lis_units.ItemsSource = units;
            lis_sports.ItemsSource = sports;

        }

        private void AddSportClicked(object sender, EventArgs e)
        {
            if (lis_sports.SelectedItem != null)
            {
                SportType sport = (SportType)lis_sports.SelectedItem;
                lis_sports.SelectedItem = null;
                Application.Current.MainPage = new NavigationPage(new AddSport(Application.Current.MainPage, sport));
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(new AddSport(Application.Current.MainPage));
            }
        }

        private void AddUnitClicked(object sender, EventArgs e)
        {
            if(lis_units.SelectedItem != null)
            {
                Unit unit = (Unit)lis_units.SelectedItem;
                lis_units.SelectedItem = null;
                Application.Current.MainPage = new NavigationPage(new AddUnit(Application.Current.MainPage, unit));
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(new AddUnit(Application.Current.MainPage));
            }
        }
    }
}