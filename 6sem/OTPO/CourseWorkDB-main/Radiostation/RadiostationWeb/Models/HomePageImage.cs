using System.ComponentModel.DataAnnotations;

namespace RadiostationWeb.Models
{
    public class HomePageImage
    {
        public int Id { get; set; }
        [DataType(DataType.Upload)]
        [Display(Name = "Upload File")]
        [Required(ErrorMessage = "Please choose file to upload.")]
        public string SrcImg { get; set; }
        public string  ImgCaption { get; set; }
    }
}
