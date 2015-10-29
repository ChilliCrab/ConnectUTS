using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Views;
using Android.Widget;

namespace ConnectUTS
{
	public class FriendAdapter : BaseAdapter<Profile>
	{
		private Activity mContext;
		private List<Profile> mUsers;
		private List<Profile> mFilterUsers;

		public FriendAdapter(Activity context, List<Profile> users)
		{
			mContext = context;
			mUsers = users;
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
			string interestsString = "Interests:";
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

			interestsString += " " + user.Interest;
			view.FindViewById<TextView> (Resource.Id.userInterests).Text = interestsString;
		
			return view;
		}

		public void Filter(string filter)
		{
			// Change to cycle through interest ARRAY
			mFilterUsers = (from user in mUsers
			                where user.Interest.ToLower ().Contains (filter.ToLower ())
			                select user).ToList ();
			NotifyDataSetChanged ();
		}
	}
}
