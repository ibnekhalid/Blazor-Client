namespace app.Core.Helpers
{
	public class PagingFilter
	{
		public int? TakeFirst { get; set; }
		public int? TakePrevious { get; set; }
		public string? After { get; set; }
		public string? Before { get; set; }
	}
}
