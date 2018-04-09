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
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GpsTracker : ContentPage
	{
		public GpsTracker ()
		{
			InitializeComponent ();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var message = "You clicked to launche GPS tracking!";
            DependencyService.Get<IMessage>().longtime(message);
        }
    }
}