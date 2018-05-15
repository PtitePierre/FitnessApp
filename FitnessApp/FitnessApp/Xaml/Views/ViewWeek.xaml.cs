using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FitnessApp.Portable;

namespace FitnessApp
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewWeek : ContentPage
	{
		public ViewWeek ()
		{
			InitializeComponent ();
		}

        private void Button_NextWeek(object sender, EventArgs e)
        {
            DependencyService.Get<IMessage>().shorttime("Next Week");
        }

        private void Button_PrevWeek(object sender, EventArgs e)
        {
            DependencyService.Get<IMessage>().shorttime("Prev Week");
        }
    }
}