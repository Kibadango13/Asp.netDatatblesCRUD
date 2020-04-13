using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webAppMVC.Models
{
    public class DetalleCLS
    {
        [Display(Name = "IIDDETALLE")]
        public int IIDDETALLE { get; set; }

        [Display(Name = "IIDFACTURA")]
        public int IIDFACTURA { get; set; }

        [Display(Name = "NOMBREPRODUCTO")]
        public String NOMBREPRODUCTO { get; set; }

        [Display(Name = "CANTIDAD")]
        public decimal CANTIDAD { get; set; }

        [Display(Name = "PRECIOuNITARIO")]
        public decimal PRECIO_UNITARIO { get; set; }


        [Display(Name = "TOTAL")]
        public decimal TOTAL { get; set; }

        [Display(Name = "ESTADO")]
        public int ESTADO { get; set; }

        //atributos adicionales para datatables 

        [Display(Name = "NUMERO_REGISTRO")]
        public int NUMERO_REGISTRO { get; set; }

        [Display(Name = "ACCION")]
        public string ACCION { get; set; }


    }
}