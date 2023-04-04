using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace icarus.application.Models
{
    public class HelperProject
    {
        public static string Produção = "Produção";
        public static string Finalizado = "Finalizado";
        public static string Confirmado = "Confirmado";
        public static string Pendente = "Pendente";
    
       public static List<SelectListItem> GetStatus()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem {Text = HelperProject.Produção, Value = HelperProject.Produção},
                new SelectListItem {Text = HelperProject.Finalizado, Value = HelperProject.Finalizado},
                new SelectListItem {Text = HelperProject.Confirmado, Value = HelperProject.Confirmado},
                new SelectListItem {Text = HelperProject.Pendente, Value = HelperProject.Pendente}
            };
        }

    }
}