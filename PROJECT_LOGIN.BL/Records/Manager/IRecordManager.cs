using System;
using System.Collections.Generic;
using System.Text;
using MVC_test.BL.Common;
using MVC_test.BL.Records.Model;

namespace MVC_test.BL.Records.Manager
{
    public interface IRecordManager
    {
        ResultModel<RecordEntityModel> AddRecord(RecordEntityModel rem);

        ResultModel<RecordEntityModel> GetRecord(string id);

        ResultModel<RecordEntityModel> RemoveRecord(string id);

        ResultModel<RecordEntityModel> SaveRecord(RecordEntityModel model);

    }
}