using System.Collections.Generic;
using Hinnova.Authorization.Users.Importing.Dto;
using Abp.Dependency;
using Hinnova.QLNS.Dtos;
using Hinnova.QLNS.Importing;

namespace Hinnova.Authorization.Users.Importing
{
    public interface IHoSoListExcelDataReader : ITransientDependency
    {
        List<HoSoImportDto> GetHoSoFromExcel(byte[] fileBytes);
    }
}
