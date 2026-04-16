using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace API.Controllers.SearchControllers;

[ApiController]
[Route("[controller]/[action]")]
public class SearchController : ControllerBase
{

    
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }


    [HttpGet()]
    public async Task<IActionResult> SearchProduct([FromQuery] string q, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var res=await _searchService.Search(q, page, pageSize); 
        
        return Ok(res);
    }
}