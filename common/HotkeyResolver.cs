using System.Collections.Generic;
using System.Linq;

using XRL.UI;

public class HotkeyResolver {
	private Dictionary<char, int> HotkeyMap = new Dictionary<char, int>();
	private HashSet<char> HotkeyConflicts = new HashSet<char>();

	public HotkeyResolver() {
		var conflictingIDs = new List<string> { "UI:Navigate/up", "UI:Navigate/down", "UI:Navigate/left", "UI:Navigate/right" };

		foreach (var id in conflictingIDs) {
			foreach (var bind in CommandBindingManager.CommandsByID[id].keyboardBindings) {
				if (bind.Key.Length == 1 && bind.Key[0] >= 'a' && bind.Key[0] <= 'z') HotkeyConflicts.Add(bind.Key[0]);
			}
		}
	}

	public List<char> resolve(IReadOnlyList<char> Hotkeys) {
		for (int i = 0; i < Hotkeys.Count; i++) {
			if (Hotkeys[i] != ' ') {
				HotkeyMap.Add(Hotkeys[i], i);
			}
		}

		foreach (var conflict in HotkeyConflicts.OrderByDescending(c => c)) {
			if (!HotkeyMap.ContainsKey(conflict)) continue;
			shiftDown(conflict, HotkeyMap.GetValueOrDefault(conflict));
			HotkeyMap.Remove(conflict);
		}

		var fixed_hotkeys = new List<char>(Enumerable.Repeat(' ', Hotkeys.Count));

		foreach (var entry in HotkeyMap) {
			fixed_hotkeys[entry.Value] = entry.Key;
		}

		return fixed_hotkeys;
	}

	private void shiftDown(char Hotkey, int Idx) {
		while (HotkeyConflicts.Contains(++Hotkey)) {}

		if (Hotkey > 'z') return;

		int idx_next;

		bool done = false;

		if (!HotkeyMap.TryGetValue(Hotkey, out idx_next)) done = true;

		HotkeyMap[Hotkey] = Idx;

		if (done) return;

		shiftDown(Hotkey, idx_next);
	}
}
