using AutoMapper;

namespace Options
{
    public class AutoMapperService : IAutoMapperService
    {
        #region Class List AutoMapping
        public List<TDestination> AutoMapping<TSource, TDestination>(List<TSource> data) where TSource : class where TDestination : class
        => mapper<TSource, TDestination>().Map<List<TSource>, List<TDestination>>(data);

        public List<TDestination> AutoMapping<TSource, TDestination>(List<TSource> data, CustomMap<TSource, TDestination> customMap) where TSource : class where TDestination : class
        => new MapperConfiguration(cfg => customMap(cfg)).CreateMapper().Map<List<TSource>, List<TDestination>>(data);
        #endregion

        public IEnumerable<TDestination> AutoMapping<TSource, TDestination>(IEnumerable<TSource> data) where TSource : class where TDestination : class
        => mapper<TSource, TDestination>().Map<IEnumerable<TSource>, IEnumerable<TDestination>>(data);

        #region  Class Array AutoMapping
        public TDestination[] AutoMapping<TSource, TDestination>(TSource[] data) where TSource : class where TDestination : class
        => mapper<TSource, TDestination>().Map<TSource[], TDestination[]>(data);
        #endregion

        #region Class AutoMapping
        public TDestination AutoMapping<TSource, TDestination>(TSource data) where TSource : class where TDestination : class
         => mapper<TSource, TDestination>().Map<TDestination>(data);


        public TDestination AutoMapping<TSource, TDestination>(TSource data, CustomMap<TSource, TDestination> customMap) where TSource : class where TDestination : class
        => new MapperConfiguration(cfg => customMap(cfg)).CreateMapper().Map<TSource, TDestination>(data);
        #endregion

        private IMapper mapper<TSource, TDestination>() => new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>()).CreateMapper();

    }
}
