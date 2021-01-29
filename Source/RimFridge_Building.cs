using RimWorld;
using SaveStorageSettingsUtil;
using System.Collections.Generic;
using Verse;

namespace RimFridge
{
    public class RimFridge_Building : Building_Storage
    {
        public RimFridge_Building() : base() { }

        public override IEnumerable<Gizmo> GetGizmos()
        {
            List<Gizmo> l = new List<Gizmo>(base.GetGizmos());
            return SaveStorageSettingsGizmoUtil.AddSaveLoadGizmos(l, "fridge", this.settings.filter);
        }

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);

            FridgeCache.RegisterFridgeBuilding(this);
        }

        public override void DeSpawn(DestroyMode mode = DestroyMode.Vanish)
        {
            base.DeSpawn(mode);

            FridgeCache.DeRegisterFridgeBuilding(this);
        }
    }
}
