using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static APPROG7311.Pages.MonetrayModel;

namespace APPROG7311.Pages.Admin
{
    public class Edit_MonetrayModel : PageModel
    {
        public MonetrayInfo monetrayInfo = new MonetrayInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String ID = Request.Query["id"];

            try 
            {
                String connectionString = "Server=tcp:disaster-alleviation-foundation.database.windows.net,1433;Initial Catalog=Disaster_Alleviation_Foundation_DB;Persist Security Info=False;User ID=dafadmin;Password=P@ssw0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql_monetray = "SELECT * FROM MONETRAY WHERE MON_ID = @id;";
                    using (SqlCommand command = new SqlCommand(sql_monetray, connection))
                    {
                        command.Parameters.AddWithValue("@id", ID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                monetrayInfo.ID = "" + reader.GetInt32(0);
                                monetrayInfo.IS_CHECKED = reader.GetString(1);
                                monetrayInfo.AMOUNT = reader.GetDouble(2);
                                monetrayInfo.START_DATE = reader.GetString(3);

                            }
                        }
                    }
                }

                }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost()
        {
            monetrayInfo.ID = Request.Form["id"];
            monetrayInfo.IS_CHECKED = Request.Form["IS_CHECKED"];
            monetrayInfo.AMOUNT = Convert.ToDouble(Request.Form["AMOUNT"]);
            monetrayInfo.START_DATE = Request.Form["START_DATE"];


            try
            {
                String connectionString = "Server=tcp:disaster-alleviation-foundation.database.windows.net,1433;Initial Catalog=Disaster_Alleviation_Foundation_DB;Persist Security Info=False;User ID=dafadmin;Password=P@ssw0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE MONETRAY SET IS_CHECKED = @IS_CHECKED, AMOUNT = @AMOUNT, START_DATE = @START_DATE WHERE MON_ID = @id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@IS_CHECKED", monetrayInfo.IS_CHECKED);
                        command.Parameters.AddWithValue("@AMOUNT", monetrayInfo.AMOUNT);
                        command.Parameters.AddWithValue("@START_DATE", monetrayInfo.START_DATE);
                        command.Parameters.AddWithValue("@id", monetrayInfo.ID);
                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }
}
