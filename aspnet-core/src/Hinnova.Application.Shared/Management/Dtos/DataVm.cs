using System;
using System.Collections.Generic;
using System.Text;

namespace Hinnova.Managerment.Dtos
{
    public class ScheduleListAngularDto
    {
        public string AttenderOutside { get; set; }
        public int BlockID { get; set; }
        public string BlockName { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateDeleted { get; set; }
        public DateTime DateModified { get; set; }
        public string DocumentLink { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime FromTime { get; set; }
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsConfirm { get; set; }
        public bool IsDelete { get; set; }
        public bool IsRegisterRoom { get; set; }
        public string ListIndividual_IsChaiman { get; set; }
        public int ParentID { get; set; }
        public int Place { get; set; }
        public int Priority { get; set; }
        public int RoomID { get; set; }
        public string RoomName { get; set; }
        public int ScheduleTypeID { get; set; }
        public string ScheduleTypeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StartTime { get; set; }
        public string Status { get; set; }
        public string StatusName { get; set; }
        public DateTime ToTime { get; set; }
        public string UnitPrepareDocument { get; set; }
        public int UserCreated { get; set; }
        public int UserDeleted { get; set; }
        public int UserModified { get; set; }

        public ScheduleListAngularDto(string attenderOutside, int blockID, string blockName, string content, DateTime date, DateTime dateCreated, DateTime dateDeleted, DateTime dateModified, string documentLink, DateTime endDate, DateTime fromTime, int id, bool isActive, bool isConfirm, bool isDelete, bool isRegisterRoom, string listIndividual_IsChaiman, int parentID, int place, int priority, int roomID, string roomName, int scheduleTypeID, string scheduleTypeName, DateTime startDate, DateTime startTime, string status, string statusName, DateTime toTime, string unitPrepareDocument, int userCreated, int userDeleted, int userModified)
        {
            this.AttenderOutside = attenderOutside;
            this.BlockID = blockID;
            this.BlockName = blockName;
            this.Content = content;
            this.Date = date;
            this.DateCreated = dateCreated;
            this.DateDeleted = dateDeleted;
            this.DateModified = dateModified;
            this.DocumentLink = documentLink;
            this.EndDate = endDate;
            this.FromTime = fromTime;
            this.Id = id;
            this.IsActive = isActive;
            this.IsConfirm = isConfirm;
            this.IsDelete = isDelete;
            this.IsRegisterRoom = isRegisterRoom;
            ListIndividual_IsChaiman = listIndividual_IsChaiman;
            this.ParentID = parentID;
            this.Place = place;
            Priority = priority;
            this.RoomID = roomID;
            RoomName = roomName;
            this.ScheduleTypeID = scheduleTypeID;
            ScheduleTypeName = scheduleTypeName;
            StartDate = startDate;
            StartTime = startTime;
            this.Status = status;
            StatusName = statusName;
            this.ToTime = toTime;
            this.UnitPrepareDocument = unitPrepareDocument;
            this.UserCreated = userCreated;
            this.UserDeleted = userDeleted;
            this.UserModified = userModified;     
        }

        public ScheduleListAngularDto() { }
    }

    public class DataVm
    {
        public bool isSucceeded { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }

        public DataVm()
        {
        }

        public DataVm(string code, string message, object data, bool isSucceeded)
        {
            this.Code = code;
            this.Data = data;
            this.Message = message;
            this.isSucceeded = isSucceeded;
        }

        public DataVm(string code, string message, object data)
        {
            this.Data = data;
            this.Code = code;
            this.Message = message;
        }

        public static DataVm Success(string code, string message, object data)
        {
            var rv = new DataVm(code, message, data);
            rv.isSucceeded = true;
            return rv;
        }

        public static DataVm Fail(string code, string message)
        {
            return new DataVm(code, message, null);
        }
    }

    public class QueryResult
    {
        public bool isSucceeded { get; set; }
        public List<object> Data { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }

        public QueryResult()
        {
        }

        public QueryResult(string code, string message, List<object> data, bool isSucceeded)
        {
            this.Code = code;
            this.Data = data;
            this.Message = message;
            this.isSucceeded = isSucceeded;
        }

        public QueryResult(string code, string message, List<object> data)
        {
            this.Data = data;
            this.Code = code;
            this.Message = message;
        }

        public static QueryResult Success(string code, string message, List<object> data)
        {
            var rv = new QueryResult(code, message, data);
            rv.isSucceeded = true;
            return rv;
        }

        public static QueryResult Fail(string code, string message)
        {
            return new QueryResult(code, message, null);
        }
    }
}
