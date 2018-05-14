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
	public partial class Welcome : ContentPage
	{
        private static User user;
		public Welcome ()
		{
			InitializeComponent ();
            CheckUser();
		}

        private async void CheckUser()
        {
            // read user.json
            user = await SaveAndLoad.LoadUser();

            bool hasUser = (user!=null);

            if (hasUser)
            {
                user_name.Text = user.Name;
                user_logged.IsVisible = true;
                user_newORloggin.IsVisible = false;
            }
            else
            {
                user_name.Text = null;
                user_logged.IsVisible = false;
                user_newORloggin.IsVisible = true;
            }
        }

        private void Button_Logout(object sender, EventArgs e)
        {
            user = null;

            user_logged.IsVisible = false;
            user_newORloggin.IsVisible = true;
        }

        private void Button_NewUser(object sender,EventArgs e)
        {
            user = new User();
            user.Name = in_username.Text;
            user.Email = in_email.Text;
            user.Password = in_pwd.Text;

            SaveAndLoad.SaveUser(user);

            user_logged.IsVisible = true;
            user_newORloggin.IsVisible = false;
        }

        private void Button_Loggin(object sender, EventArgs e)
        {

        }
	}
}