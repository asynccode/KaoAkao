using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KaoAKao.DAL;
using KaoAKao.Entity;

namespace KaoAKao.Business
{
    public class CommonBusiness
    {
        /// <summary>
        /// 判断表中某字段是否存在某值
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">字段名</param>
        /// <param name="columnValue">字段值</param>
        /// <param name="where">附加条件</param>
        /// <returns>true已存在</returns>
        public static bool isExistValue(string tableName, string columnName, string columnValue, string where)
        {
            object obj = CommonDAL.IsExistValue(tableName, columnName, columnValue, where);
            if (obj != null && obj != DBNull.Value)
            {
                return Convert.ToInt32(obj) > 0;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 修改表中某字段值
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">字段名</param>
        /// <param name="columnValue">字段值</param>
        /// <param name="where">附加条件 已 and 开头</param>
        /// <returns></returns>
        public static bool updateValue(string tableName, string columnName, string columnValue, string where)
        {
            int obj = CommonDAL.UpdateValue(tableName, columnName, columnValue, where);
            return obj > 0;
        }

        /// <summary>
        /// 获取表中某字段的值
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">字段名</param>
        /// <param name="where">附加条件 已 and 开头，后可跟GROUP BY 和 ORDER BY</param>
        /// <returns>object类型的值</returns>
        public static object GetColumnValue(string tableName, string columnName, string where)
        {
            return CommonDAL.GetColumnValue(tableName, columnName, where);
        }

        /// <summary>
        /// 获取表中某字段的值
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">字段名</param>
        /// <param name="where">附加条件 已 and 开头，后可跟GROUP BY 和 ORDER BY</param>
        /// <returns>object类型的值</returns>
        public static object GetColumnValue(SqlTransaction tran, string tableName, string columnName, string where)
        {
            return CommonDAL.GetColumnValue(tran, tableName, columnName, where);
        }

        /// <summary>
        /// 获取地区列表
        /// </summary>
        /// <returns></returns>
        public static List<AreaCityEntity> GetAreaCityList()
        {
            DataTable dt = CommonDAL.GetAreaCityList();

            List<AreaCityEntity> list = new List<AreaCityEntity>();
            foreach (DataRow dr in dt.Rows)
            {
                AreaCityEntity model = new AreaCityEntity();
                model.FillData(dr);
                list.Add(model);
            }
            return list;
        }

        #region 获取分页数据
        /// <summary>
        /// 获取分页数据集合
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columns">列明</param>
        /// <param name="condition">条件</param>
        /// <param name="key">主键，分页条件</param>
        /// <param name="orderColumn">排序字段</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCount">当前页数</param>
        /// <param name="totalNum">总记录数</param>
        /// <param name="totalPage">总页数</param>
        /// <param name="isAsc">主键是否升序</param>
        /// <returns></returns>
        public static DataTable GetPagerData(string tableName, string columns, string condition, string key, string orderColumn, int pageSize, int pageIndex, out int totalNum, out int pageCount, bool isAsc)
        {
            int asc = 0;
            if (isAsc)
            {
                asc = 1;
            }
            return CommonDAL.GetPagerData(tableName, columns, condition, key, orderColumn, pageSize, pageIndex, out totalNum, out pageCount, asc);
        }
        /// <summary>
        /// 获取分页数据集合(默认降序)
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columns">列明</param>
        /// <param name="condition">条件</param>
        /// <param name="key">主键，分页条件</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCount">当前页数</param>
        /// <param name="totalNum">总记录数</param>
        /// <param name="totalPage">总页数</param>
        /// <returns></returns>
        public static DataTable GetPagerData(string tableName, string columns, string condition, string key, int pageSize, int pageIndex, out int totalNum, out int pageCount)
        {
            return CommonDAL.GetPagerData(tableName, columns, condition, key, "", pageSize, pageIndex, out totalNum, out pageCount, 0);
        }
        /// <summary>
        /// 获取分页数据集合(默认降序)
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columns">列明</param>
        /// <param name="condition">条件</param>
        /// <param name="key">主键，分页条件</param>
        /// <param name="orderColumn">排序字段</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCount">当前页数</param>
        /// <param name="totalNum">总记录数</param>
        /// <param name="totalPage">总页数</param>
        /// <returns></returns>
        public static DataTable GetPagerData(string tableName, string columns, string condition, string key, string orderColumn, int pageSize, int pageIndex, out int totalNum, out int pageCount)
        {
            return CommonDAL.GetPagerData(tableName, columns, condition, key, orderColumn, pageSize, pageIndex, out totalNum, out pageCount, 0);
        }

        /// <summary>
        /// 获取分页数据集合
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columns">列明</param>
        /// <param name="condition">条件</param>
        /// <param name="key">主键，分页条件</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCount">当前页数</param>
        /// <param name="totalNum">总记录数</param>
        /// <param name="totalPage">总页数</param>
        /// <param name="isAsc">主键是否升序</param>
        /// <returns></returns>
        public static DataTable GetPagerData(string tableName, string columns, string condition, string key, int pageSize, int pageIndex, out int totalNum, out int pageCount, bool isAsc)
        {
            int asc = 0;
            if (isAsc)
            {
                asc = 1;
            }
            return CommonDAL.GetPagerData(tableName, columns, condition, key, "", pageSize, pageIndex, out totalNum, out pageCount, asc);
        }

        #endregion

    }
}
