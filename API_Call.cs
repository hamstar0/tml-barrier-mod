using System;


namespace Barriers {
	public static partial class BarriersAPI {
		internal static object Call( string callType, params object[] args ) {
			switch( callType ) {
			case "GetModSettings":
				return BarriersAPI.GetModSettings();
			case "SaveModSettingsChanges":
				BarriersAPI.SaveModSettingsChanges();
				return null;
			}

			throw new Exception( "No such api call " + callType );
		}
	}
}
