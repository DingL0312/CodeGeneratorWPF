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
    public class SqlService : ISqlService
    {
        private Dictionary<string, string> colTypeDic = new()
        {
           {"bigint","Long"},{"tinyint","Integer"}
        };
        private string sql;
        public ObservableCollection<TableModel> GetTables(string table_schema)
        {
            sql = $"SELECT table_name, table_comment FROM information_schema.tables WHERE table_schema = '{table_schema}' ;";
            var d = MysqlHelper.ExecuteQuery(sql, CommandType.Text);

            ObservableCollection<TableModel> tables = new ObservableCollection<TableModel>();
            for (int i = 0; i < d.Rows.Count; i++)
            {
                tables.Add(new TableModel()
                {
                    TableName = d.Rows[i][0].ToString(),
                    TableComment = d.Rows[i][1].ToString()
                });
            }

            return tables;
        }


        public ObservableCollection<ColumnModel> GetColumns(string tableName)
        {
            sql = $"SHOW FULL COLUMNS FROM {tableName};";
            var d = MysqlHelper.ExecuteQuery(sql, CommandType.Text);

            ObservableCollection<ColumnModel> tables = new ObservableCollection<ColumnModel>();
            for (int i = 0; i < d.Rows.Count; i++)
            {
                tables.Add(new ColumnModel()
                {
                    Field = d.Rows[i][0].ToString(),
                    Type = GetTypeStr(d.Rows[i][1].ToString()),
                    Length = GetLength(d.Rows[i][1].ToString()),
                    Nullable = d.Rows[i][3].ToString(),
                    Key = d.Rows[i][4].ToString(),
                    Default = d.Rows[i][5].ToString(),
                    Comment = d.Rows[i][8].ToString(),

                });
            }

            return tables;
        }

        string GetTypeStr(string? _type)
        {
            if (_type == null)
            {
                return "String";
            }

            string[] typeArr = _type.Split("(");
            if (colTypeDic.TryGetValue(typeArr[0], out string? res))
            {
                return res;
            }
            else
            {
                return "String";
            }
        }


        string GetLength(string? _type)
        {
            if (_type == null)
            {
                return "-1";
            }

            string[] typeArr = _type.Split("(");
            if (typeArr.Length > 1)
            {
                return typeArr[1].Replace(")", "").Trim();
            }

            return "-1";
        }
    }
}
