using TestApi.Services;

namespace TestApi;

public class HostedService : BackgroundService
{
	private readonly ILogger<HostedService> logger;
	private readonly IQueueService queue;
	private readonly IChannelQueue<string> channelQueue;
	public HostedService(ILogger<HostedService> logger, IQueueService queue, IChannelQueue<string> channelQueue)
	{
		this.logger = logger;
		this.queue = queue;
		this.channelQueue = channelQueue;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			var name = await channelQueue.Dequeue(stoppingToken);
			
			await Task.Delay(1000); 

			logger.LogInformation("--- execute name is " + name+" ---");
		}
	}
}

