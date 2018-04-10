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
            List<Unit> units = SaveAndLoad.loadUnits(this.GetType().GetTypeInfo().Assembly);
            List<SportType> sports = SaveAndLoad.loadSportTypes(this.GetType().GetTypeInfo().Assembly);

            InitializeComponent ();

            pic_unit.ItemsSource = units;
            pic_sportType.ItemsSource = sports;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var message = "Test toast notification!";
            DependencyService.Get<IMessage>().longtime(message);
        }
	}
}