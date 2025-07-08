using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSESG.CargoCare.Core
{

    //
    // Summary:
    //     Specifies how rows of data are sorted.
    public enum SortDir
    {
        //
        // Summary:
        //     The default. No sort order is specified.
        Unspecified = -1,
        //
        // Summary:
        //     Rows are sorted in ascending order.
        Ascending = 0,
        //
        // Summary:
        //     Rows are sorted in descending order.
        Descending = 1
    }
    public class FilterModel
    {
        private SortDir dir;

        public FilterModel()
        {
            
        }
        public FilterModel(int page, int pageSize, int totalItems, int column, string? filter, int sortColumn, SortDir dir, SelloStatus estatus = SelloStatus.None)
        {
            Page = page;
            PageSize = pageSize;
            TotalItems = totalItems;
            Column = column;
            Filter = filter;
            SortColumn = sortColumn;
            SortDir = dir;
            Estatus = estatus;
        }

        public string? Filter { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 30;
        public int TotalItems { get; set; }
        public int Column { get; set; }
        public int SortColumn { get; set; }
        public SortDir SortDir { get; set; }
        public SelloStatus Estatus { get; set; } =SelloStatus.None;

        public int Skip => (Page - 1) * PageSize;
    }
}
