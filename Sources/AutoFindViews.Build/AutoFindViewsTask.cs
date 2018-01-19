namespace AutoFindViews.Build
{
	using System;
	using Microsoft.Build.Framework;
	using Microsoft.Build.Utilities;
	using System.IO;
	using System.Linq;
	using System.Xml;
	using Microsoft.Build.Evaluation;
	using Newtonsoft.Json;

	public class AutoFindViewsTask : Task
	{
		#region Properties

		[Required]
		public string Namespace { get; set; }

		[Required]
		public string ProjectPath { get; set; }

		[Required]
		public ITaskItem Source { get; set; }

		[Output]
		public string OutputFile { get; set; }

		#endregion

		#region Fields

		private Configuration configuration;

		#endregion

		/// <summary>
		/// Reads configuration (custom mappings, ...) from the optional file placed at the root of the referencing project.
		/// </summary>
		private void ReadConfig()
		{
			var projectFolder = Path.GetDirectoryName(this.ProjectPath);
			var path = Path.Combine(projectFolder, "AutoFindViews.config");
			this.configuration = Configuration.Load(path);
			Log.LogMessage($"Custom mapping : {string.Join(", ", this.configuration.Mapping.Select(x => $"{x.Key}={x.Value}"))}");
		}

		public override bool Execute()
		{
			if (!File.Exists(this.Source.ItemSpec))
			{
				Log.LogError(null, null, null, Source, 0, 0, 0, 0, $"file {this.Source} not found");
				return false;
			}

			try
			{
				this.ReadConfig();

				Log.LogMessage($"Loading xml : {this.Source.ItemSpec}");

				var mapper = new TypeMapper(this.configuration.Mapping);

				var rootGen = new LayoutRootGenerator(Namespace);
				var layoutGen = new LayoutHolderGenerator(mapper, Namespace);

				rootGen.Generate(this.OutputFile);
				layoutGen.Generate(this.Source.ItemSpec, this.OutputFile);
				return true;
			}

			catch (XmlException xe)
			{
				Log.LogMessage("Error (xml): {0}", xe.Message);
				Log.LogError(null, null, null, Source, xe.LineNumber, xe.LinePosition, 0, 0, $"{xe.Message}");

				return false;
			}
			catch (Exception e)
			{
				Log.LogMessage("Error: {0}", e.Message);
				Log.LogError(null, null, null, Source, 0, 0, 0, 0, $"{e.Message}");
				return false;
			}
		}
	}
}
