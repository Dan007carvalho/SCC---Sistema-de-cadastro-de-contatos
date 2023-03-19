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
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        private readonly IEmail _email;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao, IEmail email)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _email = email;
        }

        public IActionResult Index()
        {
            // Se estiver logado, redirecionar para Home, senão retornar a tela de login
            if (_sessao.BuscarSessao() != null) return RedirectToAction("Index", "Home");
            return View();
        }

        public IActionResult RedefinirSenha()
        {
            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessao();
            return RedirectToAction("Index","Login");
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);

                    if (usuario != null)
                    {
                        if (usuario.SenhaValida(loginModel.Senha))
                        {
                            _sessao.CriarSessao(usuario);
                            return RedirectToAction("Index", "Home");
                        }
                        
                         TempData["MensagemErro"] = "Senha inválida, tente novamente!";
                           
                    }

                    TempData["MensagemErro"] = "Login ou senha inválidos, tente novamente!";
                }

                return View("Index");
            }
            catch (Exception e)
            {

                TempData["MensagemErro"] = "Ocorreu um erro no login, tente novamente! Detalhe do erro: " + e;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult LinkRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailELogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);

                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();
                       

                        string mensagem = "Sua nova senha é: " + novaSenha;

                       bool emailEnviado = _email.Enviar(usuario.Email, "Redefinição de senha", mensagem);

                        if (emailEnviado)
                        {
                            _usuarioRepositorio.Atualizar(usuario);
                        }
                        else
                        {
                            TempData["MensagemErro"] = "Não conseguimos enviar o email. Por favor, verifique os dados e tente novamente!";
                        }

                        TempData["MensagemSucesso"] = "Enviamos uma nova senha para seu email cadastrado!";
                        return RedirectToAction("Index", "Login");

                    }
                    
                    TempData["MensagemErro"] = "Login ou email inválidos. Por favor, verifique os dados e tente novamente!";
                    
                }

                return View("RedefinirSenha");
            }
            catch (Exception e)
            {

                TempData["MensagemErro"] = "Ocorreu um erro ao redefinir senha, verifique os dados! Detalhe do erro: " + e;
                return RedirectToAction("Index");
            }

        }
    }
}
