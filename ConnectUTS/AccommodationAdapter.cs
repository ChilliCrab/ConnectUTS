using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Views;
using Android.Widget;

using Java.Lang;
using Object = Java.Lang.Object;

using SQLite;

namespace ConnectUTS
{
	public class AccommodationAdapter : BaseAdapter<Profile>, IFilterable
	{
		private Activity mContext;
		private Profile mCurrentUser;
		private List<Profile> mListings;
		private List<Profile> mAllListings;
		private SQLiteConnection db;

		public AccommodationAdapter(Activity context, List<Profile> listings, Profile currentUser)
		{
			mContext = context;
			mListings = listings;
			mCurrentUser = currentUser;

			string path = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
			db = new SQLiteConnection (System.IO.Path.Combine(path, "Database.db"));

			Filter = new AccommodationFilter (this);
		}

		public override int Count
		{
			get
			{
				return mListings.Count;
			}
		}

		public override Profile this[int position]
		{
			get
			{
				return mListings[position];
			}
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var view = convertView;

			if (view == null) 
			{
				view = mContext.LayoutInflater.Inflate (Resource.Layout.AccommodationRowLayout, parent, false);
			}

			Profile user = mListings [position];
			Accommodation listing = db.Query<Accommodation>("SELECT * FROM Accommodation WHERE ID = '" + user.AccommodationID + "'") [0];

			view.FindViewById<TextView> (Resource.Id.accPriceSuburb).Text = "$" + listing.RentAWeek + "p.w. - " + listing.Suburb;
			view.FindViewById<TextView> (Resource.Id.accAddress).Text = listing.Address;

			string interestsString = "Owner Interest: ";
			//bool notFirstInterest = false;

			//			foreach (string interest in user.Interest) 
			//			{
			//				if (notFirstInterest) 
			//				{
			//					interestsString += ", " + interest;
			//				} 
			//				else 
			//				{
			//					interestsString += " " + interest;
			//				}
			//			}

			if (user.Interest == mCurrentUser.Interest) {
				view.FindViewById<TextView> (Resource.Id.accInterests).Text = interestsString + user.Interest + " Matched!";
			} 
			else
			{
				view.FindViewById<TextView> (Resource.Id.accInterests).Text = interestsString + user.Interest;
			}
			view.FindViewById<TextView> (Resource.Id.accDescription).Text = listing.Description;

			return view;
		}

		public Filter Filter{ get; private set; }

		public override void NotifyDataSetChanged ()
		{
			base.NotifyDataSetChanged ();
		}

		private class AccommodationFilter : Filter
		{
			private AccommodationAdapter mAdapter;

			public AccommodationFilter(AccommodationAdapter adapter)
			{
				mAdapter = adapter;
			}

			protected override FilterResults PerformFiltering(ICharSequence constraint)
			{
				var returnObject = new FilterResults ();
				var results = new List<Profile> ();

				if (mAdapter.mAllListings == null) 
				{
					mAdapter.mAllListings = mAdapter.mListings;
				}

				if (constraint == null) 
				{
					return returnObject;
				}

				if (mAdapter.mAllListings != null && mAdapter.mAllListings.Any ()) 
				{
					results.AddRange(
						mAdapter.mAllListings.Where (
							 //Check the current user's interests with the listing owner's.

							// i put search by subrub for now
							user => user.Interest.ToLower ().Contains (constraint.ToString ())));
				}

				returnObject.Values = FromArray(results.Select(
					result => result.ToJavaObject()).ToArray());
				returnObject.Count = results.Count;

				constraint.Dispose ();
				return returnObject;
			}

			protected override void PublishResults(ICharSequence constraint, FilterResults results)
			{
				using (var values = results.Values) 
				{
					mAdapter.mListings = values.ToArray<Object> ().Select (
						result => result.ToNetObject<Profile> ()).ToList ();
				}

				mAdapter.NotifyDataSetChanged ();

				constraint.Dispose ();
				results.Dispose ();
			}
		}
	}
}
