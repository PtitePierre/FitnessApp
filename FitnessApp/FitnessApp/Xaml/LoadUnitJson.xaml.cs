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
            
            #region How to load an Json file embedded resource
            
            string name = "unit.json";
            var assembly = this.GetType().GetTypeInfo().Assembly;
            var resources = assembly.GetManifestResourceNames();
            var resourceName = resources.Single(r => r.EndsWith(name, StringComparison.OrdinalIgnoreCase));
            var stream = assembly.GetManifestResourceStream(resourceName);

            List<Unit> units = new List<Unit>();

            try
            {
                using (var reader = new System.IO.StreamReader(stream))
                {

                    var json = reader.ReadToEnd();
                    var rootobject = JsonConvert.DeserializeObject<List<Unit>>(json);

                    units = rootobject;
                }

            }
            catch (Exception e)
            {
                DependencyService.Get<IMessage>().longtime(e.Message);
                Unit u1 = new Unit();
                u1.Code = "ERR";
                u1.Name = "ERROR";
                u1.Id = 1;
                units.Add(u1);

            }
            finally
            {
                string msg = "";
                foreach(Unit u in units)
                {
                    msg += u.Code + " ";
                }
                DependencyService.Get<IMessage>().shorttime(msg);
            }

            #endregion

            
            InitializeComponent();

            // TO DO : Binding Data units to ListView
        }
    }
}