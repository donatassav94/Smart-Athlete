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

namespace Smart_Athlete.Fragments
{
    public class Tren_Visi_Planai : Android.Support.V4.App.Fragment
    {
        private List<string> Planai = new List<string>();
        private ArrayAdapter mAdapter;
        private ListView PlanaiList;
        private Button Kurti_plana;
        private DB_veiksmai dbv;
        private bool Trynimas=false;
        private Switch Trinti;
        private int plan_id;

        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "duomenu_baze.db3");
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Tren_Visi_Planai, container, false);
            return view;
        }
        //Po fragmento sukurimo ir pries starta
        public override void OnStart()
        {
            Trynimas = false;
            dbv = new DB_veiksmai();
            Kurti_plana = View.FindViewById<Button>(Resource.Id.naujasplanas);
            Planai.Clear();
            PlanaiList = View.FindViewById<ListView>(Resource.Id.visiplanai);
            Trinti = View.FindViewById<Switch>(Resource.Id.my_switch);
            Trinti.Checked = false;
            Trinti.CheckedChange += Trinti_CheckedChange;
            var querry = dbv.Gauti_Planus();
            foreach (var item in querry)
            {
                Planai.Add(item.Plano_pavadinimas);
            }
            mAdapter = new ArrayAdapter(Context, Android.Resource.Layout.SimpleListItem1, Planai);
            PlanaiList.Adapter = mAdapter;
            //Paspaudimu vykdymas
            PlanaiList.ItemClick += PlanaiList_ItemClick;
            //Mygtukai
            Kurti_plana.Click += Kurti_plana_Click;
            base.OnStart();
        }

        private void Trinti_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            var zinute = Toast.MakeText(Context,(e.IsChecked ? "Trynimas įjungtas" : "Trynimas išjungtas"), ToastLength.Short);
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

        private void Kurti_plana_Click(object sender, EventArgs e)
        {
            FragmentTransaction transaction = ((MainActivity)Activity).FragmentManager.BeginTransaction();
            Plano_dialog PlanoDialog = new Plano_dialog();
            PlanoDialog.Show(transaction, "dialog fragment");
        }

        private void PlanaiList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            string pasirinkimas = Planai.ElementAt(e.Position);
            if (Trynimas == false)
            {
                Intent intent = new Intent(Activity, typeof(Tren_plano_info));
                intent.PutExtra("planas", pasirinkimas);
                Activity.StartActivity(intent);
            }
            else
            {
                var querry = dbv.Gauti_Plano_ID(pasirinkimas);
                foreach (var item in querry)
                {
                    plan_id = item.Id;
                }
                    dbv.Trinti_Plana(plan_id);
                    dbv.Trinti_Plano_Rysius(plan_id);
                    ((MainActivity)Activity).RefreshFragment(((MainActivity)Activity).Tren_Visi_Planai);
            }
        }
    }
}