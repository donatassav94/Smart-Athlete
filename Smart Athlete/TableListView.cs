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
    public class TableListView : BaseAdapter<lentele_items>
    {
        public Context myContext;
        public List<lentele_items> myItems;
        public TableListView(Context context, List<lentele_items> items)
        {
            myItems = items;
            myContext = context;
        }
        public override lentele_items this[int position]
        {
            get
            {
                return  myItems[position]; 
            }
        }

        public override int Count
        {
            get
            {
                return myItems.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            if (row == null)
            {
                row = LayoutInflater.From(myContext).Inflate(Resource.Layout.table_listview, null, false);
            }
                TextView nr = row.FindViewById<TextView>(Resource.Id.nr);
                TextView pavadinimas = row.FindViewById<TextView>(Resource.Id.plano_pavadinimas);
                TextView serijos = row.FindViewById<TextView>(Resource.Id.serijos);
                TextView pakartojimai = row.FindViewById<TextView>(Resource.Id.pakartojimai);
                nr.Text = Convert.ToString(myItems[position].nr);
                pavadinimas.Text = myItems[position].pavadinimas;
                serijos.Text = Convert.ToString(myItems[position].serijos);
                pakartojimai.Text = Convert.ToString(myItems[position].pakartojimai);
            return row;
        }
    }
}