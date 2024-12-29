using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.EJ2.FileManager.Base;
using Syncfusion.EJ2.FileManager.AzureFileProvider;
using System.Text.Json;
using System.Threading.Tasks;
using Asp.Versioning;
using Azure.Storage;
using Azure.Storage.Blobs;

namespace Pusula.Training.HealthCare.Controllers.Patients;

[Area("app")]
[ControllerName("PatientFileManager")]
[Route("api/app/patient-file-manager")]
[ApiExplorerSettings(IgnoreApi = true)]
public class PatientFileManager : HealthCareController
{
    private readonly AzureFileProvider _operation = new();
    private const string BasePath = "https://healthcareteam3.blob.core.windows.net";
    private const string AccountName = "healthcareteam3";
    private const string FolderName = "Files";

    private const string Key =
        "VRb6I3i7NqsxuaAyU52ddige5L8WIHVqyfPOev3dvTFDn7T5Ft0m/aHvdY4I2fQw/f5nA//i3l58+AStQ3JYhA==";

    protected virtual async Task CreateBlobContainerIfNotExistAsync(Guid patientId)
    {
        var containerName = GetContainerName(patientId);
        var blobServiceClient = new BlobServiceClient(
            new Uri(BasePath),
            new StorageSharedKeyCredential(AccountName, Key)
        );
        await blobServiceClient.GetBlobContainerClient(containerName).CreateIfNotExistsAsync();
    }

    protected virtual void RegisterAzure(Guid patientId)
    {
        var containerName = GetContainerName(patientId);
        var blobPath = $"{BasePath}/{containerName}/";
        _operation.SetBlobContainer(blobPath, $"{blobPath}{FolderName}");
        _operation.RegisterAzure(AccountName, Key, containerName);
    }

    protected virtual async Task InitializeAzureAsync(Guid patientId)
    {
        await CreateBlobContainerIfNotExistAsync(patientId);
        RegisterAzure(patientId);
    }

    protected virtual string GetContainerName(Guid patientId) => patientId.ToString("N");

    [HttpPost("file-operations/{patientId:guid}")]
    public async Task<object> FileOperationsAsync([FromBody] FileManagerDirectoryContent args, Guid patientId)
    {
        await InitializeAzureAsync(patientId);
        if (args.Path != "")
        {
            args.Path = !args.Path.Contains(FolderName) ?
                (FolderName + args.Path).Replace("//", "/") :
                args.Path.Replace("//", "/");
            args.TargetPath = (FolderName + args.TargetPath).Replace("//", "/");
        }

        return args.Action switch
        {
            "read" =>
                // Reads the file(s) or folder(s) from the given path.
                _operation.ToCamelCase(_operation.GetFiles(args.Path, args.ShowHiddenItems, args.Data)),
            "delete" =>
                // Deletes the selected file(s) or folder(s) from the given path.
                _operation.ToCamelCase(_operation.Delete(args.Path, args.Names, args.Data)),
            "details" =>
                // Gets the details of the selected file(s) or folder(s).
                _operation.ToCamelCase(_operation.Details(args.Path, args.Names, args.Data)),
            "create" =>
                // Creates a new folder in a given path.
                _operation.ToCamelCase(_operation.Create(args.Path, args.Name, args.Data)),
            "search" =>
                // Gets the list of file(s) or folder(s) from a given path based on the searched key string.
                _operation.ToCamelCase(
                    _operation.Search(args.Path, args.SearchString, args.ShowHiddenItems, args.CaseSensitive, args.Data)
                ),
            "rename" =>
                // Renames a file or folder.
                _operation.ToCamelCase(
                    _operation.Rename(args.Path, args.Name, args.NewName, false, args.ShowFileExtension, args.Data)
                ),
            "copy" =>
                // Copies the selected file(s) or folder(s) from a path and then pastes them into a given target path.
                _operation.ToCamelCase(
                    _operation.Copy(
                        args.Path, args.TargetPath, args.Names, args.RenameFiles, args.TargetData, args.Data
                    )
                ),
            "move" =>
                // Cuts the selected file(s) or folder(s) from a path and then pastes them into a given target path.
                _operation.ToCamelCase(
                    _operation.Move(
                        args.Path, args.TargetPath, args.Names, args.RenameFiles, args.TargetData, args.Data
                    )
                ),
            _ => string.Empty
        };
    }

    [HttpPost("upload/{patientId:guid}")]
    public ActionResult Upload([FromBody] FileManagerDirectoryContent args, Guid patientId)
    {
        RegisterAzure(patientId);
        if (args.Path != "")
        {
            args.Path = (FolderName + args.Path).Replace("//", "/");
        }

        // int chunkIndex = int.TryParse(HttpContext.Request.Form["chunk-index"], out int parsedChunkIndex) ? parsedChunkIndex : 0;
        // int totalChunk = int.TryParse(HttpContext.Request.Form["total-chunk"], out int parsedTotalChunk) ? parsedTotalChunk : 0;
        var uploadResponse = _operation.Upload(args.Path, args.UploadFiles, args.Action, args.Data);
        if (uploadResponse.Error != null)
        {
            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            Response.StatusCode = Convert.ToInt32(uploadResponse.Error.Code);
            Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = uploadResponse.Error.Message;
        }

        return Content("");
    }

    [HttpPost("download/{patientId:guid}")]
    public object Download([FromBody] string downloadInput, Guid patientId)
    {
        RegisterAzure(patientId);
        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var args = JsonSerializer.Deserialize<FileManagerDirectoryContent>(downloadInput, options);
        return _operation.Download(args.Path, args.Names, args.Data);
    }
    

    [HttpGet("get-image/{patientId:guid}")]
    public IActionResult GetImage([FromBody] FileManagerDirectoryContent args, Guid patientId)
    {
        RegisterAzure(patientId);
        return _operation.GetImage(args.Path, args.Id, true, null, args.Data);
    }
}