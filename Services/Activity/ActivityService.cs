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
            DateTime minDate = DateTime.Now.AddDays(-7);

            return await _activityRepository.getAllActivity(userId, minDate);
        }

        public async Task addActivity(int userId, ActivityType type)
        {
            await _activityRepository.addActivity(userId, type, DateTime.UtcNow);
        }
    }
}
