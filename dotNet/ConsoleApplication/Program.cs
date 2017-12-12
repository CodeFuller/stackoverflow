using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApplication.Dal;

namespace ConsoleApplication
{
	public class QueueItem
	{
		public string SessionName { get; }

		public Func<string> Func => () => SessionName;

		public QueueItem(string sessionName)
		{
			SessionName = sessionName;
		}
	}

	class Program
	{
		public static Queue<QueueItem> myQ = new Queue<QueueItem>();

		static void Main(string[] args)
		{
			myQ.Enqueue(new QueueItem("First"));
			myQ.Enqueue(new QueueItem("Second"));
			myQ.Enqueue(new QueueItem("Thrid"));
			var queueList = myQ.ToList();
			int index = queueList.FindIndex(x => x.SessionName == "First");
			var result = queueList[index].Func();

			Console.WriteLine(index);
			Console.ReadLine();
		}
	}
}
