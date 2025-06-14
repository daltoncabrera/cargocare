using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MSESG.CargoCare.Core
{
    public class Updateable : IUpdateable, IIdentificable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
        public virtual int? Correlativo { get; set; }
        public virtual int? SourceId { get; set; }
        public virtual int? EmpresaId { get; set; }
        public virtual int? ClienteId { get; set; }
        public virtual int? ServerId { get; set; }
        public virtual int? LocalId { get; set; }
        public virtual StatusType Status { get; set; } = StatusType.Creado;
        public virtual string Nota { get; set; }
        public virtual int? CreatedUser { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual int? ModifiedUser { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
        public string ExtraData { get; set; }
    }
}
