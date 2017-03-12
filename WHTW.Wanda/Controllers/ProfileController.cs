using System;
using System.Linq;
using System.Web.Http;

namespace WHTW.Wanda.Controllers
{
    using Models;

    [RoutePrefix("api")]
    [Authorize(Roles = "Doctor")]
    public class ProfileController : ApiController
    {
        [HttpGet]
        [Route("profile")]
        public IHttpActionResult List(string rg = null)
        {
            using (var dbContext = new AppContext())
            {
                IQueryable<Profile> query = dbContext.Profile;
                if (!string.IsNullOrEmpty(rg))
                    query = query.Where(a => a.RG.StartsWith(rg));
                return Ok(query.ToList());
            }
        }

        [HttpGet]
        [Route("profile/{id}")]
        public IHttpActionResult GetById(Guid id)
        {
            using (var dbContext = new AppContext())
            {
                var profile = dbContext.Profile.FirstOrDefault(a => a.Id == id);
                if (profile == null)
                    return NotFound();
                var conversationHistory = dbContext.Conversation.Where(a => a.UserId == profile.UserId).ToList();
                return Ok(new
                {
                    profile = profile,
                    conversationHistory = conversationHistory
                });
            }
        }
    }
}