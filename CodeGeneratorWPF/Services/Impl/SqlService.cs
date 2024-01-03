using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeGeneratorWPF.Helper;
using CodeGeneratorWPF.Models;

/*
 * @Description:
 * @Author: DLiang
 * @Date: 2024/1/2 10:58:29
 * @Copyright: 2023 www.31meta.cn Inc. All rights reserved.
 */

namespace CodeGeneratorWPF.Services.Impl
{
    public class SqlService:ISqlService
    {
        private string sql;
        public ObservableCollection<TableModel> GetTables()
        {
            sql = "show tables;";
            var d =   MysqlHelper.ExecuteQuery(sql,CommandType.Text);

            ObservableCollection<TableModel> tables = new ObservableCollection<TableModel>();
            for (int i = 0; i < d.Rows.Count; i++)
            {
                tables.Add(new TableModel()
                {
                    Name = d.Rows[i][0].ToString()
                });
            }

            return tables;
        }


        public ObservableCollection<ColumnModel> GetColumns(string tableName)
        {
            sql = $"DESCRIBE {tableName};";
            var d = MysqlHelper.ExecuteQuery(sql, CommandType.Text);

            ObservableCollection<ColumnModel> tables = new ObservableCollection<ColumnModel>();
            for (int i = 0; i < d.Rows.Count; i++)
            {
                tables.Add(new ColumnModel()
                {
                    Name = d.Rows[i][0].ToString()
                });
            }

            return tables;
        }
    }
}
