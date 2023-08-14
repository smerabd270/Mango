﻿using AutoMapper;
using Mango.Services.CouponApi.Models;
using Mango.Services.CouponApi.Models.Dto;

namespace Mango.Services.CouponApi
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig=new MapperConfiguration(config=>
            {
                config.CreateMap<Coupon, CouponDto>();
                config.CreateMap<CouponDto, Coupon>();
            });
            return mappingConfig;
        }
    }
}
