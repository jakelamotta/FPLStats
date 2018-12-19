using Common;
using Data.Providers;
using DataManager.EntityManager;
using FPLModeling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceInstance
{
    public class FplStats: IFplStats
    {
        private readonly IStatsProvider _statsProvider;
        private readonly IFplDataManager _fplDataManager;
        private readonly IModelCalculator _modelCalculator;


        public FplStats(IStatsProvider statsProvider, IFplDataManager fplDataManager, IModelCalculator modelCalculator, ISettingDataManager settingDataManager)
        {
            _statsProvider = statsProvider;
            _fplDataManager = fplDataManager;
            _modelCalculator = modelCalculator;

            var result = settingDataManager.GetAllSettings();

            if (!result.Status)
            {
                throw new Exception();
            }

            Utility.SetAllSettings(result.DataObject);
        }

        public void Calculate()
        {
            var allPlayers = _fplDataManager.GetAllPlayerStatistics();

            var modelResult = _modelCalculator.Calculate(allPlayers);

            if (modelResult.Status)
            {
                var saveResult = _fplDataManager.SaveCalculatedPlayerStatistics(modelResult.DataObject);
            
            }
        }

        public void UpdateData()
        {
            var seasonResult = _fplDataManager.GetNonCompleteSeasons();

            if (!seasonResult.Status)
            {
                return;
            }

            var teamResult = _statsProvider.GetTeams(seasonResult.DataObject);

            if (!teamResult.Status)
            {
                throw new Exception();
            }

            _fplDataManager.SaveTeamStatisticsList(teamResult.DataObject);

            var result = _statsProvider.GetPlayers(seasonResult.DataObject);

            if (result.Status)
            {
                _fplDataManager.SavePlayerStatisticsList(result.DataObject);
            }
        }
    }
}
