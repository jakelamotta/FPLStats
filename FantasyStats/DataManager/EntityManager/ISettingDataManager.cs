using Common.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.EntityManager
{
    public interface ISettingDataManager
    {
        BaseResultDto<List<SettingDto>> GetAllSettings();
    }
}
