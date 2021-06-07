using System.Linq;

namespace app.Core.Helpers
{
	public class AvatarStyle
	{
		public static string Get(string characterCodes, string heightPx, string widthPx, string fontRem)
		{
			var hash = characterCodes.Aggregate(0, (current, item) => item + ((current << 5) - current));

			if (heightPx.Length > 0 && widthPx.Length > 0)
				return @$"height:{heightPx}; background-color:hsl({hash % 360},100%,20%) !important; width:{widthPx}; font-size:{fontRem};";
			if (heightPx.Length > 0 && widthPx.Length > 0)
				return @$"background-color:hsl({hash % 360},100%,20%) !important; font-size:{fontRem};";
			return @$"background-color:hsl({hash % 360},100%,20%) !important;";

		}
		public static string GetBorderLeftStyle(string characterCodes)
		{
			var hash = characterCodes.Aggregate(0, (current, item) => ((int) item) + ((current << 5) - current));

			return @$"border-bottom: 5px solid hsl({hash % 360},100%,20%) !important;";

		}

	}

	
}
