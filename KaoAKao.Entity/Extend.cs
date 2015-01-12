using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KaoAKao.Entity
{
    public static class Extend
    {
        public static void FillData<T>(this DataRow dr,T model)
        {
            var cl = dr.Table.Columns;
            foreach (DataColumn col in cl)
            {
                foreach (PropertyInfo pro in model.GetType().GetProperties())
                {
                    if (pro.Name.ToUpper() == col.ColumnName.ToUpper())
                    {
                        pro.SetValue(model, dr[col.ColumnName]);
                    }
                }
            }
        }
    }
}
