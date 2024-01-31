using Happy_Devs_BE.Services.Core;

namespace Happy_Devs_BE.Services
{
    public class ActivityRepository: BaseRepository
    {
        public ActivityRepository(IConfiguration configuration) : base(configuration)
        { 
        
        }

        public async Task<List<Activiy>> getAllActivity(int userId, DateTime minDate)
        {
            List<ActivityData> activityData = await read<ActivityData>("select type, at from activity where userId = @id AND at > @mindate;", new {
                id = userId,
                mindate = minDate
            });

            return activityData.Select(activityData => new Activiy
            {
                Type = activityData.type,
                At = activityData.at,
            }).ToList();
        }

        public async Task addActivity(int userId, ActivityType type, DateTime at)
        {
            await write("insert into activity(userId, type, at) values (@userId, @type, @at);", new
            {
                userId = userId, type = type, at = at
            });
        }

        private class ActivityData
        {
            public ActivityType type { get; set; }
            public DateTime at { get; set; }
        }
    }
}
