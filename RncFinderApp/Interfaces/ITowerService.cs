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
using RncFinderApp.Models;

namespace RncFinderApp.Interfaces
{
    public interface ITowerService
    {
        IList<TowerInfo> GetAll();

        bool Add(TowerInfo tower);

        bool Update(TowerInfo tower);

        bool Delete(string towerId);
    }
}