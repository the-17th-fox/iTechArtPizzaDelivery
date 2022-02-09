﻿using PD.Domain.Constants.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Services.Pagination
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalItemsCount { get; private set; }
        public bool HasPreviousPage
        {
            get
            {
                if (TotalPages != 0
                        && CurrentPage > 1
                        && CurrentPage <= TotalPages) 
                    return true;

                else return false;
            }
        }
        public bool HasNextPage
        {
            get
            {
                if (TotalPages != 0 
                        && CurrentPage < TotalPages) 
                    return true;

                else return false;
            }
        }

        public PagedList(List<T> items, int totalPages, int itemsCount, int pageNumber, int pageSize)
        {
            TotalPages = totalPages;
            CurrentPage = pageNumber;
            PageSize = pageSize;
            TotalItemsCount = itemsCount;

            AddRange(items);
        }

        public static PagedList<T> ToPagedList(IQueryable<T> query, int pageNumber, int pageSize)
        {
            var itemsCount = query.Count();

            int totalPages = (int)Math.Ceiling(itemsCount / (double)pageSize);

            var items = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();

            return new PagedList<T>(items, totalPages, itemsCount, pageNumber, pageSize);
        }
    }
}
