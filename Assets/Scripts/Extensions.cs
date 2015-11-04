using LotsOfTowers.Framework;

namespace LotsOfTowers {
	// This class is in the root namespace on purpose!
	// Do not change
	public static class Extensions {

		// Usage:
		// string s = "tooltips.jump.title";
		// s.Localize();
		public static string Localize(this string str, string key) {
			if (GameManager.Instance.Language == "en_US") {
				return Assets.Localization.en_US.ResourceManager.GetString (key);
			} else if (GameManager.Instance.Language == "nl_NL") {
				// TODO: create nl_NL.resx and add it here
			}
		}
	}
}