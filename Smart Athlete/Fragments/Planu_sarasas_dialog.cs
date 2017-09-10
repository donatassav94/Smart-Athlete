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

namespace Smart_Athlete.Fragments
{
    class Planu_sarasas_dialog:DialogFragment
    {
        private ListView Planu_sarasas;
        private List<string> Planai;
        private ArrayAdapter planAdapter;
        private int pratimo_id;
        private int plano_id;
        DB_veiksmai dbv;

        public Planu_sarasas_dialog(int id)
        {
            pratimo_id = id;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            dbv = new DB_veiksmai();
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.Planu_sarasas_dialog, container, false);
            Planu_sarasas = view.FindViewById<ListView>(Resource.Id.visiplanaisarasas);
            Planai = new List<string>();
            var querry = dbv.Gauti_Planus();
            foreach (var item in querry)
            {
                Planai.Add(item.Plano_pavadinimas);
            }
            planAdapter = new ArrayAdapter(view.Context, Android.Resource.Layout.SimpleListItem1, Planai);
            Planu_sarasas.Adapter = planAdapter;
            //Paspaudimu vykdymas
            Planu_sarasas.ItemClick += Planu_sarasas_ItemClick;
            return view;
        }

        private void Planu_sarasas_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            string pasirinkimas = Planai.ElementAt(e.Position);
            var querry = dbv.Gauti_Plano_ID(pasirinkimas);
            foreach (var item in querry)
            {
                plano_id = Convert.ToInt32(item.Id);
            }
            dbv.Prideti_pratima(pratimo_id, plano_id);
            //((MainActivity)Activity).RefreshFragment(((MainActivity)Activity).Tren_Visi_Planai);
            this.Dismiss();
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle); //Sets the title bar to invisible
            base.OnActivityCreated(savedInstanceState);
            // Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation; //set the animation
        }
    }
}