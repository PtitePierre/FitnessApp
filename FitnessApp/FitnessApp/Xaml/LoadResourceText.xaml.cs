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
            var resources = assembly.GetManifestResourceNames();
            var resourceName = resources.Single(r => r.EndsWith(name, StringComparison.OrdinalIgnoreCase));
            var stream = assembly.GetManifestResourceStream(resourceName);

            string text = "";
            try
            {
                using (var reader = new System.IO.StreamReader(stream))
                {
                    text = reader.ReadToEnd();
                }
            }
            catch(Exception e)
            {
                DependencyService.Get<IMessage>().shorttime(e.Message);
                text = "Echec de chargement.";
            }
            finally
            {
                DependencyService.Get<IMessage>().shorttime(text);
            }
            #endregion
            


            InitializeComponent();
            
            testLabel.Text = text;
        }
	}
}