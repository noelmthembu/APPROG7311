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

namespace APPROG7311.Pages
{
    public class AdminsModel : PageModel
    {
        public MonetrayInfo monetrayInfo = new MonetrayInfo();
        public List<MonetrayInfo> listmon = new List<MonetrayInfo>();
        public GoodsInfo goodsInfo = new GoodsInfo();
        public List<GoodsInfo> listgoo = new List<GoodsInfo>();
        public DisasterInfo disasterInfo = new DisasterInfo();
        public List<DisasterInfo> listdis = new List<DisasterInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Server=tcp:disaster-alleviation-foundation.database.windows.net,1433;Initial Catalog=Disaster_Alleviation_Foundation_DB;Persist Security Info=False;User ID=dafadmin;Password=P@ssw0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql_monetray = "SELECT * FROM MONETRAY";
                    String sql_goods = "SELECT * FROM GOODS";
                    String sql_disaster = "SELECT * FROM DISASTER";
                    using (SqlCommand command = new SqlCommand(sql_monetray, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                monetrayInfo.ID = "" + reader.GetInt32(0);
                                monetrayInfo.IS_CHECKED = reader.GetString(1);
                                monetrayInfo.AMOUNT = reader.GetDouble(2);
                                monetrayInfo.START_DATE = reader.GetString(3);

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

                                goodsInfo.ID = "" + reader.GetInt32(0);
                                goodsInfo.IS_CHECKED = reader.GetString(1);
                                goodsInfo.START_DATE = reader.GetString(2);
                                goodsInfo.NUM_OF_ITEMS = reader.GetInt32(3);
                                goodsInfo.CATEGORY = reader.GetString(4);
                                goodsInfo.NEW_CATEGORY = reader.GetString(5);
                                goodsInfo.DESCRIPTION = reader.GetString(6);

                                listgoo.Add(goodsInfo);
                            }
                        }
                    }
                    //
                    using (SqlCommand command = new SqlCommand(sql_disaster, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                disasterInfo.ID = "" + reader.GetInt32(0);
                                disasterInfo.DESCRIPTION = reader.GetString(1);
                                disasterInfo.DISASTER_NAME = reader.GetString(2);
                                disasterInfo.START_DATE = reader.GetString(3);
                                disasterInfo.END_DATE = reader.GetString(4);
                                disasterInfo.AID_TYPE = reader.GetString(5);
                                disasterInfo.LOCATION = reader.GetString(6);

                                listdis.Add(disasterInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Expection: " + ex.ToString());
            }
            //Goods Donations

        }
       
    }
    
}
