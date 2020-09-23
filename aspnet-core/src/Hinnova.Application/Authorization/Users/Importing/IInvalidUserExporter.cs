using System.Collections.Generic;
using Hinnova.Authorization.Users.Importing.Dto;
using Hinnova.Dto;

namespace Hinnova.Authorization.Users.Importing
{
    public interface IInvalidUserExporter
    {
        FileDto ExportToFile(List<ImportUserDto> userListDtos);
    }
}
