using System;
using XRL.Rules;

namespace XRL.World.Parts
{

    [Serializable]
    public class LegendaryDogsWaterFinder : IPart
    {

        public override bool SameAs(IPart p)
        {
            if (!base.SameAs(p)) return false;
            return true;
        }

        public override void Register(GameObject Object)
        {
            Object.RegisterPartEvent(this, "EndTurn");
            base.Register(Object);
        }

        public override bool FireEvent(Event E)
        {
            if (E.ID == "EndTurn"
                && !ParentObject.AreHostilesNearby()
                && ParentObject.CurrentCell.ParentZone.Z <= 10
                && string.IsNullOrEmpty(ParentObject.CurrentCell.GroundLiquid)
                && Stat.Random(1, 20) == 20)
            {
                GameObject Water = GameObjectFactory.Factory.CreateObject("Water", Stat.Random(1, 4));
                ParentObject.CurrentCell.AddObject(Water);
            }

            return base.FireEvent(E);
        }

    }

}