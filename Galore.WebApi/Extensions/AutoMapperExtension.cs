using System;
using Galore.Models.Tape;
using Microsoft.AspNetCore.Builder;

namespace Galore.WebApi.Extensions
{
    public static class AutoMapperExtension
    {
        public static void ConfigureAutoMapper(this IApplicationBuilder app)
        {
            AutoMapper.Mapper.Initialize( c => {
                c.CreateMap<Tape, TapeDTO>();
                c.CreateMap<TapeDTO, Tape>();
                c.CreateMap<Tape, TapeDetailDTO>();
                c.CreateMap<TapeInputModel, Tape>()
                    .ForMember(t => t.DateCreated, opt => opt.UseValue(DateTime.Now))
                    .ForMember(t => t.DateModified, opt => opt.UseValue(DateTime.Now));
            });
        }
    }
}