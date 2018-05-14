using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DUOC.Integracion.API.Models;
using System.Data.Entity.Validation;

namespace DUOC.Integracion.API.Controllers
{
    public class ArriendoController 
    {
        /// <summary>
        /// aslkdj
        /// </summary>
        /// <returns></returns>
        public List<ProductoModels> GetProductos()
        {
            var reader = BaseController.ObtenerDatos("SELECT IDProducto, NombreProducto FROM Productos");
            List<ProductoModels> listaProductos = new List<ProductoModels>();
            while (reader.Read())
            {
                listaProductos.Add(new ProductoModels
                {
                    IDProducto = Convert.ToInt32(reader["IDProducto"]),
                    NombreProducto = reader["NombreProducto"].ToString()
                });
            }

            return listaProductos;

        }

        /// <summary>
        /// ajsjjaj
        /// </summary>
        /// <param name="arriendo"></param>
        /// <returns></returns>
        public bool Post(ArriendoModels arriendo)
        {
            using (var DatabaseContext = new MasterBikesEntities())
            {

                DatabaseContext.Database.ExecuteSqlCommand("INSERT INTO [ArriendoBicicleta]([tipo_bicicleta] ,[fec_arriendo] ,[fec_devolucion] ,[medio_pago_cod_mediopago] ) VALUES ('" + arriendo.tipo_bicicleta+"', '"+arriendo.fecha_arriendo+"', '"+arriendo.fecha_devolucion+"'," +arriendo.medio_pago_cod_mediopago+")");
                return true;
                //ArriendoBicicleta a = new ArriendoBicicleta
                //{
                //    tipo_bicicleta = arriendo.tipo_bicicleta,
                //    //fec_arriendo = arriendo.fecha_arriendo,
                //    //fec_devolucion = Convert.ToDateTime(arriendo.fecha_devolucion).f("yyyy-MM-dd"),
                //    medio_pago_cod_mediopago = arriendo.medio_pago_cod_mediopago
                //};

                //DatabaseContext.ArriendoBicicleta.Add(a);

                //try
                //{
                //    return DatabaseContext.SaveChanges() == 1 ? true : false;
                //}
                //catch (DbEntityValidationException e)
                //{
                //    var newException = e;
                //    throw e;
                //}
            }

        }

    }
}
