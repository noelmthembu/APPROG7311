using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static APPROG7311.Pages.DisasterModel;

namespace APPROG7311.Pages
{
    public class PaymentsModel : PageModel
    {
        public PaymentInfo paymentInfo = new PaymentInfo();
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
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Expection: " + ex.ToString());
            }
        }
        public void OnPost() 
        {
            paymentInfo.PAYER_NAME = Request.Form["PAYER_NAME"];
            paymentInfo.PAYMENT = Convert.ToDouble(Request.Form["PAYMENT"]);
            paymentInfo.ACTIVE_DISASTER = Request.Form["ACTIVE_DISASTER"];

            if (paymentInfo.ID.Length == 0 || paymentInfo.PAYER_NAME.Length == 0 ||
                paymentInfo.PAYMENT.ToString().Length == 0 || paymentInfo.ACTIVE_DISASTER.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

        }
        public class PaymentInfo
        {
            public String ID;
            public String PAYER_NAME;
            public Double PAYMENT;
            public String ACTIVE_DISASTER;


        }
    }
}
