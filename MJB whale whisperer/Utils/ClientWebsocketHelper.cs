using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MJB_whale_whisperer.Utils
{
    /// <summary>
    /// Event handler when data is received from the server
    /// </summary>
    /// <param name="jsonData"></param>
    /// <returns></returns>
    public delegate void PacketReceivedEvent(byte[] data);

    /// <summary>
    /// Event handler when the socket is disconnected
    /// </summary>
    /// <param name="gracefullyExit"></param>
    /// <param name="exception"></param>
    /// <returns></returns>
    public delegate void SocketDisconnectedEvent(bool gracefullyExit, Exception exception, int ReconnectingInSeconds);

    /// <summary>
    /// Event handler when the socket is connected
    /// </summary>
    /// <param name="gracefullyExit"></param>
    /// <param name="exception"></param>
    /// <returns></returns>
    public delegate void SocketConnectedEvent();

    /// <summary>
    /// Event handler to re-connect socket after it is disconnected for a set amount of time
    /// </summary>
    public delegate Task<bool> RequestSocketReconnectionOnDisconnection();

    /// <summary>
    /// Base web socket class to communicate with the WalletServer
    /// </summary>
    public class ClientWebsocketHelper
    {
        // //////////////////////////////////// Constants ///////////////////////////////////////////
        #region Constants
        /// <summary>
        /// Number of bytes to read at a time from the socket stream
        /// </summary>
        private const int RECEIVE_BLOCK_SIZE = 1500;
        #endregion

        // //////////////////////////////////// Properties ///////////////////////////////////////////
        #region Properties
        private bool _AutoReconnectSocketOnDisconnection = true;
        public bool AutoReconnectSocketOnDisconnection
        {
            get { return _AutoReconnectSocketOnDisconnection; }
            set { this._AutoReconnectSocketOnDisconnection = value; }
        }

        private uint SocketReconnectionTries = 0;


        private int _SocketReceiveMillisDelay = 10;
        public int SocketReceiveMillisDelay
        {
            get { return _SocketReceiveMillisDelay; }
            set { this._SocketReceiveMillisDelay = value; }
        }


        private bool _IsSocketConnectionAlive = false;
        /// <summary>
        /// Property set by WalletClientWebSocket
        /// </summary>
        public bool IsSocketConnectionAlive
        {
            get { return this._IsSocketConnectionAlive; }
            set { this._IsSocketConnectionAlive = value; }
        }
        #endregion

        // //////////////////////////////////// EVENTS ///////////////////////////////////////////
        #region Events
        private PacketReceivedEvent _OnPacketReceivedEvent;
        /// <summary>
        /// Event handler when data is received from the server
        /// </summary>
        public event PacketReceivedEvent OnPacketReceivedEvent
        {
            add { _OnPacketReceivedEvent += value; }
            remove { _OnPacketReceivedEvent -= value; }
        }

        private RequestSocketReconnectionOnDisconnection _OnRequestSocketReconnectionOnDisconnection;
        /// <summary>
        /// Event handler when data is received from the server
        /// </summary>
        public event RequestSocketReconnectionOnDisconnection OnRequestSocketReconnectionOnDisconnection
        {
            add { _OnRequestSocketReconnectionOnDisconnection += value; }
            remove { _OnRequestSocketReconnectionOnDisconnection -= value; }
        }

        private SocketDisconnectedEvent _OnSocketDisconnectedEvent;
        /// <summary>
        /// Event handler when the socket is disconnected
        /// </summary>
        public event SocketDisconnectedEvent OnSocketDisconnectedEvent
        {
            add { _OnSocketDisconnectedEvent += value; }
            remove { _OnSocketDisconnectedEvent -= value; }
        }

        private SocketConnectedEvent _OnSocketConnectedEvent;
        /// <summary>
        /// Event handler when the socket is connected
        /// </summary>
        public event SocketConnectedEvent OnSocketConnectedEvent
        {
            add { _OnSocketConnectedEvent += value; }
            remove { _OnSocketConnectedEvent -= value; }
        }
        #endregion

        // //////////////////////////////////// Data ///////////////////////////////////////////
        private ClientWebSocket _WebSocket = null;
        public ClientWebSocket WebSocket
        {
            get { return _WebSocket; }
            private set { }
        }

        private Mutex WebSocketTerminationMutex = new Mutex();
        private string baseURI;

        /// <summary>
        /// Instantiate a new instance of ClientWebsocketHelper class
        /// </summary>
        /// <param name="AutoReconnectSocketOnDisconnection"></param>
        public ClientWebsocketHelper(string baseURI, bool AutoReconnectSocketOnDisconnection = true, int SocketReceiveMillisDelay = 0)
        {
            this.baseURI = baseURI;
            this.AutoReconnectSocketOnDisconnection = AutoReconnectSocketOnDisconnection;
            this.SocketReceiveMillisDelay = SocketReceiveMillisDelay;
        }


        /// <summary>
        /// Connects to the socket
        /// </summary>
        /// <returns>bool - if connection is successful or not.</returns>
        public async Task<bool> ConnectAsync()
        {
            if (_WebSocket != null)
            {
                CloseSocket();
            }
            _WebSocket = new ClientWebSocket();

           /* string base64 = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(string.Format("Basic {0}:{1}",
                "",
                "")));
            _WebSocket.Options.SetRequestHeader("Authorization", "Basic " + base64);*/

            try
            {
                await _WebSocket.ConnectAsync(new Uri(baseURI), CancellationToken.None);

                // Notify event on socket connected successfully
                _OnSocketConnectedEvent?.Invoke();
            }
            catch (Exception exp)
            {
                return false;
            }

            // Set property
            _IsSocketConnectionAlive = true;

            // Start receiving in a new thread
            new Thread(async () =>
            {
                await OnReceived();
            }).Start();
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ClosureException"></param>
        public void CloseSocket(Exception ClosureException = null)
        {
            CloseSocket(false, ClosureException);
        }

        private int LastSocketReconnectionDelay_Seconds = 10; // seconds
        /// <summary>
        /// Shuts down the active web socket
        /// </summary>
        private async void CloseSocket(bool SocketClosedUnexpectedly, Exception ClosureException = null)
        {
            // Set property
            lock (this)
            {
                if (!_IsSocketConnectionAlive) // the reason this have to be locked is because when terminating the socket manually, if any active connection is currently being passed around; 'CloseSocket' will be called again for the second time with 'SocketClosedUnexpectedly' parameter as true
                {
                    return;
                }
                _IsSocketConnectionAlive = false;
            }

            if (_WebSocket != null)
            {
                // Attempts to close the socket
                Exception socketExitException = null;
                try
                {
                    await _WebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                }
                catch (Exception exp)
                {
                    socketExitException = exp;
                }
                // Fire on disconnection event
                _OnSocketDisconnectedEvent?.Invoke(ClosureException == null, ClosureException == null ? socketExitException : ClosureException, LastSocketReconnectionDelay_Seconds);

                // Cleanup.
                try
                {
                    _WebSocket.Dispose();
                }
                catch { }
                _WebSocket = null;
            }

            if (SocketClosedUnexpectedly && this.AutoReconnectSocketOnDisconnection)
            {
                await Task.Run(async () =>
                {
                    await Task.Delay(LastSocketReconnectionDelay_Seconds * 1000);
                });

                if (_OnRequestSocketReconnectionOnDisconnection == null)
                {
                    throw new Exception("_OnRequestSocketReconnectionOnDisconnection event not hooked. Auto reconnection not available");
                }
                bool success = await _OnRequestSocketReconnectionOnDisconnection?.Invoke();
                if (success)
                {
                    LastSocketReconnectionDelay_Seconds = 10; // re-connected successfully. Reset reconnection time
                }
                else
                {
                    CloseSocket(true, null);

                    LastSocketReconnectionDelay_Seconds *= 2; // multiply reconnection time by 2 whenever each one fails.
                }
            }
        }

        /// <summary>
        /// Sends a byte array to the connected socket
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public async Task<bool> Send(byte[] buffer)
        {
            try
            {
                await _WebSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                return true;
            }
            catch (Exception exp)
            {
                CloseSocket(true, exp);
            }
            return false;
        }

        /// <summary>
        /// On packet received from the web socket
        /// </summary>
        /// <param name="webSocket"></param>
        /// <returns></returns>
        private async Task OnReceived()
        {
            if (_WebSocket == null)
                return;

            byte[] Sharedbuffer = new byte[RECEIVE_BLOCK_SIZE];

            while (_WebSocket != null && _WebSocket.State == WebSocketState.Open)
            {
                //ArraySegment<byte> segment = new ArraySegment<byte>(Sharedbuffer);

                try
                {
                    using (var ms = new MemoryStream()) // auto release memory
                    {
                        WebSocketReceiveResult res;
                        do
                        {
                            res = await _WebSocket.ReceiveAsync(Sharedbuffer, CancellationToken.None);
                            if (res.MessageType == WebSocketMessageType.Close)
                            {
                                CloseSocket(true);
                                return;
                            }
                            ms.Write(Sharedbuffer, 0, res.Count);
                            // ms.Write(segment.Array, segment.Offset, res.Count);
                        }
                        while (!res.EndOfMessage);

                        ms.Seek(0, SeekOrigin.Begin);

                        // Return data
                        byte[] returnBuffer = new byte[ms.Length];
                        Buffer.BlockCopy(ms.ToArray(), 0, returnBuffer, 0, (int)ms.Length);

                        // Fires the return packet in a new thread
                        ThreadPool.QueueUserWorkItem(new WaitCallback(ExecuteReturnPacket), returnBuffer);

                        // Packet delay
                        if (SocketReceiveMillisDelay > 0)
                        {
                            Thread.Sleep(SocketReceiveMillisDelay);
                        }
                    }
                }
                catch (Exception exp)
                {
                    CloseSocket(true, exp);
                    break;
                }
            }
        }

        /// <summary>
        /// Fire
        /// </summary>
        /// <param name="buffer"></param>
        private void ExecuteReturnPacket(object buffer)
        {
            _OnPacketReceivedEvent?.Invoke((byte[])buffer);
        }
    }
}
