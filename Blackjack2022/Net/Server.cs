using Network;
using Network.Enums;

// MESSAGES
// JOIN - (NAME (CHAR), '/', PLAYERS (CHAR))(STRING) <
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

            public Connection Connection { get; set; }

            public ClientData(string name, Connection connection)
            {
                this.name = name;
                Connection = connection;
            }
        }

        public List<ClientData> clients = new List<ClientData>();

        private ServerConnectionContainer connectionContainer;

        public void Setup()
        {
            connectionContainer = ConnectionFactory.CreateServerConnectionContainer(6674, false);

            connectionContainer.ConnectionEstablished += ConnectionEstablished;

            connectionContainer.Start();
        }

        public void ConnectionEstablished(Connection connection, ConnectionType type)
        {
            
        }
    }
}
