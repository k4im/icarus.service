using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.jwtManager.Models
{
    public class RefreshToken
    {

        [Key]
        public string UserEmail { get; set; }
        
        [Required(ErrorMessage = "Campo precisa ser preenchido")]
        public string TokenRefresh { get; set; }
    }
}