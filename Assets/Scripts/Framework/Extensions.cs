using LotsOfTowers.Framework;
using SmartLocalization;

namespace LotsOfTowers
{
	// This class is in the root namespace on purpose!
	// Do not change
	public static class Extensions
	{

		static Extensions() {
			GameManager.Instance.Language = GameManager.Instance.Language;
		}

		// Usage:
		// string s = "tooltips.jump.title";
		// s.Localize();
		public static string Localize(this string str)
		{
			string localized = LanguageManager.Instance.GetTextValue(str);
			return localized == null ? str : localized;
		}
	}
}