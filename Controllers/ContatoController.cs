using CadastroContatos.Filter;
using CadastroContatos.Helper;
using CadastroContatos.Models;
using CadastroContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroContatos.Controllers
{
    [PaginaUsuarioLogado]
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        private readonly ISessao _sessao;
        public ContatoController(IContatoRepositorio contatoRepositorio, ISessao sessao )
        {
            _contatoRepositorio = contatoRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            UsuarioModel usuarioLogado = _sessao.BuscarSessao();
            List<ContatoModel> contatos = _contatoRepositorio.ListarTodos(usuarioLogado.Id);
                return View(contatos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
           ContatoModel contato = _contatoRepositorio.BuscarPorId(id);
            return View(contato);
        }

        public IActionResult Excluir(int id)
        {
            ContatoModel contato = _contatoRepositorio.BuscarPorId(id);
            return View(contato);
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _sessao.BuscarSessao();
                    contato.UsuarioId = usuarioLogado.Id;

                    _contatoRepositorio.Adicionar(contato);
                    return RedirectToAction("Index");
                }
                return View(contato);
            }
            catch (Exception e)
            {

                TempData["MensagemErro"] = "Ocorreu um erro ao cadastrar um contato, tente novamente! Detalhe do erro: " + e;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _sessao.BuscarSessao();
                    contato.UsuarioId = usuarioLogado.Id;

                    _contatoRepositorio.Atualizar(contato);
                    return RedirectToAction("Index");
                }
                return View("Editar", contato);
            }
            catch (Exception e)
            {

                TempData["MensagemErro"] = "Ocorreu um erro ao editar um contato, tente novamente! Detalhe do erro: " + e;
                return RedirectToAction("Index");
            }
        }
        
        public IActionResult Apagar(int id)
        {
            try
            {
                _contatoRepositorio.Excluir(id);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {

                TempData["MensagemErro"] = "Ocorreu um erro ao excluir um contato, tente novamente! Detalhe do erro: " + e;
                return RedirectToAction("Index");
            }
        }
    }
}
