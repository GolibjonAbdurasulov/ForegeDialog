using Web.Controllers.PicturesModelController.Dtos;

namespace Web.Controllers.ReferenceToPicturesController.Dtos;

public class ReferenceToPicturesGetDto
{
    public long Id { get; set; }
    public long CategoryId { get; set; }
    public long PicturesId { get; set; }
    public PicturesDto? Pictures { get; set; }
    public List<string> DownloadLinks { get; set; }
}