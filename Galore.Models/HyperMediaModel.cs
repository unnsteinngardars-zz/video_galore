using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;

namespace TecRad.Models
{
	public class HyperMediaModel
	{
		public HyperMediaModel() { Links = new ExpandoObject(); }
		[JsonProperty(PropertyName = "_links")]
		public ExpandoObject Links { get; set; }
	}
}