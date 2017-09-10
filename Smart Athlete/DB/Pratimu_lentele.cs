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

namespace Smart_Athlete
{
    public class Pratimu_lentele
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Grupe { get; set; }
        public string Pratimo_pavadinimas { get; set; }
        public string Pratimo_informacija { get; set; }
        public string Gifas { get; set; }

        public Pratimu_lentele(string grupe, string pratimo_pavadinimas, string pratimo_informacija, string gifas)
        {
            
            Grupe = grupe;
            Pratimo_pavadinimas = pratimo_pavadinimas;
            Pratimo_informacija = pratimo_informacija;
            Gifas = gifas;
        }
        
        public Pratimu_lentele()
        {
        }
        /*public override string ToString()
        {
            return  Grupe+" | "+Pratimo_pavadinimas + " | " + Pratimo_informacija+" | "+Gifas;
        }*/
    }
}