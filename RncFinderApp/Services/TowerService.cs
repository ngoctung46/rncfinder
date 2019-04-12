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
using RncFinderApp.Interfaces;
using RncFinderApp.Models;

namespace RncFinderApp.Services
{
    public class TowerService : ITowerService
    {
        private TowerLab towerLab;

        public TowerService()
        {
            towerLab = TowerLab.GetInstance();
        }

        public bool Add(TowerInfo tower)
        {
            try
            {
                towerLab.Add(tower);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(string towerId)
        {
            throw new NotImplementedException();
        }

        public IList<TowerInfo> GetAll()
        {
            return towerLab.Get();
        }

        public bool Update(TowerInfo tower)
        {
            throw new NotImplementedException();
        }
    }
}