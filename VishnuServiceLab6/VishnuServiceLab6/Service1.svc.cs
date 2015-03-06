using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace VishnuServiceLab7
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "queryInfo/{userName}")]
        public string AuthenticateUser(string userName)
        {

            //Declare Connection by passing the connection string from the web config file
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);

            //Open the connection
            conn.Open();

            //int cnt = 0;
            //string uname = "";
            string passw = "";

            //Declare the sql command
            //SqlCommand cmd = new SqlCommand("Select count(0) as Count From LoginAuth where UserName='" + userName + "'", conn);
            //SqlCommand user = new SqlCommand("Select UserName From LoginAuth where UserName='" + userName + "'", conn);
            SqlCommand pass = new SqlCommand("Select Password From LoginAuth where UserName='" + userName + "'", conn);
            
            //SqlDataReader ureader = user.ExecuteReader();
            SqlDataReader preader = pass.ExecuteReader();
            //while (ureader.Read())
            //{
                //cnt = Convert.ToInt32(reader["Count"]);
                //uname = Convert.ToString(ureader["UserName"]);
            //}

             while (preader.Read())
            {
                //cnt = Convert.ToInt32(reader["Count"]);
                passw = Convert.ToString(preader["Password"]);
            }

            //ureader.Close();
            preader.Close();
            //close the connection
            conn.Close();

            //if (uname != null)
            //{
                return passw;
            //}
            //else
            //{
            //    return null;
            //}

            //if (cnt == 1)
            //{
                //string temp = "Welcome" + userName.ToString();
                //return temp;
            //}
            //else
            //{
            //    return "Username not available";
            //}

            //return uname;
            //return cnt.ToString();
            //return "Vishnu";
            //return uname;

        }
    }
}
