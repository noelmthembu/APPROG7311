using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static APPROG7311.Pages.DisasterModel;
using static APPROG7311.Pages.GoodsModel;
using static APPROG7311.Pages.MonetrayModel;

namespace APPROG7311.Pages.Admin
{
    public class AllocationModel : PageModel
    {
        public AllocationInfo paymentInfo = new AllocationInfo();
        public MonetrayInfo monetrayInfo = new MonetrayInfo();
        public List<MonetrayInfo> listmon = new List<MonetrayInfo>();
        public GoodsInfo goodsInfo = new GoodsInfo();
        public List<GoodsInfo> listgoo = new List<GoodsInfo>();
        public DisasterInfo disasterInfo = new DisasterInfo();
        public List<DisasterInfo> listdis = new List<DisasterInfo>();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            try
            {
                String connectionString = "Server=tcp:disaster-alleviation-foundation.database.windows.net,1433;Initial Catalog=Disaster_Alleviation_Foundation_DB;Persist Security Info=False;User ID=dafadmin;Password=P@ssw0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql_disaster = "SELECT * FROM DISASTER";
                    String sql_monetray = "SELECT * FROM MONETRAY";
                    String sql_goods = "SELECT * FROM GOODS";
                    using (SqlCommand command = new SqlCommand(sql_disaster, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                disasterInfo.DISASTER_NAME = reader.GetString(2);
                                listdis.Add(disasterInfo);
                            }
                        }
                    }
                    //
                    using (SqlCommand command = new SqlCommand(sql_monetray, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                monetrayInfo.AMOUNT = reader.GetDouble(2);
                                listmon.Add(monetrayInfo);
                            }
                        }
                    }
                    //
                    using (SqlCommand command = new SqlCommand(sql_goods, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                goodsInfo.CATEGORY = reader.GetString(4);
                                goodsInfo.NEW_CATEGORY = reader.GetString(5);
                                listgoo.Add(goodsInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Expection: " + ex.ToString());
            }
        }
        public void OnPost() 
        {
            paymentInfo.ALL_AMOUNT = Convert.ToDouble(Request.Form["ALL_AMOUNT"]);
            paymentInfo.ACTIVE_DISASTER = Request.Form["ACTIVE_DISASTER"];
            paymentInfo.ALL_GOODS = Request.Form["ALL_GOODS"];


            try
            {
                String connectionString = "Server=tcp:disaster-alleviation-foundation.database.windows.net,1433;Initial Catalog=Disaster_Alleviation_Foundation_DB;Persist Security Info=False;User ID=dafadmin;Password=P@ssw0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO ALLOCATIONS VALUES(@ALL_GOODS, @ALL_AMOUNT, @ACTIVE_DISASTER)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ALL_GOODS", paymentInfo.ALL_GOODS);
                        command.Parameters.AddWithValue("@ALL_AMOUNT", paymentInfo.ALL_AMOUNT);
                        command.Parameters.AddWithValue("@ACTIVE_DISASTER", paymentInfo.ACTIVE_DISASTER);
                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
   
            successMessage = "Added Successfully!";

        }
        public class AllocationInfo
        {
            public String ID;
            public String ALL_GOODS;
            public Double ALL_AMOUNT;
            public String ACTIVE_DISASTER;


        }
    }
}
