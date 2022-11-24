// See https://aka.ms/new-console-template for more information
using ConsoleApp1;
using Fleck;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Bcpg;
using System.Net.Sockets;
using System.Text;
using WebSocketGame1;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        enum Command
        {
            Login = 10001, //登录
            Register,
            UserInfo,
            KickOut, //踢出
            Rank = 20001,
            RankList,
            StartGame = 30001,
        };

        private static Dictionary<string, IWebSocketConnection> sockets = new Dictionary<string, IWebSocketConnection>();

        static void Main(string[] args)
        {
            var ranklist = new RankList();

            var server = new WebSocketServer("ws://0.0.0.0:8181");
            server.Start(socket =>
            {
                string userid = "";
                socket.OnOpen = () =>
                {
                    Console.WriteLine("Open!");
                };
                socket.OnClose = () => Console.WriteLine("Close!");
                socket.OnError = message =>
                {
                    Console.WriteLine("error:" + message);
                };
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
                    Console.WriteLine("接收客户端{0}消息{1}", socket.ConnectionInfo.ClientIpAddress,strContent);
                    //开始解析
                    JObject dicContent = JsonConvert.DeserializeObject<JObject>(strContent.ToString());
                    if(dicContent != null)
                    {
                        switch ((Command)int.Parse(dicContent["command"].ToString()))
                        {
                            case Command.Login:
                                {
                                    Console.WriteLine("开始登陆");
                                    string qudaoid = dicContent["qudaoid"].ToString();
                                    var result = Tool.Login(qudaoid);
                                    result.Add("command", dicContent["command"]);
                                    string resultStr = JsonConvert.SerializeObject(result);
                                    //返回给客户端
                                    socket.Send(Encoding.UTF8.GetBytes(resultStr));

                                    Console.WriteLine("开始保存socket");

                                    if (result["userid"]!= null)
                                    {
                                        userid = result["userid"].ToString();

                                        addSocketList(userid, socket);

                                        //更新所在省
                                        string region = Tool.updateRegion(userid, socket.ConnectionInfo.ClientIpAddress);
                                        if (region != "")
                                        {
                                            result["region"] = region;
                                            result["command"] = ((int)Command.UserInfo);
                                            resultStr = JsonConvert.SerializeObject(result);
                                            //返回给客户端
                                            socket.Send(Encoding.UTF8.GetBytes(resultStr));
                                        }
                                    }
                                }
                                break;
                            case Command.Register:
                                {
                                    Console.WriteLine("开始注册");
                                    string qudaoid = dicContent["qudaoid"].ToString();
                                    string name = dicContent["name"].ToString();
                                    var result = Tool.Register(qudaoid, name);
                                    result.Add("command", dicContent["command"]);
                                    string resultStr = JsonConvert.SerializeObject(result);
                                    //返回给客户端
                                    socket.Send(Encoding.UTF8.GetBytes(resultStr));

                                    Console.WriteLine("开始保存socket");
                                    userid = result["userid"].ToString();

                                    addSocketList(userid, socket);
                                    //更新所在省
                                    string region = Tool.updateRegion(userid, socket.ConnectionInfo.ClientIpAddress);
                                    if (region != "")
                                    {
                                        result["region"] = region;
                                    }
                                    result["command"] = ((int)Command.UserInfo);
                                    resultStr = JsonConvert.SerializeObject(result);
                                    //返回给客户端
                                    socket.Send(Encoding.UTF8.GetBytes(resultStr));
                                }
                                break;
                            case Command.Rank:
                                {
                                    Console.WriteLine("记录排行");
                                    if (userid != "")
                                    {
                                        string score = dicContent["score"].ToString();
                                        var result = Tool.Rank(userid, score);
                                        result.Add("command", dicContent["command"]);
                                        string resultStr = JsonConvert.SerializeObject(result);
                                        //返回给客户端
                                        socket.Send(Encoding.UTF8.GetBytes(resultStr));
                                    }
                                }
                                break;
                            case Command.RankList:
                                {
                                    //不实时请求数据库，读取缓存的数据
                                    var result = ranklist.getList();
                                    if(result["command"] == null)
                                    {
                                        result.Add("command", dicContent["command"]);
                                    }
                                    string resultStr = JsonConvert.SerializeObject(result);
                                    //返回给客户端
                                    socket.Send(Encoding.UTF8.GetBytes(resultStr));

                                }
                                break;
                            case Command.StartGame:
                                {
                                    var result = Tool.StartGame(userid);
                                    result.Add("command", dicContent["command"]);
                                    string resultStr = JsonConvert.SerializeObject(result);
                                    //返回给客户端
                                    socket.Send(Encoding.UTF8.GetBytes(resultStr));
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
                JObject result = new JObject();
                result.Add("command", (int)Command.KickOut);
                string resultStr = JsonConvert.SerializeObject(result);

                sockets[userid].Send(Encoding.UTF8.GetBytes(resultStr));
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
