using CadastroContatos.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroContatos.Models
{
    public class UsuarioSemSenhaModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Obrigatório o nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Obrigatório o login")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Obrigatório o email")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Obrigatório o perfil")]
        public PerfilEnum? Perfil { get; set; }
        

    }
}
