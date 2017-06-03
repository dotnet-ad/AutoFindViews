namespace AutoFindViews.Build
{
	using System;
	using Microsoft.Build.Evaluation;
	using System.Linq;

	public static class ProjectExtensions
	{
		/// <summary>
		/// Adds an item to the project if not already present.
		/// </summary>
		/// <param name="type">Type of item.</param>
		/// <param name="absolutePath">Absolute path of the item.</param>
		public static bool AddItemIfNotExists(this Project project, string type, string absolutePath)
		{
			var projectPath = new Uri(project.FullPath);
			var filePath = new Uri(absolutePath);
			var diff = projectPath.MakeRelativeUri(filePath);
			var path = diff.OriginalString.Replace('/', '\\');

			if (!project.Items.Any(x => x.ItemType == type && x.UnevaluatedInclude.Replace('/', '\\') == path))
			{
				project.AddItem(type, path);
				return true;
			}

			return false;
		}
	}
}
