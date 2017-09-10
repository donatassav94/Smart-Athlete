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

namespace Smart_Athlete.Fragments
{
    public class Plano_edit_dialog : DialogFragment
    {
        private EditText Vieta_plane;
        private EditText Seriju_kiekis;
        private EditText Pakartojimu_kiekis;
        private int vt;
        private int sr;
        private int pk;
        private int planid;
        private string prat_pavad;
        private int prat_id;
        private Button Keisti;
        private Button Trinti;
        DB_veiksmai dbv;
        

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            dbv = new DB_veiksmai();
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.Plano_edit_dialog, container, false);
            Vieta_plane = view.FindViewById<EditText>(Resource.Id.vieta_plane);
            Seriju_kiekis = view.FindViewById<EditText>(Resource.Id.seriju_kiekis);
            Pakartojimu_kiekis = view.FindViewById<EditText>(Resource.Id.pakartojimu_kiekis);
            Keisti = view.FindViewById<Button>(Resource.Id.keisti);
            Trinti = view.FindViewById<Button>(Resource.Id.trinti);
            Keisti.Click += Keisti_Click;
            Trinti.Click += Trinti_Click;
            Vieta_plane.Text = vt.ToString();
            Seriju_kiekis.Text = sr.ToString();
            Pakartojimu_kiekis.Text = pk.ToString();
            return view;
        }

        public Plano_edit_dialog(int vieta,int serijos, int pakartojimai,int planoid, string pratimo_pav)
        {
            vt = vieta;
            sr = serijos;
            pk = pakartojimai;
            planid = planoid;
            prat_pavad = pratimo_pav;
        }

        private void Trinti_Click(object sender, EventArgs e)
        {
            var querry = dbv.Gauti_Pratimo_ID(prat_pavad);
            foreach (var item in querry)
            {
                prat_id = item.Id;
            }
            dbv.Trinti_Plano_Pratima(planid,prat_id,vt);
            ((Tren_plano_info)Activity).RefreshListView();
            this.Dismiss();
        }

        private void Keisti_Click(object sender, EventArgs e)
        {
            var querry=dbv.Gauti_Pratimo_ID(prat_pavad);
            foreach(var item in querry)
            {
                prat_id = item.Id;
            }
            dbv.Keisti_Plano_Pratimo_Informacija(Convert.ToInt32(Vieta_plane.Text), Convert.ToInt32(Seriju_kiekis.Text), Convert.ToInt32(Pakartojimu_kiekis.Text),planid,prat_id);
            ((Tren_plano_info)Activity).RefreshListView();
            this.Dismiss();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

    }
}