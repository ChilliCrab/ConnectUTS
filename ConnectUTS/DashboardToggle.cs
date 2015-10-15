using System;

using Android.Views;

using SupportActionBarDrawerToggle = Android.Support.V7.App.ActionBarDrawerToggle;
using Android.Support.V7.App;
using Android.Support.V4.Widget;


namespace ConnectUTS
{
	public class DashboardToggle : SupportActionBarDrawerToggle
	{
		private AppCompatActivity mHostActivity;
		private int mOpenedResource;
		private int mClosedResource;

		public DashboardToggle (AppCompatActivity host, DrawerLayout drawerLayout, int openedResource, int closedResource)
			: base(host, drawerLayout, openedResource, closedResource)
		{
			mHostActivity = host;
			mOpenedResource = openedResource;
			mClosedResource = closedResource;
		}

		public override void OnDrawerOpened(View drawerView)
		{
			base.OnDrawerOpened(drawerView);
			mHostActivity.SupportActionBar.SetTitle(mOpenedResource);
		}

		public override void OnDrawerClosed(View drawerView)
		{
			base.OnDrawerClosed(drawerView);
			mHostActivity.SupportActionBar.SetTitle(mClosedResource);
		}

		public override void OnDrawerSlide(View drawerView, float slideOffset)
		{
			base.OnDrawerSlide(drawerView, slideOffset);
		}

		public void SetClosedTitle(int closedResource)
		{
			mClosedResource = closedResource;
		}
	}
}

