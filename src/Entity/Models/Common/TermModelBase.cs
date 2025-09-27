using System.Collections.Generic;

namespace Entity.Models.Common;

public class TermModelBase
{
    public List<string>? FilteringExpressions { get; set; }
    public string? FilterPropName { get; set; }
    public string? FilterPropValue { get; set; }
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 10;
    public string? SortPropName { get; set; }
    public string SortDirection { get; set; } = "asc";
    public int Total { get; set; }
}