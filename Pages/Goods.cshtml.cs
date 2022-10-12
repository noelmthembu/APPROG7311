using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace APPROG7311.Pages
{
    public class GoodsModel : PageModel
    {
        public GoodsInfo goodsInfo = new GoodsInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
;           goodsInfo.IS_CHECKED = Request.Form["IS_CHECKED"];
            goodsInfo.START_DATE = Request.Form["START_DATE"];
            goodsInfo.NUM_OF_ITEMS = Convert.ToInt32(Request.Form["NUM_OF_ITEMS"]);
            goodsInfo.CATEGORY = Request.Form["CATEGORY"];
            goodsInfo.NEW_CATEGORY = Request.Form["NEW_CATEGORY"];
            goodsInfo.DESCRIPTION = Request.Form["DESCRIPTION"];




            try
            {
                String connectionString = "Server=tcp:disaster-alleviation-foundation.database.windows.net,1433;Initial Catalog=Disaster_Alleviation_Foundation_DB;Persist Security Info=False;User ID=dafadmin;Password=P@ssw0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO GOODS VALUES(@IS_CHECKED, @START_DATE, @NUM_OF_ITEMS, @CATEGORY,@NEW_CATEGORY, @DESCRIPTION)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@IS_CHECKED", goodsInfo.IS_CHECKED);
                        command.Parameters.AddWithValue("@START_DATE", goodsInfo.START_DATE);
                        command.Parameters.AddWithValue("@NUM_OF_ITEMS", goodsInfo.NUM_OF_ITEMS);
                        command.Parameters.AddWithValue("@CATEGORY", goodsInfo.CATEGORY);
                        command.Parameters.AddWithValue("@NEW_CATEGORY", goodsInfo.NEW_CATEGORY);
                        command.Parameters.AddWithValue("@DESCRIPTION", goodsInfo.DESCRIPTION);
                        

                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            goodsInfo.NUM_OF_ITEMS = 0;
            goodsInfo.DESCRIPTION = "";
            goodsInfo.START_DATE = "";
            goodsInfo.CATEGORY = "";
            goodsInfo.NEW_CATEGORY = "";
            successMessage = "Added Successfully!";
            Response.Redirect("/Payments");

        }
        public class GoodsInfo
        {
            public String ID;
            public String IS_CHECKED;
            public String START_DATE;
            public int NUM_OF_ITEMS;
            public String CATEGORY;
            public String NEW_CATEGORY;
            public String DESCRIPTION;

        }
    }
}
