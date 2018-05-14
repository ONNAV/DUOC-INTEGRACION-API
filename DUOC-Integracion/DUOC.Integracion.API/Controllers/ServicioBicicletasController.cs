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
    public class ServicioBicicletasController : ApiController
    {
        private MasterBikesEntities db = new MasterBikesEntities();

        /// <summary>
        /// Lista de Todos los servicios de reparacion de bicicletas
        /// </summary>
        /// <returns>IQueryable<ServicioBicicleta></returns>
        public IQueryable<ServicioBicicleta> GetServicioBicicleta()
        {
            return db.ServicioBicicleta;
        }

        /// <summary>
        /// Obtencion de un servicio de reparacion. EJ: api/ServicioBicicletas/1
        /// </summary>
        /// <param name="id">ID Servicio</param>
        /// <returns>Servicio Bicicleta</returns>
        [ResponseType(typeof(ServicioBicicleta))]
        public IHttpActionResult GetServicioBicicleta(long id)
        {
            ServicioBicicleta servicioBicicleta = db.ServicioBicicleta.Find(id);
            if (servicioBicicleta == null)
            {
                return NotFound();
            }

            return Ok(servicioBicicleta);
        }

        /// <summary>
        /// Actualizacion de un servicio de reparacion
        /// </summary>
        /// <param name="id">ID servicio</param>
        /// <param name="servicioBicicleta">Servicio Bicicleta</param>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutServicioBicicleta(long id, ServicioBicicleta servicioBicicleta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != servicioBicicleta.IDServicio)
            {
                return BadRequest();
            }

            db.Entry(servicioBicicleta).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicioBicicletaExists(id))
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
        /// Creacion de un nuevo servicio de reparacion de bicicletas
        /// </summary>
        /// <param name="servicioBicicleta"> Servicio de bicicleta</param>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(ServicioBicicleta))]
        public IHttpActionResult PostServicioBicicleta(ServicioBicicleta servicioBicicleta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ServicioBicicleta.Add(servicioBicicleta);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = servicioBicicleta.IDServicio }, servicioBicicleta);
        }

        /// <summary>
        /// Eliminacion de servicio de reparacion de bicicletas
        /// </summary>
        /// <param name="id">ID Servicio</param>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(ServicioBicicleta))]
        public IHttpActionResult DeleteServicioBicicleta(long id)
        {
            ServicioBicicleta servicioBicicleta = db.ServicioBicicleta.Find(id);
            if (servicioBicicleta == null)
            {
                return NotFound();
            }

            db.ServicioBicicleta.Remove(servicioBicicleta);
            db.SaveChanges();

            return Ok(servicioBicicleta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ServicioBicicletaExists(long id)
        {
            return db.ServicioBicicleta.Count(e => e.IDServicio == id) > 0;
        }
    }
}