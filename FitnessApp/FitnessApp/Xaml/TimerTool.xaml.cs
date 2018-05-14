using FitnessApp.Portable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microcharts;
using SkiaSharp;
using Microcharts.Forms;
using Entry = Microcharts.Entry;

namespace FitnessApp
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TimerTool : ContentPage
    {
        private bool run;
        private DateTime start;
        public TimerTool ()
		{
			InitializeComponent ();
            run = false;
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
                    lab_ticks.Text = (++nb).ToString();
                    UpDateTimer(nb, max);

                    return (nb < max); // True = Repeat again, False = Stop the timer
                });

            }
            else
            {
                DependencyService.Get<IMessage>().longtime("No time set.");
            }

        }

        private void Button_StartChrono(object sender, EventArgs e)
        {
            if (!run)
            {
                run = true;
                btn_chrono.Text = "Stop";
                start = DateTime.Now;
            }
            else
            {
                run = false;
                btn_chrono.Text = "Start";
            }
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                lab_chrono.Text = (DateTime.Now - start).ToString(@"hh\:mm\:ss");

                return (run); // True = Repeat again, False = Stop the timer
            });
        }

        private void UpDateTimer(int nb, int max)
        {
            // set chart entries
            var entries = new[]{
                     new Entry(max)
                     {
                         Label = "total",
                         ValueLabel = max.ToString(),
                         Color = SKColor.Parse("#3498db")
                     },
                     new Entry(nb)
                     {
                         Label = "done",
                         ValueLabel = nb.ToString(),
                         Color = SKColor.Parse("#2c3e50")
                     }};

            cha_chrono.Chart = new RadialGaugeChart() { Entries = entries };
        }
	}
}