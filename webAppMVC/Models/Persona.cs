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
    
    public partial class Persona
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Persona()
        {
            this.FACTURAs = new HashSet<FACTURA>();
        }
    
        public int IIDPersona { get; set; }
        public Nullable<int> IIDCUIDAD { get; set; }
        public string CEDULA { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDO { get; set; }
        public string TELEFONO { get; set; }
        public Nullable<int> ESTADO { get; set; }
    
        public virtual CUIDAD CUIDAD { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FACTURA> FACTURAs { get; set; }
    }
}
