using LotsOfTowers.Framework;
using SmartLocalization;
using UnityEngine;

namespace LotsOfTowers
{
	// This class is in the root namespace on purpose!
	// Do not change
	public static class Extensions
	{

		static Extensions() {
			GameManager.Instance.Language = GameManager.Instance.Language;
		}

		/// <summary>
        /// Localizes a string, ex: string.to.localize.Localize()
        /// </summary>
        /// <param name="str">string to localize</param>
        /// <returns>localized string</returns>
		public static string Localize(this string str)
		{
			string localized = LanguageManager.Instance.GetTextValue(str);
			return localized == null || localized == "" ? str : localized;
		}
	}
}