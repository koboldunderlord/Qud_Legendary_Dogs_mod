using System;
using System.Collections.Generic;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Parts.Effects;

namespace XRL.World
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

        public static string BuildGoodTitle()
        {
            StringBuilder VeryGoodBuilder = new StringBuilder();
            int Chance = Stat.RandomCosmetic(1, 2);
            if (Chance == 2)
            {
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
    }
}