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
using System.IO;
using SQLite;
using Smart_Athlete.DB;
using Android.Util;

namespace Smart_Athlete
{
    public class DB_veiksmai
    {
        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "duomenu_baze.db3");

        //Suranda konkretaus pratimo informacija pagal pratimo pavadinima
        public List<Pratimu_lentele> Pratimo_Info_Suradimas(string choice)
        {
            var db = new SQLiteConnection(dbPath);
            var querry = db.Query<Pratimu_lentele>("SELECT * FROM Pratimu_lentele WHERE Pratimo_pavadinimas=?", choice);
            return querry;
        }

        //Suranda pratimo ID pagal pavadinima
        public List<Pratimu_lentele> Gauti_Pratimo_ID(string pavadinimas)
        {
            var db = new SQLiteConnection(dbPath);
            var querry = db.Query<Pratimu_lentele>("SELECT ID FROM Pratimu_lentele WHERE Pratimo_pavadinimas=?", pavadinimas);
            return querry;
        }

        //Surenka pratimu sarasa
        public List<Pratimu_lentele> Gauti_Pratimus(string grupe)
        {
            var db = new SQLiteConnection(dbPath);
            var querry = db.Query<Pratimu_lentele>("SELECT Pratimo_pavadinimas FROM Pratimu_lentele WHERE Grupe=?", grupe);
            return querry;
        }

        //Sukuria treniruociu plana nauja ir empty
        public void Kurti_Nauja_Plana(string pavad)
        {
            var db = new SQLiteConnection(dbPath);
            List<Tren_planu_lentele> lentele = new List<Tren_planu_lentele>();
            string data = DateTime.Now.ToString();
            lentele.Add(new Tren_planu_lentele(pavad, data));
            foreach (var item in lentele)
            { 
                db.Insert(item);
            }
        }

        //Prideda nauja pratima i mano pratimai
        public void Kurti_Nauja_Pratima(string pavad,string info)
        {
            var db = new SQLiteConnection(dbPath);
            List<Pratimu_lentele> lentele = new List<Pratimu_lentele>();
            lentele.Add(new Pratimu_lentele("Mano pratimai", pavad, info,"nėra"));
            foreach (var item in lentele)
            {
                db.Insert(item);
            }
        }

        //Istrina pratima is pratimu lenteles
        public void Trinti_Pratima(int id)
        {
            var db = new SQLiteConnection(dbPath);
            db.Query<Pratimu_lentele>("DELETE FROM Pratimu_lentele WHERE Id=?", id);
        }

        //Prideda pratima i treniruociu plana
        public void Prideti_pratima(int pratimas, int planas)
        {
            int kelintas = 0;
            var db = new SQLiteConnection(dbPath);
            List<Rysiu_lentele> lentele = new List<Rysiu_lentele>();
            var querry = db.Query<Rysiu_lentele>("SELECT * FROM Rysiu_lentele WHERE plano_id=?", planas);
            foreach (var item in querry)
            {
                kelintas++;
            }
            string data = DateTime.Now.ToString();
            lentele.Add(new Rysiu_lentele(planas, pratimas, kelintas+1, 0, 0));
            foreach (var item in lentele)
            {
                db.Insert(item);
            }

        }

        //Pakeicia plano pratimo vieta plane, seriju kieki, pakartojimu kieki
        public void Keisti_Plano_Pratimo_Informacija(int vieta, int serijos, int pakartojimai, int planoid, int pratimoid)
        {
            var db = new SQLiteConnection(dbPath);
            List<Rysiu_lentele> lentele = new List<Rysiu_lentele>();
            db.Query<Rysiu_lentele>("UPDATE Rysiu_lentele SET vieta_plane=? WHERE plano_id=? AND pratimo_id=?", vieta,planoid,pratimoid);
            db.Query<Rysiu_lentele>("UPDATE Rysiu_lentele SET seriju_kiekis=? WHERE plano_id=? AND pratimo_id=?", serijos, planoid, pratimoid);
            db.Query<Rysiu_lentele>("UPDATE Rysiu_lentele SET pakartojimu_kiekis=? WHERE plano_id=? AND pratimo_id=?", pakartojimai, planoid, pratimoid);
        }

        //Suranda plano id pagal plano pavadinima
        public List<Tren_planu_lentele> Gauti_Plano_ID(string pavad)
        {
            var db = new SQLiteConnection(dbPath);
            var querry = db.Query<Tren_planu_lentele>("SELECT Id FROM Tren_planu_lentele WHERE Plano_pavadinimas=?", pavad);
            return querry;
        }

        //Istrina plano pratima.
        public void Trinti_Plano_Pratima(int planas, int pratimas, int vietaplane)
        {
            var db = new SQLiteConnection(dbPath);
            db.Query<Rysiu_lentele>("DELETE FROM Rysiu_lentele WHERE plano_id=? AND pratimo_id=? AND vieta_plane=?", planas, pratimas, vietaplane);
        }

