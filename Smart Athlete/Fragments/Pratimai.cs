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
using System.IO;
using SQLite;

namespace Smart_Athlete.Fragments
{
    public class Pratimai : Android.Support.V4.App.Fragment
    {
        private DB_veiksmai dbv;
        private List<string> mGrupes = new List<string>();
        private ArrayAdapter mAdapter;
        private ListView mGrupeslist;
        public bool Trynimas;
        private Button manoPratimas;
        private Switch Trinti;
        private TextView Grupe;
        private int prat_id;
        private string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "duomenu_baze.db3");

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override Android.Views.View OnCreateView(Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Android.OS.Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Pratimai, container, false);
            return view;
        }
       
        public override void OnStart()
        {
            Trynimas = false;
            mGrupes.Clear();
            dbv = new DB_veiksmai();
            mGrupeslist = View.FindViewById<ListView>(Resource.Id.pratimai);
            manoPratimas = View.FindViewById<Button>(Resource.Id.addpratimas);
            Trinti = View.FindViewById<Switch>(Resource.Id.my_switchpra);
            Trinti.Checked = false;
            Grupe = View.FindViewById<TextView>(Resource.Id.praid);
            ISharedPreferences prefs =Activity.GetSharedPreferences("saves", FileCreationMode.Private);
            String grupe = prefs.GetString("grupe","Nera tokio");
            Grupe.Text = grupe;
            if(grupe=="Mano pratimai")
            {
                manoPratimas.Visibility = ViewStates.Visible;
                Trinti.Visibility = ViewStates.Visible;
                manoPratimas.Click += ManoPratimas_Click;
                Trinti.CheckedChange += Trinti_CheckedChange;
            }
            //Gaunam pratimus
            var querry = dbv.Gauti_Pratimus(grupe);
            foreach (var item in querry)
            {
                    mGrupes.Add(item.Pratimo_pavadinimas);
            }
            mAdapter = new ArrayAdapter(Context, Android.Resource.Layout.SimpleListItem1, mGrupes);
            mGrupeslist.Adapter = mAdapter;
            //Paspaudimu vykdymas
            mGrupeslist.ItemClick += mGrupeslist_ItemClick;
            base.OnStart();
        }

        private void Trinti_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            var zinute = Toast.MakeText(Context, (e.IsChecked ? "Trynimas įjungtas" : "Trynimas išjungtas"), ToastLength.Short);
            zinute.Show();
            if (e.IsChecked)
            {
                Trynimas = true;
            }
            else
            {
                Trynimas = false;
            }
        }

        private void ManoPratimas_Click(object sender, EventArgs e)
        {
            FragmentTransaction transaction = ((MainActivity)Activity).FragmentManager.BeginTransaction();
            Pratimo_sukurimas_dialog PratimoSukurimasDialog = new Pratimo_sukurimas_dialog();
            PratimoSukurimasDialog.Show(transaction, " fragment");
        }

        private void mGrupeslist_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            string pasirinkimas = mGrupes.ElementAt(e.Position);
            if (Trynimas == false)
            {
                Intent intent = new Intent(Activity, typeof(Pratimo_info));
                intent.PutExtra("pratimas", pasirinkimas);
                Activity.StartActivity(intent);
            }
            else
            {
                var querry = dbv.Gauti_Pratimo_ID(pasirinkimas);
                foreach (var item in querry)
                {
                    prat_id = item.Id;
                }
                    dbv.Trinti_Pratima(prat_id);
                    dbv.Trinti_Visu_Planu_Pratima(prat_id);
                    ((MainActivity)Activity).RefreshFragment(((MainActivity)Activity).Pratimai);
                
            }
        }
    }
}