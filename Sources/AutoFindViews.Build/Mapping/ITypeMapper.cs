namespace AutoFindViews.Build
{
	using System.Xml.Linq;

	public interface ITypeMapper
	{
		string Get(XElement xml);
	}
}
