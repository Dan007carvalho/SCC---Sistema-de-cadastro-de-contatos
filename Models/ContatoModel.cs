using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroContatos.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Obrigatório o nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Obrigatório o email")]
        [EmailAddress(ErrorMessage ="Email inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Obrigatório o telefone")]
        [Phone(ErrorMessage ="Telefone inválido")]
        public string Telefone { get; set; }
        public int UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}
