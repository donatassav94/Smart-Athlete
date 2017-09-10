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
    public class Tren_planu_lentele
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Plano_pavadinimas { get; set; }
        public string Data { get; set; }

        public Tren_planu_lentele(string plano_pavadinimas, string data)
        {
            Plano_pavadinimas = plano_pavadinimas;
            Data = data;
        }

        public Tren_planu_lentele()
        {
        }

    }
}