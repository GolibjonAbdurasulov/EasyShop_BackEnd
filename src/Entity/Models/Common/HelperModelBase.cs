using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models.Common;

public abstract class HelperModelBase<T> : ModelBase<T> where T : struct
{
    [Column("code")] public int? Code { get; set; }
    [Column("group_name", TypeName = "jsonb")] public MultiLanguageField? GroupName { get; set; }
}