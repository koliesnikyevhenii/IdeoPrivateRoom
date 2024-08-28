using AutoMapper;
using IdeoPrivateRoom.DAL.Models;

namespace IdeoPrivateRoom.WebApi.Mapping.Converters;

public class VacationsPagedListConverter<TSource, TDestination> : ITypeConverter<PagedList<TSource>, PagedList<TDestination>>
{
    private readonly IMapper _mapper;

    public VacationsPagedListConverter(IMapper mapper)
    {
        _mapper = mapper;
    }

    public PagedList<TDestination> Convert(PagedList<TSource> source, PagedList<TDestination> destination, ResolutionContext context)
    {
        var mappedData = _mapper.Map<IEnumerable<TDestination>>(source.Data);
        return new PagedList<TDestination>(mappedData, source.PageNumber, source.PageSize, source.TotalRecords);
    }
}