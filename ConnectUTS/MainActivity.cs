using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using SQLite;

namespace ConnectUTS
{
	[Activity (Label = "ConnectUTS", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button createAccountButton = FindViewById<Button> (Resource.Id.createAccountButton);
			Button testButton = FindViewById<Button> (Resource.Id.testButton);

			string path = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
			var accountDB = new SQLiteConnection (System.IO.Path.Combine(path, "account.db"));
			accountDB.CreateTable<Account> ();

			createAccountButton.Click += (object sender, EventArgs e) => 
			{
				var intent = new Intent(this, typeof(CreateAccountActivity));
				StartActivity(intent);
			};

			testButton.Click += (object sender, EventArgs e) => 
			{
				var stuList = accountDB.Query<Account>("SELECT * FROM Account WHERE StudentID = '12466170'");
				string message = "";
				if (stuList.Count == 0)
				{
					message = "Student not Exist";
				}
				else
				{
					var stu = stuList[0];
					message += stu.StudentID + "\n" + stu.Password;
				}
				var dbAlert = new AlertDialog.Builder(this);
				dbAlert.SetMessage(message);
				dbAlert.SetNegativeButton("OK", delegate{});
				dbAlert.Show();
			};


//			var account = new Account ();
//			account.StudentID = "12463170";
//			account.Password = "Test123";
//			accountDB.Insert (account);
		}


	}
}


