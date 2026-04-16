using Services.Dtos.SearchDtos;

namespace Services.Interfaces;

public interface ISearchService
{
    public Task<List<SearchResponse>> Search(string q, int page = 1, int pageSize = 20);
}