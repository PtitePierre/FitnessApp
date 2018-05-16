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

        /// <summary>
        /// Check if there is a user loaded in SavaAndLoad
        /// Display the corresponding Page.Content
        /// </summary>
        private void CheckUser()
        {
            // read user.json
            user = SaveAndLoad.GetUser();

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Logout(object sender, EventArgs e)
        {
            user = null;
            SaveAndLoad.Logout();

            user_logged.IsVisible = false;
            user_newORloggin.IsVisible = true;
        }

        /// <summary>
        /// create a new user from the form
        /// change the Visible Page.Content
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Loggin(object sender, EventArgs e)
        {
            SaveAndLoad.LoadUser(in_userLoggin.Text, in_pwdloggin.Text);
            user_logged.IsVisible = false;
            user_newORloggin.IsVisible = true;
            CheckUser();
        }
	}
}