using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Telephony;
using Android.Telephony.Gsm;
using Android.Views;
using Android.Widget;
using RncFinderApp.Models;
using RncFinderApp.Services;

namespace RncFinderApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private TextView cellText;
        private TelephonyManager telephony;
        private PhoneStateListener phoneStateListener;
        private TowerService towerService;
        private RecyclerView recyclerView;
        private RecyclerView.LayoutManager layoutManager;
        private CellAdapter cellAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            towerService = new TowerService();

            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            layoutManager = new LinearLayoutManager(this);
            recyclerView.SetLayoutManager(layoutManager);
            cellAdapter = new CellAdapter(towerService.GetAll());
            recyclerView.SetAdapter(cellAdapter);

            //Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            //SetSupportActionBar(toolbar);
            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;
            cellText = FindViewById<TextView>(Resource.Id.cell_text);
            telephony = GetSystemService(TelephonyService) as TelephonyManager;
            phoneStateListener = new CellListener(ref cellText, telephony);
            telephony.Listen(phoneStateListener, PhoneStateListenerFlags.CellLocation);

            (towerService.GetAll() as ObservableCollection<TowerInfo>).CollectionChanged += (s, e) =>
            {
                cellAdapter.NotifyDataSetChanged();
            };
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        private class CellListener : PhoneStateListener
        {
            private TextView cellText;
            private readonly TelephonyManager telephonyManager;
            private TowerService towerService;

            public CellListener(ref TextView textView, TelephonyManager telephonyManager)
            {
                cellText = textView;
                this.telephonyManager = telephonyManager;
                towerService = new TowerService();
            }

            public override void OnCellLocationChanged(CellLocation location)
            {
                var connectedCells = telephonyManager.AllCellInfo
                    .Where(x => x.IsRegistered).ToList();
                string info = string.Empty;
                for (int i = 0; i < connectedCells.Count; i++)
                {
                    var tower = GetTowerInfo(connectedCells[i]);
                    if (tower == null) return;
                    towerService.Add(tower);
                    info += $"{i + 1}.{tower.Network}: {tower.ToString()}\n";
                }
                cellText.Text = info;
            }

            private TowerInfo GetTowerInfo(CellInfo registeredCell)
            {
                if (registeredCell is CellInfoGsm)
                {
                    var gsmCell = (registeredCell as CellInfoGsm).CellIdentity;
                    return new TowerInfo
                    {
                        Id = gsmCell.Cid,
                        Lac = gsmCell.Lac,
                        Mnc = gsmCell.Mnc
                    };
                }
                if (registeredCell is CellInfoWcdma)
                {
                    var wcdmaCell = (registeredCell as CellInfoWcdma).CellIdentity;
                    return new TowerInfo
                    {
                        Id = wcdmaCell.Cid % 65536,
                        Lac = wcdmaCell.Lac,
                        Rnc = wcdmaCell.Cid / 65536,
                        Mnc = wcdmaCell.Mnc
                    };
                }
                if (registeredCell is CellInfoLte)
                {
                    var lteCell = (registeredCell as CellInfoLte).CellIdentity;
                    return new TowerInfo
                    {
                        Id = lteCell.Ci % 65536,
                        Mnc = lteCell.Mnc,
                        Rnc = lteCell.Ci / 65536,
                        Ci = lteCell.Ci,
                        Pci = lteCell.Pci,
                        Tac = lteCell.Tac,
                        Lac = lteCell.Tac
                    };
                }
                return null;
            }
        }

        public class CellViewHolder : RecyclerView.ViewHolder
        {
            public TextView TextView { get; set; }

            public CellViewHolder(View itemView) : base(itemView)
            {
                TextView = itemView.FindViewById<TextView>(Resource.Id.textView);
            }
        }

        public class CellAdapter : RecyclerView.Adapter
        {
            public IList<TowerInfo> towers;

            public CellAdapter(IList<TowerInfo> towers)
            {
                this.towers = towers;
            }

            public override int ItemCount => towers.Count;

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                CellViewHolder vh = holder as CellViewHolder;
                vh.TextView.Text = towers[position].ToString();
            }

            public override RecyclerView.ViewHolder
                OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                View itemView = LayoutInflater.From(parent.Context)
                    .Inflate(Resource.Layout.cell_view, parent, false);
                CellViewHolder vh = new CellViewHolder(itemView);
                return vh;
            }
        }
    }
}