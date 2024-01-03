using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

/*
 * @Description:
 * @Author: DLiang
 * @Date: 2023/10/25 18:38:44
 * @Copyright: 2023 www.31meta.cn Inc. All rights reserved.
 */

namespace CodeGeneratorWPF
{
    public class SQLHelper
    {

        //通过构造函数来实例化所需的ADO对象
        private SqlConnection sqlconn = null;
        private SqlCommand sqlcmd = null;
        private SqlDataReader sdr = null;

        /// <summary>
        /// 将连接数据对象封装到构造函数中
        /// </summary>
        public SQLHelper()
        {

            //连接数据库的字符串（配置文件的使用）
            string connStr = ConfigurationManager.ConnectionStrings["Vas2PlatConnectionString"].ToString();

            //连接数据库对象
            sqlconn = new SqlConnection(connStr);

        }

        /// <summary>
        /// 将连接数据对象封装到构造函数中
        /// </summary>
        public SQLHelper(string host, string user, string pwd)
        {
            string connStr = $"Data Source={host};Persist Security Info=True;User ID={user};Password={pwd}";
            //连接数据库对象
            sqlconn = new SqlConnection(connStr);

        }


        public SQLHelper(string connStr)
        {
            sqlconn = new SqlConnection(connStr);
        }

        /// <summary>
        /// 得到连接数据库对象，抽象连接的过程
        /// </summary>
        /// <returns>返回一个连接数据对象</returns>
        public SqlConnection GetConn()
        {

            //判断现行数据库连接状态来判断是否执行数据连接
            if (sqlconn.State == ConnectionState.Closed)
            {
                sqlconn.Open();
            }
            return sqlconn;
        }

        public int ExecuteNonQueryByTransaction(List<string> sqList, CommandType commandType)
        {
            //定义int类型的变量
            int res = 0;
            sqlcmd = new SqlCommand();
            sqlcmd.Connection = GetConn();
            try
            {
                sqlcmd.Transaction = sqlconn.BeginTransaction();
                foreach (var sql in sqList)
                {
                    sqlcmd.CommandText = sql;
                    res += sqlcmd.ExecuteNonQuery();
                }

                sqlcmd.Transaction.Commit();
                return res;
            }
            catch (SqlException a)
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
                sqlconn.Close();
            }

        }

        /// <summary>
        /// 执行传入增 删 改的SQL语句与参数组
        /// </summary>
        /// <param name="strsql">传入增 删 改 SQL语句</param>
        /// <param name="paras">传入的参数数组</param>
        /// <param name="cmdtype">命令类型</param>
        /// <returns>返回受影响的行数</returns>
        public int ExecuteNonQuery(string strsql, SqlParameter[] paras, CommandType cmdtype)
        {
            //定义int类型的变量
            int res;

            //using语句执行SQL语句
            using (sqlcmd = new SqlCommand(strsql, GetConn()))
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
        public int ExecuteNonQuery(string sql, CommandType cmdtype)
        {
            //定义存储返回行数的变量
            int res;
            //try catch语句实施查询并在查询结束后，关闭数据库连接
            try
            {

                //将SQL语句与连接对象传入执行查询对象
                sqlcmd = new SqlCommand(sql, GetConn());

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
                if (sqlconn.State == ConnectionState.Open)
                {

                    sqlconn.Close();

                }
            }
        }


        /// <summary>
        /// 该方法执行传入的SQL语句
        /// </summary>
        /// <param name="cmdtext">传入的SQL语句或者需要执行的存储过程</param>
        /// <param name="cmdtype">执行的命令类型</param>
        /// <returns>返回DataTable类型的数据</returns>
        public DataTable ExecuteQuery(string cmdtext, CommandType cmdtype)
        {

            //实例化datatable对象
            DataTable dt = new DataTable();

            //传入需要执行的SQL语句与连接数据库对象
            sqlcmd = new SqlCommand(cmdtext, GetConn());

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
        public DataTable ExecuteQuery(string sql, SqlParameter[] paras, CommandType cmdtype)
        {

            //实例化datatable对象
            DataTable dt = new DataTable();

            //实例化数据库执行对象
            SqlCommand sqlcmd = new SqlCommand();

            //需要执行数据库操作的类别
            sqlcmd.CommandType = cmdtype;

            //执行SQL语句
            //sqlcmd = new SqlCommand(sql, GetConn ());
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
