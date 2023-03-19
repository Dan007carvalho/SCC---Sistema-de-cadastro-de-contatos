using CadastroContatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroContatos.Repositorio
{
    public interface IContatoRepositorio
    {
        bool Excluir(int id);
        ContatoModel Atualizar(ContatoModel contato);
        ContatoModel BuscarPorId(int id);
        List<ContatoModel> ListarTodos(int usuarioId);
        ContatoModel Adicionar(ContatoModel contato);
    }
}
