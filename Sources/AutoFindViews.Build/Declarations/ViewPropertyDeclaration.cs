namespace AutoFindViews.Build
{
	using System;
	using System.Collections.Generic;
	using System.Xml;
	using System.Xml.Linq;

	public class ViewPropertyDeclaration
	{
		public ViewPropertyDeclaration(string type, string id, string source, string line, bool isInclude)
		{
			this.Id = id;
			this.Type = type;
			this.Source = source;
			this.Line = line;
			this.IsInclude = isInclude;
		}

		public string Type { get; }

		public string Id { get; }

		public string Line { get; }

		public string Source { get; }

		public bool IsInclude { get; }

		#region Constants

		private static readonly XNamespace AndroidNamespace = "http://schemas.android.com/apk/res/android";

		private const string IdPrefix = "@+id/";

		#endregion

		/// <summary>
		/// Searches for all identifier declarations into the given axml.
		/// </summary>
		/// <returns>The declarations.</returns>
		/// <param name="element">Element.</param>
		public static ViewPropertyDeclaration[] ParseDeclarations(XElement element, ITypeMapper mapper)
		{
			var result = new List<ViewPropertyDeclaration>();

			var idatt = element.Attribute(AndroidNamespace + "id");

			if(idatt != null && idatt.Value.StartsWith(IdPrefix, StringComparison.Ordinal))
			{
				var isInclude = element.Name.LocalName == "include";
				var id = idatt.Value.Substring(IdPrefix.Length);

				string type = null;

				if(isInclude)
				{
					var layoutatt = element.Attribute("layout");
					type = LayoutHolderGenerator.CreateClassName(layoutatt.Value.Replace("@layout/", ""));
				}
				else
				{
					type = mapper.Get(element);
				}

				var source = idatt.Value;
				var lineinfo = element as IXmlLineInfo;
				var line = lineinfo?.LineNumber.ToString() ?? "?";
				result.Add(new ViewPropertyDeclaration(type, id, source, line, isInclude));
			}

			foreach (var child in element.Elements())
			{
				var childDeclarations = ParseDeclarations(child, mapper);
				result.AddRange(childDeclarations);
			}

			return result.ToArray();
		}
	}
}
