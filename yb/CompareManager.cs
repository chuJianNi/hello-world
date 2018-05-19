using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;

namespace LiaoChengZYSI
{
    /// <summary>
    /// 医保人员、科室对照底层方法
    /// </summary>
    public class CompareManager : Neusoft.FrameWork.Management.Database
    {
        /// <summary>
        /// 查询已对照的人员信息
        /// </summary>
        /// <returns></returns>
        public DataSet QueryComparedEmployeeInfo()
        {
            string strSQL = string.Empty;

            DataSet ds = new DataSet();

            try
            {
                //查询sql语句
                if (this.Sql.GetSql("FC.Local.QueryComparedEmploye.List", ref strSQL) == -1)
                {
                    this.Err = "没有找到索引为【FC.Local.QueryComparedEmploye.List】的sql语句";
                    return null;
                }

                //执行sql语句
                this.ExecQuery(strSQL,ref ds);
            }
            catch (Exception ex)
            {
                this.Err = "获取已对照人员信息时出现异常！" + ex.Message;
                return null;
            }
           
            return ds;
        }


        /// <summary>
        /// 查询已对照的科室信息
        /// </summary>
        /// <returns></returns>
        public DataSet QueryComparedDeptInfo()
        {
            string strSQL = string.Empty;

            DataSet ds = new DataSet();

            try
            {
                //查询SQL语句
                if (this.Sql.GetSql("FC.Local.QueryComparedDept.List", ref strSQL) == -1)
                {
                    this.Err = "没有找到索引为【FC.Local.QueryComparedDept.List】的sql语句";
                    return null;
                }

                //执行SQL语句
                this.ExecQuery(strSQL,ref ds);
            }
            catch (Exception ex)
            {
                this.Err = "获取已对照科室信息时出现异常！" + ex.Message;
                return null;
            }
           
            return ds;
        }

        /// <summary>
        /// 查询未对照的人员信息
        /// </summary>
        /// <returns></returns>
        public DataSet QueryUnComparedEmplInfo()
        {
            string strSQL = "";
            DataSet ds = new DataSet();

            try
            {
                if (this.Sql.GetSql("FC.Local.QueryUnComparedEmpl.Info", ref strSQL) == -1)
                {

                    this.Err = "没有找到索引为【FC.Local.QueryUnComparedEmpl.Inf】的SQL语句";
                    return null;
                }

                this.ExecQuery(strSQL, ref ds);
            }
            catch (Exception e)
            {
                this.Err = "执行索引为【FC.Local.QueryUnComparedEmpl.Inf】的SQL语句出现异常 " + e.Message;
                return null;
            }
           

            return ds;
        }

        /// <summary>
        /// 查询未对照的科室信息
        /// </summary>
        /// <returns></returns>
        public DataSet QueryUnComparedDeptInfo()
        {
            string strSQL = "";
            DataSet ds = new DataSet();

            try
            {
                if (this.Sql.GetSql("FC.Local.QueryUnComparedDept.Info", ref strSQL) == -1)
                {

                    this.Err = "没有找到索引为【FC.Local.QueryUnComparedDept.Info】的SQL语句";
                    return null;
                }

                this.ExecQuery(strSQL, ref ds);
            }
            catch (Exception e)
            {
                this.Err = "执行索引为【FC.Local.QueryUnComparedDept.Info】的SQL语句出现异常 " + e.Message;
                return null;
            }
            
            return ds;
        }

        /// <summary>
        /// 保存对照人员信息
        /// </summary>
        /// <param name="empl"></param>
        /// <returns></returns>
        public int InsertEmplCompareInfo(Neusoft.HISFC.Models.Base.Employee empl,string operCode,DateTime operDate)
        {
            int returnValue = 0;

            string strSQL = "";
            //查找SQL语句
            if (this.Sql.GetSql("FC.Local.InsetCompareInfo.List", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为【FC.Local.InsetCompareInfo.List】的sql语句";
                return -1;
            }

            //格式化语句
            strSQL = string.Format(strSQL, empl.ID, empl.Name,empl.Password, "0", empl.User01.ToString(), empl.User02.ToString(), empl.User03.ToString(), operCode, operDate);

            //执行语句
            returnValue = this.ExecNoQuery(strSQL);


            return returnValue;
        }

        /// <summary>
        /// 更新人员对照信息
        /// </summary>
        /// <param name="empl"></param>
        /// <param name="operCode"></param>
        /// <param name="operDate"></param>
        /// <returns></returns>
        public int UpadteEmplCompareInfo(Neusoft.HISFC.Models.Base.Employee empl, string operCode, DateTime operDate)
        {
            int returnValue = 0;

            string strSQL = "";
            //查找SQL语句
            if (this.Sql.GetSql("FC.Local.UpdateCompareInfo.List", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为【FC.Local.UpdateCompareInfo.List】的sql语句";
                return -1;
            }

            //格式化语句
            strSQL = string.Format(strSQL, empl.ID, empl.User01.ToString(), empl.User02.ToString(), empl.User03.ToString(), operCode, operDate,empl.Password);

            //执行语句
            returnValue = this.ExecNoQuery(strSQL);


            return returnValue;
        }

        /// <summary>
        /// 保存对照科室信息
        /// </summary>
        /// <param name="empl"></param>
        /// <returns></returns>
        public int InsertDeptCompareInfo(Neusoft.HISFC.Models.Base.Department dept, string operCode, DateTime operDate)
        {
            int returnValue = 0;

            string strSQL = "";
            //查找SQL语句
            if (this.Sql.GetSql("FC.Local.InsetCompareInfo.List", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为【FC.Local.InsetCompareInfo.List】的sql语句";
                return -1;
            }

            //格式化语句
            strSQL = string.Format(strSQL, dept.ID, dept.Name,"", "1", dept.User01.ToString(), dept.User02.ToString(), dept.User03.ToString(), operCode, operDate);

            //执行语句
            returnValue = this.ExecNoQuery(strSQL);


            return returnValue;
        }

        /// <summary>
        /// 更新科室对照信息
        /// </summary>
        /// <param name="empl"></param>
        /// <param name="operCode"></param>
        /// <param name="operDate"></param>
        /// <returns></returns>
        public int UpadteDeptCompareInfo(Neusoft.HISFC.Models.Base.Department dept, string operCode, DateTime operDate)
        {
            int returnValue = 0;

            string strSQL = "";
            //查找SQL语句
            if (this.Sql.GetSql("FC.Local.UpdateCompareInfo.List", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为【FC.Local.UpdateCompareInfo.List】的sql语句";
                return -1;
            }

            //格式化语句
            strSQL = string.Format(strSQL, dept.ID, dept.User01.ToString(), dept.User02.ToString(), dept.User03.ToString(), operCode, operDate,"");

            //执行语句
            returnValue = this.ExecNoQuery(strSQL);


            return returnValue;
        }
    }
}
