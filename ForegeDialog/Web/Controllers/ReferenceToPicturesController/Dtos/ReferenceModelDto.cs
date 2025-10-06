namespace Web.Controllers.ReferenceToPicturesController.Dtos;

public class ReferenceModelDto
{
    public long Id { get; set; }
    public long CategoryId { get; set; }
    public long PicturesModelId { get; set; }
    public List<string> DownloadLinks { get; set; }
}