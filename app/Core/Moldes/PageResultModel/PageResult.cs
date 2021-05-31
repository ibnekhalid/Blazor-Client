using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Core.Moldes.PageResultModel
{
	public class PageResult<T>
	{
		public List<T> Data { get; set; }
		public long TotalCount { get; set; }
		public PageInfo PageInfo { get; set; }
		
		public PageResult(dynamic? result)
		{
			if (result is not null)
			{
				string str = JsonConvert.SerializeObject(result.Nodes);
				
				this.Data = JsonConvert.DeserializeObject<List<T>>(str);
				TotalCount = (long)result.TotalCount;
				string pinfo = JsonConvert.SerializeObject(result.PageInfo);
				PageInfo = JsonConvert.DeserializeObject<PageInfo>(pinfo);
			
			}
		}
	}
	public class PageInfo
	{
		public string StartCursor { get; set; }
		public string EndCursor { get; set; }
		public bool HasNextPage { get; set; }
		public bool HasPreviousPage { get; set; }
	}
}
