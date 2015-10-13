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
	[Activity (Label = "ConnectUTS", MainLauncher = true, Icon = "@drawable/ic_launcher")]
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
			Button loginButton = FindViewById<Button> (Resource.Id.loginButton);
			EditText loginIDInput = FindViewById<EditText> (Resource.Id.loginIDInput);
			EditText loginPasswordInput = FindViewById<EditText> (Resource.Id.loginPasswordInput);

			string path = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
			var accountDB = new SQLiteConnection (System.IO.Path.Combine(path, "account.db"));
			accountDB.CreateTable<Account> ();

			string loginID = String.Empty;
			string loginPassword = String.Empty;

			loginButton.Click += (sender, e) =>
			{
				string message = "";
				loginID = loginIDInput.Text;
				loginPassword = loginPasswordInput.Text;
				if (String.IsNullOrWhiteSpace(loginID) || String.IsNullOrWhiteSpace(loginPassword))
				{
					message = "Please fill in the detail";
				}
				else
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
				var loginAlert = new AlertDialog.Builder(this);
				loginAlert.SetMessage(message);
				loginAlert.SetNeutralButton("got it", delegate{});
				loginAlert.Show();
			};

			createAccountButton.Click += (object sender, EventArgs e) => 
			{
				var intent = new Intent(this, typeof(CreateAccountActivity));
				StartActivity(intent);
			};

			testButton.Click += (object sender, EventArgs e) => 
			{
				var stuList = accountDB.Query<Account>("SELECT * FROM Account");
				string message = "";
				foreach (var stu in stuList)
				{
					message += stu.StudentID + " " + stu.Password +"\n";
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


