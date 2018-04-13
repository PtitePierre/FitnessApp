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
            List<Session> sessions = SaveAndLoad.LoadSessions(this.GetType().GetTypeInfo().Assembly);

            InitializeComponent ();

            lis_sessions.ItemsSource = sessions;
        }
	}
}