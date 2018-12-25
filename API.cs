﻿using Barriers.Entities.Barrier.NpcBarrier;
using Barriers.Entities.Barrier.PlayerBarrier;
using HamstarHelpers.Helpers.DotNetHelpers;
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

		public static PlayerBarrierEntity CreatePlayerBarrier( Player player, Vector2 position, int power, float hpScale, float radiusScale, float defenseScale, float shrinkResistScale, float regenScale ) {
			return PlayerBarrierEntity.CreatePlayerBarrierEntity( player, power, hpScale, radiusScale, defenseScale, shrinkResistScale, regenScale, position );
		}

		public static NpcBarrierEntity CreateNpcBarrier( [Nullable]NPC npc, Vector2 position, float hp, float radius, int defense, float shrinkResistScale, float regenScale ) {
			return NpcBarrierEntity.CreateNpcBarrierEntity( npc, position, hp, radius, defense, shrinkResistScale, regenScale );
		}
	}
}
