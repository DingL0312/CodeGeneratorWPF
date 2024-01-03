using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeGeneratorWPF.Models;
using MySqlConnector;

/*
 * @Description:
 * @Author: DLiang
 * @Date: 2024/1/2 10:56:07
 * @Copyright: 2023 www.31meta.cn Inc. All rights reserved.
 */

namespace CodeGeneratorWPF.Services
{
    public interface ISqlService
    {
        ObservableCollection<TableModel> GetTables();
        ObservableCollection<ColumnModel> GetColumns(string tableName);
    }
}
