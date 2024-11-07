using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace webapidemo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		static List<string> items = new List<string> { "value1", "value2" };
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return items;
		}

		[HttpGet("{id}")]
		// [Route("{id}")]
		public string GetByID(int id)
		{
			return items[id];
		}
		[HttpGet("Show")]
		public string GetShow()
		{
			return "Welcome in apiii";
		}

		[HttpGet("ShowNumber")]
		public int GetNumber(int num)
		{
			return num;
		}

	}
}
