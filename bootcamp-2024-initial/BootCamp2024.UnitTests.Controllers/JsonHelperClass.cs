using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootCamp2024.UnitTests.Controllers
{
	public static class JsonHelperClass
	{
		public static Dictionary<string, string> JsonSerializeAndDeserialize(object? responseValue)
		{
			var jsonResponse = JsonConvert.SerializeObject(responseValue);
			var deserializedResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResponse);

			return deserializedResponse != null ? deserializedResponse : new Dictionary<string, string>();
		}
	}
}
