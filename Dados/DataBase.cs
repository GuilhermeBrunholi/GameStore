using System.Collections.Generic;
using Dominio;
using Dominio.Entidades;

namespace Dados
{
    public class DataBase
    {
        public List<Produto> ListaProdutos()
        {
            List<Produto> dbProduto = new List<Produto>();

            Produto prod1 = new Produto();
            Produto prod2 = new Produto();
            Produto prod3 = new Produto();
            Produto prod4 = new Produto();

            prod1.id = 1;
            prod1.nome = "Counter-Strike";
            prod1.preco = 15.99;
            prod1.imagem = "http://jstationx.com/wp-content/uploads/2016/01/Counter-Strike-Global-Offensive-700x400.png";
            prod1.descricao = "Nada";

            prod2.id = 2;
            prod2.nome = "Fortnite";
            prod2.preco = 5.99;
            prod2.imagem = "https://cdn3.i-scmp.com/sites/default/files/styles/700x400/public/images/methode/2018/08/15/1bd025fe-a042-11e8-90bf-ccc49f9b020a_image_hires_122932.JPG?itok=Hz_25QG_&v=1534307360";
            prod2.descricao = "Nada";

            prod3.id = 3;
            prod3.nome = "Grand Theft Auto V";
            prod3.preco = 49.99;
            prod3.imagem = "https://cdn.i-scmp.com/sites/default/files/styles/700x400/public/2018/09/17/grand-theft-auto-v-wide_0.jpg?itok=Cfw4nW2B&v=1537180611";
            prod3.descricao = "Nada";

            prod4.id = 4;
            prod4.nome = "Red Dead Redemption II";
            prod4.preco = 199.99;
            prod4.imagem = "https://imgrosetta.mynet.com.tr/file/9991582/9991582-728xauto.jpg";
            prod4.descricao = "Nada";

            dbProduto.Add(prod1);
            dbProduto.Add(prod2);
            dbProduto.Add(prod3);
            dbProduto.Add(prod4);

            return dbProduto;
        }

        public List<Usuario> ListaUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();
            Usuario use1 = new Usuario();
            Usuario use2 = new Usuario();

            use1.id = 1;
            use1.nome = "Usuario1";
            use1.email = "Email1@emil.com";
            use1.senha = "000001";

            use2.id = 2;
            use2.nome = "Usuario2";
            use2.email = "Email2@emil.com";
            use2.senha = "000002";

            lista.Add(use1);
            lista.Add(use2);
            return lista;
        }

        public List<Carrinho> Carrinhos()
        {
            List<Carrinho> carrinho = new List<Carrinho>();

            Carrinho c1 = new Carrinho();
            Carrinho c2 = new Carrinho();

            c1.id = 1;
            c1.idProduto = 1;
            c1.idUsuario = 1;

            c2.id = 2;
            c2.idProduto = 2;
            c2.idUsuario = 2;

            carrinho.Add(c1);
            carrinho.Add(c2);
            return carrinho;
        }
    }
}