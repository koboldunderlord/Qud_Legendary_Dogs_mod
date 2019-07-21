using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
    [Serializable]
    public class LegendaryDogsGivesRep : GivesRep
    {
    	public override void Register(GameObject Object)
		{
			Object.RegisterPartEvent(this, "LegendaryDogsDisplayNameSet");
			base.Register(Object);
		}

        public new int FillInRelatedFactions(bool Initial = false)
        {
        	LegendaryDogsDogHero1 HeroPart = ParentObject.GetPart<LegendaryDogsDogHero1>();
            if (HeroPart.GoodFactionPenalty > 0)
            {
            	IPart.AddPlayerMessage("we're in!");
                foreach (KeyValuePair<string, FactionInfo> Faction in Factions.FactionList)
                {
                    if (Faction.Value.bVisible && !ParentObject.pBrain.FactionMembership.ContainsKey(Faction.Value.Name))
                    {
                        FriendorFoe FoF = new FriendorFoe();
                        FoF.faction = Faction.Value.Name;
                        FoF.status = "friend";
                        FoF.reason = "for being a " + HeroPart.VeryGoodText + " dog";
                        relatedFactions.Add(FoF);
                        if (ParentObject.pBrain.FactionFeelings.ContainsKey(Faction.Value.Name))
						{
							if (Initial)
							{
								Dictionary<string, int> factionFeelings = ParentObject.pBrain.FactionFeelings;
								factionFeelings[Faction.Value.Name] += 100;
							}
						}
						else
						{
							ParentObject.pBrain.FactionFeelings.Add(Faction.Value.Name, 100);
						}
                    }
                }
                repValue = HeroPart.GoodFactionPenalty;
                return relatedFactions.Count;
            } else
            {
                int Count = base.FillInRelatedFactions(Initial);
                for (int i = 0; i < relatedFactions.Count; i++) 
                {
                	FriendorFoe FoF = relatedFactions[i];
                	if (FoF.status == "friend")
                	{
                		FoF.reason = LegendaryDogsGenerateFriendOrFoe.getLikeReason();
                	} else {
                		FoF.reason = LegendaryDogsGenerateFriendOrFoe.getHateReason();
                	}
                }

                
                return Count;
            }
        }

        public override bool FireEvent(Event E)
		{
			if (E.ID == "FactionsAdded")
			{
				// Do nothing - wait for ObjectCreated
				return true;
			} else if (E.ID == "LegendaryDogsDisplayNameSet") 
			{
				FillInRelatedFactions(Initial: true);
				return true;
			}
			return base.FireEvent(E);
		}
    }
}
