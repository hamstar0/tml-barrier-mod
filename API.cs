﻿using Terraria;


namespace Barriers {
	public static partial class BarriersAPI {
		public static BarriersConfigData GetModSettings() {
			return BarriersMod.Instance.Config;
		}

		public static void SaveModSettingsChanges() {
			BarriersMod.Instance.ConfigJson.SaveFile();
		}
	}
}
