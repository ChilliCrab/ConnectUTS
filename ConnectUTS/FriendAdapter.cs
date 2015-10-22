using System;
using System.Collections.Generic;

using Android.App;
using Android.Views;
using Android.Widget;

namespace ConnectUTS
{
	public class FriendAdapter : BaseAdapter<Profile>
	{
		private Activity mContext;
		private List<Profile> mUsers;

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
			// view.FindViewById<TextView> (Resource.Id.userInterests).Text = user.Interests;

			return view;
		}
	}
}

