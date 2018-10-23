using System;
using Galore.Models.Loan;
using Galore.Models.Tape;
using Galore.Models.User;
using Galore.Models.Review;
using Microsoft.AspNetCore.Builder;

namespace Galore.WebApi.Extensions
{
    public static class AutoMapperExtension
    {
        public static void ConfigureAutoMapper(this IApplicationBuilder app)
        {
            AutoMapper.Mapper.Initialize(c =>
            {
                c.CreateMap<Tape, TapeDTO>();
                c.CreateMap<TapeDTO, Tape>();
                c.CreateMap<Tape, TapeDetailDTO>();
                c.CreateMap<TapeInputModel, Tape>()
                    .ForMember(t => t.DateCreated, opt => opt.UseValue(DateTime.Now))
                    .ForMember(t => t.DateModified, opt => opt.UseValue(DateTime.Now))
                    .ForMember(l => l.ReleaseDate, opt => opt.MapFrom(src => DateTime.Parse(src.ReleaseDate)));

                c.CreateMap<User, UserDTO>();
                c.CreateMap<UserDTO, User>();
                c.CreateMap<User, UserDetailDTO>();
                c.CreateMap<UserInputModel, User>()
                    .ForMember(u => u.DateCreated, opt => opt.UseValue(DateTime.Now))
                    .ForMember(u => u.DateModified, opt => opt.UseValue(DateTime.Now));
                c.CreateMap<LoanInputModel, Loan>()
                    .ForMember(l => l.BorrowDate, opt => opt.MapFrom(src => DateTime.Parse(src.BorrowDate)));

                c.CreateMap<Review, ReviewDTO>();
                c.CreateMap<ReviewDTO, Review>();
                c.CreateMap<Review, UserDetailDTO>();
                c.CreateMap<ReviewInputModel, Review>()
                    .ForMember(u => u.DateCreated, opt => opt.UseValue(DateTime.Now))
                    .ForMember(u => u.DateModified, opt => opt.UseValue(DateTime.Now));
            });
        }
    }
}