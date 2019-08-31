using System;
using System.Collections.Generic;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Parts.Effects;

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
            base.Name = "LegendaryDogsDogHero1";
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
            Object.RegisterPartEvent(this, "ObjectCreated");
            Object.RegisterPartEvent(this, "BeforeDeathRemoval");
            Object.RegisterPartEvent(this, "AfterLookedAt");
        }

        public void AddModifiers()
        {
            if (ParentObject.DisplayName.Contains("Playful")
                || ParentObject.DisplayName.Contains("Energetic")
                || ParentObject.DisplayName.Contains("Exciting")
                || ParentObject.DisplayName.Contains("Wild"))
            {
                ParentObject.Statistics["MoveSpeed"].BaseValue += Stat.Random(5, 15);
            }
            else if (ParentObject.DisplayName.Contains("Funny")
              || ParentObject.DisplayName.Contains("Awkward")
              || ParentObject.DisplayName.Contains("Clueless")
              || ParentObject.DisplayName.Contains("Floppy-tongued"))
            {
                Description Desc = ParentObject.GetPart<Description>();
                Desc._Short = Desc._Short + " =pronouns.Subjective= seems very silly.";

            }
            else if (ParentObject.DisplayName.Contains("Clever"))
            {
                if (!ParentObject.HasPart("LegendaryDogsWaterFinder"))
                {
                    ParentObject.AddPart<LegendaryDogsWaterFinder>(new LegendaryDogsWaterFinder(), true);
                }
            }
            else if (ParentObject.DisplayName.Contains("Soft"))
            {
                if (!ParentObject.HasPart("LegendaryDogsPetTitlePart"))
                {
                    ParentObject.AddPart<LegendaryDogsPetTitlePart>(new LegendaryDogsPetTitlePart(), true);
                }
            }
            else if (ParentObject.DisplayName.Contains("Loyal"))
            {
                // nothing
            }
            else if (ParentObject.DisplayName.Contains("Loveable"))
            {
                // handled via effects
            }
            else if (ParentObject.DisplayName.Contains("Messy"))
            {
                ParentObject.MakeBloody(LegendaryDogsDogBuilder.MessyLiquids.GetRandomElement());
            }
            else if (ParentObject.DisplayName.Contains("Happy"))
            {
                // nothing
            }
            else if (ParentObject.DisplayName.Contains("Simple"))
            {
                // nothing
            }
            else if (ParentObject.DisplayName.Contains("Loving"))
            {
                // nothing
            }
            else if (ParentObject.DisplayName.Contains("Content"))
            {
                // nothing
            }
            else if (ParentObject.DisplayName.Contains("Good"))
            {
                this.GoodFactionPenalty = 100 + 25 * (ParentObject.DisplayName.Split(new[] { "Very" }, StringSplitOptions.None).Length - 1);
            }
            else if (ParentObject.DisplayName.Contains("Caring"))
            {
                // nothing
            }
            else if (ParentObject.DisplayName.Contains("Fluffy"))
            {
                if (!ParentObject.HasPart("LegendaryDogsPetTitlePart"))
                {
                    ParentObject.AddPart<LegendaryDogsPetTitlePart>(new LegendaryDogsPetTitlePart("fluffy"), true);
                }
            }
            else if (ParentObject.DisplayName.Contains("Smooth"))
            {
                if (!ParentObject.HasPart("LegendaryDogsPetTitlePart"))
                {
                    ParentObject.AddPart<LegendaryDogsPetTitlePart>(new LegendaryDogsPetTitlePart("smooth"), true);
                }
            }
            else if (ParentObject.DisplayName.Contains("Warm"))
            {
                if (!ParentObject.HasPart("LegendaryDogsPetTitlePart"))
                {
                    ParentObject.AddPart<LegendaryDogsPetTitlePart>(new LegendaryDogsPetTitlePart("warm"), true);
                }
            }
            else if (ParentObject.DisplayName.Contains("Stub-tailed"))
            {
                // nothing
            }
        }

        public override bool FireEvent(Event E)
        {
            if (E.ID == "ObjectCreated")
            {
                if (bCreated)
                {
                    return true;
                }
                bCreated = true;

                string Title = LegendaryDogsDogBuilder.Titles.GetRandomElement();
                if (Title == "Good")
                {
                    Title = LegendaryDogsDogBuilder.BuildGoodTitle();
                    VeryGoodText = Title.ToLower();
                }
                ParentObject.DisplayName = LegendaryDogsDogBuilder.BuildName(ParentObject.DisplayName, Title);
                AddModifiers();
            }
            else if (E.ID == "BeforeDeathRemoval" && this.GoodFactionPenalty > 0)
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
