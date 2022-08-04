using System;
using System.Collections.Generic;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts
{
    [Serializable]
    public class LegendaryDogsDogHero1 : IPart
    {
        public bool bCreated;
        public int GoodFactionPenalty;
        public string VeryGoodText;

        public LegendaryDogsDogHero1()
        {
            if (this.VeryGoodText == null)
            {
                this.VeryGoodText = "";
            }
        }

        public override bool SameAs(IPart p)
        {
            return false;
        }

        public override void Register(GameObject Object)
        {
            Object.RegisterPartEvent(this, "BeforeDeathRemoval");
            Object.RegisterPartEvent(this, "AfterLookedAt");
            base.Register(Object);
        }

        public override bool FireEvent(Event E)
        {
            if (E.ID == "BeforeDeathRemoval" && this.GoodFactionPenalty > 0)
            {
                GameObject gameObjectParameter = E.GetGameObjectParameter("Killer");
                if (gameObjectParameter != null && (gameObjectParameter.IsPlayer() || gameObjectParameter.IsPlayerLed()))
                {
                    Popup.Show("You murdered a " + VeryGoodText + " dog.  You are cursed.");
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
