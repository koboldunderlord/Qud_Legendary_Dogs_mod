using System;
using System.Collections.Generic;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Effects;
using XRL.World.Parts;
using XRL.Messages;

namespace XRL.World.Encounters.EncounterObjectBuilders
{
    public class LegendaryDogsDogBuilder
    {
        public static string[] MessyLiquids =
        {
            "blood-9999",
            "sap-9999"
        };

        public static string[] Prefixes =
        {
            "be",
            "ba",
            "ta",
            "to",
            "ru",
            "ro",
            "te",
            "ta",
            "da",
            "du",
            "di",
            "do",
            "u",
            "a",
            "i",
            "pu"
        };

        public static string[] Infixes =
        {
            "d",
            "dd",
            "m",
            "mm",
            "nn",
            "n",
            "p",
            "pp",
            "g",
            "gg"
        };

        public static string[] Postfixes =
        {
            "y",
            "o",
            "a",
            "i",
            "us"
        };

		public static string[] Titles = {
            "Playful",
            "Exciting",
            "Funny",
            "Clever",
            "Soft",
            "Loyal",
            "Wild",
            "Energetic",
            "Loveable",
            "Messy",
            "Clueless",
            "Happy",
            "Simple",
            "Loving",
            "Content",
            "Good",
            "Caring",
            "Fluffy",
            "Smooth",
            "Warm",
            "Awkward",
            "Floppy-tongued",
            "Stub-tailed"
        };

        public static string BuildTitle()
        {
            string Title = Titles.GetRandomElement();
            if (Title == "Good") 
            {
                Title = BuildGoodTitle();
            }
            return Title;
        }

        public static string BuildGoodTitle()
        {
            StringBuilder VeryGoodBuilder = new StringBuilder();
            int Chance = Stat.RandomCosmetic(1, 2);
            if (Chance == 2)
            {
                MessageQueue.AddPlayerMessage("Very");
                VeryGoodBuilder.Append("Very");
                Chance = Stat.RandomCosmetic(1, 2);
                while (Chance == 2)
                {
                    VeryGoodBuilder.Append(", Very");
                    Chance = Stat.RandomCosmetic(1, 2);
                }
                VeryGoodBuilder.Append(" ");
            }
            VeryGoodBuilder.Append("Good");

            return VeryGoodBuilder.ToString();
        }

        public static string BuildName(string BaseName, string Title)
        {
            StringBuilder NameBuilder = new StringBuilder();
            NameBuilder.Append("&M");
            string Prefix = Prefixes.GetRandomElement();
            NameBuilder.Append(char.ToUpper(Prefix[0]));
            NameBuilder.Append(Prefix.Substring(1));

            if (Stat.RandomCosmetic(1, 2) == 2)
            {
                NameBuilder.Append(Prefixes.GetRandomElement());
            }
            NameBuilder.Append(Infixes.GetRandomElement());
            NameBuilder.Append(Postfixes.GetRandomElement());
            NameBuilder.Append(" the ");
            NameBuilder.Append(Title);
            string[] DisplayName = BaseName.Split(' ');
            foreach (string s in DisplayName)
            {
                NameBuilder.Append(" ");
                NameBuilder.Append(s.Substring(0, 1).ToUpper());
                NameBuilder.Append(s.Substring(1));
            }
            return NameBuilder.ToString();
        }

        public static void AddModifiers(GameObject dog)
        {
            if (dog.DisplayName.Contains("Playful")
                || dog.DisplayName.Contains("Energetic")
                || dog.DisplayName.Contains("Exciting")
                || dog.DisplayName.Contains("Wild"))
            {
                dog.Statistics["MoveSpeed"].BaseValue += Stat.Random(5, 15);
            }
            else if (dog.DisplayName.Contains("Funny")
              || dog.DisplayName.Contains("Awkward")
              || dog.DisplayName.Contains("Clueless")
              || dog.DisplayName.Contains("Floppy-tongued"))
            {
                Description Desc = dog.GetPart<Description>();
                Desc._Short = Desc._Short + " =pronouns.Subjective= seems very silly.";

            }
            else if (dog.DisplayName.Contains("Clever"))
            {
                if (!dog.HasPart("LegendaryDogsWaterFinder"))
                {
                    dog.AddPart<LegendaryDogsWaterFinder>(new LegendaryDogsWaterFinder(), true);
                }
            }
            else if (dog.DisplayName.Contains("Soft"))
            {
                if (!dog.HasPart("LegendaryDogsPetTitlePart"))
                {
                    dog.AddPart<LegendaryDogsPetTitlePart>(new LegendaryDogsPetTitlePart(), true);
                }
            }
            else if (dog.DisplayName.Contains("Loyal"))
            {
                // nothing
            }
            else if (dog.DisplayName.Contains("Loveable"))
            {
                // handled via effects
            }
            else if (dog.DisplayName.Contains("Messy"))
            {
                dog.MakeBloody(MessyLiquids.GetRandomElement());
            }
            else if (dog.DisplayName.Contains("Happy"))
            {
                // nothing
            }
            else if (dog.DisplayName.Contains("Simple"))
            {
                // nothing
            }
            else if (dog.DisplayName.Contains("Loving"))
            {
                // nothing
            }
            else if (dog.DisplayName.Contains("Content"))
            {
                // nothing
            }
            else if (dog.DisplayName.Contains("Good"))
            {
                LegendaryDogsDogHero1 heroPart = dog.GetPart<LegendaryDogsDogHero1>();
                heroPart.GoodFactionPenalty = 100 + 25 * (dog.DisplayName.Split(new[] { "Very" }, StringSplitOptions.None).Length - 1);
            }
            else if (dog.DisplayName.Contains("Caring"))
            {
                // nothing
            }
            else if (dog.DisplayName.Contains("Fluffy"))
            {
                if (!dog.HasPart("LegendaryDogsPetTitlePart"))
                {
                    dog.AddPart<LegendaryDogsPetTitlePart>(new LegendaryDogsPetTitlePart("fluffy"), true);
                }
            }
            else if (dog.DisplayName.Contains("Smooth"))
            {
                if (!dog.HasPart("LegendaryDogsPetTitlePart"))
                {
                    dog.AddPart<LegendaryDogsPetTitlePart>(new LegendaryDogsPetTitlePart("smooth"), true);
                }
            }
            else if (dog.DisplayName.Contains("Warm"))
            {
                if (!dog.HasPart("LegendaryDogsPetTitlePart"))
                {
                    dog.AddPart<LegendaryDogsPetTitlePart>(new LegendaryDogsPetTitlePart("warm"), true);
                }
            }
            else if (dog.DisplayName.Contains("Stub-tailed"))
            {
                // nothing
            }
        }

        

        public bool BuildObject(GameObject dog, string Context = null)
        {
            dog.HasProperName = true;

            return true;
        }
    }
}