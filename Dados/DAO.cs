using System.Collections.Generic;
using System.Data;
using Dominio;
using Dominio.Entidades;
using Microsoft.Data.Sqlite;

namespace Dados
{
    public class DAO
    {
        private readonly SqliteConnection _conexao;

        public DAO()
        {
            _conexao = new SqliteConnection("Data Source=../Dados/store.db");
        }

        public List<Produto> TodosProdutos()
        {
            _conexao.Open();
            List<Produto> lista = new List<Produto>();
            SqliteCommand cmd = _conexao.CreateCommand();
            cmd.CommandText = "SELECT * FROM Produtos;";
            cmd.CommandType = CommandType.Text;
            SqliteDataReader data = cmd.ExecuteReader();
            while (data.Read())
            {
                Produto p = new Produto();
                p.id = data.GetInt32(0);
                p.nome = data.GetString(1);
                p.preco = data.GetDouble(2);
                p.imagem = data.GetString(3);
                lista.Add(p);
            }
            _conexao.Close();
            return lista;
        }

        public Usuario Login(string email, string senha)
        {
            _conexao.Open();
            SqliteCommand cmd = _conexao.CreateCommand();
            cmd.CommandText = "SELECT * FROM Usuarios WHERE email = '" + email + "' and senha = '" + senha + "';";
            cmd.CommandType = CommandType.Text;
            SqliteDataReader data = cmd.ExecuteReader();
            Usuario usuario = new Usuario();
            while (data.Read())
            {
                usuario.id = data.GetInt32(0);
                usuario.nome = data.GetString(1);
                usuario.email = data.GetString(2);
                usuario.senha = data.GetString(3);
            }
            _conexao.Close();
            return usuario;
        }

        public List<Produto> Carrinho(int id)
        {
            _conexao.Open();
            List<Produto> lista = new List<Produto>();
            SqliteCommand cmd = _conexao.CreateCommand();
            cmd.CommandText = "SELECT c.id, p.nome, p.preco FROM Carrinhos AS c JOIN Produtos AS p ON c.idProduto = p.id WHERE c.idUsuario = " + id + ";";
            cmd.CommandType = CommandType.Text;
            SqliteDataReader data = cmd.ExecuteReader();
            while (data.Read())
            {
                Produto p = new Produto();
                p.id = data.GetInt32(0);
                p.nome = data.GetString(1);
                p.preco = data.GetDouble(2);
                lista.Add(p);
            }
            _conexao.Close();
            return lista;
        }

        public void AddProdutoCarrinho(int idProduto, int idUsuario)
        {
            _conexao.Open();
            SqliteCommand cmd = _conexao.CreateCommand();
            cmd.CommandText = "INSERT INTO Carrinhos (idProduto, idUsuario) VALUES (" + idProduto + "," + idUsuario + ");";
            cmd.CommandType = CommandType.Text;
            int rows = cmd.ExecuteNonQuery();
            _conexao.Close();

        }

        public void RemoveProdutoCarrinho(int id)
        {
            _conexao.Open();
            SqliteCommand cmd = _conexao.CreateCommand();
            cmd.CommandText = "DELETE FROM Carrinhos WHERE id = " + id + ";";
            cmd.CommandType = CommandType.Text;
            int rows = cmd.ExecuteNonQuery();
            _conexao.Close();
        }

        public void Finalizar(int id)
        {
            _conexao.Open();
            SqliteCommand cmd = _conexao.CreateCommand();
            cmd.CommandText = "INSERT INTO Compras SELECT id, idProduto, idUsuario FROM Carrinhos WHERE idUsuario = " + id + "";
            cmd.CommandType = CommandType.Text;
            int rows = cmd.ExecuteNonQuery();
            if (rows != 0)
            {
                SqliteCommand _cmd = _conexao.CreateCommand();
                _cmd.CommandText = "DELETE FROM Carrinhos WHERE idUsuario = " + id + ";";
                _cmd.CommandType = CommandType.Text;
                int row = _cmd.ExecuteNonQuery();
            }
            _conexao.Close();
        }

        public List<Pagamento> FormaPagamento()
        {
            _conexao.Open();
            List<Pagamento> lista = new List<Pagamento>();
            SqliteCommand cmd = _conexao.CreateCommand();
            cmd.CommandText = "SELECT * FROM Forma_Pagamento;";
            cmd.CommandType = CommandType.Text;
            SqliteDataReader data = cmd.ExecuteReader();
            while (data.Read())
            {
                Pagamento p = new Pagamento();
                p.id = data.GetInt32(0);
                p.formaDePagamento = data.GetString(1);
                lista.Add(p);
            }
            _conexao.Close();
            return lista;
        }

        public List<Produto> Compras(int id)
        {
            _conexao.Open();
            List<Produto> lista = new List<Produto>();
            SqliteCommand cmd = _conexao.CreateCommand();
            cmd.CommandText = "SELECT c.id, p.nome, p.preco FROM Compras AS c JOIN Produtos AS p ON c.idProduto = p.id WHERE c.idUsuario = " + id + ";";
            cmd.CommandType = CommandType.Text;
            SqliteDataReader data = cmd.ExecuteReader();
            while (data.Read())
            {
                Produto p = new Produto();
                p.id = data.GetInt32(0);
                p.nome = data.GetString(1);
                p.preco = data.GetDouble(2);
                lista.Add(p);
            }
            _conexao.Close();
            return lista;
        }

    }
}