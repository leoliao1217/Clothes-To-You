﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class SocketThread
{
    public struct Struct_Internet
    {
        public string ip;
        public int port;
    }

    private Socket clientSocket;//連線使用的Socket
    private Struct_Internet internet;
    public string receiveMessage;
    private string sendMessage;

    private Thread threadReceive;
    private Thread threadConnect;

    public SocketThread(AddressFamily family, SocketType socketType, ProtocolType protocolType, string ip, int port)
    {
        clientSocket = new Socket(family, socketType, protocolType);
        internet.ip = ip;
        internet.port = port;
        receiveMessage = null;
    }

    public void StartConnect()
    {
        threadConnect = new Thread(Accept);
        threadConnect.Start();
    }

    public void StopConnect()
    {
        try
        {
            clientSocket.Close();
        }
        catch (Exception)
        {

        }
    }

    public void Send(string message)
    {
        if (message == null)
            throw new NullReferenceException("message不可為Null");
        else
            sendMessage = message;
        SendMessage();
    }

    public void SendUTF8(string message)
    {
        if (message == null)
            throw new NullReferenceException("message不可為Null");
        else
            sendMessage = message;
        SendMessageUTF8();
    }

    public void Receive()
    {
        if (threadReceive != null && threadReceive.IsAlive == true)
            return;
        threadReceive = new Thread(ReceiveMessage);
        threadReceive.IsBackground = true;
        threadReceive.Start();
    }

    public void ReceiveUTF8()
    {
        if (threadReceive != null && threadReceive.IsAlive == true)
            return;
        threadReceive = new Thread(ReceiveMessageUTF8);
        threadReceive.IsBackground = true;
        threadReceive.Start();
    }

    private void Accept()
    {
        try
        {
            clientSocket.Connect(IPAddress.Parse(internet.ip), internet.port);//等待連線，若未連線則會停在這行
        }
        catch (Exception)
        {
        }
    }

    private void SendMessage()
    {
        try
        {
            if (clientSocket.Connected == true)
            {
                clientSocket.Send(Encoding.ASCII.GetBytes(sendMessage));
            }
        }
        catch (Exception)
        {

        }
    }

    private void SendMessageUTF8()
    {
        try
        {
            if (clientSocket.Connected == true)
            {
                clientSocket.Send(Encoding.UTF8.GetBytes(sendMessage));
            }
        }
        catch (Exception)
        {

        }
    }

    private void ReceiveMessage()
    {
        if (clientSocket.Connected == true)
        {
            byte[] bytes = new byte[256];
            long dataLength = clientSocket.Receive(bytes);

            receiveMessage = Encoding.ASCII.GetString(bytes);
        }
    }

    private void ReceiveMessageUTF8()
    {
        if (clientSocket.Connected == true)
        {
            byte[] bytes = new byte[256];
            long dataLength = clientSocket.Receive(bytes);

            receiveMessage = Encoding.UTF8.GetString(bytes);
        }
    }
}

