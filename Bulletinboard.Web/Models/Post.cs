using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Bulletinboard.Web.Models
{

    // Designing model with data annotation validation
    public class PostViewModel
    {
        public int PostId { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title can't be blank")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description can't be blank")]
        public string Description { get; set; }

    }
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}