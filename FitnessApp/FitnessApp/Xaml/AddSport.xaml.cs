using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using FitnessApp.Portable;

namespace FitnessApp
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddSport : ContentPage
    {
        private Page Page;
        public AddSport (Page page, SportType sport = null)
        {
            Page = page;
            InitializeComponent();

            if (sport != null)
            {
                in_Name.Text = sport.Name;
            }
            else
            {
                DependencyService.Get<IMessage>().shorttime("no sport parameter");
            }
        }

        private void Button_CancelClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = Page;
        }
    }
}