
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

using SQLite;
namespace ConnectUTS
{
	[Activity (Label = "Create Account")]			
	public class CreateAccountActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.CreateAccount);

			EditText studentIDInput = FindViewById<EditText> (Resource.Id.studentIDInput);
			EditText passwordInput = FindViewById<EditText> (Resource.Id.passwordInput);
			EditText rePasswordInput = FindViewById<EditText> (Resource.Id.rePasswordInput);
			Button registerAccountButton = FindViewById<Button> (Resource.Id.registerAccountButton);

			string studentID = String.Empty;
			string password = String.Empty;
			string rePassword = String.Empty;

			string path = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
			var accountDB = new SQLiteConnection (System.IO.Path.Combine(path, "account.db"));

			registerAccountButton.Click += (object sender, EventArgs e) => {
				studentID = studentIDInput.Text;
				password = passwordInput.Text;
				rePassword = rePasswordInput.Text;

				if(String.IsNullOrWhiteSpace(studentID) || String.IsNullOrWhiteSpace(password) || String.IsNullOrWhiteSpace(rePassword))
				{
					var notFilledAlert = new AlertDialog.Builder(this);
					notFilledAlert.SetMessage("Please fill the required field");
					notFilledAlert.SetNegativeButton("OK", delegate{});
					notFilledAlert.Show();
				}
				else
				{
					string message = "";
					var result = accountDB.Query<Account>("SELECT * FROM Account WHERE StudentID = '" + studentID + "'");
					if (result.Count != 0)
					{
						message = "Student ID already exist";
					}
					else
					{
						if (!password.Equals(rePassword))
						{
							message = "Password mismatch";
						}
						else
						{
							Account acc = new Account();
							acc.StudentID = studentID;
							acc.Password = password;
							accountDB.Insert(acc);
							message = "Account created";
						}
					}
					var filledAlert = new AlertDialog.Builder(this);
					filledAlert.SetMessage(message);
					filledAlert.SetNegativeButton("OK", delegate{});
					filledAlert.Show();
					
				}
		
			};


		}
	}
}

