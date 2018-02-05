using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace myWebsite.Models
{
    public class ProjectContext : DbContext
    {
        public DbSet<ProjectViewModel> ProjectList { get; set; }

    }

    [Table("Projects")]
    public class ProjectViewModel
    {
        [Required]
        [Display(Name = "Id")]
        [DataType(DataType.Text)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

       
        [Display(Name = "Description")]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        
        [Display(Name = "Url")]
        [DataType(DataType.Url)]
        public string Url { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "ImagePath")]
        public string ImagePath { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Position")]
        public byte Position { get; set; }
    }
}
