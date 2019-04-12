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

namespace RncFinderApp.Models
{
    public class TowerInfo : IComparable<TowerInfo>
    {
        public int? Id { get; set; }
        public int? Lac { get; set; }
        public int? Rnc { get; set; }
        public int Mnc { get; set; }
        public int? Tac { get; set; }
        public long? Ci { get; set; }
        public int? Pci { get; set; }

        public override string ToString()
        {
            var baseStr = string.Empty;
            if (Lac != null && Tac == null) baseStr += $"LAC: {Lac} ";
            if (Tac != null) baseStr += $"TAC: {Tac} ";
            if (Id != null) baseStr += $"ID: {Id} ";
            if (Rnc != null) baseStr += $"RNC: {Rnc} ";
            if (Ci != null) baseStr += $"CI: {Ci} ";
            return baseStr;
        }

        public string Network
        {
            get
            {
                switch (Mnc)
                {
                    case 1: return "MoBi";
                    case 2: return "ViNa";
                    case 4: return "VTel";
                    case 5: return "VNam";
                    case 6: return "GMobi";
                    default: return string.Empty;
                }
            }
        }

        public int CompareTo(TowerInfo that)
        {
            if (Id > that.Id) return -1;
            if (Id == that.Id && Lac == that.Rnc) return 0;
            return 1;
        }
    }
}