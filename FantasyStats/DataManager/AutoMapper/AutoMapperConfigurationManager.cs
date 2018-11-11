using AutoMapper;
using Common.Dtos;
using DataManager.Models;

namespace DataManager.AutoMapper
{
    public class AutoMapperConfigurationManager
    {
        public IMapper Mapper { get; set; }

        public AutoMapperConfigurationManager()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMissingTypeMaps = true;
                InitLeagueMapping(cfg);
                InitTeamMapping(cfg);
                InitPlayerMapping(cfg);
            });

            Mapper = config.CreateMapper();
            //config.AssertConfigurationIsValid();
        }

        public void InitLeagueMapping(IMapperConfigurationExpression cfg)
        {

            cfg.CreateMap<League, LeagueDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country));

            cfg.CreateMap<Season, SeasonDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.EndYear, opt => opt.MapFrom(src => src.EndYear))
                .ForMember(dest => dest.StartYear, opt => opt.MapFrom(src => src.StartYear))
                .ForMember(dest => dest.League, opt => opt.MapFrom(src => src.League))
                .ForMember(dest => dest.IsCurrent, opt => opt.MapFrom(src => src.IsCurrent))
                .ForMember(dest => dest.DataComplete, opt => opt.MapFrom(src => src.DataComplete));
        }

        public void InitTeamMapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Team, TeamDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            cfg.CreateMap<SeasonTeam, SeasonTeamDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Season, opt => opt.MapFrom(src => src.Season))
                .ForMember(dest => dest.Team, opt => opt.MapFrom(src => src.Team));
        }

        public void InitPlayerMapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Player, PlayerDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.ExternalId))
                .ForMember(dest => dest.SecondName, opt => opt.MapFrom(src => src.SecondName))
                .ForMember(dest => dest.LastCost, opt => opt.MapFrom(src => src.LastCost))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => (Constants.PositionEnum)src.Position.AppId));

            cfg.CreateMap<PlayerSeasonStatistics, PlayerSeasonStatisticsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Player, opt => opt.MapFrom(src => src.Player))
                .ForMember(dest => dest.SeasonTeam, opt => opt.MapFrom(src => src.SeasonTeam))
                .ForMember(dest => dest.Assists, opt => opt.MapFrom(src => src.Assists))
                .ForMember(dest => dest.BonusPointSystem, opt => opt.MapFrom(src => src.BonusPointSystem))
                .ForMember(dest => dest.CleanSheets, opt => opt.MapFrom(src => src.CleanSheets))
                .ForMember(dest => dest.Goals, opt => opt.MapFrom(src => src.Goals))
                .ForMember(dest => dest.OwnGoals, opt => opt.MapFrom(src => src.OwnGoals))
                .ForMember(dest => dest.PenaltiesMissed, opt => opt.MapFrom(src => src.PenaltiesMissed))
                .ForMember(dest => dest.RedCards, opt => opt.MapFrom(src => src.RedCards))
                .ForMember(dest => dest.XA, opt => opt.MapFrom(src => src.XA))
                .ForMember(dest => dest.XA90, opt => opt.MapFrom(src => src.XA90))
                .ForMember(dest => dest.XG, opt => opt.MapFrom(src => src.XG))
                .ForMember(dest => dest.XG90, opt => opt.MapFrom(src => src.XG90))
                .ForMember(dest => dest.MinutesPlayed, opt => opt.MapFrom(src => src.MinutesPlayed))
                .ForMember(dest => dest.Apps, opt => opt.MapFrom(src => src.Apps));
        }
    }
}
