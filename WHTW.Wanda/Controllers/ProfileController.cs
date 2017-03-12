using System.Collections.Generic;
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
        public IHttpActionResult GetById(string rg = null)
        {
            Profile profile;

            using (var dbContext = new AppContext())
            {
                profile = dbContext.Profile.FirstOrDefault(a => a.RG == rg);
                profile.User = null;
                if (profile == null)
                    return NotFound();
            }
            List<Conversation> conversationHistory;
            using (var dbContext = new AppContext())
                conversationHistory = dbContext.Conversation.Where(a => a.UserId == profile.UserId).ToList();
            User user;
            using (var dbContext = new AppContext())
                user = dbContext.User.FirstOrDefault(a => a.RG == rg);
            return Ok(new
            {
                user = user,
                profile = profile,
                conversationHistory = conversationHistory
            });
        }
    }
}