namespace HtmxProject.Application.Base
{
	public class NameValueDto<TValue> where TValue : struct
	{
		public string Name { get; set; } = "";
		public TValue Value { get; set; }
	}
}