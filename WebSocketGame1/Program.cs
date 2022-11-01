// See https://aka.ms/new-console-template for more information
using ConsoleApp1;
using Fleck;
using Org.BouncyCastle.Bcpg;
using System.Net.Sockets;
using System.Text;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        enum Command
        {
            Login = 10001, //登录
            Register,
            Kickout, //踢出
            Rank = 20001,
            RankList,
        };

        private static Dictionary<string, IWebSocketConnection> sockets = new Dictionary<string, IWebSocketConnection>();

        static void Main(string[] args)
        {
            var server = new WebSocketServer("ws://0.0.0.0:8181");
            server.Start(socket =>
            {
                string userid = "";
                socket.OnOpen = () => Console.WriteLine("Open!");
                socket.OnClose = () => Console.WriteLine("Close!");
                //收字符串
                socket.OnMessage = message =>
                {
                    Console.WriteLine(message);
                    //socket.Send(message);
                };
                //收字节数组
                socket.OnBinary = binary =>
                {
                    string strContent = Encoding.UTF8.GetString(binary, 0, binary.Length);
                    Console.WriteLine("接收客户端消息{0}", strContent);
                    //开始解析
                    var array = strContent.Split(",");
                    if (array.Length > 0)
                    {
                        string str = array[0] + ",";//回传值加上消息头
                        switch ((Command)int.Parse(array[0]))
                        {
                            case Command.Login:
                                {
                                    Console.WriteLine("开始登陆");
                                    string qudaoid = array[1];
                                    str += Tool.Login(qudaoid);
                                    //返回给客户端
                                    socket.Send(Encoding.UTF8.GetBytes(str));

                                    Console.WriteLine("开始保存socket");
                                    var strarray = str.Split(",");
                                    userid = strarray[1];
                                    
                                    addSocketList(userid, socket);
                                }
                                break;
                            case Command.Register:
                                {
                                    Console.WriteLine("开始注册");
                                    string qudaoid = array[1];
                                    string name = array[2];
                                    str += Tool.Register(qudaoid, name);
                                    //返回给客户端
                                    socket.Send(Encoding.UTF8.GetBytes(str));

                                    Console.WriteLine("开始保存socket");
                                    var strarray = str.Split(",");
                                    userid = strarray[1];

                                    addSocketList(userid, socket);
                                }
                                break;
                            case Command.Rank:
                                {
                                    Console.WriteLine("记录排行");
                                    if(userid != "")
                                    {
                                        string score = array[1];
                                        str += Tool.Rank(userid, score);
                                    }
                                }
                                break;
                            case Command.RankList:
                                {

                                }
                                break;
                        }
                    }
                };
            });
            while (true)
            {
                string info = Console.ReadLine();
                if (info.Equals("exit")) break;
            }
        }

        static void addSocketList(string userid, IWebSocketConnection socket)
        {
            if (sockets.ContainsKey(userid))
            {
                //列表中已有该用户就踢掉上个连接
                var str = (int)Command.Kickout + ",";
                sockets[userid].Send(Encoding.UTF8.GetBytes(str));
                sockets[userid].Close();
                sockets[userid] = socket;
            }
            else
            {
                //列表中没有该用户就加入列表
                sockets.Add(userid, socket);
            }
        }
    }
}
