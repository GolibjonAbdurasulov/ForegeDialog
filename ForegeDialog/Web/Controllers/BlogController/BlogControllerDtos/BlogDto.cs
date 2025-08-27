using Entity.Models.Common;

namespace Web.Controllers.BlogController.BlogControllerDtos;

public class BlogDto
{
    public long Id { get; set; }   
    public MultiLanguageField Subject { get; set; }   
    public MultiLanguageField Title { get; set; }   
    public MultiLanguageField Text { get; set; }   
    public List<long> Categories { get; set; }   
    public List<long> Tags { get; set; } 
    public List<Guid> Images{ get; set; }
    public string ReadingTime{ get; set; }   
    public DateTime PublishedDate { get; set; } 
    public long PublisherId { get; set; }
}