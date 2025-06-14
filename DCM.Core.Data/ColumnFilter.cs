namespace DCM.Core.Data
{
  public class ColumnFilter
  {
    private const string GreaterThanOrEqual = ">=";
    private const string LessThanOrEqual = "<=";

    public ColumnModel Column { get; set; }

    public Compare Comparison { get; set; }

    public object Value1 { get; set; }

    public object Value2 { get; set; }

    public string GetAsString()
    {
      string empty = string.Empty;
      string str;
      if (this.Comparison == Compare.Like && MarhexTypes.IsString(this.Column.Tipo))
        str = string.Format("{0}.Contains(\"{1}\")", (object) this.Column.Nombre, this.Value1);
      else if (this.Comparison == Compare.Between)
        str = string.Format("({0} {1} {2}) and ({0} {3} {4})", (object) this.Column.Nombre, (object) ">=", this.Value1, (object) "<=", this.Value2);
      else
        str = string.Format("{0} {1} {2}", (object) this.Column.Nombre, (object) MarhexTypes.GetComparisonOperator(this.Comparison), this.Value1);
      return str;
    }
  }
}
