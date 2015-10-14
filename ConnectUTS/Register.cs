
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
	[Activity (Label = "Register")]			
	public class Register : Activity
	{
		private EditText gRegisterId;
		private EditText gRegisterPassword;
		private EditText gConfirmPassword;
		private EditText gRegisterName;
		private EditText gRegisterEmail;
		private EditText gRegisterNationality;
		private Button gSubmitButton;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
		
			SetContentView (Resource.Layout.RegisterScreen);

			gRegisterId = FindViewById <EditText>(Resource.Id.registerId);
			gRegisterPassword = FindViewById <EditText>(Resource.Id.registerPassword);
			gConfirmPassword = FindViewById <EditText>(Resource.Id.confirmPassword);
			gRegisterName = FindViewById <EditText>(Resource.Id.registerName);
			gRegisterEmail = FindViewById <EditText>(Resource.Id.registerEmail);
			gRegisterNationality = FindViewById <EditText>(Resource.Id.registerNationality);
			gSubmitButton = FindViewById<Button> (Resource.Id.submitBtn);

			gSubmitButton.Click += GSubmitButton_Click;;
		}

		void GSubmitButton_Click (object sender, EventArgs e)
		{
			//check all the field is filled
			//if the student id & email has not been registered before
			//use the data to make a new account and direct to login screen

			Intent intent = new Intent (this, typeof(Login));
			StartActivity (intent);

			//else show message saying the error
		}
			


	}
}

