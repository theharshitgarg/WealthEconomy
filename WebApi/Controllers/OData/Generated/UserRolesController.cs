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
    using WebApi.Controllers.Extensions;

    public abstract class BaseUserRolesController : BaseODataController
    {
        public BaseUserRolesController()
		{
			MainUnitOfWork = new UserRoleUnitOfWork();		
		}

		protected UserRoleUnitOfWork MainUnitOfWork { get; private set; }

        // GET odata/UserRole
        //[Queryable]
        public virtual IQueryable<UserRole> Get()
        {
			var userId = this.GetCurrentUserId();
			if (!userId.HasValue)
                throw new HttpResponseException(HttpStatusCode.Unauthorized);	

			var list = MainUnitOfWork.AllLive;
			list = list.Where(item => item.UserId == userId.Value);
            return list;
        }

        // GET odata/UserRole(5)
        //[Queryable]
        public virtual SingleResult<UserRole> Get([FromODataUri] int roleId)
        {
            return SingleResult.Create(MainUnitOfWork.AllLive.Where(userRole => userRole.RoleId == roleId));
        }

        // PUT odata/UserRole(5)
        public virtual async Task<IHttpActionResult> Put([FromODataUri] int roleId, UserRole userRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (roleId != userRole.RoleId)
            {
                return BadRequest();
            }

            try
            {
                await MainUnitOfWork.UpdateAsync(userRole);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await MainUnitOfWork.All.AnyAsync(item => item.RoleId == userRole.RoleId))
                {
                    return Conflict();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok(userRole);
        }

        // POST odata/UserRole
        public virtual async Task<IHttpActionResult> Post(UserRole userRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await MainUnitOfWork.InsertAsync(userRole);
            }
            catch (DbUpdateException)
            {
                if (await MainUnitOfWork.All.AnyAsync(item => item.RoleId == userRole.RoleId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(userRole);
        }

        // PATCH odata/UserRole(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public virtual async Task<IHttpActionResult> Patch([FromODataUri] int roleId, Delta<UserRole> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userRole = await MainUnitOfWork.AllLive.SingleOrDefaultAsync(item => item.RoleId == roleId);
            if (userRole == null)
            {
                return NotFound();
            }

            var patchEntity = patch.GetEntity();

            // TODO How is passed ModelState.IsValid?
            if (patchEntity.RowVersion == null)
                throw new InvalidOperationException("RowVersion property of the entity cannot be null");

            if (!userRole.RowVersion.SequenceEqual(patchEntity.RowVersion))
            {
                return Conflict();
            }

            patch.Patch(userRole);
            await MainUnitOfWork.UpdateAsync(userRole);

            return Ok(userRole);
        }

        // DELETE odata/UserRole(5)
        public virtual async Task<IHttpActionResult> Delete([FromODataUri] int roleId)
        {
            var userRole = await MainUnitOfWork.AllLive.SingleOrDefaultAsync(item => item.RoleId == roleId);
            if (userRole == null)
            {
                return NotFound();
            }

            await MainUnitOfWork.DeleteAsync(userRole.RoleId);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }

    public partial class UserRolesController : BaseUserRolesController
    {
	}
}
