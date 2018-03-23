using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Text;
using System.Web.Script.Serialization;

namespace Demo
{
    /// <summary>
    /// Summary description for GetEvents
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class GetEvents : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public void GetEventNames()
        {
            List<EveName> eve = new List<EveName>();
            string conn = ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(conn))
            {
                SqlCommand command = new SqlCommand("select * from Events where salesrep=11425",connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    EveName e1 = new EveName();
                    e1.eName = (reader["Name"].ToString());
                    e1.ID = Convert.ToInt32(reader["ID"]);
                    eve.Add(e1);
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(eve));
            }
        }
    }

    public class EveName
    {
        public string eName { get; set; }
        public int ID { get; set; }
    }
}
