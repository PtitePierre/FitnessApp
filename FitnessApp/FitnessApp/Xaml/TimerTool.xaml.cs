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

            if(in_timerSpan.Text != null)
            {
                lab_time.Text = " / " + in_timerSpan.Text;
                int max = Int32.Parse(in_timerSpan.Text);
                int nb = 0;

                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    //var txt = (++nb).ToString();
                    //DependencyService.Get<IMessage>().shorttime(txt);

                    lab_ticks.Text = (++nb).ToString();

                    return (nb < max); // True = Repeat again, False = Stop the timer
                });

            }
            else
            {
                DependencyService.Get<IMessage>().longtime("No time set.");
            }

        }
	}
}