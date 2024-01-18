using AutoMapper;
using AutoMapper.QueryableExtensions;
using EnglishService.Utities;
using System.Linq.Expressions;

namespace EnglishService.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<TDestination> To<TDestination>(
            this IQueryable source,
            params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            return source.ProjectTo(MappingConfig.MapperInstance.ConfigurationProvider, membersToExpand);
        }

        public static IQueryable<TDestination> To<TDestination>(
            this IQueryable source,
            object parameters)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ProjectTo<TDestination>(MappingConfig.MapperInstance.ConfigurationProvider, parameters);
        }
    }
}
