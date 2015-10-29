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
	public class AccommodationAdapter : BaseAdapter<Accommodation>, IFilterable
	{
		private Activity mContext;
		private List<Accommodation> mListings;
		private List<Accommodation> mAllListings;

		public AccommodationAdapter(Activity context, List<Accommodation> listings)
		{
			mContext = context;
			mListings = listings;

			Filter = new AccommodationFilter (this);
		}

		public override int Count
		{
			get
			{
				return mListings.Count;
			}
		}

		public override Accommodation this[int position]
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
				view = mContext.LayoutInflater.Inflate (Resource.Layout.UsersRowLayout, parent, false);
			}

			Profile user = mListings [position];

			view.FindViewById<TextView> (Resource.Id.userName).Text = user.StudentName;
			view.FindViewById<TextView> (Resource.Id.userNationality).Text = "Nationality: " + user.Nationality;
			view.FindViewById<TextView> (Resource.Id.userInterests).Text = "Interests: " + user.Interest;

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
							//user => user.Interest.ToLower ().Contains (constraint.ToString ())));
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
