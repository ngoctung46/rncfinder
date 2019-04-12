using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Google.Cloud.Firestore;
using RncFinderApp.Models;

namespace RncFinderApp
{
    public class TowerLab
    {
        private ObservableCollection<TowerInfo> towers;
        private static TowerLab instance = null;
        private readonly string projectId = "rncfinder";
        private FirestoreDb db;

        private TowerLab()
        {
            db = FirestoreDb.Create(projectId);
            towers = new ObservableCollection<TowerInfo>();
        }

        public static TowerLab GetInstance()
        {
            if (instance == null)
            {
                instance = new TowerLab();
            }
            return instance;
        }

        public void Add(TowerInfo tower)
        {
            if (!towers.Any(x => x.Id == tower.Id && x.Lac == tower.Lac))
            {
                Console.WriteLine($"Added {tower.ToString()}");
                towers.Add(tower);
                return;
            }
        }

        public ObservableCollection<TowerInfo> Get() => towers;

        //private async ObservableCollection<TowerInfo> GetTowers()
        //{
        //    return Task.CompletedTask;
        //}
    }
}