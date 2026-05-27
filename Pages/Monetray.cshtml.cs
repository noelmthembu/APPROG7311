using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace APPROG7311.Pages
{
    public class MonetrayModel : PageModel
    {
        public MonetrayInfo monetrayInfo = new MonetrayInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            monetrayInfo.IS_CHECKED = Request.Form["IS_CHECKED"];
            monetrayInfo.AMOUNT = Convert.ToDouble(Request.Form["AMOUNT"]);
            monetrayInfo.START_DATE = Request.Form["START_DATE"];




            try
            {
                String connectionString = "Data Source=disaster-alleviation-foundation.database.windows.net;Initial Catalog=Disaster_Alleviation_Foundation_DB;User ID=dafadmin;Password=P@ssw0rd;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO MONETRAY VALUES(@IS_CHECKED, @AMOUNT, @START_DATE)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@IS_CHECKED", monetrayInfo.IS_CHECKED);
                        command.Parameters.AddWithValue("@AMOUNT", monetrayInfo.AMOUNT);
                        command.Parameters.AddWithValue("@START_DATE", monetrayInfo.START_DATE);
                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            monetrayInfo.IS_CHECKED = "";
            monetrayInfo.AMOUNT = 0;
            monetrayInfo.START_DATE = "";
            successMessage = "Added Successfully!";

        }
        public class MonetrayInfo
        {
            public String id;
            public String IS_CHECKED;
            public Double AMOUNT;
            public String START_DATE;

        }
    }
}
