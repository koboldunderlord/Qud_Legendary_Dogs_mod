using System;
using System.Collections.Generic;   
using XRL.UI;

namespace XRL.World.Parts
{
    public class LegendaryDogsWorgGivesRep : IPart
    {
    	public override void Register(GameObject Object)
		{
			Object.RegisterPartEvent(this, "ObjectCreated");
			base.Register(Object);
		}

        public void ReplaceFactions()
        {
        	LegendaryDogsWorgHero1 HeroPart = ParentObject.GetPart<LegendaryDogsWorgHero1>();
            GivesRep GivesRepPart = ParentObject.GetPart<GivesRep>();
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
