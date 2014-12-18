using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaoAKao.DAL
{
    public class CommonDAL : BaseDAL
    {
        /// <summary>
        /// 获取地区列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAreaCityList()
        {
            DataTable dt = GetDataTable("select * from AreaCity");
            return dt;
        }

        /// <summary>
        /// 获取表中某字段的值
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">字段名</param>
        /// <param name="where">附加条件 已 and 开头，后可跟GROUP BY 和 ORDER BY</param>
        /// <returns></returns>
        public static object GetColumnValue(SqlTransaction tran, string tableName, string columnName, string where)
        {
            StringBuilder build = new StringBuilder();
            build.Append("select " + columnName + " from " + tableName);
            build.Append(" where 1=1 " + where);

            return ExecuteScalar(tran, build.ToString());
        }
        /// <summary>
        /// 获取表中某字段的值
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">字段名</param>
        /// <param name="where">附加条件 已 and 开头，后可跟GROUP BY 和 ORDER BY</param>
        /// <returns></returns>
        public static object GetColumnValue(string tableName, string columnName, string where)
        {
            StringBuilder build = new StringBuilder();
            build.Append("select " + columnName + " from " + tableName);
            build.Append(" where 1=1 " + where);

            return ExecuteScalar(build.ToString());
        }
        /// <summary>
        /// 修改表中某字段值
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">字段名</param>
        /// <param name="columnValue">字段值</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public static int UpdateValue(string tableName, string columnName, string columnValue, string where)
        {
            StringBuilder build = new StringBuilder();
            build.Append("Update " + tableName + " set " + columnName + " = @columnValue ");
            if (!string.IsNullOrEmpty(where))
            {
                build.Append(" where " + where);
            }
            SqlParameter[] paras = { 
                                    new SqlParameter("@columnValue",columnValue)
                                   };
            return ExecuteNonQuery(build.ToString(), paras, CommandType.Text);
        }

        /// <summary>
        /// 获取表中某字段值
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">字段名</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public static object GetValue(string tableName, string columnName, string where)
        {
            StringBuilder build = new StringBuilder();
            build.Append("select " + columnName + " from " + tableName);
            if (!string.IsNullOrEmpty(where))
            {
                build.Append(" where " + where);
            }
            return ExecuteScalar(build.ToString());
        }


        /// <summary>
        /// 判断表中某字段是否存在某值
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">字段名</param>
        /// <param name="columnValue">字段值</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public static object IsExistValue(string tableName, string columnName, string columnValue, string where)
        {
            StringBuilder build = new StringBuilder();
            build.Append("select count(0) from " + tableName);
            build.Append(" where " + columnName + " = @columnValue ");
            if (!string.IsNullOrEmpty(where))
            {
                build.Append(" and " + where);
            }
            SqlParameter[] paras = { 
                                    new SqlParameter("@columnValue",columnValue)
                                   };
            return ExecuteScalar(build.ToString(), paras, CommandType.Text);
        }

        /// <summary>
        /// 获取分页数据集合
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columns">列明</param>
        /// <param name="condition">条件</param>
        /// <param name="key">主键，分页条件</param>
        /// <param name="orderColumn">排序字段</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="totalNum">总记录数</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="isAsc">0主键降序 1主键升序</param>
        /// <returns></returns>
        public static DataTable GetPagerData(string tableName, string columns, string condition, string key, string orderColumn, int pageSize, int pageIndex, out int totalNum, out int pageCount, int isAsc)
        {
            string procName = "P_GetPagerData";
            SqlParameter[] paras = { 
                                        new SqlParameter("@tableName",DbType.String),
                                        new SqlParameter("@columns",DbType.String),
                                        new SqlParameter("@condition",DbType.String),
                                        new SqlParameter("@key",DbType.String),
                                        new SqlParameter("@orderColumn",DbType.String),
                                        new SqlParameter("@pageSize",DbType.Int32),
                                        new SqlParameter("@pageIndex",DbType.Int32),
                                        new SqlParameter("@totalCount",DbType.Int32),
                                        new SqlParameter("@pageCount",DbType.Int32),
                                        new SqlParameter("@isAsc",DbType.Int32),
                                   };
            paras[0].Value = tableName;
            paras[1].Value = columns;
            paras[2].Value = condition;
            paras[3].Value = key;
            paras[4].Value = orderColumn;
            paras[5].Value = pageSize;
            paras[6].Value = pageIndex;
            paras[7].Direction = ParameterDirection.Output;
            paras[8].Direction = ParameterDirection.Output;
            paras[9].Value = isAsc;

            DataTable dt = GetDataTable(procName, paras, CommandType.StoredProcedure);
            totalNum = Convert.ToInt32(paras[7].Value);
            pageCount = Convert.ToInt32(paras[8].Value);
            return dt;
        }
    }
}
