using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace FoundersPC.Web
{
	public class GlobalContext
	{
		public static readonly HttpClient WeClient = new();

		static GlobalContext()
		{
			WeClient.BaseAddress = new Uri("https://localhost:5001/api/");
			WeClient.DefaultRequestHeaders.Clear();
			WeClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			WeClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
		}
	}
}