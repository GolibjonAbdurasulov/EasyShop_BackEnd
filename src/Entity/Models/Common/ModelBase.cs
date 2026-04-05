using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models.Common;

public abstract class ModelBase<TId> where TId: struct
{
    [Column("id")]
    public  virtual TId Id { get; set; }
}