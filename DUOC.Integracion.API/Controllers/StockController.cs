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
    public class StockController : ApiController
    {
        private MasterBikesEntities db = new MasterBikesEntities();

        /// <summary>
        /// Lista de productos con sus respectivos stock
        /// </summary>
        /// <returns>IQueryable Stock</returns>
        public IQueryable<stock> Getstock()
        {
            return db.stock;
        }

        /// <summary>
        /// Stock de  un producto determinado
        /// </summary>
        /// <returns>IQueryable stock</returns>
        [ResponseType(typeof(stock))]
        public IHttpActionResult Getstock(long id)
        {
            stock stock = db.stock.Find(id);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock);
        }

        /// <summary>
        /// Actualizacion del stock de un producto determinado
        /// </summary>
        /// <param name="id">ID de Stock</param>
        /// <param name="stock">Stock</param>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult Putstock(long id, stock stock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stock.id_stock)
            {
                return BadRequest();
            }

            db.Entry(stock).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!stockExists(id))
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
        /// Creacion de un nuevo stock para un producto determinado
        /// </summary>
        /// <param name="stock">Stock</param>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(stock))]
        public IHttpActionResult Poststock(stock stock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.stock.Add(stock);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = stock.id_stock }, stock);
        }

        /// <summary>
        /// Eliminacion del stock de un producto 
        /// </summary>
        /// <param name="id">Id Stock</param>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(stock))]
        public IHttpActionResult Deletestock(long id)
        {
            stock stock = db.stock.Find(id);
            if (stock == null)
            {
                return NotFound();
            }

            db.stock.Remove(stock);
            db.SaveChanges();

            return Ok(stock);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool stockExists(long id)
        {
            return db.stock.Count(e => e.id_stock == id) > 0;
        }
    }
}