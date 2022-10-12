using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace APPROG7311.Pages
{
    public class AdminsLoginModel : PageModel
    {
        public AdminInfo  adminInfo = new AdminInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            adminInfo.EMAIL = Request.Form["EMAIL"];
            adminInfo.PASSWORD = Request.Form["PASSWORD"];

            if (adminInfo.EMAIL.Length == 0 || adminInfo.PASSWORD.Length == 0)
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
                    String sql = "SELECT * FROM ADMIN WHERE EMAIL = @EMAIL AND PASSWORD = @PASSWORD;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@EMAIL", adminInfo.EMAIL);
                        command.Parameters.AddWithValue("@PASSWORD", adminInfo.PASSWORD);
                        command.ExecuteNonQuery();

                    }
                }
            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            //Register User
            adminInfo.EMAIL = "";
            adminInfo.PASSWORD = "";
            successMessage = "Logged Successfully!";
            Response.Redirect("/Admin/Admins");

            //Admin

        }
        public class AdminInfo
        {
            public String ID;
            public String EMAIL;
            public String PASSWORD;


        }
    }
}
