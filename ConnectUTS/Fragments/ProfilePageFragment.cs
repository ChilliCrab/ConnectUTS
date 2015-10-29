
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
		TextView profileIdInput;
		EditText profileNameInput;
		EditText profileNationalityInput;
		EditText profileYearInput;
		EditText profileContactNumberInput;
		Spinner profileFieldOfStudyInput;
		Spinner profileInterestInput;
		Button profileEditButton;
		SQLiteConnection accountDB;
		string mode = "Confirmed";
		string studentID = "";
		List<Profile> prof = null;
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
			profileYearInput.Enabled = canEdit;
			profileContactNumberInput.Enabled = canEdit;
			profileFieldOfStudyInput.Enabled = canEdit;
			profileInterestInput.Enabled = canEdit;
		}
		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment


			studentID = Arguments.GetString ("studentID");

			View view = inflater.Inflate(Resource.Layout.ProfilePageLayout, container, false);
			profileIdInput = view.FindViewById<TextView>(Resource.Id.profileIdInput);
			profileNameInput = view.FindViewById<EditText>(Resource.Id.profileNameInput);
			profileNationalityInput = view.FindViewById<EditText>(Resource.Id.profileNationalityInput);
			profileYearInput = view.FindViewById<EditText>(Resource.Id.profileYearInput);
			profileContactNumberInput = view.FindViewById<EditText>(Resource.Id.profileContactInput);
			profileFieldOfStudyInput = view.FindViewById<Spinner>(Resource.Id.profileFieldOfStudyInput);
			profileInterestInput = view.FindViewById<Spinner>(Resource.Id.profileInterestInput);
			profileEditButton = view.FindViewById<Button> (Resource.Id.profileEditButton);

			profileFieldOfStudyInput.Enabled = false;
			profileInterestInput.Enabled = false;

			string path = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
			accountDB = new SQLiteConnection (System.IO.Path.Combine(path, "Database.db"));
			//acc = accountDB.Query<Account>("SELECT * FROM Account WHERE StudentID = '12463170'");
			prof = accountDB.Query<Profile>("SELECT * FROM Profile WHERE StudentID = '" + studentID + "'");
			var adapter = profileInterestInput.Adapter;

			profileIdInput.Text = prof[0].StudentID;
			profileNameInput.Text = prof[0].StudentName;
			profileNationalityInput.Text = prof[0].Nationality;
			profileContactNumberInput.Text = prof[0].ContactNumber;
			//profileFieldOfStudyInput.Text = prof [0].Degree;
//			for (int i = 0; i<adapter.Count; i++)
//			{
//				if(adapter.GetItemId(i).ToString().Equals(prof [0].Interest))
//					profileInterestInput.SetSelection(i);
//			}
			profileYearInput.Text = prof [0].Year;

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
					accountDB.Query<Profile>("UPDATE Profile SET StudentName = '" + profileNameInput.Text + "' WHERE StudentID = '" + studentID + "'");
					accountDB.Query<Profile>("UPDATE Profile SET Nationality = '" + profileNationalityInput.Text + "' WHERE StudentID = '" + studentID + "'");
					accountDB.Query<Profile>("UPDATE Profile SET ContactNumber = '" + profileContactNumberInput.Text + "' WHERE StudentID = '" + studentID + "'");

					break;
				}

			};
			return view;
		}
	}
}

