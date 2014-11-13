//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web.Controllers.OData
{
    using BusinessObjects;
    using Facade;
    using Microsoft.AspNet.Identity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.ModelBinding;
    using System.Web.Http.OData;

    public abstract class BaseElementFieldController : BaseODataController
    {
        public BaseElementFieldController()
		{
			MainUnitOfWork = new ElementFieldUnitOfWork();		
		}

		protected ElementFieldUnitOfWork MainUnitOfWork { get; private set; }

        // GET odata/ElementField
        //[Queryable]
        public virtual IQueryable<ElementField> Get()
        {
			var list = MainUnitOfWork.AllLive;
            return list;
        }

        // GET odata/ElementField(5)
        //[Queryable]
        public virtual SingleResult<ElementField> Get([FromODataUri] int key)
        {
            return SingleResult.Create(MainUnitOfWork.AllLive.Where(elementField => elementField.Id == key));
        }

        // PUT odata/ElementField(5)
        public virtual async Task<IHttpActionResult> Put([FromODataUri] int key, ElementField elementField)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != elementField.Id)
            {
                return BadRequest();
            }

            try
            {
                await MainUnitOfWork.UpdateAsync(elementField);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MainUnitOfWork.Exists(key))
                {
                    return NotFound();
                }
                else
                {
                    return Conflict();
                }
            }

            return Ok(elementField);
        }

        // POST odata/ElementField
        public virtual async Task<IHttpActionResult> Post(ElementField elementField)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await MainUnitOfWork.InsertAsync(elementField);
            }
            catch (DbUpdateException)
            {
                if (MainUnitOfWork.Exists(elementField.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(elementField);
        }

        // PATCH odata/ElementField(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public virtual async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<ElementField> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var elementField = await MainUnitOfWork.FindAsync(key);
            if (elementField == null)
            {
                return NotFound();
            }

            var patchEntity = patch.GetEntity();
            if (!elementField.RowVersion.SequenceEqual(patchEntity.RowVersion))
            {
                return Conflict();
            }

            patch.Patch(elementField);
            await MainUnitOfWork.UpdateAsync(elementField);

            return Ok(elementField);
        }

        // DELETE odata/ElementField(5)
        public virtual async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            var elementField = await MainUnitOfWork.FindAsync(key);
            if (elementField == null)
            {
                return NotFound();
            }

            await MainUnitOfWork.DeleteAsync(elementField.Id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }

    public partial class ElementFieldController : BaseElementFieldController
    {
	}
}
