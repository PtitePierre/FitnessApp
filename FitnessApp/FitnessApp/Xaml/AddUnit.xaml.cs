﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FitnessApp.Portable;

namespace FitnessApp
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddUnit : ContentPage
	{
        private Page Page;
		public AddUnit (Page page, Unit unit = null)
		{
            Page = page;
			InitializeComponent ();
            
            if (unit != null)
            {
                in_Code.Text = unit.Code;
                in_Name.Text = unit.Name;
            }
            else
            {
                DependencyService.Get<IMessage>().shorttime("no unit parameter");
            }
		}

        private void Button_CancelClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = Page;            
        }
	}
}