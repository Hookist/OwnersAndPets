using OwnersProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace OwnersProject.Controllers
{
    public class PetController : ApiController
    {
        OwnersAndPetsContext context;

        public PetController()
        {
            context = new OwnersAndPetsContext();
        }

        // GET: api/Pet
        [ResponseType(typeof(IEnumerable<Pet>))]
        public async Task<HttpResponseMessage> Get(HttpRequestMessage request)
        {
            var pets = await context.SelectPets();
            return request.CreateResponse(HttpStatusCode.OK, pets);
        }

        [ResponseType(typeof(Pet))]
        public async Task<HttpResponseMessage> Get(HttpRequestMessage request, int id)
        {
            var pet = await context.SelectPet(id);
            return request.CreateResponse(HttpStatusCode.OK, pet);
        }

        //GET: api/Pet/GetPetsByOwner/5
        [ResponseType(typeof(IEnumerable<Pet>))]
        public async Task<HttpResponseMessage> GetPetsByOwner(HttpRequestMessage request, int id)
        {
            var pets = await context.SelectByOwnerId(id);
            return request.CreateResponse(HttpStatusCode.OK, pets);
        }

        // POST: api/Pet
        public async Task<HttpResponseMessage> Post(HttpRequestMessage request, [FromBody]Pet value)
        {
            await context.InsertPet(value);
            return request.CreateResponse(HttpStatusCode.OK, "Iserted");
        }

        // PUT: api/Pet/5
        public async Task<HttpResponseMessage> Put(HttpRequestMessage request, int id, [FromBody]Pet value)
        {
            await context.UpdatePet(id, value);
            return request.CreateResponse(HttpStatusCode.OK, "Updated");
        }

        // DELETE: api/Pet/5
        public async Task<HttpResponseMessage> Delete(HttpRequestMessage request, int id)
        {
            await context.DeletePet(id);
            return request.CreateResponse(HttpStatusCode.OK, "Deleted");
        }
    }
}

