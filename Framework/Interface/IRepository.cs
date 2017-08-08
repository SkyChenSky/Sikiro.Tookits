using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Framework.Common.Interface
{
    public interface IRepository<T> : IReadRepository<T> where T : class
    {
        #region Add 

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>影响行数</returns>
        bool Add(T entity);

        #endregion

        #region Update 

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>影响行数</returns>
        bool Update(T entity);

        /// <summary>
        /// 按条件更新实体
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="expression">更新字段</param>
        /// <returns></returns>
        int Update(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> expression);

        #endregion

        #region Delete 

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Delete(T entity);

        /// <summary>
        /// 删除按条件
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Delete(Expression<Func<T, bool>> predicate);

        #endregion

        #region BatchAdd

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities"></param>
        void BatchAdd(IEnumerable<T> entities);

        #endregion
    }
}
