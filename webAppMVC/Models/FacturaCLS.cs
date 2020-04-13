using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webAppMVC.Models
{
    public class FacturaCLS
    {
        // Tabla de Facturacion
        [Display(Name = "IIDFACTURA")]
        public int IIDFACTURA { get; set; }

        [Display(Name = "IIDPERSONA")]
        public int IIDPERSONA { get; set; }

        [Display(Name = "Numero de Factura")]
        public String NUMFACTURA { get; set; }

        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FECHA { get; set; }

        [Display(Name = "IVA")]
        public String IVA { get; set; }

        [Display(Name = "ESTADO")]
        public string ESTADO { get; set; }

        //Propiedade adicionale para insertar el detalle dinamicamente como object
        public virtual IList<DetalleCLS> DETALLE { get; set; }
        public virtual IList<PersonaCLS> Persona { get; set; }

        //ATRIBUTES PARA DATATABLE


        [Display(Name = "IIDDETALLE")]
        public int IIDDETALLE { get; set; }

        [Display(Name = "TOTAL")]
        public string TOTAL_FACTURA { get; set; }

        [Display(Name = "NOMBRE")]
        [StringLength(5, ErrorMessage = "Longitud maxima es 5")]
        public String NOMBRE { get; set; }

        [Display(Name = "Cuidad")]
        [StringLength(5, ErrorMessage = "Longitud maxima es 5")]
        public String CUIDAD { get; set; }

        [Display(Name = "TELEFONO")]
        [StringLength(5, ErrorMessage = "Longitud maxima es 5")]
        public String TELEFONO { get; set; }
    }

}