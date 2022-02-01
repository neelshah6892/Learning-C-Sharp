using System.Net.Sockets;
using System.Text;

namespace TCPClient
{
    class Program
    {
        static void Main(String[] args)
        {
            TcpClient clientSocket = new TcpClient();
            clientSocket.Connect("localhost", 5000);
            Console.WriteLine(clientSocket.Connected);

            var stream = clientSocket.GetStream();

            clientSocket.SendTimeout = 30;
            clientSocket.ReceiveTimeout = 30;

            var txt = Encoding.ASCII.GetBytes("\ntest");

            stream.Write(txt, 0, txt.Length);

            byte[] bt = new byte[1024];

            int bytesread = clientSocket.Client.Receive(bt);
            string result = Encoding.ASCII.GetString(bt, 0, bytesread);
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}