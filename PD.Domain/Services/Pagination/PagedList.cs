using PD.Domain.Constants.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Services.Pagination
{
    public class PagedList<T> : List<T>
    {
        private int _currentPage;
        public int CurrentPage
        {
            get => _currentPage;
            private set
            {
                _currentPage = (value > TotalPages) ? TotalPages : value;
            }
        }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalItemsCount { get; private set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public PagedList(List<T> items, int totalPages, int itemsCount, int pageNumber, int pageSize)
        {
            TotalPages = totalPages;
            CurrentPage = pageNumber;
            PageSize = pageSize;
            TotalItemsCount = itemsCount;

            AddRange(items);
        }

        public static PagedList<T> ToPagedList(List<T> list, int pageNumber, int pageSize)
        {
            var itemsCount = list.Count();

            int totalPages = (int)Math.Ceiling(itemsCount / (double)pageSize);
            pageNumber = (pageNumber > totalPages) ? totalPages : pageNumber;

            var items = list
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedList<T>(items, totalPages, itemsCount, pageNumber, pageSize);
        }
    }
}
