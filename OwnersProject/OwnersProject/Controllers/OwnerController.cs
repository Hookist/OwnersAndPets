using OwnersProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace OwnersProject.Controllers
{
    public class OwnerController : ApiController
    {
        OwnersAndPetsContext context;
        public OwnerController()
        {
            context = new OwnersAndPetsContext();
        }

        // GET: api/Owner
        [ResponseType(typeof(IEnumerable<Owner>))]
        public async Task<HttpResponseMessage> Get(HttpRequestMessage request)
        {
            var owners = await context.SelectOwners();
            return request.CreateResponse(HttpStatusCode.OK, owners);
        }

        // GET: api/Owner/5
        [ResponseType(typeof(Owner))]
        public async Task<HttpResponseMessage> Get(HttpRequestMessage request, int id)
        {
            var owner = await context.SelectOwner(id);
            return request.CreateResponse(HttpStatusCode.OK, owner);
        }

        // POST: api/Owner
        public async Task<HttpResponseMessage> Post(HttpRequestMessage request, [FromBody]Owner value)
        {
            await context.InsertOwner(value);
            return request.CreateResponse(HttpStatusCode.OK, "Inserted");
        }

        // PUT: api/Owner/5
        public async Task<HttpResponseMessage> Put(HttpRequestMessage request, int id, [FromBody]Owner value)
        {
            await context.UpdateOwner(id, value);
            return request.CreateResponse(HttpStatusCode.OK, "Updated");
        }

        // DELETE: api/Owner/5
        public async Task<HttpResponseMessage> Delete(HttpRequestMessage request, int id)
        {
            await context.DeleteOwner(id);
            return request.CreateResponse(HttpStatusCode.OK, "Deleted");
        }
    }
}
