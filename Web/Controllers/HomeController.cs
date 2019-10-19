using Dados;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly DAO _db;

        public HomeController()
        {
            _db = new DAO();
        }
        public IActionResult Index()
        {
            var produtos = _db.TodosProdutos();
            ViewBag.games = produtos;
            ViewBag.use = HttpContext.Session.GetInt32("idUser");
            ViewBag.usuario = HttpContext.Session.GetString("nomeUser");
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            var usuario = _db.Login(email, senha);
            if (usuario == null || usuario.id == 0)
            {
                return View();
            }
            else
            {
                ViewBag.use = usuario.id;
                HttpContext.Session.SetInt32("idUser", usuario.id);
                HttpContext.Session.SetString("nomeUser", usuario.nome);
                return RedirectToAction("Index");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.SetInt32("idUser", 0);
            return RedirectToAction("Index");
        }

        public IActionResult Carrinho()
        {
            ViewBag.use = HttpContext.Session.GetInt32("idUser");
            ViewBag.usuario = HttpContext.Session.GetString("nomeUser");
            var id = HttpContext.Session.GetInt32("idUser");
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var produtos = _db.Carrinho(Convert.ToInt32(id));
            double valorTotal = 0;
            foreach (var item in produtos)
            {
                valorTotal += item.preco;
            }
            ViewBag.produtos = produtos;
            ViewBag.valorTotal = valorTotal;
            return View();
        }

        public IActionResult AdcCarrinho(int id)
        {
            var idUsuario = HttpContext.Session.GetInt32("idUser");
            if (idUsuario == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                _db.AddProdutoCarrinho(id, Convert.ToInt32(idUsuario));
                return RedirectToAction("Carrinho");
            }
        }

        public IActionResult RemoveItemCarrinho(int id)
        {
            _db.RemoveProdutoCarrinho(id);
            return RedirectToAction("Carrinho");
        }

        public IActionResult FinalizarCompra()
        {
            ViewBag.use = HttpContext.Session.GetInt32("idUser");
            ViewBag.usuario = HttpContext.Session.GetString("nomeUser");
            ViewBag.forma = _db.FormaPagamento();
            return View();
        }

        [HttpPost]
        public IActionResult ConfirmarCompra(int id)
        {
            var idUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("idUser"));
            _db.Finalizar(idUsuario);
            return RedirectToAction("Carrinho");
        }

        public IActionResult Compras()
        {
            ViewBag.use = HttpContext.Session.GetInt32("idUser");
            ViewBag.usuario = HttpContext.Session.GetString("nomeUser");
            var idUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("idUser"));
            ViewBag.produtos = _db.Compras(idUsuario);
            return View();
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        public IActionResult FinalizarCadastro(string nome, string email, string senha)
        {
            _db.NovoUsuario(nome, email, senha);
            return RedirectToAction("Login");
        }

    }
}
