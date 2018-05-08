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
	public partial class TimerTool : ContentPage
	{
		public TimerTool ()
		{
			InitializeComponent ();
		}

        private void Button_StartTimer(object sender, EventArgs e)
        {
            var message = "You clicked to start the timer!";
            DependencyService.Get<IMessage>().longtime(message);
        }
	}
}