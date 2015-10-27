
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

namespace ConnectUTS
{
	[Activity (Label = "DatabaseActivity")]			
	public class DatabaseActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView(Resource.Layout.DatabaseScreen);
			Button setup = FindViewById<Button> (Resource.Id.databaseSetup);
			Button reset = FindViewById<Button> (Resource.Id.databaseReset);
			Button check = FindViewById<Button> (Resource.Id.databaseCheck);

			setup.Click += delegate {
				
				string message = DatabaseSetup.InitializeDatabase();
				var dbAlert = new AlertDialog.Builder(this);
				dbAlert.SetMessage(message);
				dbAlert.SetNegativeButton("OK", delegate{});
				dbAlert.Show();
			};

			reset.Click += delegate {
				
				string message = DatabaseSetup.ResetDatabase();
				var dbAlert = new AlertDialog.Builder(this);
				dbAlert.SetMessage(message);
				dbAlert.SetNegativeButton("OK", delegate{});
				dbAlert.Show();
			};

			check.Click += (sender, e) => 
			{
				string message = DatabaseSetup.CheckDatabase();
				var dbAlert = new AlertDialog.Builder(this);
				dbAlert.SetMessage(message);
				dbAlert.SetNegativeButton("OK", delegate{});
				dbAlert.Show();
			};
		}
	}
}

