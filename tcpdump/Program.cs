using System;
using System.Net.Sockets;

namespace tcpdump
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient tcpClient = new TcpClient();

            tcpClient.Connect("localhost", 80);

            var stream = tcpClient.GetStream();

            var buffer = new byte[2048];

            var responseSize = stream.Read(buffer);

            var responseData = System.Text.Encoding.ASCII.GetString(buffer, 0, responseSize);

            Console.WriteLine(responseData);
            Console.ReadKey();
        }
    }
}