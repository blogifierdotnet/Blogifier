using System.Linq;
using System.Threading.Tasks;

namespace Core.Data
{
    public interface ISettingsRepository : IRepository<Setting>
    {
        Task SaveSetting(string key, string newVal);
    }

    public class SettingsRepository : Repository<Setting>, ISettingsRepository
    {
        AppDbContext _db;

        public SettingsRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task SaveSetting(string key, string newVal)
        {
            var setting = _db.Settings.Where(s => s.SettingKey == key).FirstOrDefault();

            if (setting == null)
            {
                _db.Settings.Add(new Setting { SettingKey = key, SettingValue = newVal });
            }
            else
            {
                setting.SettingValue = newVal;
            }
            await _db.SaveChangesAsync();

            if (key == "app-title") AppSettings.Title = newVal;
            if (key == "app-desc") AppSettings.Description = newVal;
            if (key == "app-logo") AppSettings.Logo = newVal;
            if (key == "app-cover") AppSettings.Cover = newVal;
            if (key == "app-theme") AppSettings.Theme = newVal;
            if (key == "app-post-list-type") AppSettings.PostListType = newVal;
            if (key == "app-items-per-page") AppSettings.ItemsPerPage = int.Parse(newVal);
        }
    }
}