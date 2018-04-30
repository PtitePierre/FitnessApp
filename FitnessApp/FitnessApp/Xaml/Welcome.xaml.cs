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
	public partial class Welcome : ContentPage
	{
		public Welcome ()
		{
			InitializeComponent ();
            CheckUser();
		}

        private void CheckUser()
        {
            // read user.json
            bool hasUser = true;
            if (hasUser)
            {
                user_logged.IsVisible = true;
                user_new.IsVisible = false;
            }
            else
            {
                user_logged.IsVisible = false;
                user_new.IsVisible = true;
            }
        }

        private void Button_Logout(object sender, EventArgs e)
        {
            InitializeComponent();

            user_logged.IsVisible = false;
            user_new.IsVisible = true;
        }

        private void Button_NewUser(object sender,EventArgs e)
        {
            InitializeComponent();

            user_logged.IsVisible = true;
            user_new.IsVisible = false;
        }
	}
}