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
	public partial class AddSession : ContentPage
	{
		public AddSession ()
		{
			InitializeComponent ();
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            var message = "Test toast notification!";
            DependencyService.Get<IMessage>().longtime(message);
        }
	}
}