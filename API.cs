using Barriers.Entities.Barrier.PlayerBarrier;
using Microsoft.Xna.Framework;
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

		public static void CreateFriendlyBarrier( Player player, Vector2 position, int power, float hpScale, float radiusScale, float defenseScale, float shrinkResistScale, float regenScale ) {
			PlayerBarrierEntity.CreatePlayerBarrierEntity( player, power, hpScale, radiusScale, defenseScale, shrinkResistScale, regenScale, position );
		}
	}
}
