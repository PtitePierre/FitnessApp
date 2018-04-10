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
        private Contexte contexte;

		public LoadResourceText ()
        {

            #region How to load a text file embedded resource

            string name = "PCLTextResource.txt";
            var assembly = this.GetType().GetTypeInfo().Assembly;

            string text = DAO.LoadText(name, assembly);

            #endregion

            InitializeComponent();
            
            testLabel.Text = text;
        }
	}
}