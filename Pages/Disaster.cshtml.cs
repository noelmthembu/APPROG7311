using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace APPROG7311.Pages
{
    public class DisasterModel : PageModel
    {
        public DisasterInfo disasterInfo = new DisasterInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            disasterInfo.DESCRIPTION = Request.Form["DESCRIPTION"];
            disasterInfo.DISASTER_NAME = Request.Form["DISASTER_NAME"];
            disasterInfo.START_DATE = Request.Form["START_DATE"];
            disasterInfo.END_DATE = Request.Form["END_DATE"];
            disasterInfo.AID_TYPE = Request.Form["AID_TYPE"];
            disasterInfo.LOCATION = Request.Form["LOCATION"];




            try 
            {
                String connectionString = "Server=tcp:disaster-alleviation-foundation.database.windows.net,1433;Initial Catalog=Disaster_Alleviation_Foundation_DB;Persist Security Info=False;User ID=dafadmin;Password=P@ssw0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO DISASTER VALUES(@DESCRIPTION, @DISASTER_NAME, @START_DATE, @END_DATE, @AID_TYPE, @LOCATION)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@DESCRIPTION", disasterInfo.DESCRIPTION);
                        command.Parameters.AddWithValue("@DISASTER_NAME", disasterInfo.DISASTER_NAME);
                        command.Parameters.AddWithValue("@START_DATE", disasterInfo.START_DATE);
                        command.Parameters.AddWithValue("@END_DATE", disasterInfo.END_DATE);
                        command.Parameters.AddWithValue("@AID_TYPE", disasterInfo.AID_TYPE);
                        command.Parameters.AddWithValue("@LOCATION", disasterInfo.LOCATION);
                        command.ExecuteNonQuery();

                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            disasterInfo.DESCRIPTION = "";
            disasterInfo.LOCATION = "";
            disasterInfo.AID_TYPE = "";
            disasterInfo.START_DATE = "";
            disasterInfo.END_DATE = "";
            successMessage = "Added Successfully!";
        }
        public class DisasterInfo
        {
            public String ID;
            public String DESCRIPTION;
            public String DISASTER_NAME;
            public String START_DATE;
            public String END_DATE;
            public String LOCATION;
            public String AID_TYPE;

        }
    }
}
