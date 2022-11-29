using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketGame1
{
    internal class UserPower
    {
        int POWERMAX = 60;
        JArray jArray;
        private static Timer timer;
        public UserPower()
        {
            jArray = new JArray();
            SelectAll();

            timer = new Timer(new TimerCallback(timerCall), this, 0, 1000);//创建定时器
        }

        void SelectAll()
        {
            MySqlConnection conn = new MySqlConnection(Tool.connectStr);//还未与数据库建立连接
            try//捕捉异常，并打印
            {
                conn.Open();//建立连接
                Console.WriteLine("已经与数据库建立连接");

                string sql = String.Format("SELECT * FROM game1.usersinfo");//数据库操作命令,读取数据行数

                MySqlCommand cmd = new MySqlCommand(sql, conn);//数据库命令类
                MySqlDataReader reader = cmd.ExecuteReader();
                //reader.Read();
                while (reader.Read())//Read每次调用会返回Ture或False的值，使用while循环来全部遍历
                {
                    if (reader.HasRows)//存在
                    {
                        AddUser(reader.GetString("userid"), reader.GetString("power"));
                        //string userid = reader.GetString("userid");
                        //string score = reader.GetString("score");
                    }
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally//无论如何都会去执行的语句
            {
                conn.Close();//关闭连接
            }
        }

        private void timerCall(object? obj)
        {
            foreach (var item in jArray)
            {
                var jo = item as JObject;
                var power = int.Parse(jo["power"].ToString());
                if (power < POWERMAX)
                {
                    var time = int.Parse(jo["powertime"].ToString());
                    time--;
                    if (time <= 0)
                    {
                        time = 360;
                        //加体力
                        var userid = jo["userid"].ToString();
                        var result = Tool.AddPower(userid);
                        if (result["result"].ToString() == "true")
                        {
                            power++;
                            jo["power"] = power;
                            var resultContent = new JObject();
                            resultContent.Add("command", (int)Program.Command.AddUserPower);
                            resultContent.Add("power", power);
                            resultContent.Add("powertime", time);
                            //通知客户端加了体力
                            string resultStr = JsonConvert.SerializeObject(resultContent);
                            //返回给客户端
                            Program.sockets[userid].Send(Encoding.UTF8.GetBytes(resultStr));
                        }
                    }
                    jo["powertime"] = time;
                }
            }
        }

        public int AddUser(string userid, string power)
        {
            JObject jo = new JObject();
            jo.Add("userid", userid);
            jo.Add("power", power);
            jo.Add("powertime", 360);//6分钟
            jArray.Add(jo);

            return 360;
        }

        public int GetPowerTime(string userid)
        {
            foreach (var item in jArray)
            {
                var jo = item as JObject;
                if(jo["userid"].ToString() == userid)
                {
                    var time = int.Parse(jo["powertime"].ToString());
                    return time;
                }
            }
            return 0;
        }

        public JObject UpdatePower(string userid,int addpower)
        {
            var resultContent = new JObject();
            MySqlConnection conn = new MySqlConnection(Tool.connectStr);//还未与数据库建立连接
            try//捕捉异常，并打印
            {
                conn.Open();//建立连接
                Console.WriteLine("已经与数据库建立连接");

                string sql = String.Format("UPDATE game1.usersinfo SET power = power + {0} WHERE userid = '{1}' ", addpower, userid);

                MySqlCommand cmd = new MySqlCommand(sql, conn);//数据库命令类
                int result = cmd.ExecuteNonQuery();//返回值是数据库受影响行数的记录
                if (result > 0)
                {
                    resultContent.Add("result", "true");
                    foreach (var item in jArray)
                    {
                        var jo = item as JObject;
                        if (jo["userid"].ToString() == userid)
                        {
                            var power = int.Parse(jo["power"].ToString());
                            power += addpower;
                            jo["power"] = power;
                            resultContent.Add("power", power);
                            if (power < POWERMAX)
                            {
                                if(power - addpower >= POWERMAX)
                                {
                                    resultContent.Add("powertime", 360);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally//无论如何都会去执行的语句
            {
                conn.Close();//关闭连接
            }
            return resultContent;
        }
    }
}
