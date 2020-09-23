

using Abp.Application.Services.Dto;
using System.IO;
using System.Threading;
using System.Threading.Tasks;


using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Hinnova.QLVB.Dtos
{
    public class upload_image : EntityDto
    {
        //
        // Summary:
        //     Gets the raw Content-Disposition header of the uploaded file.
        string ContentDisposition { get; }
        //
        // Summary:
        //     Gets the raw Content-Type header of the uploaded file.
        string ContentType { get; }
        //
        // Summary:
        //     Gets the file name from the Content-Disposition header.
        string FileName { get; }
        //
        // Summary:
        //     Gets the header dictionary of the uploaded file.
  
        //
        // Summary:
        //     Gets the file length in bytes.
        long Length { get; }
        //
        // Summary:
        //     Gets the form field name from the Content-Disposition header.
        string Name { get; }

        //
        // Summary:
        //     Copies the contents of the uploaded file to the target stream.
        //
        // Parameters:
        //   target:
        //     The stream to copy the file contents to.


    }
}
