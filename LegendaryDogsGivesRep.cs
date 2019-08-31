using System;
using System.Collections.Generic;   
using XRL.UI;

namespace XRL.World.Parts
{
    public class LegendaryDogsGivesRep : IPart
    {
    	public override void Register(GameObject Object)
		{
			Object.RegisterPartEvent(this, "ObjectCreated");
			base.Register(Object);
		}

        public void ReplaceFactions()
        {
        	LegendaryDogsDogHero1 HeroPart = ParentObject.GetPart<LegendaryDogsDogHero1>();
            GivesRep GivesRepPart = ParentObject.GetPart<GivesRep>();
            if (HeroPart.GoodFactionPenalty > 0)
            {
                GivesRepPart.ResetRelatedFactions();
                foreach (KeyValuePair<string, FactionInfo> Faction in Factions.FactionList)
                {
                    if (Faction.Value.bVisible && !ParentObject.pBrain.FactionMembership.ContainsKey(Faction.Value.Name))
                    {
                        FriendorFoe FoF = new FriendorFoe();
                        FoF.faction = Faction.Value.Name;
                        FoF.status = "friend";
                        FoF.reason = "for being a " + HeroPart.VeryGoodText + " dog";
                        GivesRepPart.relatedFactions.Add(FoF);
                        if (ParentObject.pBrain.FactionFeelings.ContainsKey(Faction.Value.Name))
						{
							Dictionary<string, int> factionFeelings = ParentObject.pBrain.FactionFeelings;
							factionFeelings[Faction.Value.Name] += HeroPart.GoodFactionPenalty;
						}
						else
						{
							ParentObject.pBrain.FactionFeelings.Add(Faction.Value.Name, HeroPart.GoodFactionPenalty);
						}
                    }
                }
                GivesRepPart.repValue = HeroPart.GoodFactionPenalty;
            } else
            {
                for (int i = 0; i < GivesRepPart.relatedFactions.Count; i++) 
                {
                	FriendorFoe FoF = GivesRepPart.relatedFactions[i];
                	if (FoF.status == "friend")
                	{
                		FoF.reason = LegendaryDogsGenerateFriendOrFoe.getLikeReason();
                	} else {
                		FoF.reason = LegendaryDogsGenerateFriendOrFoe.getHateReason();
                	}
                }
            }
        }

        public override bool FireEvent(Event E)
		{
			if (E.ID == "ObjectCreated")
            {
                ReplaceFactions();
                ParentObject.RemovePart(this);
            }
			return base.FireEvent(E);
		}
    }
}
