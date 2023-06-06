using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApi.Services;

namespace TestApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NameController : ControllerBase
	{
		private readonly IQueueService queue;
		private readonly IChannelQueue<string> channel;

		public NameController(IQueueService queue, IChannelQueue<string> channel)
		{
			this.queue = queue;
			this.channel = channel;
		}

		[HttpPost]
		public async Task<IActionResult> Add(string[] names)
		{
			foreach (var item in names)
				await channel.Enqueue(item);
			return Ok();
		}
	}
}
