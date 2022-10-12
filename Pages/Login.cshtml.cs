using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static APPROG7311.Pages.AdminsModel;

namespace APPROG7311.Pages
{
    public class LoginModel : PageModel
    {
        public USERInfo userInfo = new USERInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            userInfo.EMAIL = Request.Form["EMAIL"];
            userInfo.PASSWORD = Request.Form["PASSWORD"];

            if (userInfo.EMAIL.Length == 0 || userInfo.PASSWORD.Length == 0)
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
                    String sql = "SELECT * FROM USERS WHERE EMAIL = @EMAIL AND PASSWORD = @PASSWORD;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@EMAIL", userInfo.EMAIL);
                        command.Parameters.AddWithValue("@PASSWORD", userInfo.PASSWORD);
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
            userInfo.EMAIL = "";
            userInfo.PASSWORD = "";
            successMessage = "Logged Successfully!";
            Response.Redirect("/Donate");

            //Admin
            
        }
        public class USERInfo
        {
            public String ID;
            public String USERNAME;
            public String EMAIL;
            public String PASSWORD;


        }

    }
}
