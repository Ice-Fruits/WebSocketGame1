using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketGame1
{
    internal class RankList
    {
        JObject m_list;
        private static Timer timer;
        public RankList()
        {
            m_list = Tool.RankList();
            timer = new Timer(new TimerCallback(timerCall), this, 0,10000);//创建定时器
        }

        

        private void timerCall(object? obj)
        {
            m_list = Tool.RankList();
            Console.WriteLine("刷新一次排行榜");
        }

        public JObject getList()
        {
            return m_list;
        }
    }
}
