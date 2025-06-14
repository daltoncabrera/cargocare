namespace DCM.Core.Data
{
  public class SearchResult
  {
    private ColumnModel column;
    private object value;

    public ColumnModel Column
    {
      get
      {
        return this.column;
      }
    }

    public object Value
    {
      get
      {
        return this.value;
      }
    }

    public SearchResult(ColumnModel column, object value)
    {
      this.column = column;
      this.value = value;
    }
  }
}
