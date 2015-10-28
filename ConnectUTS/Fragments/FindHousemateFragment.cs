
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
	public class FindHousemateFragment : Fragment
	{
		EditText Address;
		EditText Suburb;
		EditText Rent;
		EditText PreferredContact;
		EditText Description;
		Button editButton;
		SQLiteConnection db;
		string mode = "Confirmed";
		string studentID = "";
		string accoID = "";
		List<Accommodation> acco = null;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		private void changeEdittable(bool canEdit)
		{
			Address.Enabled = canEdit;
			Suburb.Enabled = canEdit;
			Rent.Enabled = canEdit;
			PreferredContact.Enabled = canEdit;
			Description.Enabled = canEdit;
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);
			studentID = Arguments.GetString ("studentID");

			View view = inflater.Inflate(Resource.Layout.FindHousemateLayout, container, false);
			Address = view.FindViewById<EditText> (Resource.Id.housemateAddressInput);
			Suburb = view.FindViewById<EditText> (Resource.Id.housemateSuburbInput);
			Rent = view.FindViewById<EditText> (Resource.Id.housemateRentInput);
			PreferredContact = view.FindViewById<EditText> (Resource.Id.housemateContactInput);
			Description = view.FindViewById<EditText> (Resource.Id.housemateDescriptionInput);
			editButton = view.FindViewById<Button> (Resource.Id.housemateEditButton);

			string path = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
			db = new SQLiteConnection (System.IO.Path.Combine(path, "Database.db"));
			var prof = db.Query<Profile>("SELECT * FROM Profile WHERE StudentID = '" + studentID + "'");
			accoID = prof [0].AccommodationID;
			acco = db.Query<Accommodation>("SELECT * FROM Accommodation WHERE ID = '" + accoID + "'");

			Address.Text = acco [0].Address;
			Suburb.Text = acco [0].Suburb;
			Rent.Text = acco [0].RentAWeek;
			PreferredContact.Text = acco [0].PreferredContact;
			Description.Text = acco [0].Description;

			editButton.Click += (sender, e) => {
				switch(mode)
				{
				case "Confirmed":
					mode = "Edittable";
					changeEdittable(true);
					editButton.Text = "Submit";
					break;
				case "Edittable":
					mode = "Confirmed";
					changeEdittable(false);
					editButton.Text = "Edit";
					db.Query<Accommodation>("UPDATE Accommodation SET Address = '" + Address.Text + "' WHERE ID = '" + accoID + "'");
					db.Query<Accommodation>("UPDATE Accommodation SET Suburb = '" + Suburb.Text + "' WHERE ID = '" + accoID + "'");
					db.Query<Accommodation>("UPDATE Accommodation SET RentAWeek = '" + Rent.Text + "' WHERE ID = '" + accoID + "'");
					db.Query<Accommodation>("UPDATE Accommodation SET PreferredContact = '" + PreferredContact.Text + "' WHERE ID = '" + accoID + "'");
					db.Query<Accommodation>("UPDATE Accommodation SET Description = '" + Description.Text + "' WHERE ID = '" + accoID + "'");
					break;
				}

			};
			return view;
		}
	}
}

