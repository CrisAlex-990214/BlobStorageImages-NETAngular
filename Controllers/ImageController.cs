using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;

namespace BlobStorage.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        [HttpPost]
        public async Task<string[]> Upload()
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=codeandcrisst;AccountKey=zODMoTV7HeDUldzjBhQWcDU6zNkC+uV0peg4kvbJkc7xqmlU5B4klcH3r8dLHEvVVHKK2J9qK68Z+AStpJWHIQ==;EndpointSuffix=core.windows.net";

            var blobClient = new BlobContainerClient(connectionString, "images");

            var form = await Request.ReadFormAsync();

            var tasks = form.Files.Select(async x =>
            {
                var blob = blobClient.GetBlobClient(x.FileName);
                await blob.UploadAsync(x.OpenReadStream());
                return blob.Uri.ToString();
            });

            return await Task.WhenAll(tasks);
        }
    }
}