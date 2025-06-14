using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core
{
    public interface IUpdateable
    {
        int Id { get; set; }
        int? Correlativo { get; set; }
        int? SourceId { get; set; }
        int? ClienteId { get; set; }
        int? EmpresaId { get; set; }
        int? ServerId { get; set; }
        int? LocalId { get; set; }
        StatusType Status { get; set; } 
        int? CreatedUser { get; set; }
        string? Nota { get; set; }
        DateTime? CreatedDate { get; set; }
        int? ModifiedUser { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
}
