using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webAppMVC.Models
{
    public class PersonaCLS
    {
        [Display(Name = "IIDCLIENTE")]
        public int IIDPERSONA { get; set; }

        [Display(Name = "NOMBRE")]
        public string NOMBRE { get; set; }

        [Display(Name = "APELLIDOo")]
        public string APELLIDOo { get; set; }

        [Display(Name = "Telefono")]
        public string TELEFONO { get; set; }

        [Display(Name = "ESTADO")]
        public string ESTADO { get; set; }
    }
}