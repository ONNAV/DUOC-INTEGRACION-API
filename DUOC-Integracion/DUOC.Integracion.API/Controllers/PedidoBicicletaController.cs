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
    public class PedidoBicicletaController : ApiController
    {
        private MasterBikesEntities db = new MasterBikesEntities();

        /// <summary>
        /// Lista de todos los pedidos de bicicletas realizados
        /// </summary>
        /// <returns>IQueryable Pedido Bicicleta</returns>
        /// <remarks>
        /// Details about the interface go here.
        /// </remarks>
        public IQueryable<Pedido_Bicicleta> GetPedido_Bicicleta()
        {
            return db.Pedido_Bicicleta;
        }

        /// <summary>
        /// Obtencion de un pedido de bicicleta
        /// </summary>
        /// <param name="id">Id del pedido</param>
        /// <returns>Pedido_Bicicleta</returns>
        [ResponseType(typeof(Pedido_Bicicleta))]
        public IHttpActionResult GetPedido_Bicicleta(int id)
        {
            Pedido_Bicicleta pedido_Bicicleta = db.Pedido_Bicicleta.Find(id);
            if (pedido_Bicicleta == null)
            {
                return NotFound();
            }

            return Ok(pedido_Bicicleta);
        }

        /// <summary>
        /// Actualización de un pedido de bicicleta
        /// </summary>
        /// <param name="id">ID del pedido</param>
        /// <param name="pedido_Bicicleta">Object Bicicleta</param>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPedido_Bicicleta(int id, Pedido_Bicicleta pedido_Bicicleta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pedido_Bicicleta.id_pedido)
            {
                return BadRequest();
            }

            db.Entry(pedido_Bicicleta).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Pedido_BicicletaExists(id))
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
        /// Creación de un pedido de bicicleta
        /// </summary>
        /// <param name="pedido_Bicicleta">Object Pedido Bicicleta</param>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(Pedido_Bicicleta))]
        public IHttpActionResult PostPedido_Bicicleta(Pedido_Bicicleta pedido_Bicicleta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pedido_Bicicleta.Add(pedido_Bicicleta);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pedido_Bicicleta.id_pedido }, pedido_Bicicleta);
        }

        /// <summary>
        /// Eliminación de un pedido de bicicleta
        /// </summary>
        /// <param name="id">ID del pedido</param>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(Pedido_Bicicleta))]
        public IHttpActionResult DeletePedido_Bicicleta(int id)
        {
            Pedido_Bicicleta pedido_Bicicleta = db.Pedido_Bicicleta.Find(id);
            if (pedido_Bicicleta == null)
            {
                return NotFound();
            }

            db.Pedido_Bicicleta.Remove(pedido_Bicicleta);
            db.SaveChanges();

            return Ok(pedido_Bicicleta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Pedido_BicicletaExists(int id)
        {
            return db.Pedido_Bicicleta.Count(e => e.id_pedido == id) > 0;
        }
    }
}