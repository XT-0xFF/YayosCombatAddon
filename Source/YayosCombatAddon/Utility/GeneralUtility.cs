﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;

namespace YayosCombatAddon
{
	public static class GeneralUtility
	{
		public static void ShowRejectMessage(Thing target, string text) =>
			Messages.Message(text, new LookTargets(target), MessageTypeDefOf.RejectInput, historical: false);


		public static bool AnyReservableReachableThing(this ThingDef def, Pawn pawn, int minAmmoNeeded)
		{
			// check if any thing of the desire def is available
			var listsByDef = pawn?.Position.GetRegion(pawn.Map)?.ListerThings?.listsByDef;
			if (listsByDef?.ContainsKey(def) == true)
			{
				// check if any thing found can be reserved and reached
				foreach (var thing in listsByDef[def])
				{
					if (ReservationUtility.CanReserveAndReach(pawn, thing, PathEndMode.ClosestTouch, pawn.NormalMaxDanger(), stackCount: minAmmoNeeded))
						return true;
				}
			}
			return false;
		}


		public static void IncreaseOrAdd<T>(this Dictionary<T, int> dictionary, T t, int count)
		{
			if (dictionary.ContainsKey(t))
				dictionary[t] += count;
			else
				dictionary.Add(t, count);
		}
		public static void DecreaseOrRemove<T>(this Dictionary<T, int> dictionary, T t, int count)
		{
			if (dictionary.ContainsKey(t))
			{
				var value = dictionary[t] - count;
				if (value > 0)
					dictionary[t] = value;
				else
					dictionary.Remove(t);
			}
		}
	}
}