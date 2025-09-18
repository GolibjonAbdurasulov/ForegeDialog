namespace Web.Controllers.PicturesModelController.Dtos;

public class PicturesCreationDto
{
    public long  CategoryId { get; set; }
    public List<long> ImagesIds { get; set; }
}