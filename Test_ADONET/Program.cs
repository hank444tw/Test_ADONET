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

            //--------------------DataReader物件--------------------
            string sql = "Select * from Table_product";
            SqlCommand cmd = new SqlCommand(sql,cn); //建立Commad物件，並設定要執行的sql語法

            cn.Open(); //開啟與資料庫的連線

            if (cn.State == ConnectionState.Open)
            {
                Console.WriteLine($"{cn.Database} 資料庫已連線"); //顯示資料庫名稱
            }

            SqlDataReader dr = cmd.ExecuteReader(); //建立DataReader物件，並執行cmd物件的sql語法

            Console.WriteLine("以DataReader物件讀取資料表");
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
                Console.WriteLine($"");
            }

            //--------------------DataReader物件 End--------------------

            //--------------------DataSet物件--------------------
            DataSet ds = new DataSet();//建立DataSet物件

            string sql_adp_p = "Select * from Table_product";
            SqlDataAdapter adp_p = new SqlDataAdapter(sql_adp_p, cn); //建立DataAdapter物件，設定執行的Sql和連接cn物件
            adp_p.Fill(ds, "Table_product"); //將資料表放入DataSet物件中，Fill方法會自己連線資料庫，不用特別open()

            string sql_adp_d = "Select * from Table_productDetail";
            SqlDataAdapter adp_d = new SqlDataAdapter(sql_adp_d, cn);
            adp_d.Fill(ds, "Table_productDetail"); //DataSet物件可有多個資料表

            DataTable dtP, dtD; //建立DataTable物件
            dtP = ds.Tables["Table_product"]; //將DataSet的資料表指向dtp
            dtD = ds.Tables[1];

            Console.WriteLine("DataSet裡共有" + ds.Tables.Count + "個DataTable物件");
            Console.WriteLine($"Table_product資料表共有{dtP.Columns.Count}個資料欄位");

            for(int r = 0;r < dtP.Rows.Count; r++) //透過巢狀迴圈逐一取得資料表每一個欄位的資料
            {
                for(int c = 0;c < dtP.Columns.Count; c++)
                {
                    Console.Write($" {dtP.Rows[r][c]}");
                }
                Console.WriteLine("");
            }

            cn.Close(); //關閉與資料庫的連線

            if (cn.State == ConnectionState.Closed)
            {
                Console.WriteLine($"{cn.Database}資料連線已關閉"); //顯示資料庫名稱
                Console.WriteLine("");
            }
            //--------------------DataSet物件 End--------------------

            //--------------------使用Comnand物件CRUD--------------------
            string sql_c = "Insert into Table_product (Product,CreatTime,Price) values ('產品3',GETDATE(),100)";
            SqlCommand cmd_c = new SqlCommand(sql_c,cn); 

            cn.Open(); //開啟與資料庫的連線
            if (cn.State == ConnectionState.Open)
            {
                Console.WriteLine($"{cn.Database} 資料庫已連線"); //顯示資料庫名稱
            }
            Console.WriteLine($"新增{cmd_c.ExecuteNonQuery()}筆資料完成");

            string sql_u = "Update Table_product Set Price = 600 Where Product = '產品3'";
            cmd_c = new SqlCommand(sql_u, cn);
            Console.WriteLine($"更改{cmd_c.ExecuteNonQuery()}筆資料完成");

            string sql_d = " delete Table_product where Product = '產品3'";
            cmd_c = new SqlCommand(sql_d, cn);
            Console.WriteLine($"刪除{cmd_c.ExecuteNonQuery()}筆資料完成");

            cn.Close(); //關閉與資料庫的連線
            if (cn.State == ConnectionState.Closed)
            {
                Console.WriteLine($"{cn.Database}資料連線已關閉"); //顯示資料庫名稱
            }
            //--------------------使用Comnand物件CRUD End--------------------

            Console.Read();
        }
    }
}
