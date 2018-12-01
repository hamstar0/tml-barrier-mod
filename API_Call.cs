using System;


namespace Barriers {
	public static partial class BarriersAPI {
		internal static object Call( string call_type, params object[] args ) {
			switch( call_type ) {
			case "GetModSettings":
				return BarriersAPI.GetModSettings();
			case "SaveModSettingsChanges":
				BarriersAPI.SaveModSettingsChanges();
				return null;
			}

			throw new Exception( "No such api call " + call_type );
		}
	}
}
