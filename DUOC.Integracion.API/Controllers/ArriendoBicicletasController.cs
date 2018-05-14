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
    public class ArriendoBicicletasController : ApiController
    {
        private MasterBikesEntities db = new MasterBikesEntities();

        /// <summary>
        ///  Lista de Todos los Arriendos
        /// </summary>
        /// <returns>IQueryable ArriendoBicicleta</returns>
        public IQueryable<ArriendoBicicleta> GetArriendoBicicleta()
        {
            return db.ArriendoBicicleta;
        }

        /// <summary>
        /// Obtención de datos del arriendo de una bicicleta
        /// </summary>
        /// <param name="id">ID Arriendo</param>
        /// <returns>Arriendo de la bicicleta</returns>
        [ResponseType(typeof(ArriendoBicicleta))]
        public IHttpActionResult GetArriendoBicicleta(int id)
        {
            ArriendoBicicleta arriendoBicicleta = db.ArriendoBicicleta.Find(id);
            if (arriendoBicicleta == null)
            {
                return NotFound();
            }

            return Ok(arriendoBicicleta);
        }

        /// <summary>
        /// Actualización de Arriendo de bicicleta por medio del ID de creación
        /// </summary>
        /// <param name="id">ID Arriendo</param>
        /// <param name="arriendoBicicleta">Object Bicicleta</param>
        /// <returns>IHttpActionResult </returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutArriendoBicicleta(int id, ArriendoBicicleta arriendoBicicleta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != arriendoBicicleta.crr_arriendo)
            {
                return BadRequest();
            }

            db.Entry(arriendoBicicleta).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArriendoBicicletaExists(id))
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
        /// Creación de Arriendo de bicicleta
        /// </summary>
        /// <param name="arriendoBicicleta">Bicicleta Object</param>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(ArriendoBicicleta))]
        public IHttpActionResult PostArriendoBicicleta(ArriendoBicicleta arriendoBicicleta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ArriendoBicicleta.Add(arriendoBicicleta);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = arriendoBicicleta.crr_arriendo }, arriendoBicicleta);
        }

        /// <summary>
        /// Eliminación de un arriendo de biciclieta mediante el ID de creación
        /// </summary>
        /// <param name="id">ID Arriendo</param>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(ArriendoBicicleta))]
        public IHttpActionResult DeleteArriendoBicicleta(int id)
        {
            ArriendoBicicleta arriendoBicicleta = db.ArriendoBicicleta.Find(id);
            if (arriendoBicicleta == null)
            {
                return NotFound();
            }

            db.ArriendoBicicleta.Remove(arriendoBicicleta);
            db.SaveChanges();

            return Ok(arriendoBicicleta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArriendoBicicletaExists(int id)
        {
            return db.ArriendoBicicleta.Count(e => e.crr_arriendo == id) > 0;
        }
    }
}