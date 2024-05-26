using AutoMapper;
using System.Xml;

namespace StellarJadeManager.Shared
{
    public class MapperProfile: AutoMapper.Profile
    {

        public MapperProfile()
        {
            CreateMap<ProfilePutRequestDTO, Shared.Profile>()
            .ForMember(dest => dest.ProfileName, opt => opt.Condition(src => src.ProfileName != null))
            .ForMember(dest => dest.CurrentJades, opt => opt.Condition(src => src.CurrentJades != null))
            .ForMember(dest => dest.SelectedDate, opt => opt.Condition(src => src.SelectedDate != null))
            .ForMember(dest => dest.MoCStars, opt => opt.Condition(src => src.MoCStars != null))
            .ForMember(dest => dest.PfStars, opt => opt.Condition(src => src.PfStars != null))
            .ForMember(dest => dest.SupplyPass, opt => opt.Condition(src => src.SupplyPass != null));

            CreateMap<PatchDTO, Patch>()
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.ReleaseDate.AddDays(7 * src.WeeksCount)))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Banners, opt => opt.Ignore())
                .ForMember(dest => dest.Events, opt => opt.Ignore());

            CreateMap<WarpDTO, Warp>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.uid))
                .ForMember(dest => dest.GachaId, opt => opt.MapFrom(src => Convert.ToInt32(src.gacha_id)))
                .ForMember(dest => dest.GachaType, opt => opt.MapFrom(src => Convert.ToInt32(src.gacha_type)))
                .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => Convert.ToInt32(src.item_id)))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => Convert.ToInt32(src.count)))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => Convert.ToDateTime(src.time)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.Lang, opt => opt.MapFrom(src => src.lang))
                .ForMember(dest => dest.ItemType, opt => opt.MapFrom(src => src.item_type))
                .ForMember(dest => dest.RankType, opt => opt.MapFrom(src => Convert.ToInt32(src.rank_type)))
                .ForMember(dest => dest.Guarantee, opt => opt.Ignore())
                .ForMember(dest => dest.Pity, opt => opt.Ignore())
                .ForMember(dest => dest.Gacha, opt => opt.Ignore())
                .ForMember(dest => dest.Item, opt => opt.Ignore())
                .ForMember(dest => dest.UserBannerInfo, opt => opt.Ignore())
                ;


        }
    }
}
