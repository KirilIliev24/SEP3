using Data.DatabaseModels;
using System;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Data.DataAccess;

namespace Data
{
    class Program
    {
        static void Main(string[] args)
        {
            DataModel model = new DataModel();
            FunctionMapper mapper = new FunctionMapper();
            var ip = new byte[] { 127, 0, 0, 1 };
            TcpListener server = new TcpListener(new IPAddress(ip), 8888);
            server.Start();
            Byte[] send = new byte[50000];
            Byte[] receive = new byte[5000];
            string jsonString = "";

            Console.WriteLine("Waiting for connection...");
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Connection!...");

                NetworkStream stream = client.GetStream();
                stream.Read(receive, 0, receive.Length);
                string received = Encoding.ASCII.GetString(receive);
                Console.WriteLine(received);
                jsonObject jsono = JsonConvert.DeserializeObject<jsonObject>(received);
                jsonString = mapper.Map(jsono);
                
                receive = new byte[5000];
                received = "";

                send = Encoding.UTF8.GetBytes(jsonString);
                stream.Write(send, 0, send.Length);
            }
        }
    }
}
