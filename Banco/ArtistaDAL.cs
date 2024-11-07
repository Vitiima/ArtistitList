using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;

namespace ScreenSound.Banco
{
    public class ArtistaDAL
    { 
        // Método para listar artistas
        public IEnumerable<Artista> ListarArtistas()
        {
            var connection = new Connection().ObterConexao();
            var lista = new List<Artista>();
            connection.Open();
         
            string dataConsult = "SELECT * FROM Artistas";
            SqlCommand command = new SqlCommand(dataConsult, connection);
            using (SqlDataReader reader = command.ExecuteReader()) 
            {
                while (reader.Read())
                {
                    string nomeArtista = Convert.ToString(reader["Nome"]);
                    string bioArtista = Convert.ToString(reader["Bio"]); // corrigido de "Descricao" para "Bio"
                    int id = Convert.ToInt32(reader["Id"]); // corrigido de "ID" para "Id" (consistência com a coluna)
                    Artista artista = new Artista(nomeArtista, bioArtista) { Id = id };
                    lista.Add(artista);
                }
            }
            
            connection.Close();
            return lista;
        }

        // Método para inserir um novo artista
        public void InserirArtista(Artista artista)
        {
            var connection = new Connection().ObterConexao();
            connection.Open();
            
            string dataInsert = "INSERT INTO Artistas (Nome, FotoPerfil, Bio) VALUES (@nome, @perfilPadrao, @bio)";
            SqlCommand command = new SqlCommand(dataInsert, connection);

            command.Parameters.AddWithValue("@nome", artista.Nome);
            command.Parameters.AddWithValue("@perfilPadrao", artista.FotoPerfil);
            command.Parameters.AddWithValue("@bio", artista.Bio);

            int retorno = command.ExecuteNonQuery();
            
            Console.WriteLine($"Linhas afetadas: {retorno}");
            connection.Close();
        }

        // Método para atualizar um artista
        public void AlterarArtista(Artista artista)
        {
            var connection = new Connection().ObterConexao();
            connection.Open();

            string dataAlter = "UPDATE Artistas SET Nome = @nome, FotoPerfil = @fotoPerfil, Bio = @bio WHERE Id = @id";
            SqlCommand command = new SqlCommand(dataAlter, connection);

            command.Parameters.AddWithValue("@nome", artista.Nome);
            command.Parameters.AddWithValue("@fotoPerfil", artista.FotoPerfil);
            command.Parameters.AddWithValue("@bio", artista.Bio);
            command.Parameters.AddWithValue("@id", artista.Id);
            
            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas: {retorno}");
            connection.Close();
        }

        // Método para excluir um artista
        public void ExcluirArtista(Artista artista)
        {
            var connection = new Connection().ObterConexao();
            connection.Open();

            string dataDelete = "DELETE FROM Artistas WHERE Id = @id";
            SqlCommand command = new SqlCommand(dataDelete, connection);

            command.Parameters.AddWithValue("@id", artista.Id); // Apenas o parâmetro "@id" é necessário para exclusão
            
            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas: {retorno}");
            connection.Close();
        }
    }
}
