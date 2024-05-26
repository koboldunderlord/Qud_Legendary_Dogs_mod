using System;
using System.Collections.Generic;   
using XRL.UI;
using XRL.World;
using XRL.Messages;
using XRL.Collections;

namespace XRL.World.Parts
{
    public class LegendaryDogsGivesRep : GivesRep
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

            LegendaryDogsDogHero1 HeroPart = dog.GetPart<LegendaryDogsDogHero1>();

            if (ParentObject.DisplayName.Contains("Good"))
            {
                RepPart.ResetRelatedFactions();
                foreach (Faction Faction in Factions.loop())
                {
                    if (Faction.Visible && !dog.pBrain.Allegiance.ContainsKey(Faction.Name))
                    {
                        FriendorFoe FoF = new FriendorFoe();
                        FoF.faction = Faction.Name;
                        FoF.status = "friend";
                        FoF.reason = "for being a " + HeroPart.Title + " dog";
                        RepPart.relatedFactions.Add(FoF);
                        if (dog.pBrain.FactionFeelings.ContainsKey(Faction.Name))
                        {
                            StringMap<int> factionFeelings = dog.pBrain.FactionFeelings;
                            factionFeelings[Faction.Name] += HeroPart.GoodFactionPenalty;
                        }
                        else
                        {
                            dog.pBrain.FactionFeelings.Add(Faction.Name, HeroPart.GoodFactionPenalty);
                        }
                    }
                }
                RepPart.repValue = HeroPart.GoodFactionPenalty;
            } else {
                MessageQueue.AddPlayerMessage("fire");
                MessageQueue.AddPlayerMessage(RepPart.relatedFactions.Count.ToString());
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
