using FitnessApp.Portable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FitnessApp
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoadResourceText : ContentPage
	{
		public LoadResourceText()
        {

            #region loading a text file embedded resource

            string name = "PCLTextResource.txt";
            var assembly = this.GetType().GetTypeInfo().Assembly;

            string text = SaveAndLoad.LoadText(name, assembly);

            #endregion

            InitializeComponent();
            
            testLabel.Text = text;
        }
	}
}