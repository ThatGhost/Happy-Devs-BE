using Happy_Devs_BE.Services;

using Microsoft.AspNetCore.Mvc;

namespace Happy_Devs_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly ActivityService _activityService;
        private readonly UsersAuthenticationService _usersAuthenticationService;
        public ActivityController(
            ActivityService activityService,
            UsersAuthenticationService usersAuthenticationService
            )
        { 
            _activityService = activityService;
            _usersAuthenticationService = usersAuthenticationService;
        }

        [HttpGet("{id}")]
        public async Task<List<ActivityResponse>> get(int id)
        {
            await _usersAuthenticationService.authenticateUser(Request.Headers);

            return (await _activityService.getAllActivity(id)).Select(a => new ActivityResponse
            {
                type = (int)(a.Type),
                at = a.At,
            }).ToList();
        }
    }
}
