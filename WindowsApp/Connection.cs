using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using System.Net.Sockets;
using Windows.Storage.Streams;
using System.Net;

namespace WindowsApp
{
    public class Connection
    {
        private StreamSocket streamsocket;

        public Connection()
        {
            streamsocket = new StreamSocket();
        }

        async public void Connect()
        {                        
                HostName serverHost = new HostName("192.168.4.1");              
                string serverPort = "80";
                await streamsocket.ConnectAsync(serverHost, serverPort);

                IPAddress localAddr = IPAddress.Parse("192.168.4.1");
                //TcpListener server = new TcpListener(localAddr, 9595);


        }

        async public void SendData(string mes)
        {
            Stream streamOut = streamsocket.OutputStream.AsStreamForWrite();
            StreamWriter writer = new StreamWriter(streamOut);
            string request = mes;
            await writer.WriteLineAsync(request);
            await writer.FlushAsync();
        }

        async public void SendChar(char[] mes)
        {
            Stream streamOut = streamsocket.OutputStream.AsStreamForWrite();
            StreamWriter writer = new StreamWriter(streamOut);
            //string request = mes;
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
