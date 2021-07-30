using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingNote.DBSource
{
    class Logger
    {
        public static void WriteLog(Exception ex) 
        {
            //Logger輸出形式
            string msg =
                $@"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}
                {ex.ToString()}
                ";

            //把檔案寫到指定路徑
            System.IO.File.AppendAllText("D:\\Logs\\Log.log", msg);

            throw ex;
        }
    }
}
