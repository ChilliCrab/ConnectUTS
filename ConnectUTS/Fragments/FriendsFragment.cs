
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

namespace ConnectUTS
{
	public class FriendsFragment : Fragment
	{
		List<Profile> mUsers;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.Friends, container, false);

			DisplayUsers (view);

			return base.OnCreateView (inflater, container, savedInstanceState);
		}

		private void DisplayUsers (View view)
		{
			mUsers = new List<Profile> ();

			// Add users to the mUsers list
			// Inflate the listview with the mUsers list
			ListView usersList = view.FindViewById<ListView>(Resource.Id.listFriends);
			usersList.Adapter = new FriendAdapter (Activity, mUsers);
		}
	}
}

