﻿using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Tool
    {
        static string connectStr = "server=127.0.0.1;port=3306;database=game1;user=root;password=hqq88888;";//sql连接数据参数
        static void Read()
        {
            MySqlConnection conn = new MySqlConnection(connectStr);//还未与数据库建立连接
            try//捕捉异常，并打印
            {
                conn.Open();//建立连接
                Console.WriteLine("已经与数据库建立连接");

                string sql = "select * from users";//数据库操作命令,查询users表
                //string sql = "select id,username,registerdate from users";//数据库操作命令，查询users表指定列
                MySqlCommand cmd = new MySqlCommand(sql, conn);//数据库命令类
                                                               //cmd.ExecuteReader();//执行一些查询
                                                               //cmd.ExecuteNonQuery();//插入，删除
                                                               //cmd.ExecuteScalar();//执行一些查询，返回一个单个的值

                MySqlDataReader reader = cmd.ExecuteReader();
                //reader.Read();//每次调用Read，读取一条记录，打开第一行。把reader当作一本书
                //Console.WriteLine(reader[0].ToString()+reader[1].ToString()+ reader[2].ToString());//打印当前行的三列数据
                //reader.Read();//第二次调用，打开第二行的数据
                //Console.WriteLine(reader[0].ToString() + reader[1].ToString() + reader[2].ToString());//打印当前行的三列数据
                while (reader.Read())//Read每次调用会返回Ture或False的值，使用while循环来全部遍历
                {
                    Console.WriteLine("执行了一次用户名与密码的查询");
                    Console.WriteLine(reader[0].ToString() + reader[1].ToString() + reader[2].ToString());//打印当前行的三列数据

                    //其他方式：
                    // Console.WriteLine(reader.GetInt32(0) + " " + reader.GetString(1) + " " + reader.GetString(2));
                    // Console.WriteLine(reader.GetInt32("id") + " " + reader.GetString("username") + " " + reader.GetString("password"));
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
        }
        static void Insert()//封装测试-数据库插入操作
        {
            MySqlConnection conn = new MySqlConnection(connectStr);//还未与数据库建立连接
            try//捕捉异常，并打印
            {
                conn.Open();//建立连接
                Console.WriteLine("已经与数据库建立连接");

                string sql = "insert into users(username,password) values('游客','123')";//数据库操作命令,插入数据
                //string sql = "insert into users(username,password,registerdate) values('游客2','123','2021-1-3')";//数据库操作命令,插入数据
                //string sql = "insert into users(username,password,registerdate) values('游客3','123','"+DateTime.Now+"')";//数据库操作命令,插入数据

                MySqlCommand cmd = new MySqlCommand(sql, conn);//数据库命令类
                int result = cmd.ExecuteNonQuery();//返回值是数据库受影响行数的记录
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
        static void Update()//封装测试-数据库更新数据
        {
            MySqlConnection conn = new MySqlConnection(connectStr);//还未与数据库建立连接
            try//捕捉异常，并打印
            {
                conn.Open();//建立连接
                Console.WriteLine("已经与数据库建立连接");

                string sql = "update users set username ='更新后的用户名',password='123' where id =3";//数据库操作命令,更新数据
                //string sql = "insert into users(username,password,registerdate) values('游客2','123','2021-1-3')";//数据库操作命令,插入数据
                //string sql = "insert into users(username,password,registerdate) values('游客3','123','"+DateTime.Now+"')";//数据库操作命令,插入数据

                MySqlCommand cmd = new MySqlCommand(sql, conn);//数据库命令类
                int result = cmd.ExecuteNonQuery();//返回值是数据库受影响行数的记录

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
        static void Delete()//封装测试-删除数据
        {
            MySqlConnection conn = new MySqlConnection(connectStr);//还未与数据库建立连接
            try//捕捉异常，并打印
            {
                conn.Open();//建立连接
                Console.WriteLine("已经与数据库建立连接");

                string sql = "delete from users where id=7 ";//数据库删除数据
                //string sql = "insert into users(username,password,registerdate) values('游客2','123','2021-1-3')";//数据库操作命令,插入数据
                //string sql = "insert into users(username,password,registerdate) values('游客3','123','"+DateTime.Now+"')";//数据库操作命令,插入数据

                MySqlCommand cmd = new MySqlCommand(sql, conn);//数据库命令类
                int result = cmd.ExecuteNonQuery();//返回值是数据库受影响行数的记录

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
        static void ReadUsersCount()//封装测试-查询一些值
        {
            MySqlConnection conn = new MySqlConnection(connectStr);//还未与数据库建立连接
            try//捕捉异常，并打印
            {
                conn.Open();//建立连接
                Console.WriteLine("已经与数据库建立连接");

                string sql = "select count(*) from users";//数据库操作命令,读取数据行数

                MySqlCommand cmd = new MySqlCommand(sql, conn);//数据库命令类
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                int count = Convert.ToInt32(reader[0].ToString());
                Console.WriteLine(count);

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
        static void ExecuteScalar()//封装测试-执行一些查询，返回一个单个的值
        {
            MySqlConnection conn = new MySqlConnection(connectStr);//还未与数据库建立连接
            try//捕捉异常，并打印
            {
                conn.Open();//建立连接
                Console.WriteLine("已经与数据库建立连接");

                string sql = "select count(*) from users";//数据库操作命令,读取数据行数

                MySqlCommand cmd = new MySqlCommand(sql, conn);//数据库命令类


                object o = cmd.ExecuteScalar();
                int count = Convert.ToInt32(o.ToString());
                Console.WriteLine(count);

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

        public static string Login(string qudaoid)
        {
            MySqlConnection conn = new MySqlConnection(Tool.connectStr);//还未与数据库建立连接
            try//捕捉异常，并打印
            {
                conn.Open();//建立连接
                Console.WriteLine("已经与数据库建立连接");

                string sql = String.Format("SELECT * FROM game1.users WHERE qudaoid='{0}'",qudaoid);//数据库操作命令,读取数据行数

                MySqlCommand cmd = new MySqlCommand(sql, conn);//数据库命令类
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)//存在
                {
                    string userid = reader[2].ToString();
                    Console.WriteLine(userid);
                    reader.Close();
                    //找到后再去查找用户信息表
                    sql = String.Format("SELECT * FROM game1.usersinfo WHERE userid='{0}'", userid);
                    cmd.CommandText = sql;
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    string result = reader[0].ToString();
                    result += ",";
                    result += reader[1].ToString();
                    result += ",";
                    result += reader[2].ToString();
                    return result;
                }
                else
                {
                    //不存在
                    return "";
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return "";
            }
            finally//无论如何都会去执行的语句
            {
                conn.Close();//关闭连接
            }
        }

        public static string Register(string qudaoid,string name)
        {
            MySqlConnection conn = new MySqlConnection(Tool.connectStr);//还未与数据库建立连接
            try//捕捉异常，并打印
            {
                conn.Open();//建立连接
                Console.WriteLine("已经与数据库建立连接");

                string sql = String.Format("SELECT * FROM game1.users WHERE qudaoid='{0}'", qudaoid);//数据库操作命令,读取数据行数

                MySqlCommand cmd = new MySqlCommand(sql, conn);//数据库命令类
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)//存在
                {
                    string userid = reader[2].ToString();
                    Console.WriteLine(userid);
                    reader.Close();
                    //找到后再去查找用户信息表
                    sql = String.Format("SELECT * FROM game1.usersinfo WHERE userid='{0}'", userid);
                    cmd.CommandText = sql;
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    string result = reader[0].ToString();
                    result += ",";
                    result += reader[1].ToString();
                    result += ",";
                    result += reader[2].ToString();
                    return result;
                }
                else
                {
                    //不存在
                    //开始创建用户，创建用户信息
                    reader.Close();
                    string userid = Guid.NewGuid().ToString();
                    Console.WriteLine("创建用户" + userid);
                    sql = String.Format("INSERT INTO game1.users(qudaoid, userid) VALUES('{0}', '{1}')", qudaoid, userid);
                    cmd.CommandText = sql;
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    reader.Close();

                    //找到后再去查找用户信息表
                    sql = String.Format("INSERT INTO game1.usersinfo(userid, name) VALUES('{0}', '{1}')", userid, name);
                    cmd.CommandText = sql;
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    reader.Close();
                    sql = String.Format("SELECT * FROM game1.usersinfo WHERE userid='{0}'", userid);
                    cmd.CommandText = sql;
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    string result = reader[0].ToString();
                    result += ",";
                    result += reader[1].ToString();
                    result += ",";
                    result += reader[2].ToString();
                    return result;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return "";
            }
            finally//无论如何都会去执行的语句
            {
                conn.Close();//关闭连接
            }
        }

        public static string Rank(string userid, string score)
        {
            MySqlConnection conn = new MySqlConnection(Tool.connectStr);//还未与数据库建立连接
            try//捕捉异常，并打印
            {
                conn.Open();//建立连接
                Console.WriteLine("已经与数据库建立连接");

                string sql = String.Format("SELECT * FROM game1.rank WHERE userid='{0}'", userid);//数据库操作命令,读取数据行数

                MySqlCommand cmd = new MySqlCommand(sql, conn);//数据库命令类
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)//存在
                {
                    Console.WriteLine(userid);
                    reader.Close();
                    //找到后再去查找用户信息表
                    sql = String.Format("UPDATE game1.rank SET score = '{0}' WHERE userid = '{1}' ", score, userid);
                    cmd.CommandText = sql;
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    string result = "";
                    if (reader.HasRows)
                    {
                        result = "success";
                    }
                    return result;
                }
                else
                {
                    //不存在
                    //开始创建
                    reader.Close();
                    Console.WriteLine("创建排行" + userid);
                    sql = String.Format("INSERT INTO game1.rank(userid, score) VALUES('{0}', '{1}')", userid, score);
                    cmd.CommandText = sql;
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    string result = "";
                    if (reader.HasRows)
                    {
                        result = "success";
                    }
                    return result;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return "";
            }
            finally//无论如何都会去执行的语句
            {
                conn.Close();//关闭连接
            }
        }

        public static string RankList()
        {
            MySqlConnection conn = new MySqlConnection(Tool.connectStr);//还未与数据库建立连接
            try//捕捉异常，并打印
            {
                conn.Open();//建立连接
                Console.WriteLine("已经与数据库建立连接");

                string sql = String.Format("SELECT * FROM game1.rank limit 100");//数据库操作命令,读取数据行数

                MySqlCommand cmd = new MySqlCommand(sql, conn);//数据库命令类
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)//存在
                {
                    reader.Close();
                    //找到后再去查找用户信息表
                    sql = String.Format("UPDATE game1.rank SET score = '{0}' WHERE userid = '{1}' ", score, userid);
                    cmd.CommandText = sql;
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    string result = "";
                    if (reader.HasRows)
                    {
                        result = "success";
                    }
                    return result;
                }
                else
                {
                    
                    return "";
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return "";
            }
            finally//无论如何都会去执行的语句
            {
                conn.Close();//关闭连接
            }
        }

    }
}
