using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MVC_test.BL.Common;
using MVC_test.BL.Records.Model;
using MVC_test.DataAccess;

namespace MVC_test.BL.Records.Manager
{
    public class RecordManager : IRecordManager
    {
        public RecordListModel RecordList;
        public static ResultModel<RecordEntityModel> NewRecord()
        {
            return new ResultModel<RecordEntityModel>
            {
                value = new RecordEntityModel(),
                isSuccess = true
            };
        }
        public ResultModel<RecordEntityModel> AddRecord(RecordEntityModel rem)
        {
            ResultModel<RecordEntityModel> result = new ResultModel<RecordEntityModel>
            {
                value = rem
            };
            try
            {
                if (string.IsNullOrEmpty(rem.Fname))
                {
                    result.Message = "Field First name cannot be left empty!";
                    throw new Exception();
                }
                if (string.IsNullOrEmpty(rem.Lname))
                {
                    result.Message = "Field Last name cannot be left empty!";
                    throw new Exception();
                }
                if (string.IsNullOrEmpty(rem.Phone))
                {
                    result.Message = "Field Phone cannot be left empty!";
                    throw new Exception();
                }
                if (string.IsNullOrEmpty(rem.Password))
                {
                    result.Message = "Field Password cannot be left empty!";
                    throw new Exception();
                }
                if (RecordList.Mlist.Any(i => i.Phone == rem.Phone))
                {
                    result.Message = "The user with that Phone number exists already!";
                    throw new Exception();
                }
                RecordList.Mlist.Add(rem);
                result.isSuccess = true;
            }
            catch (Exception) { }

            return result;
        }
        public ResultModel<RecordListModel> GetList()
        {
            return new ResultModel<RecordListModel>
            {
                value = RecordList,
                isSuccess = true
            };
        }

        public ResultModel<RecordEntityModel> GetRecord(string id)
        {
            ResultModel<RecordEntityModel> result = new ResultModel<RecordEntityModel>
            {
            };
            try
            {
                if (!RecordList.Mlist.Any(i => i.Phone == id))
                {
                    result.Message = "The user doesn't exist!";
                    throw new Exception();
                }
                for (int i = 0; i < RecordList.Mlist.Count; i++)
                {
                    if (RecordList.Mlist[i].Phone == id)
                    {
                        result.isSuccess = true;
                        result.value = RecordList.Mlist[i]; break;
                    }
                }
            }
            catch (Exception) { }
            return result;
        }
        public ResultModel<RecordEntityModel> RemoveRecord(string id)
        {
            ResultModel<RecordEntityModel> result = new ResultModel<RecordEntityModel>();
            try
            {
                if (RecordList.Mlist.Any(i => i.Phone == id))
                {
                    result.isSuccess = true;
                    result.value = RecordList.Mlist.FirstOrDefault(i => i.Phone == id);
                    RecordList.Mlist.Remove(result.value);
                }
                else
                {
                    result.Message = "The user doesn't exist!";
                    throw new Exception();
                }
            }
            catch (Exception) { }
            return result;
        }
        public ResultModel<RecordEntityModel> SaveRecord(RecordEntityModel model)
        {
            var result = new ResultModel<RecordEntityModel>
            {
                value = model
            };

            try
            {
                if (string.IsNullOrEmpty(model.Fname))
                {
                    result.Message = "Field First name cannot be left empty!";
                    throw new Exception();
                }
                if (string.IsNullOrEmpty(model.Lname))
                {
                    result.Message = "Field Last name cannot be left empty!";
                    throw new Exception();
                }
                if (string.IsNullOrEmpty(model.Phone))
                {
                    result.Message = "Field Phone cannot be left empty!";
                    throw new Exception();
                }
                if (string.IsNullOrEmpty(model.Password))
                {
                    result.Message = "";
                    throw new Exception("Field Password cannot be left empty!");
                }

                if (!string.IsNullOrEmpty(model.Phone))
                {
                    if (RecordManager.LoadRecords().Any(i => i.Phone == model.Phone))
                    {
                        var findRecordModel = RecordList.Mlist.FirstOrDefault(x => x.Phone == model.Phone);
                        findRecordModel.Fname = model.Fname;
                        findRecordModel.Lname = model.Lname;
                        findRecordModel.Phone = model.Phone;
                        findRecordModel.Password = model.Password;
                    }
                    else
                    {
                        result.Message = "User with specified phone number doesn't exist";
                        throw new Exception();
                    }
                }
                else
                {
                    RecordList.Mlist.Add(model);
                }

                result.isSuccess = true;
            }
            catch (Exception ex) { result.Message = ex.Message; }
            return result;
        }

        public static int CreateDBEntry(string fn, string ln, string ph)
        {
            RecordEntityModel data = new RecordEntityModel
            {
                Fname = fn,
                Lname = ln,
                Phone = ph
            };
            string sql = @"insert into dbo.Table1(Fname,Lname,Phone)
                values(@Fname,@Lname,@Phone);";
            return SqlDataAccess.SaveData(sql, data);
        }
        public static List<RecordEntityModel> LoadRecords()
        {
            string sql = "select Id, Fname, Lname, Phone from dbo.Table1";
            return SqlDataAccess.LoadData<RecordEntityModel>(sql);
        }
        public static ResultModel<RecordEntityModel> GetRecordFromDB(string pid)
        {
            string sql = "select Id, Fname, Lname, Phone from dbo.Table1";

            List<RecordEntityModel> all = SqlDataAccess.LoadData<RecordEntityModel>(sql);
            ResultModel<RecordEntityModel> result = new ResultModel<RecordEntityModel>();
                if (all.Any(x => x.Phone == pid))
                {
                    result.value = all.FirstOrDefault(x => x.Phone == pid);
                    result.isSuccess = true;
                }
                else
                {
                    result.isSuccess = false;
                    result.Message = "DataBase search was unsuccessfull";
                }
            return result;
        }
        static public ResultModel<RecordEntityModel> RemoveRecordFromDB(string id)
        {
            string sql = "delete from dbo.Table1 where Phone = cast(" + id + " as BIGINT)";
            SqlDataAccess.SaveData<RecordEntityModel>(sql, null);
            return new ResultModel<RecordEntityModel>
            {
                isSuccess = true
            };
        }
        static public ResultModel<RecordEntityModel> UpdateRecordInDB(RecordEntityModel model)
        {
            ResultModel<RecordEntityModel> result = new ResultModel<RecordEntityModel>();
            string sql = @"UPDATE dbo.Table1 SET Fname='" + model.Fname + "', Lname = '" + model.Lname + "', Phone = '" + model.Phone + "' WHERE Phone = cast(" + model.Phone + "as BIGINT)";
            SqlDataAccess.SaveData<RecordEntityModel>(sql, null);
            return new ResultModel<RecordEntityModel>
            {
                isSuccess = true
            };
        }
    }
   
}
