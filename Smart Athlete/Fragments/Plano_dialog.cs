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

namespace Smart_Athlete
{
    class Plano_dialog : DialogFragment
    {
        private EditText Plano_pavad;
        private Button Sukurti_plan;
        DB_veiksmai dbv;
        

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            dbv = new DB_veiksmai();
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.Plano_sukurimas_dialog, container, false);

            Plano_pavad = view.FindViewById<EditText>(Resource.Id.plano_pavadinimas);
            Sukurti_plan = view.FindViewById<Button>(Resource.Id.sukurti_plana);

            Sukurti_plan.Click += Sukurti_plan_Click;

            return view;
        }

        void Sukurti_plan_Click(object sender, EventArgs e)
        {
            string tekstas = Plano_pavad.Text;
            dbv.Kurti_Nauja_Plana(tekstas);
            ((MainActivity)Activity).RefreshFragment(((MainActivity)Activity).Tren_Visi_Planai);
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