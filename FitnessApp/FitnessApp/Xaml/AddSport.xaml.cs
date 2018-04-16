using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using FitnessApp.Portable;
using System.Reflection;

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

        private void Button_SubmitClicked(object sender, EventArgs e)
        {
            if (in_Name.Text != null)
            {
                SportType newSport = new SportType();
                newSport.Name = in_Name.Text;

                SaveAndLoad.SaveSport(newSport);

                in_Name.Text = null;
                DependencyService.Get<IMessage>().shorttime("New sport type saved");
            }
        }
    }
}