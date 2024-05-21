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
        }
    }
}
