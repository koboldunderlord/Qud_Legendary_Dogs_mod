using System;
using System.Collections.Generic;   
using XRL.UI;
using XRL.Messages;

namespace XRL.World.Parts
{
    public class LegendaryDogsWorgGivesRep : GivesRep
    {
        public bool FixedRep;

        public override void Register(GameObject Object)
        {
            FixedRep = false;
            base.Register(Object);
        }
        
        public void ReplaceFactions() 
        {
            GivesRep RepPart = this;
            GameObject dog = ParentObject;
                
            foreach (FriendorFoe FoF in RepPart.relatedFactions) 
            {
                if (FoF.status == "friend")
                {
                    MessageQueue.AddPlayerMessage("like");
                    FoF.reason = LegendaryDogsGenerateFriendOrFoe.getLikeReason();
                } else {
                    MessageQueue.AddPlayerMessage("dislike");
                    FoF.reason = LegendaryDogsGenerateFriendOrFoe.getHateReason();
                }
            }
        }

public override bool HandleEvent(GetShortDescriptionEvent E)
{
    if (!FixedRep) {
        ReplaceFactions();
        FixedRep = true;
    }
    return base.HandleEvent(E);
}
    }
}
