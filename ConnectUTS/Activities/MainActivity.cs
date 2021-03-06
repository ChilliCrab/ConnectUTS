﻿using System;

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
	[Activity (Label = "ConnectUTS", MainLauncher = true, Icon = "@drawable/ic_launcher", Theme = "@style/ConnectUtsTheme")]	
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
			TextView forgottenPassword = FindViewById<TextView> (Resource.Id.pwdRecoveryTxt);
			Button facebookButton = FindViewById<Button> (Resource.Id.facebookButton);

			// Set the font to "Din Bold"
			Typeface dinBold = Typeface.CreateFromAsset (this.Assets, "fonts/din-bold.ttf");

			title.SetTypeface (dinBold, TypefaceStyle.Normal);
			loginButton.SetTypeface (dinBold, TypefaceStyle.Normal);
			registerButton.SetTypeface (dinBold, TypefaceStyle.Normal);
			testButton.SetTypeface (dinBold, TypefaceStyle.Normal);
			forgottenPassword.SetTypeface (dinBold, TypefaceStyle.Normal);
			facebookButton.SetTypeface (dinBold, TypefaceStyle.Normal);

			// local database connection
			string path = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
			var accountDB = new SQLiteConnection (System.IO.Path.Combine(path, "Database.db"));


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
						message = GetString(Resource.String.no_account);
					}
					else
					{
						var stu = stuList[0];
						if (!stu.Password.Equals(loginPassword))
						{
							message = GetString(Resource.String.password_incorrect);
						}
						else
						{
							message = GetString(Resource.String.log_in_successful);

							var intent = new Intent(this, typeof(DashboardActivity));
							intent.PutExtra("studentID", loginID);
							StartActivity(intent);
							// Stops user from pressing back button to return.
							Finish();
						}
					}
				}
				else
				{
					message = "Please fill in the detail";
				}
				var loginAlert = new AlertDialog.Builder(this);
				loginAlert.SetMessage(message);
				loginAlert.Show();
			};

			registerButton.Click += (object sender, EventArgs e) => 
			{
				var intent = new Intent(this, typeof(CreateAccountActivity));
				StartActivity(intent);
			};

			testButton.Click += (object sender, EventArgs e) => 
			{
					var intent = new Intent(this, typeof(DatabaseActivity));
					StartActivity(intent);
			};

			facebookButton.Click += delegate
			{
				// Opens external Facebook page where users can like the UTS:Connect page.
				SendToFacebook();
			};

			// database set up
			//DatabaseSetup.InitializeDatabase ();
		}

		private void SendToFacebook()
		{
			Android.Net.Uri uri = Android.Net.Uri.Parse("https://m.facebook.com/Connect-UTS-1486585674979293/");
			Intent intent = Intent.CreateChooser(new Intent(Intent.ActionView, uri), "Open with");
			StartActivity(intent);
		}
	}
}


