using System.Collections.Generic;
using HarmonyLib;

namespace ViFix.HarmonyPatches
{
	[HarmonyPatch(typeof(XRL.UI.Popup))]
	class PopupPatch {
		[HarmonyPrefix]
		[HarmonyPatch("PickOption")]
		static void Prefix(ref IReadOnlyList<char> Hotkeys) {
			if (Hotkeys is not null) {
				var hotkeyResolver = new HotkeyResolver();
				Hotkeys = hotkeyResolver.resolve(Hotkeys);
			}
		}
	}
}
