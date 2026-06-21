using System.Collections.Generic;
using HarmonyLib;

namespace ViFix.HarmonyPatches
{
	[HarmonyPatch(typeof(XRL.UI.Popup))]
	class PopupPatch {
		[HarmonyPrefix]
		[HarmonyPatch("PickOption")]
		static void Prefix(ref IReadOnlyList<string> Options, ref IReadOnlyList<char> Hotkeys, ref string PopupID) {
			var hotkeyResolver = new HotkeyResolver();
			Hotkeys = hotkeyResolver.resolve(Hotkeys);
		}
	}
}
