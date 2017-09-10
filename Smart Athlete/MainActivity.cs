using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Support.V4.Widget;
using Android.Support.V4.App;
using Android.Content.Res;
using Android.Support.V7.App;
using Smart_Athlete.Fragments;
using SupportFragment = Android.Support.V4.App.Fragment;
using System.IO;
using SQLite;

namespace Smart_Athlete
{
    [Activity(Label = "Smart Athlete", MainLauncher = true, Icon = "@drawable/barbell_icon", Theme = "@style/MyTheme")]
    public class MainActivity : ActionBarActivity
    {
        private Android.Support.V7.Widget.Toolbar mToolBar;
        private DrawerLayout mDrawerLayout;
        private List<string> mLeftItems = new List<string>();
        private ArrayAdapter mLeftAdapter;
        private ListView mLeftDrawer;
        private MyActionBarDrawerToggle mDrawerToggle;
        private SupportFragment mCurrentFragment;
        public Pratimai Pratimai;
        private Main_Fragment Main_Fragment;
        public Tren_Visi_Planai Tren_Visi_Planai;
        private Stack<SupportFragment> mFragmentStack;
        public Bundle args = new Bundle();
        private DB_veiksmai dbv;

        protected override void OnCreate(Bundle bundle)
        {
            dbv = new DB_veiksmai();
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.myDrawer);
            mToolBar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            mFragmentStack = new Stack<SupportFragment>();
            Pratimai = new Pratimai();
            Main_Fragment = new Main_Fragment();
            Tren_Visi_Planai = new Tren_Visi_Planai();
            //Uzkraunam DB jei dar nera
            dbv.DB_Sukurimas();
            // Kairis 
            mLeftDrawer = FindViewById<ListView>(Resource.Id.leftListView);
            mLeftDrawer.Tag = 0;
            mLeftItems.Add("Krūtinė");
            mLeftItems.Add("Nugara");
            mLeftItems.Add("Tricepsas");
            mLeftItems.Add("Bicepsas");
            mLeftItems.Add("Trapecija");
            mLeftItems.Add("Pečiai");
            mLeftItems.Add("Presas");
            mLeftItems.Add("Kojos");
            mLeftItems.Add("Mano pratimai");
            SetSupportActionBar(mToolBar);
            mDrawerToggle = new MyActionBarDrawerToggle(this, mDrawerLayout, Resource.String.open_drawer, Resource.String.close_drawer);
            mLeftAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, mLeftItems);
            mLeftDrawer.Adapter = mLeftAdapter;

            mDrawerLayout.SetDrawerListener(mDrawerToggle);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            mDrawerToggle.SyncState();

            //Main fragment
            var trans = SupportFragmentManager.BeginTransaction();
            trans.Add(Resource.Id.fragmentContainer, Main_Fragment, "main_Fragment");
            trans.Add(Resource.Id.fragmentContainer, Pratimai, "Pratimai");
            trans.Hide(Pratimai);
            trans.Add(Resource.Id.fragmentContainer, Tren_Visi_Planai, "Tren_Visi_Planai");
            trans.Hide(Tren_Visi_Planai);
            trans.Commit();
            mCurrentFragment = Main_Fragment;
            
            
            //Paspaudimu vykdymas
            mLeftDrawer.ItemClick += MLeftDrawer_ItemClick;

            //Del vartymo is horiztalaus i vertikalu ir atvirksciai. Jei bundle nera dar =null tai programa katik ijungta
            if (bundle != null)
            {
                if (bundle.GetString("DrawerState") == "Opened")
                {
                    SupportActionBar.SetTitle(Resource.String.open_drawer);
                }
                else
                {
                    SupportActionBar.SetTitle(Resource.String.close_drawer);
                }
            }
            else
            {
                SupportActionBar.SetTitle(Resource.String.close_drawer);
            }
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


