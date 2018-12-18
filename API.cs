using Terraria;


namespace Barriers {
	public static partial class BarriersAPI {
		public static BarriersConfigData GetModSettings() {
			return BarriersMod.Instance.Config;
		}

		public static void SaveModSettingsChanges() {
			BarriersMod.Instance.ConfigJson.SaveFile();
		}

		////////////////

		//public static void CreateFriendlyBarrier( Player player, int maxRadius, int maxHp, float regenRatePerSecond, int defense, float shrinkResistScale ) {
		//	BarrierEntity.CreateBarrierEntity()
		//}
	}
}
