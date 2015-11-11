using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using CT.Repository.Infrastructure;
using CT.Repository.Repository.Meals;
using Microsoft.AspNet.Identity;
using CT.Repository.Models.CalorieTracking;
using CT.WebAPI.HttpPipeline;
using System.Net;
using System.Threading.Tasks;
using System;

namespace CT.WebAPI.Controllers
{
    [RoutePrefix("api/meals")]
    public class MealsController : BaseApiController
    {
        MealRepository mealRepository = new MealRepository(ApplicationDbContext.Create());

        [Authorize]
        [Route("")]
        public IHttpActionResult GetMeals() {
            var identity = User.Identity as ClaimsIdentity;
            return Ok(mealRepository.GetMealsByUserId(identity.GetUserId()));
        }

        [Authorize]
        [Route("filter")]
        public IHttpActionResult GetMealsByDateAndTime([FromUri]DateTime startDate, [FromUri]DateTime endDate, [FromUri]DateTime  fromTime, [FromUri]DateTime toTime)
        {
            var identity = User.Identity as ClaimsIdentity;
            return Ok(mealRepository.GetMealsByUserIdFilteredByDateAndTime(identity.GetUserId(), startDate, endDate, fromTime, toTime));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult AddMeal([FromBody] MealBindingModel mealBindingModel)
        {

            var identity = User.Identity as ClaimsIdentity;
            mealBindingModel.UserId = identity.GetUserId();

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            mealRepository.InsertAndSubmit(mealBindingModel.CreateMeal());
            return Ok();
        }


        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult UpdateMeal(int id, [FromBody] MealBindingModel mealBindingModel)
        {

            var identity = User.Identity as ClaimsIdentity;
            if (mealBindingModel.UserId != identity.GetUserId()){
                throw new ApiException(HttpStatusCode.Unauthorized, "You are not authorized to change this particular record");
            }

            if (!ModelState.IsValid){
                return BadRequest(ModelState);

            }

            Meal retrievedMeal = mealRepository.GetById(id);
            if (retrievedMeal == null){
                throw new ApiException(HttpStatusCode.BadRequest, "There are no entries for supplied id");
            }

            mealRepository.UpdateAndSubmit(mealBindingModel.UpdateModel(retrievedMeal));
            return Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        public void Delete(int id){
            Meal item = mealRepository.GetById(id);
            if (item == null) {
                throw new ApiException(HttpStatusCode.NotFound, "Meal not found so it couldn't be deleted");
            }
            mealRepository.DeleteAndSubmit(item);
        }
    }
}
