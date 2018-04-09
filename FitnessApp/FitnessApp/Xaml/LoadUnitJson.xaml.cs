using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;

using FitnessApp.Portable;

namespace FitnessApp
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoadUnitJson : ContentPage
	{
		public LoadUnitJson ()
		{
            /*
            #region How to load an Json file embedded resource
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(LoadResourceText)).Assembly;

            Stream stream = assembly.GetManifestResourceStream("unit.json");

            Unit[] units;

            try
            {
                using (var reader = new System.IO.StreamReader(stream))
                {

                    var json = reader.ReadToEnd();
                    var rootobject = JsonConvert.DeserializeObject<Unit[]>(json);

                    units = rootobject;
                }

            }
            catch (Exception e)
            {
                if (Device.RuntimePlatform == Device.Android)
                {

                    
                }
            }

            #endregion
            /*
            var listView = new ListView();
            listView.ItemsSource = units;

            /*
            Content = new StackLayout
            {
                Padding = new Thickness(0, 20, 0, 0),
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children = {
                    new Label { Text = "Embedded Resource JSON File (PCL)",
                        FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
                        FontAttributes = FontAttributes.Bold
                    }, listView
                }
            };
            */
            

            // NOTE: use for debugging, not in released app code!
            //foreach (var res in assembly.GetManifestResourceNames()) 
            //	System.Diagnostics.Debug.WriteLine("found resource: " + res);


            InitializeComponent();
        }
    }
}