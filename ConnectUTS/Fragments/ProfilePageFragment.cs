
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using SQLite;

namespace ConnectUTS
{
	public class ProfilePageFragment : Fragment
	{
		EditText profileIdInput;
		EditText profileNameInput;
		EditText profileNationalityInput;
		EditText profileContactNumberInput;
//		EditText profileDegreeInput;
//		EditText profileYearInput;
//		EditText profileInterestInput;
		Button profileEditButton;
		SQLiteConnection accountDB;
		string mode = "Confirmed";
		//Account acc;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here

		}

		private void changeEdittable(bool canEdit)
		{
			profileNameInput.Enabled = canEdit;
			profileNationalityInput.Enabled = canEdit;
			profileContactNumberInput.Enabled = canEdit;
//			profileDegreeInput.Enabled = canEdit;
//			profileYearInput.Enabled = canEdit;
//			profileInterestInput.Enabled = canEdit;
		}
		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment


			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);


			View view = inflater.Inflate(Resource.Layout.ProfilePageLayout, container, false);
			profileIdInput = view.FindViewById<EditText>(Resource.Id.profileIdInput);
			profileNameInput = view.FindViewById<EditText>(Resource.Id.profileNameInput);
			profileNationalityInput = view.FindViewById<EditText>(Resource.Id.profileNationalityInput);
			profileContactNumberInput = view.FindViewById<EditText>(Resource.Id.profileContactInput);
			//			profileDegreeInput = view.FindViewById<EditText>(Resource.Id);
			//			profileYearInput = view.FindViewById<EditText>(Resource.Id);
			//			profileInterestInput = view.FindViewById<EditText>(Resource.Id);
			profileEditButton = view.FindViewById<Button> (Resource.Id.profileEditButton);

			string path = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
			accountDB = new SQLiteConnection (System.IO.Path.Combine(path, "account.db"));
			//acc = accountDB.Query<Account>("SELECT * FROM Account WHERE StudentID = '12463170'");
			var prof = accountDB.Query<Profile>("SELECT * FROM Profile WHERE StudentID = '12463170'");

			profileIdInput.Text = prof[0].StudentID;
			prof [0].StudentName = "Po-Hao Chen";
			profileNameInput.Text = prof[0].StudentName;
			profileNationalityInput.Text = prof[0].Nationality;
			profileContactNumberInput.Text = prof[0].ContactNumber;	

			profileEditButton.Click += (sender, e) => {
				switch(mode)
				{
				case "Confirmed":
					mode = "Edittable";
					changeEdittable(true);
					profileEditButton.Text = "Confirm";
					break;
				case "Edittable":
					mode = "Confirmed";
					changeEdittable(false);
					profileEditButton.Text = "Edit";
					break;
				}

			};
			return view;
		}
	}
}

