using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ConsoleApplication
{
	public class BaseTCPSocketListener : IInputListener
	{
		#region Events/Properties

		public event EventHandler<Exception> OnError;

		public event EventHandler<string> OnDataReceived;

		private string host;

		private int port;

		private int delayToClearBufferSeconds = 5;

		private TcpClient client;

		/// <summary>
		/// Will accumulate data as it's received
		/// </summary>
		private string DataBuffer { get; set; }

		/// <summary>
		/// Store time of last data receipt. Need this in order to purge data after delay
		/// </summary>
		private DateTime LastDataReceivedOn { get; set; }

		#endregion

		public BaseTCPSocketListener()
		{
			// Preset all entries
			this.LastDataReceivedOn = DateTime.UtcNow;
			this.DataBuffer = string.Empty;

		}

		public void Init(string config)
		{
			// Parse info
			var bits = config.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
			this.host = bits[0];
			var hostBytes = this.host.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
			var hostIp = new IPAddress(new[] { byte.Parse(hostBytes[0]), byte.Parse(hostBytes[1]), byte.Parse(hostBytes[2]), byte.Parse(hostBytes[3]) });
			this.port = int.Parse(bits[1]);
			this.delayToClearBufferSeconds = int.Parse(bits[2]);

			// Close open client
			if (this.client?.Client != null)
			{
				//this.client?.Close();
				this.client.Client.Disconnect(true);
				//this.client.Dispose();
				this.client = null;
			}

			// Connect to client
			this.client = new TcpClient();
			if (!this.client.ConnectAsync(hostIp, this.port).Wait(2500))
				throw new Exception($"Failed to connect to {this.host}:{this.port} in allotted time");

			this.EstablishReceiver();
		}

		protected void DataReceived(IAsyncResult result)
		{
			var state = (StateObject) result.AsyncState;

			// End the data receiving that the socket has done and get the number of bytes read.
			var bytesCount = 0;

			try
			{
				SocketError errorCode;
				bytesCount = state.Socket.EndReceive(result, out errorCode);
				if (errorCode != SocketError.Success)
				{
					bytesCount = 0;
				}
			}
			catch (Exception ex)
			{
				this.RaiseOnErrorToClient(new Exception(nameof(this.DataReceived)));
				this.RaiseOnErrorToClient(ex);
			}

			if (bytesCount > 0)
			{
				// Convert the data we have to a string.
				this.DataBuffer += Encoding.UTF8.GetString(state.Buffer, 0, bytesCount);

				// Record last time data received
				this.LastDataReceivedOn = DateTime.UtcNow;
				this.RaiseOnDataReceivedToClient(this.DataBuffer);

				this.DataBuffer = string.Empty;
				this.EstablishReceiver();
			}
		}

		private void EstablishReceiver()
		{
			try
			{
				var state = new StateObject(client.Client);
				// Set up again to get the next chunk of data.
				this.client.Client.BeginReceive(state.Buffer, 0, state.Buffer.Length, SocketFlags.None, this.DataReceived, state);
			}
			catch (Exception ex)
			{
				this.RaiseOnErrorToClient(new Exception(nameof(this.EstablishReceiver)));
				this.RaiseOnErrorToClient(ex);
			}
		}

		private void RaiseOnErrorToClient(Exception ex)
		{
			if (this.OnError == null) return;

			foreach (Delegate d in this.OnError.GetInvocationList())
			{
				var syncer = d.Target as ISynchronizeInvoke;
				if (syncer == null)
				{
					d.DynamicInvoke(this, ex);
				}
				else
				{
					syncer.BeginInvoke(d, new object[] { this, ex });
				}
			}
		}

		private void RaiseOnDataReceivedToClient(string data)
		{
			if (this.OnDataReceived == null) return;

			foreach (Delegate d in this.OnDataReceived.GetInvocationList())
			{
				var syncer = d.Target as ISynchronizeInvoke;
				if (syncer == null)
				{
					d.DynamicInvoke(this, data);
				}
				else
				{
					syncer.BeginInvoke(d, new object[] { this, data });
				}
			}
		}
	}
}
