namespace AutoFindViews.Build
{
	using System.IO;
	using System.Collections.Generic;
	using Newtonsoft.Json;

	public class Configuration
	{
		public static Configuration Load(string path) 
		{
			if(File.Exists(path))
			{
				var content = File.ReadAllText(path);
				return JsonConvert.DeserializeObject<Configuration>(content);
			}

			return new Configuration();
		}

		[JsonProperty("mapping")]
		public Dictionary<string, string> Mapping { get; set; } = new Dictionary<string, string>();
	}
}
