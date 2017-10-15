namespace AutoFindViews.Build
{
	using System.IO;

	public static class Templates
	{
		/// <summary>
		/// Loads an embedded template from its name (without ".template" extension);
		/// </summary>
		/// <returns>The template content.</returns>
		/// <param name="name">Template name.</param>
		public static string LoadTemplate(string name)
		{
			var assembly = typeof(AutoFindViewsTask).Assembly;
			var templatePath = $"{assembly.GetName().Name}.Generators.Templates.{name.Replace(".", "_")}.template";
			using (var stream = assembly.GetManifestResourceStream(templatePath))
			using (var reader = new StreamReader(stream))
			{
				return reader.ReadToEnd();
			}
		}
	}
}
