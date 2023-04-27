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
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string TokenRefresh { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime ExpiraEm { get; set; }
    }
}