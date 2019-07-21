using System;
using XRL.UI;

namespace XRL.World.Parts
{
    [Serializable]
    public class LegendaryDogsPetTitlePart : IPart
    {
        public string Descriptor;

        public LegendaryDogsPetTitlePart() : this("soft")
        { 
        }

        public LegendaryDogsPetTitlePart(string Descriptor)
        {
            base.Name = "LegendaryDogsSoftTitlePart";
            this.Descriptor = Descriptor;
        }

        public override bool SameAs(IPart p)
        {
            return false;
        }

        public override void Register(GameObject Object)
        {
            Object.RegisterPartEvent(this, "ObjectPetted");
        }

        public override bool FireEvent(Event E)
        {
            if (E.ID == "ObjectPetted")
            {
                GameObject Petter = E.GetParameter<GameObject>("Petter");
                GameObject Petted = E.GetParameter<GameObject>("Object");
                if (Petter.IsPlayer() && Petted.SameAs(ParentObject))
                {
                    Popup.Show("You can't help but stop and notice how " + this.Descriptor + " " + ParentObject.the + ParentObject.DisplayNameOnlyDirect + "&y" + "'s fur is.  It's incredible!");
                }
            }
            return base.FireEvent(E);
        }
    }
}
