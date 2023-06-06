using System.Threading.Channels;

namespace TestApi.Services
{

  public  interface IChannelQueue<T>
	{
		ValueTask Enqueue(T item);
		ValueTask<T> Dequeue(CancellationToken cancellationToken);

	}

	public class ChannelQueue : IChannelQueue<string>
	{

		private Channel<string> _channel;

		public ChannelQueue(IConfiguration configuration)
		{
			int.TryParse(configuration["QueueCapacity"], out int capacity);

			BoundedChannelOptions op = new BoundedChannelOptions(capacity)
			{
				FullMode = BoundedChannelFullMode.Wait
			};

			_channel = Channel.CreateBounded<string>(op);

		}

		public async ValueTask<string> Dequeue(CancellationToken cancellationToken)
		{
			var item = await _channel.Reader.ReadAsync(cancellationToken);
			return item;
		}

		public async ValueTask  Enqueue(string item)
		{
			ArgumentNullException.ThrowIfNull(item);

			await _channel.Writer.WriteAsync(item);


		}
	}
}
