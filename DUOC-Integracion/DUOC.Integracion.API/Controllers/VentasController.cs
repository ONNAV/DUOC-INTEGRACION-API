using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

using DUOC.Integracion.API;

namespace DUOC.Integracion.API.Controllers
{
    /// <summary>
    /// Servicio REST el cual realiza la mantención de las ventas de Master Bikes
    /// </summary>
    public class VentasController : ApiController
    {
        private MasterBikesEntities db = new MasterBikesEntities();

        /// <summary>
        /// Obtención total de las ventas en la base de Master Bikes
        /// </summary>
        /// <returns>Lista de Ventas realizadas en el sistema</returns>
        public IQueryable<Ventas> GetVentas()
        {
            return db.Ventas;
        }


        /// <summary>
        /// Obtención de una venta en base a su ID de venta
        /// </summary>
        /// <param name="id">ID Venta</param>
        /// <returns>IHttpActionResult de Venta</returns>
        [ResponseType(typeof(Ventas))]
        public IHttpActionResult GetVentas(int id)
        {
            Ventas ventas = db.Ventas.Find(id);
            if (ventas == null)
            {
                return NotFound();
            }

            return Ok(ventas);
        }

        /// <summary>
        /// Actualización de una venta
        /// </summary>
        /// <param name="id">ID Venta</param>
        /// <param name="venta">Object Venta</param>
        /// <returns>IHttpActionResult de Venta</returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVentas(int id, Ventas ventas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ventas.crr_venta)
            {
                return BadRequest();
            }

            db.Entry(ventas).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VentasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Creación de una venta
        /// </summary>
        /// <param name="id">Sub Total</param>
        /// <param name="iva">IVA</param>
        /// <param name="Total">Total</param>
        /// <param name="MedioPago">Medio Pago</param>
        /// <param name="Cuotas">Cuotas</param>
        /// <param name="Vuelto">Vuelto</param>
        /// <param name="usuario_persona_rut">Rut Persona</param>
        /// <returns>IHttpActionResult de Venta</returns>
        [ResponseType(typeof(Ventas))]
        public IHttpActionResult PostVentas(Ventas ventas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ventas.Add(ventas);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ventas.crr_venta }, ventas);
        }

        /// <summary>
        /// Eliminación de una venta en base a su ID de venta
        /// </summary>
        /// <param name="id">ID Venta</param>
        /// <returns>IHttpActionResult de Venta</returns>
        [ResponseType(typeof(Ventas))]
        public IHttpActionResult DeleteVentas(int id)
        {
            Ventas ventas = db.Ventas.Find(id);
            if (ventas == null)
            {
                return NotFound();
            }

            db.Ventas.Remove(ventas);
            db.SaveChanges();

            return Ok(ventas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VentasExists(int id)
        {
            return db.Ventas.Count(e => e.crr_venta == id) > 0;
        }
    }
}