namespace DCM.Core.Data
{
  public interface IMeta
  {
    IColumns ColumnsBase { get; }

    string TableName { get; }

    string FullTableName { get; }
  }
}
