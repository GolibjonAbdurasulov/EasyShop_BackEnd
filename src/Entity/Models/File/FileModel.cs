using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models.File;

[Table("file_model")]
public class FileModel : ModelBase<Guid>
{
    [Column("file_name")] public string FileName { get; set; }
    [Column("content_type")] public string ContentType { get; set; }
    [Column("path")] public string Path { get; set; }
}