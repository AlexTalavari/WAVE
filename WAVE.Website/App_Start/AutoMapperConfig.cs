using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using AutoMapper.Impl;
using WAVE.Website.Models;

namespace WAVE.Website.App_Start
{
    public static class AutoMapperConfig
    {
        internal static void RegisterAutoMapper()
        {
            //Mapper.CreateMap< User,UserDto>();
            //Mapper.CreateMap<List<UserDto>, List<User>>();

            //Mapper.CreateMap<Action, HomePageDto>();
            //Mapper.CreateMap<List<HomePageDto>,List<Action>>();

            //Mapper.CreateMap<User, UserDataDto>();            
            //.IgnoreAllNonExisting();

            Mapper.CreateMap<LayoutModel, HomeModel>();
            Mapper.CreateMap<LayoutModel, MessagesModel>();
            Mapper.CreateMap<LayoutModel, UserModel>();
            Mapper.CreateMap<LayoutModel, EventsDetailsModel>();
            Mapper.CreateMap<LayoutModel, EventsIndexModel>();
            Mapper.CreateMap<LayoutModel, LoginModel>();
            Mapper.CreateMap<LayoutModel, ExternalLogin>();
            Mapper.CreateMap<LayoutModel, LocalPasswordModel>();
            Mapper.CreateMap<LayoutModel, RegisterExternalLoginModel>();

            foreach (var map in Mapper.GetAllTypeMaps())
            {
                if (typeof (LayoutModel).IsAssignableFrom(map.DestinationType))
                {
                    var propInfo = map.DestinationType.GetProperty("Title");
                    if (propInfo != null)
                        map.FindOrCreatePropertyMapFor(new PropertyAccessor(propInfo)).Ignore();
                }
            }
        }

        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> expression)
        {
            var sourceType = typeof (TSource);
            var destinationType = typeof (TDestination);
            var existingMaps =
                Mapper.GetAllTypeMaps()
                    .First(x => x.SourceType.Equals(sourceType) && x.DestinationType.Equals(destinationType));
            foreach (var property in existingMaps.GetUnmappedPropertyNames())
            {
                expression.ForMember(property, opt => opt.Ignore());
            }
            return expression;
        }
    }
}