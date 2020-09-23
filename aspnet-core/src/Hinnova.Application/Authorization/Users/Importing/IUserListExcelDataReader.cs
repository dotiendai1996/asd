using System.Collections.Generic;
using Hinnova.Authorization.Users.Importing.Dto;
using Abp.Dependency;

namespace Hinnova.Authorization.Users.Importing
{
    public interface IUserListExcelDataReader: ITransientDependency
    {
        List<ImportUserDto> GetUsersFromExcel(byte[] fileBytes);
    }
}
