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
	public partial class ViewSessionList : ContentPage
	{
		public ViewSessionList ()
        {
            InitializeComponent ();
            FillListView();
        }

        private async void FillListView()
        {
            List<Session> sessions = await SaveAndLoad.LoadSessions();

            lis_sessions.ItemsSource = sessions;
        }
	}
}