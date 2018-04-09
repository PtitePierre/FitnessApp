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
using FitnessApp.Droid;
using FitnessApp.Portable;
using Xamarin.Forms;

[assembly:Dependency(typeof(Message_Droid))]
namespace FitnessApp.Droid
{
    public class Message_Droid : IMessage
    {
        public void longtime(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }

        public void shorttime(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show();
        }
    }
}