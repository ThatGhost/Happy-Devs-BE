namespace Happy_Devs_BE.Services
{
    public class ActivityService
    {
        private readonly ActivityRepository _activityRepository;
        public ActivityService(ActivityRepository activityRepository)
        { 
            _activityRepository = activityRepository;
        }

        public async Task<List<Activiy>> getAllActivity(int userId)
        {
            return await _activityRepository.getAllActivity(userId);
        }

        public async Task addActivity(int userId, ActivityType type)
        {
            await _activityRepository.addActivity(userId, type, DateTime.UtcNow);
        }
    }
}
