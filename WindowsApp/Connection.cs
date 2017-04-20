using System;
using System.Net;


using System.Diagnostics;
using System.IO;

using Windows.Networking;
using Windows.Networking.Sockets;
using System.Net.Sockets;
using System.Text;

namespace WindowsApp
{
    public class Connection
    {
        private StreamSocket streamsocket;
        private TcpClient client;

        public Connection()
        {
            streamsocket = new StreamSocket();
        }

        async public void Connect()
        {
            HostName serverHost = new HostName("192.168.4.1");
            string serverPort = "80";
            await streamsocket.ConnectAsync(serverHost, serverPort);

            client = new TcpClient();
            await client.ConnectAsync("192.168.4.1", 8888);
            //NetworkStream stream = client.GetStream();

        }

        public void SendData(string mes)
        {
            //Stream streamOut = streamsocket.OutputStream.AsStreamForWrite();
            //StreamWriter writer = new StreamWriter(streamOut);
            //string request = mes;
            //await writer.WriteLineAsync(request);
            //await writer.FlushAsync();
            NetworkStream stream = client.GetStream();
            byte[] data = Encoding.ASCII.GetBytes(mes);
            stream.Write(data, 0, data.Length);
            stream.Flush();


        }

        async public void SendChar(char[] mes)
        {
            Stream streamOut = streamsocket.OutputStream.AsStreamForWrite();
            StreamWriter writer = new StreamWriter(streamOut);
            await writer.WriteLineAsync(mes);
            await writer.FlushAsync();
        }

        public string ReadData()
        {
            Stream streamIn = streamsocket.InputStream.AsStreamForRead();
            StreamSocketListener streamSocketListner = new StreamSocketListener();
            StreamReader reader = new StreamReader(streamIn);
            string response = reader.ReadLine();
            return response;
        }

        public byte[] ReadBytes()
        {
            Stream streamIn = streamsocket.InputStream.AsStreamForRead();

            byte[] buffer = new byte[1024];

            int count = streamIn.Read(buffer, 0, buffer.Length);
            Debug.WriteLine(count);

            MemoryStream memoryStream = new MemoryStream(buffer);

            byte[] data = new byte[count];
            memoryStream.Read(data, 0, count);
            string response = System.Text.Encoding.UTF8.GetString(data);

            Debug.WriteLine(response);

            return data;
        }


        public void Disconnect()
        {
            streamsocket.Dispose();
        }


        public void setStreamSocket(StreamSocket ss)
        {
            streamsocket = ss;
        }


        public StreamSocket getStreamSocket()
        {
            return streamsocket;
        }




    }
}
