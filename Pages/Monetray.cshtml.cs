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
            monetrayInfo.END_DATE = Request.Form["END_DATE"];

            if (monetrayInfo.ID.Length == 0 || monetrayInfo.IS_CHECKED.Length == 0 ||
                monetrayInfo.AMOUNT.ToString().Length == 0 || monetrayInfo.START_DATE.Length == 0 ||
                monetrayInfo.END_DATE.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }



            try
            {
                String connectionString = "Server=tcp:disaster-alleviation-foundation.database.windows.net,1433;Initial Catalog=Disaster_Alleviation_Foundation_DB;Persist Security Info=False;User ID=dafadmin;Password=P@ssw0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO MONETARY VALUES(@IS_CHECKED, @AMOUNT, @START_DATE, @END_DATE)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@IS_CHECKED", monetrayInfo.IS_CHECKED);
                        command.Parameters.AddWithValue("@AMOUNT", monetrayInfo.AMOUNT);
                        command.Parameters.AddWithValue("@START_DATE", monetrayInfo.START_DATE);
                        command.Parameters.AddWithValue("@END_DATE", monetrayInfo.END_DATE);
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
            monetrayInfo.END_DATE = "";
            successMessage = "Added Successfully!";

        }
        public class MonetrayInfo
        {
            public String ID;
            public String IS_CHECKED;
            public Double AMOUNT;
            public String START_DATE;
            public String END_DATE;

        }
    }
}
