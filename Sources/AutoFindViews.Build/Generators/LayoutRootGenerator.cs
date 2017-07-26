namespace AutoFindViews.Build
{
	using System.IO;
	using Microsoft.Build.Evaluation;

	public class LayoutRootGenerator
	{
		public LayoutRootGenerator(string nspace)
		{
			this.nspace = nspace;
		}

		private string nspace;

		/// <summary>
	 	/// Generates the layout holder class from the axml file.
		/// </summary>
		/// <param name="xml">Axml content.</param>
		public bool Generate(string outputPath)
		{
			const string name = "Layouts.g.cs";
			const string template = "Layouts.Designer.cs";
			var parent = Directory.GetParent(outputPath).ToString();
			var path = Path.Combine(parent, name);

			//Prepare output folder
			if (!Directory.Exists(parent))
			{
				Directory.CreateDirectory(parent);
			}

			if (!File.Exists(path))
			{
				var content = Templates.LoadTemplate(template);
				content = string.Format(content, nspace);
				File.WriteAllText(path, content);
				return true;
			}

			return false;
		}
	}
}
