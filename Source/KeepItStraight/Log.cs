﻿/*
	This file is part of KeepItStraight
		© 2024 LisiasT : http://lisias.net <support@lisias.net>
		© 2018-2024 Maja
		© 2016-2018 RealGecko

	THIE FILE is licensed to you under:

	* WTFPL - http://www.wtfpl.net
		* Everyone is permitted to copy and distribute verbatim or modified
			copies of this license document, and changing it is allowed as long
			as the name is changed.

	THIE FILE is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
*/
using System;

namespace KeepItStraight
{
	public static class Log
	{
		internal static void force(string msg, params object[] @params)
		{
			UnityEngine.Debug.LogFormat("[KeepItStraight] " + msg, @params);
		}
	}
}
