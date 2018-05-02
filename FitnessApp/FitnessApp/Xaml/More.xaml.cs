using FitnessApp.Portable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitnessApp
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class More : ContentPage
	{
		public More ()
		{
			InitializeComponent ();
		}

        private void Button_ReloadUnit(object sender, EventArgs e)
        {
            SaveAndLoad.LoadUnits(true);
        }

        private void Button_ReloadSport(object sender, EventArgs e)
        {
            SaveAndLoad.LoadSports(true);
        }

        private void Button_ReloadSession(object sender, EventArgs e)
        {
            SaveAndLoad.LoadSessions(true);
        }
    }
}