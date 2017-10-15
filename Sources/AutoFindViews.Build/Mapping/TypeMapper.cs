namespace AutoFindViews.Build
{
	using System.Collections.Generic;
	using System.Xml.Linq;
	using System.Linq;

	public class TypeMapper : ITypeMapper
	{
		public TypeMapper(Dictionary<string, string> custom)
		{
			this.customMapping = custom;
		}

		#region Constants

		private const string DefaultTypeNamespace = "Android.Widget";

		public static readonly Dictionary<string, string> DefaultMapping = new Dictionary<string, string>()
		{
			{ "View", "Android.Views.View" },
		};

		#endregion

		#region Fields

		private Dictionary<string, string> customMapping; 

		#endregion

		public string Get(XElement xml)
		{
			var name = xml.Name.LocalName;

			if (customMapping.TryGetValue(name, out string customResult))
				return customResult;

			if (DefaultMapping.TryGetValue(name, out string defaultResult))
				return defaultResult;

			var splits = name.Split('.');

			if (splits.Length == 1)
				return $"{DefaultTypeNamespace}.{name}";

			return string.Join(".", splits.Select(x => x.FirstLetterToUpper()));
		}
	}
}
