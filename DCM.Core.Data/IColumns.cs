using System.Collections.Generic;

namespace DCM.Core.Data
{
  public interface IColumns
  {
    List<ColumnModel> PrimaryKeys { get; }

    List<ColumnModel> GetAll();
  }
}
