﻿using common;
using NLog;
using System;
using System.Net;
using System.Net.Sockets;

namespace wServer.networking
{
    internal class PolicyServer
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private readonly TcpListener _listener;
        private bool _started;

        public PolicyServer() => _listener = new TcpListener(IPAddress.Any, 843);

        private static void ServePolicyFile(IAsyncResult ar)
        {
            try
            {
                var cli = (ar.AsyncState as TcpListener).EndAcceptTcpClient(ar);
                (ar.AsyncState as TcpListener).BeginAcceptTcpClient(ServePolicyFile, ar.AsyncState);

                var s = cli.GetStream();
                var rdr = new NReader(s);
                var wtr = new NWriter(s);
                if (rdr.ReadNullTerminatedString() == "<policy-file-request/>")
                {
                    wtr.WriteNullTerminatedString(
                        @"<cross-domain-policy>" +
                        @"<allow-access-from domain=""*"" to-ports=""*"" />" +
                        @"</cross-domain-policy>");
                    wtr.Write((byte)'\r');
                    wtr.Write((byte)'\n');
                }
                cli.Close();
            }
            catch { }
        }

        public void Start()
        {
            try
            {
                _listener.Start();
                _listener.BeginAcceptTcpClient(ServePolicyFile, _listener);
                _started = true;
            }
            catch
            {
                Log.Warn("Could not start Socket Policy Server, is port 843 occupied?");
                _started = false;
            }
        }

        public void Stop()
        {
            if (!_started)
                return;

            Log.Warn("Stopping policy server...");
            _listener.Stop();
        }
    }
}
