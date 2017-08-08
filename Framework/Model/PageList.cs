using System.Collections.Generic;

namespace Framework.Common.Model
{
    /// <summary>
    /// 分页实体类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageList<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页项</param>
        /// <param name="totalCount">总数</param>
        /// <param name="items">元素</param>
        public PageList(int pageIndex, int pageSize, int totalCount, List<T> items)
        {
            _totalCount = totalCount;
            _pageSize = pageSize;
            _pageIndex = pageIndex;
            Items = items;
            _totalPage = _totalCount % _pageSize == 0 ? _totalCount / _pageSize : _totalCount / _pageSize + 1;
        }

        private readonly int _totalCount;
        /// <summary>
        /// 总数
        /// </summary>
        public int Total => _totalCount;

        /// <summary>
        /// 元素
        /// </summary>
        public List<T> Items { get; }


        private readonly int _pageSize;
        /// <summary>
        /// 页项
        /// </summary>
        public int PageSize => _pageSize;

        private readonly int _pageIndex;
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex => _pageIndex;

        private readonly int _totalPage;
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage => _totalPage;

        /// <summary>
        /// 是否有上一页
        /// </summary>
        public bool HasPrev => _pageIndex > 1;

        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool HasNext => _pageIndex < _totalPage;
    }
}
