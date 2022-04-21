using System;
using System.Collections.Generic;

namespace DigitalFolder.Data.Dtos
{
    public class PaginationResponse<T> where T : class
    {
        public PaginationResponse(int totalCount, int page, int itemsPerPage)
        {
            TotalCount = totalCount;
            Page = page;
            TotalPages = (int)Math.Ceiling(totalCount / (double)itemsPerPage);
            ItemsPerPage = itemsPerPage;
        }

        private const int _maxItemsPerPage = 50;
        
        private int _itemsPerPage;
        public int ItemsPerPage
        {
            get => _itemsPerPage;
            set => _itemsPerPage =  value > _maxItemsPerPage ? _maxItemsPerPage : value;
        }

        public List<T> Data { get; set; }

        public int Page { get; set; } 
        public int TotalCount { get; private set; }

        public int TotalPages { get; private set; }

        public bool HasPrevious => Page > 1;

        public bool HasNext => Page < TotalPages;

    }
}
