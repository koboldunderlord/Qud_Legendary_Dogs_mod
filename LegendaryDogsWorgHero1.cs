using System;
using System.Collections.Generic;
using System.Text;
using XRL.Core;
using XRL.Names;
using XRL.Rules;
using XRL.World.Encounters;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	[Serializable]
	public class LegendaryDogsWorgHero1 : IPart
	{
		public bool bCreated;

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

		public LegendaryDogsWorgHero1()
		{
			base.Name = "LegendaryDogsWorgHero1";
		}

		public override bool SameAs(IPart p)
		{
			return false;
		}

		public override void Register(GameObject Object)
		{
			Object.RegisterPartEvent(this, "EnteredCell");
		}

		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell")
			{
				try
				{
					if (bCreated)
					{
						return true;
					}
					bCreated = true;
					GameObject parentObject = ParentObject;

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
					NameBuilder.Append(parentObject.DisplayName);

					parentObject.DisplayName = NameBuilder.ToString();
				}
				catch
				{
				}
			}
			return true;
		}
	}
}
