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
    
    public partial class persona
    {
        public int rut { get; set; }
        public decimal dv { get; set; }
        public string nombre { get; set; }
        public string paterno { get; set; }
        public string materno { get; set; }
        public Nullable<System.DateTime> nacimiento { get; set; }
    
        public virtual operador operador { get; set; }
        public virtual usuario usuario { get; set; }
    }
}