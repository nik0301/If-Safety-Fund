using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using IFLike.Domain;
using IFLike.Dto;

namespace IFLike.Services
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration GetConfig()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Poll, PollDto>();
                cfg.CreateMap<PollDto, Poll>().ForAllMembers(opts=>opts.Ignore());

                cfg.CreateMap<PollItemDto, PollItem>().ForAllMembers(opts => opts.Ignore()); ;
                cfg.CreateMap< PollItem, PollItemDto> ();



            });
            return config;
        }
    }
}