        //Istrina visu planu pratima.
        public void Trinti_Visu_Planu_Pratima(int pratimas)
        {
            var db = new SQLiteConnection(dbPath);
            db.Query<Rysiu_lentele>("DELETE FROM Rysiu_lentele WHERE pratimo_id=?", pratimas);
        }

        //Suranda pratimo pavadinima pagal id
        public List<Pratimu_lentele> Gauti_Pratimo_Pavadinima(int id)
        {
            var db = new SQLiteConnection(dbPath);
            var querry = db.Query<Pratimu_lentele>("SELECT Pratimo_pavadinimas FROM Pratimu_lentele WHERE Id=?", id);
            return querry;
        }

        //Istrina visa plana.
        public void Trinti_Plana(int planas)
        {
            var db = new SQLiteConnection(dbPath);
            db.Query<Tren_planu_lentele>("DELETE FROM Tren_planu_lentele WHERE Id=?", planas);
        }

        //Istrina plano rysius rysiu lentelej.
        public void Trinti_Plano_Rysius(int planas)
        {
            var db = new SQLiteConnection(dbPath);
            db.Query<Rysiu_lentele>("DELETE FROM Rysiu_lentele WHERE plano_id=?", planas);
        }

        //Surenka treniruociu planu sarasa
        public List<Tren_planu_lentele> Gauti_Planus()
        {
            var db = new SQLiteConnection(dbPath);
            //db.DeleteAll<Tren_planu_lentele>();
            var querry = db.Query<Tren_planu_lentele>("SELECT Plano_pavadinimas FROM Tren_planu_lentele");
            return querry;
        }

        //Surenka treniruociu plano informacija
        public List<Rysiu_lentele> Gauti_Plano_Informacija(int pl_id)
        {
            var db = new SQLiteConnection(dbPath);
            //db.DeleteAll<Rysiu_lentele>();
            var querry = db.Query<Rysiu_lentele>("SELECT * FROM Rysiu_lentele WHERE Plano_id=? ORDER BY vieta_plane ASC", pl_id);
            return querry;
        }

