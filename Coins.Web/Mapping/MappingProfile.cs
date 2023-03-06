using AutoMapper;
using Coins.Core.Models.Domins.Home;
using Coins.Core.Models.Domins.Social;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Entities.Domins.Auth;
using Coins.Entities.Domins.StoresInfo;
using Coins.Web.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region User
            CreateMap<ApplicationUser, CasherVM>().ReverseMap();
            CreateMap<ApplicationUser, CasherEditVM>().ReverseMap();
            #endregion

            #region Store
            CreateMap<Stores, StoreVM>().ReverseMap();
            CreateMap<Stores, StoreEditVM>().ReverseMap();
            CreateMap<Stores, MyStoreEditVM>().ReverseMap();
            CreateMap<Stores, StoreSettingsVM>().ReverseMap();
            CreateMap<StoreBranchs, StoreBranchObjVM>().ReverseMap();
            CreateMap<StoreProducts, StoreProductVM>().ReverseMap();
            CreateMap<StoreCategory, StoreCategoryVM>().ReverseMap();
            CreateMap<StorePriceType, StorePriceTypeVM>().ReverseMap();
            //CreateMap<StorePriceType, StorePriceTypeVM>().ReverseMap();
            CreateMap<Advantages, AdvantagesVM>().ReverseMap();
            CreateMap<SocialTypesStores, SocialStoreVM>().ReverseMap();
            CreateMap<SocialTypesStores, SocialStoreGMVM>().ReverseMap();
            CreateMap<Voucher, VoucherVM>().ReverseMap();
            CreateMap<SliderHome, SliderHomeVM>().ReverseMap();
            #endregion

        }
    }
}
