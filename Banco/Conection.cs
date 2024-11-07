using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;
namespace ScreenSound.Banco;

public class Connection
{
     private string connectionString = "Server=localhost;Database=master;Integrated Security=True;TrustServerCertificate=True;";


    public SqlConnection ObterConexao()
    {
        return new SqlConnection(connectionString);
    }
}