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
	public partial class AddUnit : ContentPage
	{
        private Page Page;
		public AddUnit (Page page, Unit unit = null)
		{
            Page = page;
			InitializeComponent ();
            
            if (unit != null)
            {
                in_Code.Text = unit.Code;
                in_Name.Text = unit.Name;
                btn_Submit.IsEnabled = false;
            }
            /*
            else
                DependencyService.Get<IMessage>().shorttime("no unit parameter");
            */
		}

        private void Button_CancelClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = Page;
        }

        private void Button_SubmitClicked(object sender, EventArgs e)
        {
            if(in_Code.Text != null && in_Name.Text != null)
            {
                Unit newUnit = new Unit();
                newUnit.Code = in_Code.Text;
                newUnit.Name = in_Name.Text;

                SaveAndLoad.SaveUnit(newUnit);

                in_Name.Text = null;
                in_Code.Text = null;

                Application.Current.MainPage = Page;
            }
            else
            {
                DependencyService.Get<IMessage>().shorttime("Incomplete information!");
            }
        }
	}
}