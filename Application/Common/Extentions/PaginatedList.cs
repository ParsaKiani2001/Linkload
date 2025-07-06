using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Extentions
{
    public class PaginatedList<T>
    {
        public List<T> Models { get; set; }
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public PaginatedList(List<T> data,int pageIndex,int count,int pageSize,int totalCount) { 
            Models = data;
            PageIndex = pageIndex;
            Count = count;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, PaginatedRequest paginatedRequest)
        {
            var totalRow = await source.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRow / (double)paginatedRequest.PageSize);
            var pageIndex = paginatedRequest.PageIndex > totalPages ? totalPages != 0 ? totalPages : 1 : paginatedRequest.PageIndex;
            var items = await source.Skip((pageIndex - 1) * paginatedRequest.PageSize).Take(paginatedRequest.PageSize).ToListAsync();
            return new PaginatedList<T>(items,pageIndex,totalPages, paginatedRequest.PageSize, totalRow);
        }
        public static PaginatedList<T> Create(IQueryable<T> source, PaginatedRequest paginatedRequest)
        {
            var totalRow = source.Count();
            var totalPages = (int)Math.Ceiling(totalRow / (double)paginatedRequest.PageSize);
            var pageIndex = paginatedRequest.PageIndex > totalPages ? totalPages != 0 ? totalPages : 1 : paginatedRequest.PageIndex;
            var items = source.Skip((pageIndex - 1) * paginatedRequest.PageSize).Take(paginatedRequest.PageSize).ToList();
            return new PaginatedList<T>(items, pageIndex, totalPages, paginatedRequest.PageSize, totalRow);
        }

        public static async Task<List<T>> CreateListAsync(IQueryable<T> source, PaginatedRequest paginatedRequest)
        {
            var totalRow = await source.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRow / (double)paginatedRequest.PageSize);
            var pageIndex = paginatedRequest.PageIndex > totalPages ? totalPages != 0 ? totalPages : 1 : paginatedRequest.PageIndex;
            var items = await source.Skip((pageIndex - 1) * paginatedRequest.PageSize).Take(paginatedRequest.PageSize).ToListAsync();
            return items;
        }
    }
}
