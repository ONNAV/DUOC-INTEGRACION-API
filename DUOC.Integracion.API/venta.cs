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
    
    public partial class venta
    {
        public int crr_venta { get; set; }
        public Nullable<System.DateTime> fec_venta { get; set; }
        public Nullable<int> subtotal { get; set; }
        public Nullable<int> iva { get; set; }
        public Nullable<int> total { get; set; }
        public Nullable<int> medio_pago_cod_mediopago { get; set; }
        public Nullable<short> cuotas { get; set; }
        public Nullable<int> vuelto { get; set; }
        public Nullable<int> usuario_persona_rut { get; set; }
    }
}