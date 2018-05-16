using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitnessApp
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Chronometer : ContentPage
    {
        private bool run;
        private TimeSpan total;
        private DateTime start;
        private Stopwatch sw;

        public Chronometer ()
		{
			InitializeComponent ();
            sw = new Stopwatch();
            run = false;
            total = TimeSpan.FromSeconds(0);
            btn_pause.IsEnabled = false;
        }

        private void Button_StartChrono(object sender, EventArgs e)
        {
            if (!run)
            {
                run = true;
                btn_pause.IsEnabled = true;
                btn_start.IsEnabled = false;
                start = DateTime.Now;
                RunTimer();

                sw = Stopwatch.StartNew();
            }
        }

        private void Button_PauseChrono(object sender, EventArgs e)
        {
            if (run)
            {
                sw.Stop();

                run = false;
                total += sw.Elapsed;
                UpdateLabel(total);
                btn_start.IsEnabled = true;
                btn_pause.IsEnabled = false;
            }
        }

        private void Button_ResetChrono(object sender, EventArgs e)
        {
            total = TimeSpan.FromSeconds(0);
            UpdateLabel(total);
            btn_pause.IsEnabled = false;
            btn_start.IsEnabled = true;
            run = false;
        }

        private void RunTimer()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(50), () =>
            {
                TimeSpan ts = total + sw.Elapsed;
                if(run)
                    UpdateLabel(ts);

                return (run); // True = Repeat again, False = Stop the timer
            });
        }

        private void UpdateLabel(TimeSpan span)
        {
            lab_chrono.Text = span.ToString(@"hh\:mm\:ss\.ff");
        }
    }
}