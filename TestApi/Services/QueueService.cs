namespace TestApi.Services;

	public interface IQueueService
	{
	 void Enqueue(string item);
	string Dequeue();
	public bool HasNext();
	}

	public class QueueService :IQueueService
	{

	private Queue<string> queue=new();

	public void Enqueue(string item) =>queue.Enqueue(item);
	public string Dequeue() => queue.Dequeue();
	public bool HasNext() => queue.Count > 0;
	

	}

    
