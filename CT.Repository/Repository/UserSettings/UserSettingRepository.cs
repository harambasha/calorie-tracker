using System.Collections.Generic;
using System.Linq;
using CT.Repository.Infrastructure;
using CT.Repository.Models.UserSetting;

namespace CT.Repository.Repository.UserSettings
{
    public class UserSettingRepository : GenericRepository<UserSetting>, IUserSettingRepository
    {
        public UserSettingRepository(ApplicationDbContext dataContext)
            : base(dataContext)
        {
        }

        public List<UserSetting> GetUserSettingsByUserId(string userId)
        {
            return DataSource().Where(us => us.UserId == userId).ToList();
        }
    }
}
