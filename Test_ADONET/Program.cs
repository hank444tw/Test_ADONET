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

            string sql = "Select * from Table_product";
            SqlCommand cmd = new SqlCommand(sql,cn); //建立Commad物件，並設定要執行的sql語法

            cn.Open(); //開啟與資料庫的連線

            if (cn.State == ConnectionState.Open)
            {
                Console.WriteLine($"{cn.Database} 資料庫已連線"); //顯示資料庫名稱
            }

            SqlDataReader dr = cmd.ExecuteReader(); //建立DataReader物件，並執行cmd物件的sql語法

            Console.Write(" 資料表欄位名稱 ");
            for (int i = 0; i < dr.FieldCount; i++) //FieldCount傳回資料表資料行數量
            {
                Console.Write($" {dr.GetName(i)}"); //取得資料欄位名稱
            }
            Console.WriteLine("");

            Console.Write(" Price欄位資料 ");
            while (dr.Read()) //true表示DataReader指標尚未到EOF
            {
                Console.Write($" {dr["Pid"].ToString()}");
                Console.Write($" {dr.GetString(1)}"); //以索引方式讀取資料會比欄位名稱，效率更快
                Console.Write($" {dr.GetSqlDateTime(2).ToString()}"); //執行效率最快(限使用SQL Server7.0以上)
                Console.WriteLine($" {dr["Price"].ToString()}");
                Console.Write(" Price欄位資料 ");
            }
            Console.WriteLine("");

            cn.Close(); //關閉與資料庫的連線

            if (cn.State == ConnectionState.Closed)
            {
                Console.WriteLine($"{cn.Database}資料連線已關閉"); //顯示資料庫名稱
            }

            Console.Read();
        }
    }
}
