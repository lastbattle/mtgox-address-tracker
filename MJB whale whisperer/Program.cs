using MJB_whale_whisperer.Constants;
using MJB_whale_whisperer.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;

namespace MJB_whale_whisperer
{
    class Program
    {
        // Telegram
        private static TelegramBotClient telegramClient;

        // Websocket
        private static ClientWebsocketHelper wsHelper; 

        private static System.Threading.Timer WebsocketTimerTask = null;
        private const int WEB_SOCKET_TASK_SECONDS = 60;

        // Backlogs
        private static List<string> ChatBacklogs = new List<string>();

        private static System.Threading.Timer ChatbacklogTimerTask = null;
        private const int CHAT_BACKLOG_TASK_SECONDS = 120;

        static void Main(string[] args)
        {
            // Set console log output
            ConsoleOutputFileStream cfs = new ConsoleOutputFileStream(Console.Out, "Logs.txt");
            Console.SetOut(cfs);

            // Init blockchain.info
            Initialize();

            while (true)
            {
                Console.ReadLine();
            }
        }

        private static async void Initialize()
        {            
            // Telegram
            Console.WriteLine("Initializing Telegram");
            if (telegramClient != null)
            {
                telegramClient = null;
            }
            telegramClient = new TelegramBotClient(APIKeysConstants.TELEGRAM_BOT_API_KEY);

            // Execute a timer
            ChatbacklogTimerTask = new System.Threading.Timer(new TimerCallback(ChatbacklogTimeoutTask), null,
                1000L * CHAT_BACKLOG_TASK_SECONDS,
                1000L * CHAT_BACKLOG_TASK_SECONDS);

            // Blockchain.info
            Console.WriteLine("Initializing blockchain.info websocket watcher task.");

            // documentation: https://blockchain.info/api/api_websocket
            wsHelper = new ClientWebsocketHelper("wss://ws.blockchain.info/inv", true, 
                100); // introduce some delays to save cpu cycles, this could be 0 if needed
            wsHelper.OnPacketReceivedEvent += WsHelper_OnPacketReceivedEvent;
            wsHelper.OnSocketDisconnectedEvent += WsHelper_OnSocketDisconnectedEvent;
            wsHelper.OnSocketConnectedEvent += WsHelper_OnSocketConnectedEvent;
            wsHelper.OnRequestSocketReconnectionOnDisconnection += WsHelper_OnRequestSocketReconnectionOnDisconnection;

            bool connected = await wsHelper.ConnectAsync();

            WebsocketTimerTask = new System.Threading.Timer(new TimerCallback(WebsocketPingTask), null,
                1000L * WEB_SOCKET_TASK_SECONDS,
                1000L * WEB_SOCKET_TASK_SECONDS);
        }


        #region Event Helpers
        /// <summary>
        /// Ping task for websocket to check if the connection is alive.
        /// </summary>
        /// <param name="callback"></param>
        private static async void WebsocketPingTask(object callback)
        {
           // Console.WriteLine("[{0}] Ping task request", DateTime.Now.ToString());

            JObject pingJObject = new JObject();
            pingJObject.Add("op", "ping");


            bool sentSuccessfully = await wsHelper.Send(new UTF8Encoding().GetBytes(pingJObject.ToString()));
        }

        private static async Task<bool> WsHelper_OnRequestSocketReconnectionOnDisconnection()
        {
            try
            {
                return await wsHelper.ConnectAsync();
            }
            catch { }
            return false;
        }

        private async static void WsHelper_OnSocketConnectedEvent()
        {
            Console.WriteLine("[{0}] Websocket connected. Registering bitcoin addresses now: ", DateTime.Now.ToString());

            string[][] AllAddresses = { AddressConstants.MTGOX_TRUSTEE_ADDRESSES, AddressConstants.BITMAIN_BITCOIN_ADDRESSES };
            foreach (string[] addresses in AllAddresses)
            {
                foreach (string trustee_addr in addresses)
                {
                    JObject regJObject = new JObject();
                    regJObject.Add("op", "addr_sub");
                    regJObject.Add("addr", trustee_addr);

                    bool sentSuccessfully = await wsHelper.Send(new UTF8Encoding().GetBytes(regJObject.ToString()));
                }
            }

            Console.WriteLine("[{0}] {1} addresses registered for listening.",
                DateTime.Now.ToString(),
                AddressConstants.MTGOX_TRUSTEE_ADDRESSES.Length + AddressConstants.BITMAIN_BITCOIN_ADDRESSES.Length);
        }

        private static void WsHelper_OnSocketDisconnectedEvent(bool gracefullyExit, Exception exception, int ReconnectingInSeconds)
        {
            Console.WriteLine("[{0} Websocket disconnected. Gracefully exit: {1}, Reconnecting: {2}\r\n{3}",
                DateTime.Now.ToString(),
                gracefullyExit, ReconnectingInSeconds, exception != null ? exception.ToString() : string.Empty);
        }

