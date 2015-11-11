using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using CT.Repository.Infrastructure;
using Microsoft.AspNet.Identity;
using CT.WebAPI.HttpPipeline;
using System.Net;
using CT.Repository.Repository.UserSettings;
using CT.Repository.Models.UserSetting;

namespace CT.WebAPI.Controllers
{
    [RoutePrefix("api/user-settings")]
    public class UserSettingsController : BaseApiController
    {
        UserSettingRepository userSettingsRepository = new UserSettingRepository(ApplicationDbContext.Create());

        [Authorize]
        [Route("")]
        public IHttpActionResult GetUserSettings()
        {
            if (User == null){
                return Unauthorized();
            }
            var identity = User.Identity as ClaimsIdentity;
            return Ok(userSettingsRepository.GetUserSettingsByUserId(identity.GetUserId()));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult AddUserSetting([FromBody] UserSettingBindingModel userSetting)
        {
            var identity = User.Identity as ClaimsIdentity;
            userSetting.UserId = identity.GetUserId();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            userSettingsRepository.InsertAndSubmit(userSetting.CreateUserSetting());
            return Ok();
        }


        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult UpdateUserSetting(int id, [FromBody] UserSettingBindingModel userSetting)
        {
            var identity = User.Identity as ClaimsIdentity;
            if (userSetting.UserId != identity.GetUserId())
            {
                throw new ApiException(HttpStatusCode.Unauthorized, "You are not authorized to change this particular record");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserSetting retrievedUserSetting = userSettingsRepository.GetById(id);
            if (retrievedUserSetting == null)
            {
                throw new ApiException(HttpStatusCode.BadRequest, "There are no entries for supplied id");
            }

            userSettingsRepository.UpdateAndSubmit(userSetting.UpdateModel(retrievedUserSetting));
            return Ok();
        }
    }
}
