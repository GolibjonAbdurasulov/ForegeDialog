using DatabaseBroker.Extensions;
using DatabaseBroker.Repositories.FileRepository;
using Entity.Models.Common;
using Entity.Models.File;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Web.Common;

namespace Web.Controllers.FileController;

[ApiController]
[Route("[controller]/[action]")]
public class FileController : ControllerBase
{
    
    private readonly IFileRepository _fileRepository;
    private readonly IFileService _fileService;

    public FileController(IFileRepository fileRepository, IFileService fileService)
    {
        _fileRepository = fileRepository;
        _fileService = fileService;
    }

    [HttpGet,
     ProducesResponseType(typeof(ResponseModelBase<IEnumerable<FileModel>>), 200),
    ]
    public async Task<ResponseModelBase> GetAllAsync([FromQuery] TermModelBase q)
    {
        return await _fileRepository.GetByTermsAsync(q);
    }

    [HttpPost()]
    public async Task<ResponseModelBase> UploadFileAsync(IFormFile file)
    {
        var result = await _fileService.UploadFileAsync(file);
        return (result, 200);
    }

    [HttpPut]
    public async ValueTask<ResponseModelBase> ReplaceFileAsync(Guid id, IFormFile file)
    {
        var result = await _fileService.UpdateFileAsync(id, file);
        return (result, 200);
    }

    [HttpDelete]
    public async ValueTask<ResponseModelBase> DeleteAsync(Guid id)
    {
        var result = await _fileService.DeleteAsync(id);
        return (result, 200);
    }
    
    [HttpGet("download")]
    public async Task<IActionResult> DownloadFileAsync(Guid id)
    {
        try
        {
            var stream = await _fileService.SendFileAsync(id);
            var file = await _fileRepository.GetByIdAsync(id);
            var contentType = "application/octet-stream"; // Fayl turi (MIME turi) ni aniqlash kerak
            var fileName = Path.GetFileName(file.Path);

            // Fayl oqimini qaytarish
            return File(stream, contentType, fileName);
        }
        catch (FileNotFoundException)
        {
            return NotFound();
        }
        
        catch (Exception ex)
        {
            return StatusCode(500, "Ichki server xatosi: " + ex.Message);
        }
    }

   
    
}