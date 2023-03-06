using AutoMapper;
using Coins.Core.Models.Domins.Auth;
using Coins.Core.Models.Domins.Social;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Core.Models.DtoAPI.Auth;
using Coins.Core.Models.DtoAPI.Store;
using Coins.Core.Models.DtoAPI.User;
using Coins.Entities.Domins.Auth;
using Coins.Entities.Domins.Notification;
using Coins.Entities.Domins.StoresInfo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region User
            CreateMap<UserProfileDto, ApplicationUser>().ReverseMap();
            CreateMap<RegisterDto, ApplicationUser>()
                .ForSourceMember(x => x.Password, opt => opt.DoNotValidate()).ReverseMap();

            CreateMap<UserVoucherDTO, UserVouchers>().ReverseMap();

            #endregion

            #region Store
            CreateMap<Stores, StoresDto>().ReverseMap();
            CreateMap<StoreBranchs, StoreBranchDto>().ReverseMap();
            CreateMap<StoreCategoryDto, StoreCategory>().ReverseMap();
            CreateMap<StorePriceTypeDto, StorePriceType>().ReverseMap();
            CreateMap<VoucherDTO, Voucher>().ReverseMap();
            CreateMap<StoreProductDTO, StoreProducts>().ReverseMap();
            CreateMap<SocialTypesStoreDto, SocialTypesStores>().ReverseMap();
            #endregion

            CreateMap<NotificationDto, Notifications>().ReverseMap();


        }
    }
}
