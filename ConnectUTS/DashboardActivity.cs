
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;

namespace ConnectUTS
{
	[Activity (Label = "Dashboard", Theme = "@style/ConnectUtsTheme")]				
	public class DashboardActivity : AppCompatActivity
	{
		private SupportToolbar mToolbar;
		private int mCurrentViewTitle = Resource.String.app_name;
		private DashboardToggle mDashboardToggle;
		private DrawerLayout mDrawerLayout;
		private ArrayAdapter mDashboardAdapter;
		private ListView mDashboard;
		private FragmentTransaction mFragmentManager;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Dashboard);
			mDrawerLayout = FindViewById<DrawerLayout> (Resource.Id.dashboardDrawer);

			mToolbar = FindViewById<SupportToolbar> (Resource.Id.toolbar);

			mDashboardToggle = new DashboardToggle (this, mDrawerLayout, Resource.String.menu_title, mCurrentViewTitle);

			DisplayToolbar (bundle);
		}

		private void DisplayToolbar(Bundle bundle)
		{
			SetSupportActionBar(mToolbar);
			SupportActionBar.SetHomeButtonEnabled(true);
			SupportActionBar.SetDisplayHomeAsUpEnabled(true);

			// Not the first time activity has been run.
			if (bundle != null)
			{
				if (bundle.GetString("DrawerState") == "Opened")
				{
					SupportActionBar.SetTitle(Resource.String.menu_title);
				}
				else
				{
					SupportActionBar.SetTitle(mCurrentViewTitle);
				}
			}
			// First time activity has been run.
			else
			{
				SupportActionBar.SetTitle(mCurrentViewTitle);
			}
		}
	}
}

