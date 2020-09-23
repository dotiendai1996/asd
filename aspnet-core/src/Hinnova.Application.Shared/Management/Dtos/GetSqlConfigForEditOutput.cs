using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.Management.Dtos
{
    public class GetSqlConfigForEditOutput
    {
		public CreateOrEditSqlConfigDto SqlConfig { get; set; }


    }
}