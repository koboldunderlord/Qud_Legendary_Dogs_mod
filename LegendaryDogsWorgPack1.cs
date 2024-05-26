using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.Rules;
using XRL.World.ObjectBuilders;

namespace XRL.World.Parts
{
	[Serializable]
	public class LegendaryDogsWorgPack1 : IPart
	{
		//
		// Fields
		//
		public bool bCreated;

		//
		// Constructors
		//
		public LegendaryDogsWorgPack1 ()
		{
			base.SetName("LegendaryDogsWorgPack1");
		}

		//
		// Methods
		//
		public override bool FireEvent (Event E)
		{
			if (E.ID == "EnteredCell") {
				try {
					if (this.bCreated) {
						return true;
					}
					this.bCreated = true;

					Physics physics = this.ParentObject.GetPart ("Physics") as Physics;
					List<Cell> cellList = new List<Cell> ();
					physics.CurrentCell.GetAdjacentCells (4, cellList, true);
					List<Cell> emptyCellList = new List<Cell> ();
					foreach (Cell current in cellList) {
						if (current.IsEmpty ()) {
							emptyCellList.Add (current);
						}
					}
					List<string> randomWorgs = new List<string> ();
					int worgs = Stat.Random (2, 5);
					for (int i = 0; i < worgs; i++) {
						randomWorgs.Add ("LegendaryDogs_Worg");
					}
					Tier1HumanoidEquipment tier1HumanoidEquipment = new Tier1HumanoidEquipment ();
					int worgIterator = 0;
					while (worgIterator < randomWorgs.Count && emptyCellList.Count > 0) {
						GameObject gameObject = GameObjectFactory.Factory.CreateObject (randomWorgs [worgIterator]);
						tier1HumanoidEquipment.Apply (gameObject, null);
						gameObject.GetPart<Brain> ().PartyLeader = this.ParentObject;
						Cell randomElement = emptyCellList.GetRandomElement (null);
						randomElement.AddObject (gameObject);
						XRLCore.Core.Game.ActionManager.AddActiveObject (gameObject);
						emptyCellList.Remove (randomElement);
						worgIterator++;
					}
				}
				catch {
				}
				return true;
			}
			return true;
		}

		public override void Register (GameObject Object)
		{
			Object.RegisterPartEvent (this, "EnteredCell");
		}

		public override bool SameAs (IPart p)
		{
			return false;
		}
	}
}
