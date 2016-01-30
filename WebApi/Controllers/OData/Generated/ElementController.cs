//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace forCrowd.WealthEconomy.WebApi.Controllers.OData
{
    using forCrowd.WealthEconomy.BusinessObjects;
    using forCrowd.WealthEconomy.Facade;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.ModelBinding;
    using System.Web.Http.OData;

    public abstract class BaseElementController : BaseODataController
    {
        public BaseElementController()
		{
			MainUnitOfWork = new ElementUnitOfWork();		
		}

		protected ElementUnitOfWork MainUnitOfWork { get; private set; }

        // GET odata/Element
        //[Queryable]
        public virtual IQueryable<Element> Get()
        {
			var list = MainUnitOfWork.AllLive;
            return list;
        }

        // GET odata/Element(5)
        //[Queryable]
        public virtual SingleResult<Element> Get([FromODataUri] int key)
        {
            return SingleResult.Create(MainUnitOfWork.AllLive.Where(element => element.Id == key));
        }

        // PUT odata/Element(5)
        public virtual async Task<IHttpActionResult> Put([FromODataUri] int key, Element element)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != element.Id)
            {
                return BadRequest();
            }

            try
            {
                await MainUnitOfWork.UpdateAsync(element);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await MainUnitOfWork.All.AnyAsync(item => item.Id == element.Id))
                {
                    return Conflict();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok(element);
        }

        // POST odata/Element
        public virtual async Task<IHttpActionResult> Post(Element element)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await MainUnitOfWork.InsertAsync(element);
            }
            catch (DbUpdateException)
            {
                if (await MainUnitOfWork.All.AnyAsync(item => item.Id == element.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(element);
        }

        // PATCH odata/Element(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public virtual async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Element> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var element = await MainUnitOfWork.AllLive.SingleOrDefaultAsync(item => item.Id == key);
            if (element == null)
            {
                return NotFound();
            }

            var patchEntity = patch.GetEntity();

            // TODO How is passed ModelState.IsValid?
            if (patchEntity.RowVersion == null)
                throw new InvalidOperationException("RowVersion property of the entity cannot be null");

            if (!element.RowVersion.SequenceEqual(patchEntity.RowVersion))
            {
                return Conflict();
            }

            patch.Patch(element);
            await MainUnitOfWork.UpdateAsync(element);

            return Ok(element);
        }

        // DELETE odata/Element(5)
        public virtual async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            var element = await MainUnitOfWork.AllLive.SingleOrDefaultAsync(item => item.Id == key);
            if (element == null)
            {
                return NotFound();
            }

            await MainUnitOfWork.DeleteAsync(element.Id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }

    public partial class ElementController : BaseElementController
    {
	}
}
