using DUOC.Cotizador.Web.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DUOC.Cotizador.Web.Controllers
{
    public class SolicitudArriendoController : Controller
    {
        // GET: SolicitudArriendo
        public ActionResult Index()
        {

            var productos = BaseController.ObtenerDatos("SELECT IDProducto, NombreProducto FROM Productos");
            var proveedores = BaseController.ObtenerDatos("SELECT IDProveedor, NombreProveedor FROM Proveedores");
            ViewBag.Productos = new MultiSelectList(productos, "IDProducto", "NombreProducto");
            ViewBag.Proveedores = new MultiSelectList(proveedores, "IDProveedor", "NombreProveedor");
            return View();
        }
        [HttpGet]
        public ActionResult getProductos(string get)
        {
            Uri UriRequest = new Uri(this.Request.Url.AbsoluteUri);
            string IDsProveedores = HttpUtility.ParseQueryString(UriRequest.Query).Get("prov[]");
            List<SelectModels> listaProductos = new List<SelectModels>();
            if (IDsProveedores != "")
            {
                var reader = BaseController.ObtenerDatos("SELECT IDProducto, NombreProducto + ' (' + CAST(ValorProducto AS VARCHAR) +' USD)' NombreProducto FROM Productos WHERE IDProveedor IN (" + IDsProveedores + ")");

                while (reader.Read())
                {
                    listaProductos.Add(new SelectModels
                    {
                        id = Convert.ToInt32(reader["IDProducto"]),
                        text = reader["NombreProducto"].ToString()
                    });
                }
            }
            return Json(listaProductos, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult generarCotizacion()
        {
            var tipoBici = Request.Form[0];
            var fechaCalculo = Request.Form[1].Split('-');
            var medio_pago_cod_mediopago = Request.Form[2];
            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["tipo_bicicleta"] = tipoBici;
                values["fec_arriendo"] = Convert.ToDateTime(fechaCalculo[0]).ToString("yyyy-MM-dd");
                values["fec_devolucion"] = Convert.ToDateTime(fechaCalculo[1]).ToString("yyyy-MM-dd");
                values["medio_pago_cod_mediopago"] = medio_pago_cod_mediopago;

                var response = client.UploadValues("http://localhost:49990/api/Arriendo/Post", values);

                var responseString = Encoding.Default.GetString(response);
            }

            Conexion.Cerrar();
            return Json(new { ID = tipoBici }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult generarCotizacionTBL(int id = 0)
        {
            DataTableModels dataTable = new DataTableModels();
            dataTable.draw = 1;
            var reader = BaseController.ObtenerDatos("CalculoCotizacion " + id.ToString());
            List<CotizacionModels> listaProductos = new List<CotizacionModels>();

            while (reader.Read())
            {

                CotizacionModels c = new CotizacionModels
                {
                    Proveedor = reader["PROVEEDOR"].ToString(),
                    Producto = reader["NombreProducto"].ToString(),
                    ValorPesos = (int)(Int64)reader["VALOR_PESOS"],
                    ValorCambio = Convert.ToDateTime(reader["FechaCambio"]).ToString("dd-MM-yyyy")
                };
                listaProductos.Add(c);
            }

            dataTable.data = listaProductos.ToArray();
            dataTable.recordsTotal = listaProductos.Count;
            dataTable.recordsFiltered = listaProductos.Count();
            return Json(dataTable, JsonRequestBehavior.AllowGet);
        }
    }
}