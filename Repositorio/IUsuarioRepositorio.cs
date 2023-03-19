using CadastroContatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroContatos.Repositorio
{
    public interface IUsuarioRepositorio
    {
        UsuarioModel BuscarPorLogin(string login);
        UsuarioModel BuscarPorEmailELogin(string email, string login);
        bool Excluir(int id);
        UsuarioModel Atualizar(UsuarioModel usuario);
        UsuarioModel BuscarPorId(int id);
        List<UsuarioModel> ListarTodos();
        UsuarioModel Adicionar(UsuarioModel usuario);
        UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel);
    }
}
