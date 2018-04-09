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
            //var editor = new Label { Text = "loading...", HeightRequest = 300 };

            #region How to load a text file embedded resource
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(LoadResourceText)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("Fitness.PCLTextResource.txt");

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
                DependencyService.Get<IMessage>().longtime(e.Message);
                text = "Echec de chargement.";
            }
            #endregion

            //editor.Text = text;

            /*
            Content = new StackLayout
            {
                Padding = new Thickness(0, 20, 0, 0),
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children = {
                    new Label { Text = "Embedded Resource Text File (PCL)",
                        FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
                        FontAttributes = FontAttributes.Bold
                    }, editor
                }
            };
            */

            // NOTE: use for debugging, not in released app code!
            //foreach (var res in assembly.GetManifestResourceNames()) 
            //	System.Diagnostics.Debug.WriteLine("found resource: " + res);

            InitializeComponent();

            // binding part [NOT WORKING]
            testLabel.SetBinding(Label.TextProperty, new Binding("Valeur"));
            contexte = new Contexte { Valeur = text };

            //DataContext = contexte;
        }
	}
}