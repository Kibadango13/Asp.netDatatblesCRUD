//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace webAppMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DETALLE
    {
        public int IIDDETALLE { get; set; }
        public int IIDFACTURA { get; set; }
        public string NOMBREPRODUCTO { get; set; }
        public Nullable<decimal> CANTIDAD { get; set; }
        public Nullable<decimal> PRECIO_UNITARIO { get; set; }
        public Nullable<decimal> TOTAL { get; set; }
        public Nullable<int> ESTADO { get; set; }
    
        public virtual FACTURA FACTURA { get; set; }
    }
}