using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Data.SqlTypes;

/*
 * @Description:
 * @Author: DLiang
 * @Date: 2024/1/2 10:48:42
 * @Copyright: 2023 www.31meta.cn Inc. All rights reserved.
 */

namespace CodeGeneratorWPF.Helper
{
    public static class MysqlHelper
    {
        public static string ConnStr;

        private static MySqlConnection sqlConn;
        private static MySqlCommand sqlcmd;
        private static MySqlDataReader sdr;

        /// <summary>
        /// 得到连接数据库对象，抽象连接的过程
        /// </summary>
        /// <returns>返回一个连接数据对象</returns>
        private static MySqlConnection GetConn()
        {
            if (sqlConn == null)
            {
                sqlConn = new MySqlConnection(ConnStr);
            }
            //判断现行数据库连接状态来判断是否执行数据连接
            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }
            return sqlConn;
        }
        public static int ExecuteNonQueryByTransaction(List<string> sqList, CommandType commandType)
        {
            //定义int类型的变量
            int res = 0;
            sqlcmd = new MySqlCommand();
            sqlcmd.Connection = GetConn();
            try
            {
                sqlcmd.Transaction = sqlConn.BeginTransaction();
                foreach (var sql in sqList)
                {
                    sqlcmd.CommandText = sql;
                    res += sqlcmd.ExecuteNonQuery();
                }

                sqlcmd.Transaction.Commit();
                return res;
            }
            catch (MySqlException a)
            {
                if (sqlcmd.Transaction != null)
                {
                    sqlcmd.Transaction.Rollback();
                }

                throw new Exception($"执行事务方法出错:{a}");
            }
            finally
            {
                if (sqlcmd.Transaction != null)
                {
                    sqlcmd.Transaction = null;
                }
                sqlConn.Close();
            }

        }

        /// <summary>
        /// 执行传入增 删 改的SQL语句与参数组
        /// </summary>
        /// <param name="strsql">传入增 删 改 SQL语句</param>
        /// <param name="paras">传入的参数数组</param>
        /// <param name="cmdtype">命令类型</param>
        /// <returns>返回受影响的行数</returns>
        public static int ExecuteNonQuery(string strsql, MySqlParameter[] paras, CommandType cmdtype)
        {
            //定义int类型的变量
            int res;

            //using语句执行SQL语句
            using (sqlcmd = new MySqlCommand(strsql, GetConn()))
            {
                sqlcmd.CommandType = cmdtype;
                sqlcmd.Parameters.AddRange(paras);
                res = sqlcmd.ExecuteNonQuery();

            }

            return res;
        }


        /// <summary>
        /// 该方法执行传入sql语句
        /// </summary>
        /// <param name="sql">传入的SQL语句</param>
        /// <param name="cmdtype">执行的命令类型</param>
        /// <returns>返回执行的记录行数</returns>
        public static int ExecuteNonQuery(string sql, CommandType cmdtype)
        {
            //定义存储返回行数的变量
            int res;
            //try catch语句实施查询并在查询结束后，关闭数据库连接
            try
            {

                //将SQL语句与连接对象传入执行查询对象
                sqlcmd = new MySqlCommand(sql, GetConn());

                //执行查询的数据库操作类别
                sqlcmd.CommandType = cmdtype;


                //执行查询
                sqlcmd.ExecuteNonQuery();

                //返回查询结果
                res = sqlcmd.ExecuteNonQuery();
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //判断现行数据连接状态来改变数据库状态
                if (sqlConn.State == ConnectionState.Open)
                {

                    sqlConn.Close();

                }
            }
        }


        /// <summary>
        /// 该方法执行传入的SQL语句
        /// </summary>
        /// <param name="cmdtext">传入的SQL语句或者需要执行的存储过程</param>
        /// <param name="cmdtype">执行的命令类型</param>
        /// <returns>返回DataTable类型的数据</returns>
        public static DataTable ExecuteQuery(string cmdtext, CommandType cmdtype)
        {

            //实例化datatable对象
            DataTable dt = new DataTable();

            //传入需要执行的SQL语句与连接数据库对象
            sqlcmd = new MySqlCommand(cmdtext, GetConn());

            //定义执行的类型
            sqlcmd.CommandType = cmdtype;

            using (sdr = sqlcmd.ExecuteReader(CommandBehavior.CloseConnection))
            {

                //将查询结果加载到dt对象中
                dt.Load(sdr);

            }

            //返回查询结果
            return dt;
        }


        /// <summary>
        /// 执行传入的SQL语句及参数组
        /// </summary>
        /// <param name="sql">传入的SQL语句</param>
        /// <param name="paras">参数数组</param>
        /// <param name="cmdtype">执行的命令类型</param>
        /// <returns></returns>
        public static DataTable ExecuteQuery(string sql, MySqlParameter[] paras, CommandType cmdtype)
        {

            //实例化datatable对象
            DataTable dt = new DataTable();

            //实例化数据库执行对象
            MySqlCommand sqlcmd = new MySqlCommand();

            //需要执行数据库操作的类别
            sqlcmd.CommandType = cmdtype;

            //执行SQL语句
            //sqlcmd = new MySqlCommand(sql, GetConn ());
            sqlcmd.Connection = GetConn();
            sqlcmd.CommandText = sql;

            //传入parameter参数组
            sqlcmd.Parameters.AddRange(paras);


            //使用using来实现数据库的关闭与打开
            using (sdr = sqlcmd.ExecuteReader(CommandBehavior.CloseConnection))
            {

                //将查询结果加载到dt对象中
                dt.Load(sdr);

            }
            //返回查询结果
            return dt;
        }

    }
}
