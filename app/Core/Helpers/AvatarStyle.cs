using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Core.Helpers
{
	public class AvatarStyle
	{
		public static string Get(string characterCodes, string heightPx, string widthPx, string fontRem)
		{
			var hash = 0;
			for (var i = 0; i < characterCodes.Length; i++)
			{
				hash = ((int)characterCodes[i]) + ((hash << 5) - hash);
			}
			if (heightPx.Length > 0 && widthPx.Length > 0)
				return @$"height:{heightPx}; background-color:hsl({hash % 360},100%,20%) !important; width:{widthPx}; font-size:{fontRem};";
			if (heightPx.Length > 0 && widthPx.Length > 0)
				return @$"background-color:hsl({hash % 360},100%,20%) !important; font-size:{fontRem};";
			return @$"background-color:hsl({hash % 360},100%,20%) !important;";

		}
		public static string GetBorderLeftStyle(string characterCodes)
		{
			var hash = 0;
			for (var i = 0; i < characterCodes.Length; i++)
			{
				hash = ((int)characterCodes[i]) + ((hash << 5) - hash);
			}
			
			return @$"border-bottom: 5px solid hsl({hash % 360},100%,20%) !important;";

		}

	}

	
}
