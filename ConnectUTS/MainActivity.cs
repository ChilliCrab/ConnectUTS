using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;

using SQLite;

namespace ConnectUTS
{
	[Activity (Label = "UTS: CONNECT", MainLauncher = true, Icon = "@drawable/ic_launcher", Theme = "@style/noTitle")]	
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.LoginScreen);

			// Get our button from the layout resource,
			// and attach an event to it
			TextView title = FindViewById<TextView>(Resource.Id.introHeading);
			Button registerButton = FindViewById<Button> (Resource.Id.registerButton);
			Button testButton = FindViewById<Button> (Resource.Id.testButton);
			Button loginButton = FindViewById<Button> (Resource.Id.loginButton);
			EditText loginIDInput = FindViewById<EditText> (Resource.Id.loginIDInput);
			EditText loginPasswordInput = FindViewById<EditText> (Resource.Id.loginPasswordInput);

			// Set the font to "Din"
			Typeface dinBold = Typeface.CreateFromAsset(this.Assets, "fonts/din-bold.ttf");

			title.SetTypeface(dinBold, TypefaceStyle.Normal);

			string path = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
			var accountDB = new SQLiteConnection (System.IO.Path.Combine(path, "account.db"));
			//accountDB.Query<Account> ("DROP TABLE Account");
			accountDB.CreateTable<Account> ();

			string loginID = String.Empty;
			string loginPassword = String.Empty;

			loginButton.Click += (sender, e) =>
			{
				string message = "";
				loginID = loginIDInput.Text;
				loginPassword = loginPasswordInput.Text;
				string[] input = {loginID, loginPassword};
				if (InputValidation.isFilled(input))
				{
					var stuList = accountDB.Query<Account>("SELECT * FROM Account WHERE StudentID = '" + loginID +"'");
					if (stuList.Count == 0)
					{
						message = "Account not exist";
					}
					else
					{
						var stu = stuList[0];
						if (!stu.Password.Equals(loginPassword))
						{
							message = "Password incorrect";
						}
						else
						{
							message = "Login Success";
							//var intent = new Intent(this, typeof());
							//StartActivity(intent);
						}
					}
				}
				else
				{
					message = "Please fill in the detail";
				}
				var loginAlert = new AlertDialog.Builder(this);
				loginAlert.SetMessage(message);
				loginAlert.SetNeutralButton("got it", delegate{});
				loginAlert.Show();
			};

			registerButton.Click += (object sender, EventArgs e) => 
			{
				var intent = new Intent(this, typeof(CreateAccountActivity));
				StartActivity(intent);
			};

			testButton.Click += (object sender, EventArgs e) => 
			{
				var stuList = accountDB.Query<Account>("SELECT * FROM Account");
				string message = "";
				if (stuList.Count != 0)
				{
					foreach (var stu in stuList)
					{
						message += stu.StudentID + " " + stu.Password + " " + stu.Nationality + "\n";
					}
				}
				else
				{
					message = "database is empty";
				}
				var dbAlert = new AlertDialog.Builder(this);
				dbAlert.SetMessage(message);
				dbAlert.SetNegativeButton("OK", delegate{});
				dbAlert.Show();
			};
			// database setup for testing
//			var account = new Account ();
//			account.StudentID = "12463170";
//			account.Password = "Test123";
//			account.StudentName = "Po-Hao Chen";
//			account.Nationality = "Taiwan";
//			accountDB.Insert (account);
//			accountDB.Delete<Account>("12463170");
//			accountDB.Delete<Account>("12466666")
		}


	}
}


