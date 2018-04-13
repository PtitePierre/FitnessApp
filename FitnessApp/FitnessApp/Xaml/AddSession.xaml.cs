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
            List<Unit> units = SaveAndLoad.LoadUnits(this.GetType().GetTypeInfo().Assembly);
            List<SportType> sports = SaveAndLoad.LoadSportTypes(this.GetType().GetTypeInfo().Assembly);

            InitializeComponent ();

            pic_unit.ItemsSource = units;
            pic_sportType.ItemsSource = sports;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Session newSession = new Session();

            newSession.Quantity = Int32.Parse(in_quantity.Text);
            newSession.SType = (SportType)pic_sportType.SelectedItem;
            newSession.SUnit = (Unit)pic_unit.SelectedItem;
            newSession.SDate = pic_date.Date + pic_time.Time;

            DependencyService.Get<IMessage>().longtime(newSession.ToString());

            SaveAndLoad.SaveSessions(this.GetType().GetTypeInfo().Assembly, newSession);
        }
    }
}