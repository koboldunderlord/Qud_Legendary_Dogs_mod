using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Parts
{
    [Serializable]
    public class LegendaryDogsDogPack1 : IPart
    {
        //
        // Fields
        //
        public bool bCreated;

        //
        // Constructors
        //
        public LegendaryDogsDogPack1()
        {
        }

        //
        // Methods
        //
        public override bool FireEvent(Event E)
        {
            if (E.ID == "EnteredCell")
            {
                if (this.bCreated)
                {
                    return true;
                }
                this.bCreated = true;

                Physics physics = this.ParentObject.GetPart("Physics") as Physics;
                List<Cell> cellList = new List<Cell>();
                physics.CurrentCell.GetAdjacentCells(4, cellList, true);
                List<Cell> emptyCellList = new List<Cell>();
                foreach (Cell current in cellList)
                {
                    if (current.IsEmpty())
                    {
                        emptyCellList.Add(current);
                    }
                }
                string ParentBlueprint = ParentObject.GetBlueprint().Inherits;
                List<string> randomDogs = new List<string>();
                int dogs = Stat.Random(2, 5);
                if (ParentObject.DisplayName.Contains("Loving")
                    || ParentObject.DisplayName.Contains("Caring")
                    || ParentObject.DisplayName.Contains("Loyal"))
                {
                    dogs += 5;
                }
                for (int i = 0; i < dogs; i++)
                {
                    randomDogs.Add(ParentBlueprint);
                }
                int dogIterator = 0;
                while (dogIterator < randomDogs.Count && emptyCellList.Count > 0)
                {
                    GameObject gameObject = GameObjectFactory.Factory.CreateObject(randomDogs[dogIterator]);
                    gameObject.GetPart<Brain>().PartyLeader = this.ParentObject;
                    Cell randomElement = emptyCellList.GetRandomElement(null);
                    randomElement.AddObject(gameObject);
                    XRLCore.Core.Game.ActionManager.AddActiveObject(gameObject);
                    emptyCellList.Remove(randomElement);
                    dogIterator++;
                }

                return true;
            }
            return true;
        }

        public override void Register(GameObject Object)
        {
            Object.RegisterPartEvent(this, "EnteredCell");
        }

        public override bool SameAs(IPart p)
        {
            return false;
        }
    }
}
