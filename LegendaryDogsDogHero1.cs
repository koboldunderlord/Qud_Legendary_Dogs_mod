using System;
using System.Collections.Generic;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Effects;
using XRL.Messages;
using XRL.World.Encounters.EncounterObjectBuilders;

namespace XRL.World.Parts
{
    [Serializable]
    public class LegendaryDogsDogHero1 : IPart
    {
        public int GoodFactionPenalty;
        public string Title;

        public LegendaryDogsDogHero1()
        {
            this.Title = LegendaryDogsDogBuilder.BuildTitle();
            this.GoodFactionPenalty = 0;
        }

        public override bool SameAs(IPart p)
        {
            return false;
        }

        public override void Register(GameObject Object)
        {
            Object.RegisterPartEvent(this, "BeforeDeathRemoval");
            Object.RegisterPartEvent(this, "AfterLookedAt");
            Object.RegisterPartEvent(this, "ObjectCreated");
            base.Register(Object);
        }

        

        public override bool FireEvent(Event E)
        {
            if (E.ID == "ObjectCreated")
            {
                ParentObject.DisplayName = LegendaryDogsDogBuilder.BuildName(ParentObject.DisplayName, Title);
                LegendaryDogsDogBuilder.AddModifiers(ParentObject);
            } else if (E.ID == "BeforeDeathRemoval" && this.GoodFactionPenalty > 0)
            {
                GameObject gameObjectParameter = E.GetGameObjectParameter("Killer");
                if (gameObjectParameter != null && (gameObjectParameter.IsPlayer() || gameObjectParameter.IsPlayerLed()))
                {
                    Popup.Show("You murdered a " + this.Title + " dog.  You are cursed.");
                    // JournalAPI.AddAccomplishment("You murdered the " + ParentObject. + str + ".", HistoricStringExpander.ExpandString("In the month of " + Calendar.getMonth() + " of " + Calendar.getYear().ToString() + ", brave =name= slew <spice.commonPhrases.odious.!random> " + str + " in single combat."), muralCategory: JournalAccomplishment.MuralCategory.Slays, muralWeight: JournalAccomplishment.MuralWeight.Low);
                }
            }
            else if (E.ID == "AfterLookedAt" && ParentObject.DisplayName.Contains("Loveable"))
            {
                GameObject Looker = E.GetGameObjectParameter("Looker");
                if (Looker == null)
                {
                    Looker = XRLCore.Core.Game.Player.Body;
                }
                if (!Looker.HasEffect("Lovesick"))
                {
                    if (Looker.IsPlayer())
                    {
                        Popup.Show("You feel your heart beating in your chest after seeing how loveable " + ParentObject.DisplayNameOnly + " is.");
                    }
                    else
                    {
                        IPart.AddPlayerMessage(Looker.The + Looker.DisplayNameOnly + Looker.Is + " visibly struck by how loveable " + ParentObject.DisplayNameOnly + " is.");
                    }

                    Looker.ApplyEffect(new Lovesick(100, ParentObject));
                }
            }
            return base.FireEvent(E);
        }
    }
}
