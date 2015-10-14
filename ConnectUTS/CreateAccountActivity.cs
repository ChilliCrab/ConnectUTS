
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
using Android.Graphics;

using SQLite;
namespace ConnectUTS
{
	[Activity (Label = "Create Account", Theme = "@style/noTitle")]			
	public class CreateAccountActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.RegisterScreen);

			TextView title = FindViewById<TextView> (Resource.Id.registerHeading);
			EditText studentIDInput = FindViewById<EditText> (Resource.Id.registerStudentIDInput);
			EditText passwordInput = FindViewById<EditText> (Resource.Id.registerPasswordInput);
			EditText rePasswordInput = FindViewById<EditText> (Resource.Id.registerRePasswordInput);
			EditText nameInput = FindViewById<EditText> (Resource.Id.registerStudentNameInput);
			EditText nationalityInput = FindViewById<EditText> (Resource.Id.registerNationalityInput);
			Button registerAccountButton = FindViewById<Button> (Resource.Id.registerAccountButton);

			// Set the font to "Din Bold"
			Typeface dinBold = Typeface.CreateFromAsset (this.Assets, "fonts/din-bold.ttf");

			title.SetTypeface (dinBold, TypefaceStyle.Normal);

			string studentID = String.Empty;
			string password = String.Empty;
			string rePassword = String.Empty;
			string name = String.Empty;
			string nationality = String.Empty;

			string path = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
			var accountDB = new SQLiteConnection (System.IO.Path.Combine(path, "account.db"));

			registerAccountButton.Click += (object sender, EventArgs e) => {
				studentID = studentIDInput.Text;
				password = passwordInput.Text;
				rePassword = rePasswordInput.Text;
				name = nameInput.Text;
				nationality = nationalityInput.Text;
				string[] input = {studentID, password, rePassword, name, nationality};
				if(InputValidation.isFilled(input))
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
							acc.StudentName = name;
							acc.Nationality = nationality;
							accountDB.Insert(acc);
							message = "Account created";
						}
					}
					var filledAlert = new AlertDialog.Builder(this);
					filledAlert.SetMessage(message);
					filledAlert.SetNeutralButton("OK", delegate{
						var intent = new Intent(this, typeof(MainActivity));
						StartActivity(intent);
					});
					filledAlert.Show();
				}
				else
				{
					var notFilledAlert = new AlertDialog.Builder(this);
					notFilledAlert.SetMessage("Please fill the required field");
					notFilledAlert.SetNegativeButton("OK", delegate{});
					notFilledAlert.Show();
				}
		
			};


		}
	}
}

