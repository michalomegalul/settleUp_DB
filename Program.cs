using Microsoft.Data.SqlClient;
using dluznik3;
new wrogram().Run();

public class wrogram
{
    public void Run()
    {
        
        new SettleUpApp().GetBalanceForUser(1);

        Console.WriteLine("Hello World!");
        /*string conStr = @"Data Source=(local);Initial Catalog=dluhy_db;User ID=user;Password=owo;Encrypt=false;Trusted_Connection=True;";
        CreateCommand(conStr);*/
    }
    private static void CreateCommand(string connectionString)
    {
        using (SqlConnection connectionuser = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand("SELECT * FROM dbo.Users", connectionuser);
            command.Connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    User user = new User(){
                        ID = reader.GetInt32(0),
                        username = reader.GetString(1),
                        password = reader.GetString(2)
                    };
                }
            }
            command.ExecuteNonQuery();
        }
        
    }
    


}

