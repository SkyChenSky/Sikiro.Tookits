using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Framework.Common.Model;

namespace Framework.Common.Interface
{
    public interface IReadRepository<T> where T : class
    {
        #region Count

        /// <summary>
        /// 查询记录数
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> predicate);

        #endregion

        #region Exists

        /// <summary>
        /// 查询记录是否存在
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        bool Exists(Expression<Func<T, bool>> predicate);

        #endregion

        #region Get

        /// <summary>
        /// 查询个实体(指定字段)
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="predicate">条件</param>
        /// <param name="selector">查询字段</param>
        /// <param name="sort">排序字段</param>
        /// <returns></returns>
        TResult Get<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, Func<Sort<T>, Sort<T>> sort);

        /// <summary>
        /// 查询个实体(指定字段)
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="predicate">条件</param>
        /// <param name="selector">查询字段</param>
        /// <returns></returns>
        TResult Get<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector);

        /// <summary>
        /// 查询个实体
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="sort"></param>
        /// <returns></returns>
        T Get(Expression<Func<T, bool>> predicate, Func<Sort<T>, Sort<T>> sort);

        /// <summary>
        /// 查询个实体
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        T Get(Expression<Func<T, bool>> predicate);

        #endregion

        #region ToList

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="predicate">条件</param>
        /// <param name="selector">查询字段</param>
        /// <param name="sort">排序字段</param>
        /// <param name="top">取前X条</param>
        /// <returns></returns>
        List<TResult> ToList<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, Func<Sort<T>, Sort<T>> sort, int? top);

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="predicate">条件</param>
        /// <param name="selector">查询字段</param>
        /// <param name="sort">排序字段</param>
        /// <returns></returns>
        List<TResult> ToList<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, Func<Sort<T>, Sort<T>> sort);

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="predicate">条件</param>
        /// <param name="selector">查询字段</param>
        /// <returns></returns>
        List<TResult> ToList<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector);

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="sort">排序字段</param>
        /// <param name="top">取前X条</param>
        /// <returns></returns>
        List<T> ToList(Expression<Func<T, bool>> predicate, Func<Sort<T>, Sort<T>> sort, int? top);

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="top">取前X条</param>
        /// <returns></returns>
        List<T> ToList(Expression<Func<T, bool>> predicate, int top);

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        List<T> ToList(Expression<Func<T, bool>> predicate);

        #endregion

        #region PageList

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="predicate">条件</param>
        /// <param name="selector">查询字段</param>
        /// <param name="sort">排序字段</param>
        /// <param name="pageIndex">页面</param>
        /// <param name="pageSize">页长</param>
        /// <returns></returns>
        PageList<TResult> PageList<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, Func<Sort<T>, Sort<T>> sort, int pageIndex, int pageSize) where TResult : class;

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="predicate">条件</param>
        /// <param name="selector">查询字段</param>
        /// <param name="pageIndex">页面</param>
        /// <param name="pageSize">页长</param>
        /// <returns></returns>
        PageList<TResult> PageList<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, int pageIndex, int pageSize) where TResult : class;

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="sort">排序</param>
        /// <param name="pageIndex">页面</param>
        /// <param name="pageSize">页长</param>
        /// <returns></returns>
        PageList<T> PageList(Expression<Func<T, bool>> predicate, Func<Sort<T>, Sort<T>> sort, int pageIndex, int pageSize);

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="pageIndex">页面</param>
        /// <param name="pageSize">页长</param>
        /// <returns></returns>
        PageList<T> PageList(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize);

        #endregion
    }
}
