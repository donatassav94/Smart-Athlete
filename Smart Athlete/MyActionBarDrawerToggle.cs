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
using Android.Support.V4.App;
using Android.Support.V4.Widget;
using Android.Support.V7.App;

namespace Smart_Athlete
{
    public class MyActionBarDrawerToggle: Android.Support.V7.App.ActionBarDrawerToggle
    {
        private ActionBarActivity mActivity;
        private int mOpen;
        private int mClose;
        public MyActionBarDrawerToggle(ActionBarActivity activity,DrawerLayout drawerLayout, int openedResource, int closedResource)
            :base(activity, drawerLayout, openedResource, closedResource)
            {
            mActivity = activity;
            mOpen = openedResource;
            mClose = closedResource;
            }
        public override void OnDrawerOpened(View drawerView)
        {
            int drawerType = (int)drawerView.Tag;
            if (drawerType == 0)
            {
                base.OnDrawerOpened(drawerView);
                mActivity.SupportActionBar.SetTitle(mOpen);
            }
        }
        public override void OnDrawerClosed(View drawerView)
        {
            int drawerType = (int)drawerView.Tag;
            if (drawerType == 0)
            {
                base.OnDrawerClosed(drawerView);
                mActivity.SupportActionBar.SetTitle(mClose);
            }
        }
        public override void OnDrawerSlide(View drawerView, float slideOffset)
        {
            int drawerType = (int)drawerView.Tag;
            if (drawerType == 0)
            {
                base.OnDrawerSlide(drawerView, slideOffset);
            }
        }
    }
}