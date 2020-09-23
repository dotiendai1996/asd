using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hinnova.Dto
{
    /// <summary>
    /// Implements <see cref="IPagedResult{T}"/>.
    /// </summary>
    /// <typeparam name="T">Type of the items in the <see cref="ListResultDto{T}.Items"/> list</typeparam>
    [Serializable]
    public class PagedFilterResultDto<T> : ListResultDto<T>, IPagedResult<T>
    {
        /// <summary>
        /// Total count of Items.
        /// </summary>
        public int TotalCount { get; set; }
       // public Object DataForFilters { get; set; }

        /// <summary>
        /// Creates a new <see cref="PagedResultDto{T}"/> object.
        /// </summary>
        public PagedFilterResultDto()
        {

        }

        /// <summary>
        /// Creates a new <see cref="PagedResultDto{T}"/> object.
        /// </summary>
        /// <param name="totalCount">Total count of Items</param>
        /// <param name="items">List of items in current page</param>
        ///
        // public PagedFilterResultDto(int totalCount, IReadOnlyList<T> items, Object dataForFilters)
        //: base(items)
        //{
        //    TotalCount = totalCount;
        //    DataForFilters = dataForFilters;
        //}
    public PagedFilterResultDto(int totalCount, IReadOnlyList<T> items)
            : base(items)
        {
            TotalCount = totalCount;
           
        }
    }
}
