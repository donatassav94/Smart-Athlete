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
using System.IO;
using SQLite;
using Felipecsl.GifImageViewLibrary;
using Smart_Athlete.Fragments;

namespace Smart_Athlete
{
    [Activity(Label = "Pratimo Informacija", Theme = "@style/MyTheme", NoHistory = true)]
    public class Pratimo_info :  AppCompatActivity
    {
        private Android.Support.V7.Widget.Toolbar mToolBar;
        private GifImageView gifImageView;
        private TextView pavadinimas;
        private TextView atlikimo_technika;
        private string atlikimas;
        private string gifas;
        private DB_veiksmai dbv;
        public int pratimo_id;
        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "duomenu_baze.db3");
        protected override void OnCreate(Bundle bundle)
        {
            dbv = new DB_veiksmai();
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Pratimo_info);
            mToolBar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbarinfo);
            SetSupportActionBar(mToolBar);
            atlikimo_technika = FindViewById<TextView>(Resource.Id.atlikimas);
            pavadinimas = FindViewById<TextView>(Resource.Id.pavadinimas);
            gifImageView = FindViewById<GifImageView>(Resource.Id.gifImageView);
            string choice = Intent.GetStringExtra("pratimas") ?? "Data not available";
            //DATABASE info gavimas
            var db = new SQLiteConnection(dbPath);
            var querry = dbv.Pratimo_Info_Suradimas(choice);
            foreach (var item in querry)
            {
                    atlikimas=item.Pratimo_informacija;
                    gifas = item.Gifas;
                    pratimo_id = item.Id;
            }
            //Pratimo rodymas
            pavadinimas.Text=choice;
            atlikimo_technika.Text = atlikimas;
            if(gifas!="nėra")
            { 
                AssetManager input = this.Assets;
                Stream sr =input.Open(gifas);
                byte[] bytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    sr.CopyTo(ms);
                    bytes = ms.ToArray();
                }
                gifImageView.SetBytes(bytes);
                gifImageView.StartAnimation();
            }
        }

        //atgal paspaudus
        public override void OnBackPressed()
        {
            Finish();
            base.OnBackPressed();
        }


        //Tas pats kaip ir OnResume()-
        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
        }


        //Kažkam pasikeitus telefone veikiant appsui jis sunaikinamas ir paleidžiamas iš naujo OnDestroy()->OnCreate().
        public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
        }


        //Meniu sukurimas
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.pratimo_info_toolbar, menu);
            return base.OnCreateOptionsMenu(menu);
        }


        //Kas vyksta kazka nuspaudus
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                //Kai + pliusas paspaudziamas.
                case Resource.Id.add:
                    Android.App.FragmentTransaction transaction = FragmentManager.BeginTransaction();
                    
                    Planu_sarasas_dialog PlanuSarasoDialog = new Planu_sarasas_dialog(pratimo_id);
                    PlanuSarasoDialog.Show(transaction, "planu saraso dialog fragment");
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}