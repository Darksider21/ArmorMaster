using ArmorMaster.Buisiness.DTO.ModelsDTO;
using ArmorMaster.Data.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Buisiness.Mapper
{
    public class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<ArmorMasterMapper>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }

    public class ArmorMasterMapper : Profile
    {
        public ArmorMasterMapper()
        {
            CreateMap<Item, ItemModel>().ReverseMap();
            CreateMap<Player, PlayerModel>().ReverseMap();
            CreateMap<ItemBonusStat, ItemBonusStatModel>().ReverseMap();
            CreateMap<Item, ItemOwnerModel>().ReverseMap();
        }

    }
}
