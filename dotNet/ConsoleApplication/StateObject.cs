using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
	public class StateObject
	{
		public Socket Socket { get; set; }

		public byte[] Buffer { get; } = new byte[1024];

		public StateObject(Socket socket)
		{
			Socket = socket;
		}
	}
}
