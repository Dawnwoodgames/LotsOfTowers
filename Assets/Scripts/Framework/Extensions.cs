using LotsOfTowers.Framework;
using SmartLocalization;

namespace LotsOfTowers
{
	// This class is in the root namespace on purpose!
	// Do not change
	public static class Extensions
	{
		// Usage:
		// string s = "tooltips.jump.title";
		// s.Localize();
		public static string Localize(this string str)
		{
			return LanguageManager.Instance.GetTextValue(str);
		}
	}
}