using Entity.Models.Blog;
using Web.Controllers.BlogController.BlogControllerDtos;

namespace Web.Controllers.ReferenceToBlogController.Dtos;

public class ReferenceToBlogGetDto
{
    public long Id { get; set; }
    public long CategoryId { get; set; }
    public long BlogId { get; set; }
    public BlogDto Blog { get; set; }
}