using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DUOC.Integracion.API.Models
{
    public class ArriendoModels
    {
        public string tipo_bicicleta { get; set; }
        public DateTime fecha_arriendo { get; set; }
        public DateTime fecha_devolucion { get; set; }
        public int medio_pago_cod_mediopago { get; set; }
    }
}