        //Left drawer itemu paspaudimo funkcija
        private void MLeftDrawer_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            ISharedPreferences prefs = GetSharedPreferences("saves", FileCreationMode.Private);
            ISharedPreferencesEditor editor = prefs.Edit();
            //Nukreipimas i fragmenta
            switch (mLeftItems[e.Position])
            {
                case "Krūtinė":
                    
                    editor.PutString("grupe", "Krūtinė");
                    RefreshFragment(Pratimai);
                    ShowFragment(Pratimai);
                    mDrawerLayout.CloseDrawer(mLeftDrawer);
                    break;
                case "Nugara":
                    editor.PutString("grupe", "Nugara");
                    RefreshFragment(Pratimai);
                    ShowFragment(Pratimai);
                    mDrawerLayout.CloseDrawer(mLeftDrawer);
                    break;
                case "Tricepsas":
                    editor.PutString("grupe", "Tricepsas");
                    RefreshFragment(Pratimai);
                    ShowFragment(Pratimai);
                    mDrawerLayout.CloseDrawer(mLeftDrawer);
                    break;
                case "Bicepsas":
                    editor.PutString("grupe", "Bicepsas");
                    RefreshFragment(Pratimai);
                    ShowFragment(Pratimai);
                    mDrawerLayout.CloseDrawer(mLeftDrawer);
                    break;
                case "Trapecija":
                    editor.PutString("grupe", "Trapecija");
                    RefreshFragment(Pratimai);
                    ShowFragment(Pratimai);
                    mDrawerLayout.CloseDrawer(mLeftDrawer);
                    break;
                case "Pečiai":
                    editor.PutString("grupe", "Pečiai");
                    RefreshFragment(Pratimai);
                    ShowFragment(Pratimai);
                    mDrawerLayout.CloseDrawer(mLeftDrawer);
                    break;
                case "Presas":
                    editor.PutString("grupe", "Presas");
                    RefreshFragment(Pratimai);
                    ShowFragment(Pratimai);
                    mDrawerLayout.CloseDrawer(mLeftDrawer);
                    break;
                case "Kojos":
                    editor.PutString("grupe", "Kojos");
                    //args.PutString("Krutine", "grupe");
                    //Pratimai.Arguments = args;
                    RefreshFragment(Pratimai);
                    ShowFragment(Pratimai);
                    mDrawerLayout.CloseDrawer(mLeftDrawer);
                    break;
                case "Mano pratimai":
                    editor.PutString("grupe", "Mano pratimai");
                    RefreshFragment(Pratimai);
                    ShowFragment(Pratimai);
                    mDrawerLayout.CloseDrawer(mLeftDrawer);
                    break;

            }
            editor.Commit();

        }

        //Paspaudus mygtuka "atgal"
        public override void OnBackPressed()
        {
            if(SupportFragmentManager.BackStackEntryCount>0)
            {
                SupportFragmentManager.PopBackStack();
                mCurrentFragment = mFragmentStack.Pop();
            }
            else
            {
                base.OnBackPressed();
            }
           
        }

        //Atnaujina fragmenta
        public void RefreshFragment(SupportFragment fragment)
        {
            var trans = SupportFragmentManager.BeginTransaction();
            trans.Detach(fragment);
            trans.Attach(fragment);
            trans.Commit();
        }

        //Fragmentu keitimas funkcija
        public void ShowFragment(SupportFragment fragment)
        {
            var trans = SupportFragmentManager.BeginTransaction();
            //trans.Detach(fragment);
            //trans.Attach(fragment);
            trans.Hide(mCurrentFragment);
            trans.Show(fragment);
            if(mCurrentFragment!=fragment)
            {
                trans.AddToBackStack(null);
                mFragmentStack.Push(mCurrentFragment);
                 mCurrentFragment = fragment;
            }
   
            trans.Commit();
        }


        //Tas pats kaip ir OnResume()-
        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            mDrawerToggle.SyncState();
        }


        //Kažkam pasikeitus telefone veikiant appsui jis sunaikinamas ir paleidžiamas iš naujo OnDestroy()->OnCreate().
        public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            mDrawerToggle.OnConfigurationChanged(newConfig); 
        }


        //Meniu sukurimas
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.action_bar, menu);
            return base.OnCreateOptionsMenu(menu);
        }


        //Kas vyksta kazka nuspaudus
        public override bool OnOptionsItemSelected(IMenuItem item)
        { 
            switch (item.ItemId)
            {
                //Kai hamburgeris paspaudziamas. Apsauga kad nesioverlapintu.
                case Android.Resource.Id.Home:
                    
                        mDrawerToggle.OnOptionsItemSelected(item);
                        return true;
                //Kai info paspaudziamas apsaugos nuo overlapu ir besikeiciantis pavadinimas
                case Resource.Id.plano_icon:
                    RefreshFragment(Tren_Visi_Planai);
                    ShowFragment(Tren_Visi_Planai);

                    if (mDrawerLayout.IsDrawerOpen(mLeftDrawer))
                    {
                        mDrawerLayout.CloseDrawer(mLeftDrawer);
                    }
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}

