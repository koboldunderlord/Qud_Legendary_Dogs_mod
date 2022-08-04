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
    public class LegendaryDogsWorgBuilder
    {
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

        public static string BuildName(string BaseName)
        {
            StringBuilder NameBuilder = new StringBuilder();
            NameBuilder.Append("&M");
            string Prefix = Prefixes.GetRandomElement();
            NameBuilder.Append(char.ToUpper(Prefix[0]));
            NameBuilder.Append(Prefix.Substring(1));

            if (Stat.RandomCosmetic(1,2) == 2) {
                NameBuilder.Append(Prefixes.GetRandomElement());
            }
            NameBuilder.Append(Infixes.GetRandomElement());
            NameBuilder.Append(Postfixes.GetRandomElement());
            NameBuilder.Append(", Awe-inspiring ");
            NameBuilder.Append(BaseName);

            return NameBuilder.ToString();
        }

        

        public bool BuildObject(GameObject dog, string Context = null)
        {
            dog.DisplayName  = BuildName(dog.DisplayName);

            dog.HasProperName = true;

            return true;
        }
    }
}