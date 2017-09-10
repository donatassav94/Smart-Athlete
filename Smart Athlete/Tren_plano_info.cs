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
using Android.Graphics.Drawables;
using System.IO;
using Android.Support.V7.App;
using SQLite;
using Android.Util;
using Smart_Athlete.Fragments;

namespace Smart_Athlete
{
    [Activity(Label = "Plano Informacija", Theme = "@style/MyTheme")]
    public class Tren_plano_info : AppCompatActivity
    {
        private Android.Support.V7.Widget.Toolbar mToolBar;
        DB_veiksmai dbv;
        public List<lentele_items> Lenteles_Items;
        private ListView Lentele;
        private int plano_id;
        private string pratimo_pavad;
        private bool edit=false;
        private string choice;
        public TableListView adapter;
        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "duomenu_baze.db3");
        protected override void OnCreate(Bundle bundle)
        {
            dbv = new DB_veiksmai();
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Tren_Plano_info);
            mToolBar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.tren_plan_toolbar);
            Lentele = FindViewById<ListView>(Resource.Id.LentelesListView);
            SetSupportActionBar(mToolBar);
            //Plano informacijos isgavimas
            choice = Intent.GetStringExtra("planas") ?? "Data not available";
            Lenteles_Items = new List<lentele_items>();
            var querry = dbv.Gauti_Plano_ID(choice);
            foreach (var item in querry)
            {
                plano_id = Convert.ToInt32(item.Id);
            }
            RefreshListView();
            Lentele.ItemClick += Lentele_ItemClick;
            //string tag = "XXXXXX";
            // Log.Info(tag, plano_id.ToString());
            //Del vartymo is horiztalaus i vertikalu ir atvirksciai. Jei bundle nera dar =null tai programa katik ijungta
            if (bundle != null)
            {
                if (bundle.GetString("edit") == "false")
                {
                    SupportActionBar.Title = choice;
                }
                else
                {
                    SupportActionBar.SetTitle(Resource.String.edit);
                }
            }
            //activity katik ijungtas
            else
            {
                SupportActionBar.Title = choice;
            }
        }

        //Isnaujo surenka info listview
        public void RefreshListView()
        {
            Lenteles_Items.Clear();
            var querry2 = dbv.Gauti_Plano_Informacija(plano_id);
            foreach (var item in querry2)
            {
                var querry3 = dbv.Gauti_Pratimo_Pavadinima(item.Pratimo_id);
                foreach (var item2 in querry3)
                {
                    pratimo_pavad = item2.Pratimo_pavadinimas;
                }
                Lenteles_Items.Add(new lentele_items() { nr = item.Vieta_plane, pavadinimas = pratimo_pavad, serijos = item.Seriju_kiekis, pakartojimai = item.Pakartojimu_kiekis });
            }

            adapter = new TableListView(this, Lenteles_Items);
            Lentele.Adapter = adapter;
        }

        //issaugo state besikeiciant orientacijai ekrano ir panasiai
        protected override void OnSaveInstanceState(Bundle outState)
        {
            if (edit==false)
            {
                outState.PutString("edit", "false");
            }
            else
            {
                outState.PutString("edit", "true");
            }
            base.OnSaveInstanceState(outState);
        }

        private void Lentele_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var pasirinkimas = Lenteles_Items. ElementAt(e.Position);
            if (edit==false)
            {
                Intent intent = new Intent(this, typeof(Pratimo_info));
                intent.PutExtra("pratimas", pasirinkimas.pavadinimas);
                this.StartActivity(intent);
            }
            else
            {
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                Plano_edit_dialog PlanoEditDialog = new Plano_edit_dialog(pasirinkimas.nr,pasirinkimas.serijos,pasirinkimas.pakartojimai,plano_id,pasirinkimas.pavadinimas);
                PlanoEditDialog.Show(transaction, "planeditdialog fragment");
            }
        }

        //atgal paspaudus
        public override void OnBackPressed()
        {
            Finish();
            base.OnBackPressed();
        }


        //Tas pats kaip ir OnResume()
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
            MenuInflater.Inflate(Resource.Menu.plano_info_toolbar, menu);
            return base.OnCreateOptionsMenu(menu);
        }


        //Kas vyksta kazka nuspaudus
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.edit_plan:
                    if(edit==false)
                    {
                        edit = true;
                        SupportActionBar.SetTitle(Resource.String.edit);
                    }
                    else
                    {
                        edit = false;
                        SupportActionBar.Title = choice;
                    }
                    
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
    }