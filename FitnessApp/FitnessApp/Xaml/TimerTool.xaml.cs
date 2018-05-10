using FitnessApp.Portable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitnessApp
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TimerTool : ContentPage
	{
		public TimerTool ()
		{
			InitializeComponent ();
        }

        private void Button_StartTimer(object sender, EventArgs e)
        {
            var message = "You clicked to start the timer!";
            DependencyService.Get<IMessage>().longtime(message);

            
            // Create an AutoResetEvent to signal the timeout threshold in the
            // timer callback has been reached.
            var autoEvent = new AutoResetEvent(false);

            var statusChecker = new StatusChecker(10);

            // Create a timer that invokes CheckStatus after one second, 
            // and every 1/4 second thereafter.
            Console.WriteLine("{0:h:mm:ss.fff} Creating timer.\n",
                              DateTime.Now);
            var stateTimer = new Timer(statusChecker.CheckStatus,
                                       autoEvent, 1000, 250);

            // When autoEvent signals, change the period to every half second.
            autoEvent.WaitOne();
            stateTimer.Change(0, 500);
            Console.WriteLine("\nChanging period to .5 seconds.\n");

            // When autoEvent signals the second time, dispose of the timer.
            autoEvent.WaitOne();
            stateTimer.Dispose();
            Console.WriteLine("\nDestroying timer.");
            
        }
	}
}