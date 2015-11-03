﻿using System;
using System.Linq;

namespace LotsOfTowers.Framework
{
	//Extension methods must be defined in a static class
	public static class StringExtension
	{
		// This is the extension method.
		// The first parameter takes the "this" modifier
		// and specifies the type for which the method is defined.
		public static string localize(this string str){
			return str;
		}
	}
}