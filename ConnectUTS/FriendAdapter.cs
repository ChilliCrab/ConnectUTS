using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Views;
using Android.Widget;

using Java.Lang;
using Object = Java.Lang.Object;

namespace ConnectUTS
{
	public class FriendAdapter : BaseAdapter<Profile>, IFilterable
	{
		private Activity mContext;
		private Profile mCurrentUser;
		private List<Profile> mUsers;
		private List<Profile> mAllUsers;

		public FriendAdapter(Activity context, List<Profile> users, Profile currentUser)
		{
			mContext = context;
			mUsers = users;
			mCurrentUser = currentUser;

			Filter = new FriendFilter (this);
		}

		public override int Count
		{
			get
			{
				return mUsers.Count;
			}
		}

		public override Profile this[int position]
		{
			get
			{
				return mUsers[position];
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
				view = mContext.LayoutInflater.Inflate (Resource.Layout.UsersRowLayout, parent, false);
			}

			Profile user = mUsers [position];

			view.FindViewById<TextView> (Resource.Id.userName).Text = user.StudentName;
			view.FindViewById<TextView> (Resource.Id.userNationality).Text = "Nationality: " + user.Nationality;
			// Cycle through array of interests and append to a string.
			string interestsString = "Matching Interests: ";
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
				view.FindViewById<TextView> (Resource.Id.userInterests).Text = interestsString + user.Interest;
			} 
			else
			{
				view.FindViewById<TextView> (Resource.Id.userInterests).Text = interestsString + "None";
			}
			return view;
		}

		public Filter Filter{ get; private set; }

		public override void NotifyDataSetChanged ()
		{
			base.NotifyDataSetChanged ();
		}

		private class FriendFilter : Filter
		{
			private FriendAdapter mAdapter;

			public FriendFilter(FriendAdapter adapter)
			{
				mAdapter = adapter;
			}

			protected override FilterResults PerformFiltering(ICharSequence constraint)
			{
				var returnObject = new FilterResults ();
				var results = new List<Profile> ();

				if (mAdapter.mAllUsers == null) 
				{
					mAdapter.mAllUsers = mAdapter.mUsers;
				}

				if (constraint == null) 
				{
					return returnObject;
				}

				if (mAdapter.mAllUsers != null && mAdapter.mAllUsers.Any ()) 
				{
					results.AddRange(
						mAdapter.mAllUsers.Where (
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
					mAdapter.mUsers = values.ToArray<Object> ().Select (
						result => result.ToNetObject<Profile> ()).ToList ();
				}

				mAdapter.NotifyDataSetChanged ();

				constraint.Dispose ();
				results.Dispose ();
			}
		}
	}
}
