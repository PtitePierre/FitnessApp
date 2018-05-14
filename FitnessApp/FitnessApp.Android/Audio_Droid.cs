using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Media;

using FitnessApp.Portable;
using FitnessApp.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(Audio_Droid))]
namespace FitnessApp.Droid
{
    public class Audio_Droid : IAudio
    {
        public Audio_Droid() { }

        private MediaPlayer _mediaPlayer;

        public bool PlayAlarm()
        {
            using (Ringtone r = RingtoneManager.GetRingtone(
                Android.App.Application.Context, 
                RingtoneManager.GetDefaultUri(RingtoneType.Alarm)
                )) { r.Play(); }

            return true;
        }
        /*
        public bool PlayMp3File(string fileName)
        {
            _mediaPlayer = MediaPlayer.Create(global::Android.App.Application.Context, Resource.Raw.test);
            _mediaPlayer.Start();

            return true;
        }

        public bool PlayWavFile(string fileName)
        {
            _mediaPlayer = MediaPlayer.Create(global::Android.App.Application.Context, Resource.Raw.ding_persevy);
            _mediaPlayer.Start();

            return true;
        }
        */
    }
}