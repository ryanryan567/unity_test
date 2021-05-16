using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class playerClient : MonoBehaviour
{
    Socket serverSocket;
    IPAddress ip;
    IPEndPoint ipEnd;
    string recvString;
    string sendString;
    byte[] recvData = new byte[1024];
    byte[] sendDate = new byte[1024];
    int recvLen;
    Thread connectThread;
    // Start is called before the first frame update
    void Start()
    {
        InitSocket();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void initSocket()
    {
        try
        {
            #server_ip_port
            ip = IPAddress.Parse("127.0.0.1");
            ipEnd = new IPEndPoint(ip, 4567);
            SocketConnect();
            SocketSend("player");
        } catch (Exception e)
        {
            Debug.Log(e);
        }
        
    }

    void SocketConnect()
    {
        if (serverSocket != null)
        {
            serverSocket.Close();
        }
        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        print("ready to connect");
        serverSocket.Connect(ipEnd);

    }

    void SocketSend(string sendStr)
    {
        //清空傳送快取
        sendData = new byte[1024];
        //資料型別轉換
        sendData = Encoding.ASCII.GetBytes(sendStr);
        //傳送
        serverSocket.Send(sendData, sendData.Length, SocketFlags.None);
    }


    void SocketQuit()
    {
        //關閉執行緒
        if (connectThread != null)
        {
            connectThread.Interrupt();
            connectThread.Abort();
        }
        //最後關閉伺服器
        if (serverSocket != null)
            serverSocket.Close();
        print("diconnect");
    }
}
