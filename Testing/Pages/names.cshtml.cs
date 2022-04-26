using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;


namespace Testing.Pages
{

    public class db
    {
       SqlConnection con;
        public db()
    {

        var configuration = GetConfiguration();
            con = new SqlConnection(configuration.GetSection("ConnectionStrings").Value);
    }

    public IConfigurationRoot GetConfiguration()
    {
        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        return builder.Build();
    }
}
    public class namesModel : PageModel
    {
        public List<NamesInfo> ListNames = new List<NamesInfo>();
        public void OnGet()
        {
            db dbcon = new db();
                try
            {
                string namesconnection = "Data Source=ALEXANDER\\BELINA;Initial Catalog=webapp;Persist Security Info=True;User ID=sa;Password=@unt13c0nn13";
                using (SqlConnection connection = new SqlConnection(namesconnection))
                //using (SqlConnection connection = dbcon.)
                {
                    connection.Open();
                    string sql = "SELECT * FROM name";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                NamesInfo name = new NamesInfo();
                                name.ID = reader.GetInt32(0);
                                name.fullname = reader.GetString(1);
                                ListNames.Add(name);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("SEE : " + ex);
            }

        }
    }

    public class NamesInfo    
    {
        public int ID; 
        public string fullname;
       
    }
}
