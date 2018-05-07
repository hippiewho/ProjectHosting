using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

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
        private string _description = " ";

        [Display(Name = "GitHubUrl")] [DataType(DataType.Url)]
        private string _githuburl = "";

        [Display(Name = "SiteUrl")] [DataType(DataType.Url)]
        private string _siteurl = "";

        [Display(Name = "OtherUrl")] [DataType(DataType.Url)]
        private string _otherurl = "";

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
        public string GitHubUrl
        {
            get => _githuburl.Trim();

            set => _githuburl = value ?? "";
        }

        public string SiteUrl
        {
            get => _siteurl.Trim();

            set => _siteurl = value ?? "";
        }

        public string OtherUrl
        {
            get => _otherurl.Trim();

            set => _otherurl = value ?? "";
        }
        public string ImagePath
        {
            get => _imagePath.Trim();

            set => _imagePath = value;
        }
    }
}