        private static async void WsHelper_OnPacketReceivedEvent(byte[] data)
        {
            string jsonStr = new UTF8Encoding().GetString(data);

            Debug.WriteLine("Received: " + jsonStr);

            JObject json = JObject.Parse(jsonStr);

            switch ((string)json["op"])
            {
                case "utx":
                    {
                        StringBuilder sb = new StringBuilder();

                        JObject xJObj = (JObject)json["x"];
                        JArray outputAddressJObj = (JArray)xJObj["out"];
                        JArray inputJObject = (JArray)xJObj["inputs"];


                        string[][] AllAddresses = { AddressConstants.MTGOX_TRUSTEE_ADDRESSES, AddressConstants.BITMAIN_BITCOIN_ADDRESSES };
                        string[] AddressSource = { "Mtgox trustee", "Bitmain"};

                        for (int i = 0; i < AllAddresses.Length; i++)
                        {
                            string[] addresses = AllAddresses[i];
                            string addressSource = AddressSource[i];

                            // check inputs to see whether it came from this address, or someone else have sent here.
                            bool isOriginatedFromTrustee = false;
                            foreach (JObject input in inputJObject)
                            {
                                string addr = (string)input["prev_out"]?["addr"];
                                foreach (string trustee_addr in addresses)
                                {
                                    if (addr == trustee_addr)
                                    {
                                        //decimal bitcoinsTransferredIn = Numeric.ConvertSatoshisToBitcoin((long)input["prev_out"]?["value"]);

                                        isOriginatedFromTrustee = true;
                                        break;
                                    }
                                }
                            }

                            if (isOriginatedFromTrustee)
                            {
                                string txHash = (string)xJObj["hash"];

                                sb.Append("New outgoing transaction from "+ addressSource + " trustee detected!!").Append(Environment.NewLine);
                                sb.Append("TX Hash: ").Append(txHash).Append(Environment.NewLine).Append(Environment.NewLine);

                                int z = 1;
                                foreach (JObject output in outputAddressJObj)
                                {
                                    decimal bitcoinsTransferredOut = Numeric.ConvertSatoshisToBitcoin((long)output["value"]);

                                    sb.Append("[").Append(z).Append("]").Append(Environment.NewLine);
                                    sb.Append("    To: ").Append((string)output["addr"]).Append(Environment.NewLine);
                                    sb.Append("    Amount: ").Append(bitcoinsTransferredOut).Append(Environment.NewLine);
                                    sb.Append("    Script: ").Append((string)output["script"]).Append(Environment.NewLine);

                                    sb.Append(Environment.NewLine);
                                    z++;
                                }

                                // Send to telegram 
                                bool sent = await SendTelegramChat(sb.ToString());
                                Console.WriteLine(sb.ToString());
                            }
                        }
                        break;
                    }
            }
        }
        #endregion

        #region Telegram
        /// <summary>
        /// Sends a message to the registered telegram supergroup chat
        /// </summary>
        /// <param name="chatMsg"></param>
        private static async Task<bool> SendTelegramChat(string chatMsg)
        {
            bool error = false;
            bool isChatNotFoundError = false;
            try
            {
                Telegram.Bot.Types.Message msg = await telegramClient.SendTextMessageAsync(APIKeysConstants.TELEGRAM_SUPERGROUP_CHATID, chatMsg);
                return true;
            }
            catch (Exception exp)
            {
                error = true;
                if (exp is Telegram.Bot.Exceptions.ChatNotFoundException)
                {
                    isChatNotFoundError = true;
                }

                Console.WriteLine(exp.ToString());
            }
            if (error && isChatNotFoundError) // ensure that its a connectivity issue, and not related to 'ChatNotFound'
            {
                lock (ChatBacklogs)
                {
                    ChatBacklogs.Add(chatMsg);
                }
            }
            return false;
        }

        /// <summary>
        /// Timer task that runs every 1 minute to send any backlog chats to telegram
        /// </summary>
        /// <param name="callback"></param>
        private static async void ChatbacklogTimeoutTask(object callback)
        {
            if (ChatBacklogs.Count > 0)
            {
                List<string> ChatBacklogsCpy;
                lock (ChatBacklogs)
                {
                    ChatBacklogsCpy = new List<string>(ChatBacklogs);
                    ChatBacklogs.Clear(); // clear original copy first
                }
                foreach (string chat in ChatBacklogsCpy)
                {
                    bool sent = await SendTelegramChat(chat); // if not sent, this will be added back automatically
                }
                ChatBacklogsCpy.Clear();
                ChatBacklogsCpy = null; // help gc a little xD
            }
        }
        #endregion
    }
}
