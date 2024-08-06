using AutoMapper;
namespace Options
{
    public delegate IMappingExpression<TSource, TDestination> CustomMap<TSource, TDestination>(IMapperConfigurationExpression cfg);
    public interface IAutoMapperService
    {
        IEnumerable<TDestination> AutoMapping<TSource, TDestination>(IEnumerable<TSource> data)
             where TSource : class
             where TDestination : class;
        List<TDestination> AutoMapping<TSource, TDestination>(List<TSource> data)
            where TSource : class
            where TDestination : class;
        List<TDestination> AutoMapping<TSource, TDestination>(List<TSource> data, CustomMap<TSource, TDestination> customMap)
            where TSource : class
            where TDestination : class;
        TDestination AutoMapping<TSource, TDestination>(TSource data)
            where TSource : class
            where TDestination : class;
        TDestination AutoMapping<TSource, TDestination>(TSource data, CustomMap<TSource, TDestination> customMap)
            where TSource : class
            where TDestination : class;
        TDestination[] AutoMapping<TSource, TDestination>(TSource[] data)
            where TSource : class
            where TDestination : class;
    }
}
