//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DUOC.Integracion.API
{
    using System;
    using System.Collections.Generic;
    
    public partial class servicio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public servicio()
        {
            this.ejecucion_servicio = new HashSet<ejecucion_servicio>();
        }
    
        public int cod_servicio { get; set; }
        public string gls_servicio { get; set; }
        public int precio { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ejecucion_servicio> ejecucion_servicio { get; set; }
    }
}