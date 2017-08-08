using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using Framework.Common.Extension;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Framework.Files
{
    #region Excel类
    /// <summary>
    /// Excel类
    /// </summary>
    public class ExcelClient
    {
        #region 初始化变量
        private XSSFWorkbook _wb;

        /// <summary>
        /// 时间样式
        /// </summary>
        private ICellStyle _dataTimeStyle;
        private ICellStyle DataTimeStyle
        {
            get
            {
                if (_dataTimeStyle.IsNotNull())
                {
                    return _dataTimeStyle;
                }

                var cellStyle = _wb.CreateCellStyle();
                cellStyle.DataFormat = _wb.CreateDataFormat().GetFormat("yyyy-MM-dd HH:mm:ss");
                _dataTimeStyle = cellStyle;

                return cellStyle;
            }

        }

        /// <summary>
        /// 表头样式
        /// </summary>
        private ICellStyle _headStyle;
        private ICellStyle HeadStyle
        {
            get
            {
                if (_headStyle.IsNotNull())
                    return _headStyle;

                //居中
                var cellStyle = _wb.CreateCellStyle();
                cellStyle.Alignment = HorizontalAlignment.Center;
                _headStyle = cellStyle;

                return cellStyle;
            }
        }
        #endregion

        #region 导出
        /// <summary>
        /// 网络中导出 Excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="fileName">文件名称（不需要后缀）</param>
        public void HttpExport<T>(IEnumerable<T> source, string fileName = "")
        {
            CreateExcel(source);
            if (string.IsNullOrEmpty(fileName))
                fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}.xlsx", fileName));
            _wb.Write(HttpContext.Current.Response.OutputStream);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 导出Excel到本地
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="fileName">文件保存路径</param>
        public void FileExport<T>(IEnumerable<T> source, string fileName)
        {
            CreateExcel(source);
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                _wb.Write(fs);
            }
        }

        /// <summary>
        /// 创建excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        private void CreateExcel<T>(IEnumerable<T> source)
        {
            _wb = new XSSFWorkbook();
            var sh = (XSSFSheet)_wb.CreateSheet("Sheet1");

            var props = _wb.GetProperties();
            props.CoreProperties.Creator = "陈珙公司";
            props.CoreProperties.Created = DateTime.Now;

            var properties = typeof(T).GetProperties().ToList();

            //构建表头
            var header = sh.CreateRow(0);
            for (var j = 0; j < properties.Count; j++)
            {
                var display = properties[j].GetCustomAttributes(typeof(DisplayAttribute), true)
                    .Cast<DisplayAttribute>()
                    .FirstOrDefault();
                var headCell = header.CreateCell(j);
                headCell.CellStyle = HeadStyle;
                headCell.SetCellValue(display != null ? display.Name : properties[j].Name);
            }
            var list = source.ToList();

            //填充数据
            for (var i = 0; i < list.Count; i++)
            {
                var r = sh.CreateRow(i + 1);
                for (var j = 0; j < properties.Count; j++)
                {
                    var value = properties[j].GetValue(list[i], null).ToStr();
                    if (properties[j].PropertyType == typeof(DateTime))
                    {
                        var dataTimeCell = r.CreateCell(j);
                        dataTimeCell.CellStyle = DataTimeStyle;
                        dataTimeCell.SetCellValue(value.TryDateTime());
                    }
                    else if (properties[j].PropertyType == typeof(bool))
                    {
                        r.CreateCell(j).SetCellValue(value.TryBool());
                    }
                    else if (properties[j].PropertyType == typeof(int) || properties[j].PropertyType == typeof(decimal) ||
                             properties[j].PropertyType == typeof(float) ||
                             properties[j].PropertyType == typeof(double) || properties[j].PropertyType == typeof(long))
                    {
                        r.CreateCell(j).SetCellValue(value.TryDouble());
                    }
                    else
                    {
                        r.CreateCell(j).SetCellValue(value);
                    }
                }
            }

            for (var j = 0; j < properties.Count; j++)
            {
                sh.AutoSizeColumn(j);
            }
        }
        #endregion

        #region 导入
        /// <summary>
        /// 本地导入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public IEnumerable<T> FileImport<T>(string fileName) where T : new()
        {
            var fileStream = new FileStream(fileName, FileMode.Open);
            return GetDataFromExcel<T>(fileStream);
        }

        /// <summary>
        /// 网络导入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="postedFile"></param>
        /// <returns></returns>
        public IEnumerable<T> HttpImport<T>(HttpPostedFileBase postedFile) where T : new()
        {
            return GetDataFromExcel<T>(postedFile.InputStream);
        }

        /// <summary>
        /// 从excel获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="excelStrem"></param>
        /// <returns></returns>
        private IEnumerable<T> GetDataFromExcel<T>(Stream excelStrem) where T : new()
        {
            _wb = new XSSFWorkbook(excelStrem);
            var list = new List<T>();
            var sh = _wb.GetSheetAt(0) as XSSFSheet;
            if (sh == null)
                return list;

            var header = sh.GetRow(0);
            var dicColumns = new Dictionary<int, string>();
            var columns = header.Cells.Select(cell => cell.StringCellValue).ToArray();
            var importEntityList = typeof(T).GetProperties().Select(property =>
            {
                var displayAttribute = property.GetCustomAttribute<DisplayAttribute>();
                var displayName = displayAttribute.IsNotNull() ? displayAttribute.Name : null;
                return new ImportEntity { PropertyName = property.Name, DisplayName = displayName };
            }).ToArray();

            for (var i = 0; i < columns.Length; i++)
            {
                var entity = importEntityList.FirstOrDefault(a => a.DisplayName == columns[i] || a.PropertyName == columns[i]);
                if (entity.IsNotNull())
                    dicColumns.Add(i, entity.PropertyName);
            }

            for (var i = 1; i <= sh.LastRowNum; i++)
            {
                var obj = new T();
                var row = sh.GetRow(i);

                foreach (var key in dicColumns.Keys)
                {
                    var property = typeof(T).GetProperty(dicColumns[key]);
                    var pType = property.PropertyType.FullName;
                    var value = row.GetCell(key).ToStr();
                    if (string.IsNullOrEmpty(value))
                        continue;

                    switch (pType)
                    {
                        case "System.Int32":
                            property.SetValue(obj, value.TryInt());
                            break;
                        case "System.Int64":
                            property.SetValue(obj, value.TryLong());
                            break;
                        case "System.Double":
                            property.SetValue(obj, value.TryDouble());
                            break;
                        case "System.Decimal":
                            property.SetValue(obj, value.TryDecimal());
                            break;
                        case "System.Boolean":
                            property.SetValue(obj, value.TryBool());
                            break;
                        case "System.Single":
                            property.SetValue(obj, value.TryFloat());
                            break;
                        case "System.DateTime":
                            property.SetValue(obj, value.TryDateTime());
                            break;
                        case "System.String":
                            property.SetValue(obj, value);
                            break;
                    }
                }
                list.Add(obj);
            }
            return list;
        }
        #endregion
    }
    #endregion
}
