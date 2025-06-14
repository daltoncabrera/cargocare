using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DCM.Core.Data
{
  public class GenericSearch
  {
    private int recordPerPage = 50;
    private const string META_PROPERTY_NAME = "Meta";
    private const string ENTITY_NAME = "Entity";
    private const int RECORDS_PER_PAGE = 50;
    private const string DESENDING_ORDER_BY = " Desc";
    private IQueryable query_original;
    private IQueryable query;
    private IMeta meta;
    private List<ColumnModel> keyList;
    private List<ColumnModel> columns;
    private List<ColumnFilter> FilterStatic;
    private ColumnModel orderBy;
    private Type tipo;
    private int count;

    public List<ColumnModel> KeyList
    {
      get
      {
        return this.keyList;
      }
    }

    public int RecordPerPage
    {
      get
      {
        return this.recordPerPage;
      }
      set
      {
        this.recordPerPage = value > 0 ? value : 50;
      }
    }

    public int TotalRecords
    {
      get
      {
        return this.count;
      }
    }

    public IMeta Meta
    {
      get
      {
        return this.meta;
      }
    }

    public IQueryable OriginalQuery
    {
      get
      {
        return this.query_original;
      }
    }

    public List<ColumnModel> Columns
    {
      get
      {
        return this.columns;
      }
    }

    public void SetEntity<T>(IQueryable<T> query, List<ColumnFilter> filterStatic = null, List<ColumnModel> columns = null, List<ColumnModel> keyList = null, ColumnModel orderBy = null)
    {
      this.keyList = keyList;
      this.columns = columns;
      this.orderBy = orderBy;
      this.query_original = (IQueryable) query;
      this.tipo = typeof (T);
      this.FilterStatic = filterStatic;
      this.searchInit();
    }

    public IQueryable GetResult(int page = 1)
    {
      int count = (page - 1) * this.RecordPerPage;
      this.query = this.query.OrderBy((this.meta.ColumnsBase.PrimaryKeys.Count > 0 ? this.meta.ColumnsBase.PrimaryKeys[0] : this.meta.ColumnsBase.GetAll()[0]).Nombre + " Desc");
      return this.query.Skip(count).Take(this.RecordPerPage).Select(string.Format("new({0})", (object) this.columns.GetNameStringSplit()));
    }

    public IQueryable GetAllResult()
    {
      this.query = this.query.OrderBy((this.meta.ColumnsBase.PrimaryKeys.Count > 0 ? this.meta.ColumnsBase.PrimaryKeys[0] : this.meta.ColumnsBase.GetAll()[0]).Nombre + " Desc");
      return this.query.Select(string.Format("new({0})", (object) this.columns.GetNameStringSplit()));
    }

    private void checkKeys()
    {
      if (this.keyList != null && this.keyList.Count > 0)
        return;
      if (this.meta.ColumnsBase.PrimaryKeys.Count > 0)
      {
        this.keyList = this.meta.ColumnsBase.PrimaryKeys;
      }
      else
      {
        if (this.columns.Count <= 0)
          return;
        this.keyList = new List<ColumnModel>()
        {
          this.columns[0]
        };
      }
    }

    public int SetFilter(List<ColumnFilter> filter)
    {
      List<ColumnFilter> columnFilterList = new List<ColumnFilter>();
      if (filter != null && filter.Count > 0)
        columnFilterList.AddRange((IEnumerable<ColumnFilter>) filter);
      if (this.FilterStatic != null && this.FilterStatic.Count > 0)
        columnFilterList.AddRange((IEnumerable<ColumnFilter>) this.FilterStatic);
      if (columnFilterList.Count > 0)
        this.query = this.query_original.Where(filter.GetFiltersInList(Compare.And));
      this.count = this.query.Count();
      return this.count;
    }

    public int ClearFilter()
    {
      this.query = this.query_original;
      this.count = this.query.Count();
      return this.count;
    }

    private void searchInit()
    {
      this.query = this.query_original;
      Expression.Parameter(this.tipo, "Entity");
      this.meta = this.tipo.GetField("Meta", BindingFlags.Static | BindingFlags.Public).GetValue((object) this.tipo) as IMeta;
      if (this.meta == null)
        throw new Exception("La entidad no fue generada con el template modificado para los fines de busquedas.");
      this.columns = this.columns ?? this.meta.ColumnsBase.GetAll();
      this.count = this.query.Count();
      this.checkKeys();
    }

    public IQueryable<T> Search<T>(List<ColumnFilter> listFilter)
    {
      return Queryable.Cast<T>(this.query_original.Where(listFilter.GetFiltersInList(Compare.And)));
    }

    public T SearchOne<T>(List<ColumnFilter> listFilter)
    {
      return Queryable.FirstOrDefault<T>(this.Search<T>(listFilter));
    }

    public static IQueryable<T> Search<T>(IQueryable<T> query, List<ColumnFilter> listFilter)
    {
      return query.Where<T>(listFilter.GetFiltersInList(Compare.And));
    }

    public static T SearchOne<T>(IQueryable<T> query, List<ColumnFilter> listFilter)
    {
      return Queryable.FirstOrDefault<T>(GenericSearch.Search<T>(query, listFilter));
    }

    public static IQueryable<T> Search<T>(IQueryable<T> query, List<ColumnFilter> listFilter, int page = 1, int recordPerpage = 50)
    {
      int count = (page - 1) * recordPerpage;
      return Queryable.Take<T>(Queryable.Skip<T>(query.Where<T>(listFilter.GetFiltersInList(Compare.And)), count), recordPerpage);
    }

    public static IQueryable<T> GetPage<T>(IQueryable<T> query, int page = 1, int recordPerpage = 50)
    {
      int count = (page - 1) * recordPerpage;
      return Queryable.Take<T>(Queryable.Skip<T>(query, count), recordPerpage);
    }
  }
}
