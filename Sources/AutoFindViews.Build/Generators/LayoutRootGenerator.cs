namespace AutoFindViews.Build
{
	using System.IO;
	using Microsoft.Build.Evaluation;

	public class LayoutRootGenerator
	{
		public LayoutRootGenerator(Project project, string nspace)
		{
			this.project = project;
			this.nspace = nspace;
		}

		private string nspace;

		private Project project;

		/// <summary>
	 	/// Generates the layout holder class from the axml file.
		/// </summary>
		/// <param name="xml">Axml content.</param>
		public bool Generate(string outputPath)
		{
			const string name = "Layouts.Designer.cs";
			var parent = Directory.GetParent(outputPath).ToString();
			var path = Path.Combine(parent, name);

			//Prepare output folder
			if (!Directory.Exists(parent))
			{
				Directory.CreateDirectory(parent);
			}

			var projectNeedsSave = this.project.AddItemIfNotExists("Folder", parent);

			if (!File.Exists(path))
			{
				var content = Templates.LoadTemplate(name);
				content = string.Format(content, nspace);
				File.WriteAllText(path, content);
			}

			projectNeedsSave |= this.project.AddItemIfNotExists("Compile", path);

			return projectNeedsSave;
		}
	}
}
