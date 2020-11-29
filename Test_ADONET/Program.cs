using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data; //引用ADO.NET基礎物件 ex:DataSet
//using System.Data.OleDb; //引用OLE DB資料來源物件 ex:Access,Excel,SQL Server7.0以上
using System.Data.SqlClient; //引用SQL Server資料來源物件

namespace Test_ADONET
{
    class Program
    {
        static void Main(string[] args)
        {
            string cnStr = "Server=localhost;Database=test1;Trusted_Connection=True;"; //建立連接字串
            SqlConnection cn = new SqlConnection(cnStr); //建立資料庫連接物件
            cn.Open(); //開啟與資料庫的連線
            
            if(cn.State == ConnectionState.Open)
            {
                Console.WriteLine($"{cn.Database} 資料庫已連線"); //顯示資料庫名稱
            }

            cn.Close(); //關閉與資料庫的連線

            if (cn.State == ConnectionState.Closed)
            {
                Console.WriteLine($"{cn.Database} 資料連線已關閉"); //顯示資料庫名稱
            }

            Console.Read();
        }
    }
}
