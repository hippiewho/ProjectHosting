using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;


namespace myWebsite.Models
{
    public class ProjectContext : DbContext
    {
        public DbSet<ProjectModel> ProjectList { get; set; }

    }

    [Table("Projects")]
    public class ProjectModel
    {
        [Required] [Display(Name = "Id")] [DataType(DataType.Text)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        private string _name = "";


        [Display(Name = "Description")] [DataType(DataType.Text)]
        private string _description = "";


        [Display(Name = "Url")] [DataType(DataType.Url)]
        private string _url = "";

        [DataType(DataType.ImageUrl)] [Display(Name = "ImagePath")]
        private string _imagePath = "";

        [Required] [DataType(DataType.Text)] [Display(Name = "Position")]
        public byte Position { get; set; }

        [Required] [DataType(DataType.Text)] [Display(Name = "UserId")]
        public int UserId { get; set; }



        public string Name
        {
            get => _name;

            set => _name = value;
        }
        public string Description
        {
            get => _description.Trim();

            set => _description = value;
        }
        public string Url
        {
            get => _url.Trim();

            set => _url = value;
        }
        public string ImagePath
        {
            get => _imagePath.Trim();

            set => _imagePath = value;
        }
    }
}
