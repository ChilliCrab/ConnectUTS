
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

			// Sets up the toggle for the dashboard drawer.
			mDashboardToggle = new DashboardToggle (this, mDrawerLayout, Resource.String.menu_title, mCurrentViewTitle);
			mDrawerLayout.SetDrawerListener (mDashboardToggle);

			// Displays the custom action bar.
			DisplayToolbar (bundle);

			mDashboardToggle.SyncState();

			// Controls the dashboard.
			InflateDashboard();
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			mDashboardToggle.OnOptionsItemSelected(item);
			return base.OnOptionsItemSelected(item);
		}

		protected override void OnSaveInstanceState(Bundle outState)
		{
			if (mDrawerLayout.IsDrawerOpen((int)GravityFlags.Left))
			{
				outState.PutString("DrawerState", "Opened");
			}

			else
			{
				outState.PutString("DrawerState", "Closed");
			}

			base.OnSaveInstanceState(outState);
		}

		protected override void OnPostCreate (Bundle savedInstanceState)
		{
			base.OnPostCreate (savedInstanceState);
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

		private void InflateDashboard()
		{
			mDashboard = FindViewById<ListView> (Resource.Id.menuList);
			mDashboard.Adapter = mDashboardAdapter;

			mDashboard.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
				switch (e.Position) {
				case 0:
					// Profile page
					mCurrentViewTitle = Resource.String.app_name;
					break;
				case 1:
					// Find friends
					mCurrentViewTitle = Resource.String.friends_title;
					break;
				case 2:
					// Find accommodation
					mCurrentViewTitle = Resource.String.bed_title;
					break;
				case 3:
					// Find a housemate
					mCurrentViewTitle = Resource.String.housemate_title;
					break;
				case 4:
					// Search all users
					mCurrentViewTitle = Resource.String.search_title;
					break;
				case 5:
					// Settings
					mCurrentViewTitle = Resource.String.settings_title;
					break;
				case 6:
					// Log Out
					var intent = new Intent(this, typeof(MainActivity));
					StartActivity(intent);
					// Stops user from pressing back button to return.
					Finish();
					break;
				}
			};
		}

		// Sets the toolbar title, switches the views and closes the dashboard.
		private void SetView(int fragmentResource, Fragment view, bool retainView)
		{
			mDashboardToggle.SetClosedTitle(mCurrentViewTitle);

			mFragmentManager = FragmentManager.BeginTransaction ();
			mFragmentManager.Replace (fragmentResource, view);

			// If true, allows the user to return to that fragment.
			// Otherwise it is destroyed.
			if(retainView)
			{
				mFragmentManager.AddToBackStack(null);
			}

			mFragmentManager.Commit ();

			mDrawerLayout.CloseDrawers ();
		}
	}
}

