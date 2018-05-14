﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace DUOC.Integracion.API
{
    public class Conexion
    {
        private static SqlConnection conn;

        public static SqlCommand Conectar()
        {
            conn = new SqlConnection("Data Source=.; Initial Catalog = MasterBikes; Integrated Security = true");
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            return cmd;
        }

        public static void Cerrar()
        {
            conn.Close();
        }
    }
}