        //Duomenu bazes sukurimas
        public void DB_Sukurimas()
        {
            //File.Delete(dbPath);
           var db = new SQLiteConnection(dbPath);
            //Pratimu lenteles patikrinimas ir sukurimas
           var pra_lent_info = db.GetTableInfo("Pratimu_lentele");
           if (pra_lent_info.Count==0)
            {
                db.CreateTable<Pratimu_lentele>();
                List<Pratimu_lentele> lentele = new List<Pratimu_lentele>();
                lentele.Add(new Pratimu_lentele("Krūtinė", 
                                                "Štangos spaudimas",
                                                "Pratimas orientuotas į: krūtinės raumenį, tačiau papildomai dirba ir tricepso raumuo kartu su priekinę delta. Leisti štangą iki krūtinės apačios poto kelti iki viršaus atgal, bet pilnai neištiesiant alkūnių.", 
                                                "benchpress.gif"));
                lentele.Add(new Pratimu_lentele("Tricepsas",
                                                " Prancūziškas spaudimas",
                                                "Pratimas orientuotas į: tricepso raumenį. Ištiesus rankas šiek tiek atgal į viršų virš galvos lenkiama per alkunes atgal, kol štanga beveik pasieks kaktą ir grįštama į pradinę padėtį stipriai suspaudus tricepsą. ",
                                                "skullcrush.gif"));
                lentele.Add(new Pratimu_lentele("Bicepsas",
                                                "Štangos lenkimas sėdint ir užlenkiant riešus į save",
                                                "Pratimas orientuotas į: bicepso ir žastinio stipinkaulio raumenis. Atsisėdama, kojos suglaudžiamos ir pasilenkiama į priekį. Štanga laikoma nusuktais nuo jūsų delnais. Lenkiama štanga į viršų ir galutiniame judėsio taške papildomai užlenkiami riešai į viršų. ",
                                                "Bicepsui_rieso_uzlenkimas.gif"));
                lentele.Add(new Pratimu_lentele("Nugara",
                                                "Prisitraukimai",
                                                "Pratimas orientuotas į: visos nugaros raumenys išskyrus nugaros apačios raumenis, o papildomai apkrauna ir bicepso raumenis. Prisitraukti iki smakro kojas laikant vietoje ir nesiūbuojant jų. Stengtis neskubėti ir nenaudoti inercijos.", 
                                                "pullups.gif"));
                lentele.Add(new Pratimu_lentele("Krūtinė", 
                                                "Plėšimas su lynais apatinei krūtinės daliai",
                                                "Pratimas orientuotas į: krūtinės apatinę dalį. Ištempus pilnai krūtinės raumenis lynai suvedami iš viršaus į apačią kuo stipriau suspaudžiant krūtinės raumenis.",
                                                "cableflys.gif"));
                lentele.Add(new Pratimu_lentele("Nugara",
                                                "Mirties trauka",
                                                "Pratimas orientuotas į: nugaros apačios raumenis, tačiau taip pat labai apkrauna ir šlaunų dvigalvį kojų raumenį, presą, trapeciją. Pratimas apkraunantys daugiausiai kūno raumenų. Įtempti nugara, pilvo presą ir išlaikant neutralią stuburo poziciją kelti štangą į viršų ir taip pat nuleisti.",
                                                "deadlift.gif"));
                lentele.Add(new Pratimu_lentele("Krūtinė",
                                                "Hantelių spaudimas suglaudus kartu siaurai",
                                                "Pratimas orientuotas į: krūtinės raumenį, bet labiau veikia vidinę jos dalį. Šiek tiek krūvio gauna ir tricepsas. Ištiesus rankas viršuje stipriai suspausti krūtinės raumenis ir visados hantelius spausti vienas į kitą.",
                                                "Hanteliu_spaudimas_suglaudus.gif"));
                lentele.Add(new Pratimu_lentele("Tricepsas",
                                                "Prancūziškas spaudimas + Spaudimas siaurai",
                                                "Pratimas orientuotas į: tricepso raumenį, tačiau šitas variantas leidžia labiau apkrauti raumenį ekscentriniu būdu (didesnė apkrova judėsio fazėje kai raumuo ištempiamas). ",
                                                "Pranc_spaud_eccentric .gif"));
                lentele.Add(new Pratimu_lentele("Tricepsas",
                                                "Lyno tiesimas atsirėmus pilvu ant suoliuko",
                                                "Pratimas orientuotas į: tricepso raumenį. ",
                                                "Lyno_tiesimas_tricepsui_pasilenkus.gif"));
                lentele.Add(new Pratimu_lentele("Presas",
                                                "Pasisukimai į šonus pagal blyną",
                                                "Pratimas orientuotas į: visos pilvo srities raumenyną, puikiai apkrauna tiek išorinius raumenis, tiek vidinius. Svarbu išlaikyti tiesų stuburą, nes kitaip galima susižeisti. Sunkus reikalaujantis patirties ir jėgos. ",
                                                "Pasisukimai_i_sonus_pagal_blyna.gif"));
                lentele.Add(new Pratimu_lentele("Tricepsas",
                                                "Prancūziškas spaudimas hanteliais juos užsukant",
                                                "Pratimas orientuotas į: tricepso raumenį, papildomas ištempimas ir riešų užsukimas leidžia raumenį paveikti maksimaliai. Reikalaujantis patirties ir jegų, nes yra sunkus. ",
                                                "sunkus_skullcrush_hanteliai_uzsukant.gif"));
                lentele.Add(new Pratimu_lentele("Kojos",
                                               "'Sissy' pritūpimai",
                                               "Pratimas orientuotas į: kojų keturgalvį raumenį. ",
                                               "sissy_squats.gif"));
                lentele.Add(new Pratimu_lentele("Pečiai",
                                               "Trauka su lynu atgal užsukant ir stovint ",
                                               "Pratimas orientuotas į: galinę ir vidurinę pečių deltas. Užsukimas judėsio gale puikiai suspaudžia pečių raumenis ir priverčia juos dirbti dar sunkiau. ",
                                               "peciai_su_lynu_atgal_stovint.gif"));
                lentele.Add(new Pratimu_lentele("Pečiai",
                                               "Spaudimas hanteliais į viršų + suvedimas prieš save",
                                               "Pratimas orientuotas į: priekinę ir vidurinę pečių deltas. Pradedant judesį suvedus hantelius prieš save ir pereinant į spaudimą iš šonų leidžia labiau apkrauti priekinę deltą. ",
                                               "peciu_press_suvedus.gif"));
                lentele.Add(new Pratimu_lentele("Trapecija",
                                               "Trauka su vienu lynu iš apačios ",
                                               "Pratimas orientuotas į: trapecija. Iš apačios traukiamas lynas keliant petį į viršų ir stipriai suspaudžiant trapecijos raumenį. ",
                                               "trapecija_su_vienu_lynu_is_apacios.gif"));
                foreach (var item in lentele)
                {
                    db.Insert(item);
                }
            }

            //Tren planu lenteles patikrinimas ir sukurimas
            var tren_plan_lent_info = db.GetTableInfo("Tren_planu_lentele");
            if (tren_plan_lent_info.Count == 0)
            {
                db.CreateTable<Tren_planu_lentele>();
            }

            //Rysiu lenteles patikrinimas ir sukurimas
            var rysiu_lent_info = db.GetTableInfo("Rysiu_lentele");
            if (rysiu_lent_info.Count == 0)
            {
                db.CreateTable<Rysiu_lentele>();
            }
        }    
    }
}