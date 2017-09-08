using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EmitMapper;

namespace Framework.Common.Extension
{
    public static class DtoExtension
    {
        /// <summary>
        /// DTO映射
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="tFrom"></param>
        /// <returns></returns>
        public static TTo ToDto<TFrom, TTo>(this TFrom tFrom, IMappingConfigurator iMappingConfigurator = null)
        {
            var mapper = iMappingConfigurator == null ? ObjectMapperManager.DefaultInstance.GetMapper<TFrom, TTo>() : ObjectMapperManager.DefaultInstance.GetMapper<TFrom, TTo>(iMappingConfigurator);
            return mapper.Map(tFrom);
        }

        /// <summary>
        /// List转DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">集合</param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> list)
        {
            var type = typeof(T);

            var properties = type.GetProperties().ToList();

            var newDt = new DataTable(type.Name);

            properties.ForEach(propertie =>
            {
                Type columnType;
                if (propertie.PropertyType.IsGenericType && propertie.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    columnType = propertie.PropertyType.GetGenericArguments()[0];
                }
                else
                {
                    columnType = propertie.PropertyType;
                }

                newDt.Columns.Add(propertie.Name, columnType);
            });

            foreach (var item in list)
            {
                var newRow = newDt.NewRow();

                properties.ForEach(propertie =>
                {
                    newRow[propertie.Name] = propertie.GetValue(item, null) ?? DBNull.Value;
                });

                newDt.Rows.Add(newRow);
            }

            return newDt;
        }
    }
}
