using System.Collections.Generic;
using Hinnova.Authorization.Users.Dto;
using Hinnova.Dto;

namespace Hinnova.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}