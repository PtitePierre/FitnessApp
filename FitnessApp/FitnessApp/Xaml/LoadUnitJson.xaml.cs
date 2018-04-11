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
            List<Unit> units = SaveAndLoad.LoadUnits(this.GetType().GetTypeInfo().Assembly);
            
            InitializeComponent();

            // TO DO : Binding Data units to ListView
            UnitList.ItemsSource = units;
        }
    }
}