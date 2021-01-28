using System;
using Verse;
using System.Collections.Generic;

namespace RimFridge
{
    public class FridgeCache : MapComponent
    {
        private const string COULD_NOT_FIND_MAP_COMP = "unable to find fridge grid in map";

        private Dictionary<IntVec3, CompRefrigerator> FridgeGrid = new Dictionary<IntVec3, CompRefrigerator>();
        public static FridgeCache[] fridgeCache = new FridgeCache[8];

        public FridgeCache(Map map) : base(map) { }

        public override void FinalizeInit()
        {
            if (this.map.Index > fridgeCache.Length)
            {
                Array.Resize(ref fridgeCache, this.map.Index + 5);
            }
            fridgeCache[this.map.Index] = this;
        }

        public bool HasFridgeAt(IntVec3 cell)
        {
            return this.FridgeGrid.ContainsKey(cell);
        }

        public static FridgeCache GetFridgeCache(int mapIndex)
        {
            return fridgeCache[mapIndex];
        }

        public static void AddFridge(CompRefrigerator comp, int mapIndex)
        {
            var c = GetFridgeCache(mapIndex);
            if (c != null)
            {
                foreach (IntVec3 cell in GenAdj.OccupiedRect(comp.parent))
                {
                    c.FridgeGrid[cell] = comp;
                }
            }
        }

        public static bool TryGetFridge(IntVec3 cell, int mapIndex, out CompRefrigerator comp)
        {
            var c = GetFridgeCache(mapIndex);
            if (c != null)
            {
                return c.FridgeGrid.TryGetValue(cell, out comp);
            }
            comp = null;
            return false;
        }

        public static void RemoveFridge(CompRefrigerator comp, int mapIndex)
        {
            var c = GetFridgeCache(mapIndex);
            if (c != null)
            {
                foreach (IntVec3 cell in GenAdj.OccupiedRect(comp.parent))
                {
                    c.FridgeGrid.Remove(cell);
                }
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();

            if (this.map.Index > fridgeCache.Length)
            {
                Array.Resize(ref fridgeCache, this.map.Index + 5);
            }
            fridgeCache[this.map.Index] = this;
        }
    }
}
