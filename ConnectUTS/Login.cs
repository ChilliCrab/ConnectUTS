//
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//
//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//
//namespace ConnectUTS
//{
//	[Activity (Label = "Login", Icon = "@drawable/ic_launcher", Theme = "@style/noTitle")]			
//	public class Login : Activity
//	{
////		private EditText gIdField;
////		private EditText gPasswordField;
////		private Button gLoginButton;
////		private Button gRegisterButton;
////		private TextView gPasswordRecoveryButton;
////		private TextView gFacebookLikeButton;
////
////		private string gLoginID = String.Empty;
////		private string gLoginPassword = String.Empty;
//
//		protected override void OnCreate (Bundle bundle)
//		{
//			base.OnCreate (bundle);
//		
//			SetContentView (Resource.Layout.LoginScreen);
//
//////			gIdField = FindViewById <EditText>(Resource.Id.idField);
//////			gPasswordField = FindViewById <EditText>(Resource.Id.pwdField);
//////			gLoginButton = FindViewById <Button>(Resource.Id.loginBtn);
//////			gRegisterButton = FindViewById <Button>(Resource.Id.registerBtn);
////			gPasswordRecoveryButton = FindViewById <TextView>(Resource.Id.pwdRecoveryTxt);
////			gFacebookLikeButton = FindViewById <TextView>(Resource.Id.likeUsTxt);
////
////
////			gLoginButton.Click += GLoginButton_Click;
////			gRegisterButton.Click += GRegisterButton_Click;
////			gPasswordRecoveryButton.Click += GPasswordRecoveryButton_Click;
////			gFacebookLikeButton.Click += GFacebookLikeButton_Click;
////		}
////
////
////		private void GLoginButton_Click(object sender, EventArgs e)
////		{
////			gLoginID = gIdField.Text;
////			gLoginPassword = gPasswordField.Text;
//
//			//Check the password and id that user fill, whether it is exist in our database
//		}
//
//		void GRegisterButton_Click (object sender, EventArgs e)
//		{
//			//Intent intent = new Intent(this, typeof(Register));
//			//StartActivity(intent);
//		}
//
//		void GPasswordRecoveryButton_Click (object sender, EventArgs e)
//		{
//
//		}
//			
//		void GFacebookLikeButton_Click (object sender, EventArgs e)
//		{
//			AlertDialog.Builder confirmationBuilder = new AlertDialog.Builder(this);
//			confirmationBuilder.SetTitle("Like Us on Facebook!");
//			confirmationBuilder.SetMessage("Under Construction!");
//			confirmationBuilder.SetCancelable(false);
//			confirmationBuilder.SetNegativeButton("Close", cancel);
//
//			AlertDialog confirmationDialog = confirmationBuilder.Create();
//			confirmationDialog.Show();
//		}
//
//		private void cancel(object sender, DialogClickEventArgs e)
//		{
//
//		}
//
//
//	}
//}
//
