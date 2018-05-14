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
        private bool timer_run;

        public TimerTool ()
		{
			InitializeComponent ();
            timer_run = false;
        }

        private void Button_StartTimer(object sender, EventArgs e)
        {
            if (!timer_run)
            {
                var message = "You clicked to start the timer!";
                DependencyService.Get<IMessage>().longtime(message);


                if (in_timerSpan.Text != null)
                {
                    lab_time.Text = " / " + in_timerSpan.Text;
                    int max = Int32.Parse(in_timerSpan.Text);
                    int nb = 0;
                    timer_run = true;

                    Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                    {
                        lab_ticks.Text = (++nb).ToString();
                        UpDateTimer(nb, max);

                        if (max - nb < 3)
                            DependencyService.Get<IAudio>().PlayAlarm();

                        if (nb == max)
                        {
                            timer_run = false;
                        }

                        return (timer_run); // True = Repeat again, False = Stop the timer
                    });
                }
                else
                {
                    DependencyService.Get<IMessage>().longtime("No time set.");
                }
            }
            else
            {

            }

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

            RadialGaugeChart c = new RadialGaugeChart() { Entries = entries };
            c.LabelTextSize = 0;
            cha_chrono.Chart = c;
        }
	}
}