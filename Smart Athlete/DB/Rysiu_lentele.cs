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
using SQLite;

namespace Smart_Athlete.DB
{
    public class Rysiu_lentele
    {
        public int Plano_id { get; set; }
        public int Pratimo_id { get; set; }
        public int Vieta_plane { get; set; }
        public int Seriju_kiekis { get; set; }
        public int Pakartojimu_kiekis { get; set; }

        public Rysiu_lentele(int plano_id, int pratimo_id, int vieta_plane, int seriju_kiekis, int pakartojimu_kiekis)
        {

            Plano_id = plano_id;
            Pratimo_id = pratimo_id;
            Vieta_plane = vieta_plane;
            Seriju_kiekis = seriju_kiekis;
            Pakartojimu_kiekis = pakartojimu_kiekis;
        }

        public Rysiu_lentele()
        {
        }
  
    }
}