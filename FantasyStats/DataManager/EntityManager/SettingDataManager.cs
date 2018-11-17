using Common.Dtos;
using DataManager.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataManager.EntityManager
{
    public class SettingDataManager : DataManagerBase, ISettingDataManager
    {
        private readonly IContextFactory _contextFactory;

        public SettingDataManager(IContextFactory contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            _contextFactory = contextFactory;
        }

        public BaseResultDto<List<SettingDto>> GetAllSettings()
        {
            var resultat = new BaseResultDto<List<SettingDto>>();
            resultat.Status = false;
            resultat.DataObject = new List<SettingDto>();

            using (var context = _contextFactory.Create())
            {
                resultat.DataObject = context.Settings.ToDtoList<SettingDto>(Mapper).ToList();
            }

            resultat.Status = resultat.DataObject != null && resultat.DataObject.Count > 0;            

            return resultat;
        }
    }
}
