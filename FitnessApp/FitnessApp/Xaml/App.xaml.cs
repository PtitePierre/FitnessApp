using FitnessApp.Portable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace FitnessApp
{
	public partial class App : Application
	{
        private Unit[] units;

		public App ()
		{
			InitializeComponent();

			MainPage = new FitnessApp.MainPage();
		}

		protected override void OnStart ()
		{
            // Handle when your app starts

            // TO DO : load data
		}

		protected override void OnSleep ()
		{
            // Handle when your app sleeps

            // TO DO : save sessions
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes

            // TO DO : save sessions
		}
	}
}
