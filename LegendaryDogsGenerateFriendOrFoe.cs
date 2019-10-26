using HistoryKit;
using System.Collections.Generic;
using XRL.Language;

namespace XRL.World.Parts
{
	public class LegendaryDogsGenerateFriendOrFoe : GenerateFriendOrFoe
	{
		private static List<string> _hateReasons;

		private static List<string> _likeReasons;

		static LegendaryDogsGenerateFriendOrFoe()
		{
			_hateReasons = new List<string>(24);
			_likeReasons = new List<string>(26);
			_hateReasons.Add("barking at their $noun");
			_hateReasons.Add("playing fetch with their cherished heirloom"); // helado
			_hateReasons.Add("burying their sacred artifact"); // NalathniDragon
			_hateReasons.Add("slaying one of their leaders");
			_hateReasons.Add("slaying one of their leaders and peeing on their corpse");
			_hateReasons.Add("eating one of their young");
			_hateReasons.Add("some reason no one remembers");
			_hateReasons.Add("tearing up their crop fields");
			_hateReasons.Add("digging up the remains of their ancestors");
			_hateReasons.Add("digging up the remains of their pets");
			_hateReasons.Add("digging up the remains of their children");
			_hateReasons.Add("eating all of their food at a party");
			_hateReasons.Add("peeing on their $noun"); // helado
			_hateReasons.Add("claiming their territory"); // NalathniDragon
			_hateReasons.Add("barking all night");
			_hateReasons.Add("whimpering for no reason and being inconsolable"); // evanbalster
			_hateReasons.Add("leading a raiding party on one of their camps");
			_hateReasons.Add("eating all their fruit");
			_hateReasons.Add("interrupting their secret ceremonies");
			_hateReasons.Add("ripping apart their favorite robot");
			_hateReasons.Add("ruining the festival of Ut yara Ux");
			_hateReasons.Add("barking at the moon");
			_hateReasons.Add("constantly demanding atttention"); // NalathniDragon
			_hateReasons.Add("jumping on the table at a ritual dinner");
			_likeReasons.Add("licking their $noun");
			_likeReasons.Add("sharing freshwater with them");
			_likeReasons.Add("sharing food with them at a supper feast");
			_likeReasons.Add("saving one of their young from drowning");
			_likeReasons.Add("resembling one of their idols");
			_likeReasons.Add("respecting the sanctity of a burial site");
			_likeReasons.Add("attending a funeral for their leader");
			_likeReasons.Add("disrupting a plot against them");
			_likeReasons.Add("fetching a sacred artifact"); // evanbalster
			_likeReasons.Add("defending them from a $noun");
			_likeReasons.Add("entertaining their children");
			_likeReasons.Add("being adorable");
			_likeReasons.Add("keeping them safe from a rival faction");
			_likeReasons.Add("being therapeutic"); // NalathniDragon
			_likeReasons.Add("their friendliness");
			_likeReasons.Add("guiding them with keen senses"); // NalathniDragon
			_likeReasons.Add("for their boundless optimism"); // NalathniDragon
			_likeReasons.Add("guiding their lost members home"); // NalathniDragon
			_likeReasons.Add("tracking down a fugitive"); // NalathniDragon
			_likeReasons.Add("for impressively loud howling"); // NalathniDragon
			_likeReasons.Add("for letting them scratch =pronouns.possessive= belly"); // NalathniDragon
			_likeReasons.Add("teaching them how to play fetch"); // NalathniDragon
			_likeReasons.Add("diagnosing their glotrot by scent"); // NalathniDragon
			_likeReasons.Add("diagnosing their ironshank by scent"); // helado
			_likeReasons.Add("cuddling with their members during hard times"); // helado
			_likeReasons.Add("warning them of an impending disaster"); // NalathniDragon
		}

		public static string getHateReason()
		{
			return _hateReasons.GetRandomElement().Replace("$noun", Grammar.Pluralize(HistoricStringExpander.ExpandString("<spice.nouns.!random>")));
		}

		public static string getLikeReason()
		{
			return _likeReasons.GetRandomElement().Replace("$noun", Grammar.Pluralize(HistoricStringExpander.ExpandString("<spice.nouns.!random>")));
		}

		public static string getRandomFaction(GameObject parent)
		{
            return GenerateFriendOrFoe.getRandomFaction(parent);
		}
	}
}
