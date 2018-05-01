using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using FitnessApp.Portable;
using System.Reflection;

namespace FitnessApp
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddSport : ContentPage
    {
        private Page Page;
        private List<int> units_id;

        public AddSport (Page page, SportType sport = null)
        {
            Page = page;
            InitializeComponent();

            if (sport != null)
            {
                in_Name.Text = sport.Name;
                units_id = sport.Units;
            }
            else
            {
                DependencyService.Get<IMessage>().shorttime("no sport parameter");
                units_id = new List<int>();
            }
            FillListViews();
        }

        private async void FillListViews()
        {
            lis_units.Children.Clear();
            List<Unit> units = await SaveAndLoad.LoadUnits();

            foreach(Unit unit in units)
            {
                Button btn = new Button();
                btn.Text = unit.ToStringBtn();
                btn.Clicked += delegate
                {
                    Button_SelectedUnit(btn);
                };

                if(units_id.IndexOf(unit.Id) != -1)
                {
                    btn.BackgroundColor = Color.FromRgba(220, 120, 220, 120);
                }

                lis_units.Children.Add(btn);
            }
        }

        private void Button_CancelClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = Page;
        }

        private void Button_SubmitClicked(object sender, EventArgs e)
        {
            if (in_Name.Text != null)
            {
                SportType newSport = new SportType();
                newSport.Name = in_Name.Text;
                newSport.Units = units_id;

                SaveAndLoad.SaveSport(newSport);

                in_Name.Text = null;
                units_id.RemoveRange(0, units_id.Count);
                FillListViews();

                DependencyService.Get<IMessage>().shorttime("New sport type saved");
            }
            Application.Current.MainPage = Page;
        }

        private void Button_SelectedUnit(Button btn)
        {
            int id = Int32.Parse(btn.Text.Substring(0, btn.Text.IndexOf(".")));
            if (units_id.IndexOf(id) == -1)
            {
                btn.BackgroundColor = Color.FromRgba(50, 120, 120, 120);
                units_id.Add(id);
            }
            else
            {
                btn.BackgroundColor = Color.FromRgba(220, 220, 220, 230);
                units_id.Remove(id);
            }

            string txt = "units : ";
            foreach (int i in units_id) { txt += i + ","; }
            DependencyService.Get<IMessage>().shorttime(txt);
        }
    }
}