using System.Collections.Generic;
using CT.Repository.Infrastructure;
using CT.Repository.Models.UserSetting;

namespace CT.Repository.Repository.UserSettings
{
    public interface IUserSettingRepository : IGenericRepository<UserSetting>
    {
        List<UserSetting> GetUserSettingsByUserId(string userId);
    }
}
