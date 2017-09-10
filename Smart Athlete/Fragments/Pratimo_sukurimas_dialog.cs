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
    public class Pratimo_sukurimas_dialog : DialogFragment
    {
        private EditText Naujo_pratimo_pavad;
        private EditText Pratimo_informacija;
        private int vt;
        private int sr;
        private int pk;
        private int planid;
        private string prat_pavad;
        private int prat_id;
        private Button Kurti;
        private DB_veiksmai dbv;


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            dbv = new DB_veiksmai();
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.Pratimo_sukurimas_dialog, container, false);
            Naujo_pratimo_pavad = view.FindViewById<EditText>(Resource.Id.pavad_naujo_prat);
            Pratimo_informacija = view.FindViewById<EditText>(Resource.Id.info_naujo_prat);
            Kurti = view.FindViewById<Button>(Resource.Id.kurtipratima);
            Kurti.Click += Kurti_Click;

            /*Vieta_plane.Text = vt.ToString();
            Seriju_kiekis.Text = sr.ToString();
            Pakartojimu_kiekis.Text = pk.ToString();*/
            return view;
        }

        private void Kurti_Click(object sender, EventArgs e)
        {
            if (Naujo_pratimo_pavad.Text != "")
            {
                dbv.Kurti_Nauja_Pratima(Naujo_pratimo_pavad.Text,Pratimo_informacija.Text);
                ((MainActivity)Activity).RefreshFragment(((MainActivity)Activity).Pratimai);
                this.Dismiss();
            }
        }

        /*public Pratimo_sukurimas_dialog(int vieta, int serijos, int pakartojimai, int planoid, string pratimo_pav)
        {
            vt = vieta;
            sr = serijos;
            pk = pakartojimai;
            planid = planoid;
            prat_pavad = pratimo_pav;
        }

       /* private void Trinti_Click(object sender, EventArgs e)
        {
            var querry = dbv.Gauti_Pratimo_ID(prat_pavad);
            foreach (var item in querry)
            {
                prat_id = item.Id;
            }
            dbv.Trinti_Plano_Pratima(planid, prat_id, vt);
            ((Tren_plano_info)Activity).RefreshListView();
            this.Dismiss();
        }

        private void Keisti_Click(object sender, EventArgs e)
        {
            var querry = dbv.Gauti_Pratimo_ID(prat_pavad);
            foreach (var item in querry)
            {
                prat_id = item.Id;
            }
            dbv.Keisti_Plano_Pratimo_Informacija(Convert.ToInt32(Vieta_plane.Text), Convert.ToInt32(Seriju_kiekis.Text), Convert.ToInt32(Pakartojimu_kiekis.Text), planid, prat_id);
            ((Tren_plano_info)Activity).RefreshListView();
            this.Dismiss();
        }*/

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

    }
}