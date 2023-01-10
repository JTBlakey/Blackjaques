using Network;
using Network.Enums;
using Network.Packets;
using Network.Interfaces;
using IPFormat;
using Network.Extensions;

// MESSAGES
// JOIN - (NAME (STRING), '/', PLAYERS (CHAR))(STRING) <
// EXIT - VOID <
// PROBE - CARDS >
// REPLY - MOVE (CHAR) <

// JOIN
// WHILE TRUE
// {
//      WAIT UNTIL PROBE
//      REPLY
// }
// EXIT

namespace Blackjack2022.Net
{
    internal class Server
    {
        internal class ClientData
        {
            public string name { get; set; }
            public int players { get; set; }

            public Connection connection { get; set; }

            public ClientData(string name, Connection connection, int players)
            {
                this.name = name;
                this.connection = connection;
                this.players = players;
            }
        }

        public static  List<ClientData> clients = new List<ClientData>();

        private ServerConnectionContainer connectionContainer;

        public void Setup()
        {
            connectionContainer = ConnectionFactory.CreateServerConnectionContainer(6674, false);

            connectionContainer.ConnectionEstablished += ConnectionEstablished;

            connectionContainer.Start();
        }

        public void ConnectionEstablished(Connection connection, ConnectionType type)
        {
            Console.WriteLine($"{connection.GetType()} connected on port {connection.IPRemoteEndPoint.Port}");

            connection.RegisterRawDataHandler("JOIN", (rawData, con) => Handlers.Join(rawData, con));
            connection.RegisterRawDataHandler("PROBE_REPLY", (rawData, con) => Handlers.ProbeReply(rawData, con));
            connection.RegisterRawDataHandler("EXIT", (rawData, con) => Handlers.Exit(rawData, con));
        }

        private class Handlers
        {
            public static void Join(RawData data, Connection connection)
            {
                string[] dataString = data.ToUTF8String().Split('/');

                string dataName = dataString[0];
                byte dataPlayers = (byte)dataString[1].ToCharArray()[0]; // packaged as a byte (char)

                clients.Add(new ClientData(dataName, connection, (int)dataPlayers));
            }

            public static void ProbeReply(RawData data, Connection connection)
            {

            }

            public static void Exit(RawData data, Connection connection)
            {
                for (int i = 0; i < clients.Count; i++)
                {
                    if (clients[i].connection == connection)
                    {
                        clients.RemoveAt(i);

                        return;
                    }
                }
            }
        }
    }
}
