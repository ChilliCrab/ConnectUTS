
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
	[Activity (Label = "Create Account", Theme = "@style/ConnectUtsTheme")]			
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
			Spinner basicInterest = FindViewById<Spinner> (Resource.Id.registerInterest);
			CheckBox agreeTac = FindViewById<CheckBox> (Resource.Id.registerAgreeTac);
			Button registerAccountButton = FindViewById<Button> (Resource.Id.registerAccountButton);
			Button cancelButton = FindViewById<Button> (Resource.Id.cancelButton);

			// Set up fonts.
			Typeface din = Typeface.CreateFromAsset (this.Assets, "fonts/din-regular.ttf");
			Typeface dinBold = Typeface.CreateFromAsset (this.Assets, "fonts/din-bold.ttf");

			// Set font to "Din".
			agreeTac.SetTypeface (din, TypefaceStyle.Normal);

			// Set font to "Din Bold".
			title.SetTypeface (dinBold, TypefaceStyle.Normal);
			registerAccountButton.SetTypeface (dinBold, TypefaceStyle.Normal);
			cancelButton.SetTypeface (dinBold, TypefaceStyle.Normal);

			string studentID = String.Empty;
			string password = String.Empty;
			string rePassword = String.Empty;
			string name = String.Empty;
			string nationality = String.Empty;
			bool tac = false;

			string path = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
			var accountDB = new SQLiteConnection (System.IO.Path.Combine(path, "account.db"));

			registerAccountButton.Click += (object sender, EventArgs e) => {
				studentID = studentIDInput.Text;
				password = passwordInput.Text;
				rePassword = rePasswordInput.Text;
				name = nameInput.Text;
				nationality = nationalityInput.Text;
				tac = agreeTac.Checked;

				string[] input = {studentID, password, rePassword, name, nationality};

				if(InputValidation.isFilled(input))
				{
					string message = "";
					var result = accountDB.Query<Account>("SELECT * FROM Account WHERE StudentID = '" + studentID + "'");

					if (result.Count != 0)
					{
						message = GetString(Resource.String.account_exists);
						DisplayUnsuccessfulAlert(message);
					}

					else if (!password.Equals(rePassword))
					{
						message = GetString(Resource.String.mismatched_passwords);
						DisplayUnsuccessfulAlert(message);
					}

					else if (basicInterest.SelectedItem.ToString()  == "Please select an interest...")
					{
						message = GetString(Resource.String.select_interest);
						DisplayUnsuccessfulAlert(message);
					}

					else if (!tac)
					{
						message = GetString(Resource.String.must_agree);
						DisplayUnsuccessfulAlert(message);
					}

					else
					{
						Account acc = new Account();
						acc.StudentID = studentID;
						acc.Password = password;
						accountDB.Insert(acc);
						Profile prof = new Profile();
						prof.StudentID = studentID;
						prof.StudentName = name;
						prof.Nationality = nationality;
						prof.ContactNumber = String.Empty;
						prof.Degree = String.Empty;
						prof.Interest = basicInterest.SelectedItem.ToString();
						prof.Year = String.Empty;
						accountDB.Insert(prof);

						var successfulAlert = new AlertDialog.Builder(this);

						successfulAlert.SetMessage(GetString(Resource.String.account_created));
						successfulAlert.SetNeutralButton("OK", delegate{
							var intent = new Intent(this, typeof(MainActivity));
							StartActivity(intent);
							// Stops user from pressing back button to return.
							Finish();
						});
						successfulAlert.Show();
					}
				}
				else
				{
					var notFilledAlert = new AlertDialog.Builder(this);

					notFilledAlert.SetMessage(GetString(Resource.String.required_fields));
					notFilledAlert.SetNegativeButton("OK", delegate{});
					notFilledAlert.Show();
				}
		
			};

			cancelButton.Click += (object sender, EventArgs e) => {
				var intent = new Intent (this, typeof(MainActivity));
				StartActivity (intent);
			};
		}

		private void DisplayUnsuccessfulAlert(String message)
		{
			var unsuccessfulAlert = new AlertDialog.Builder(this);

			unsuccessfulAlert.SetMessage(message);
			unsuccessfulAlert.SetNegativeButton("OK", delegate{});
			unsuccessfulAlert.Show();
		}
	}
}

