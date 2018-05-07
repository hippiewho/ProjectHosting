using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace myWebsite.Models
{
    public class ProjectHelperClass
    {
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

        [Display(Name = "GitHubUrl")]
        [DataType(DataType.Url)]
        public string GitHubUrl { get; set; }

        [Display(Name = "SiteUrl")]
        [DataType(DataType.Url)]
        public string SiteUrl { get; set; }

        [Display(Name = "OtherUrl")]
        [DataType(DataType.Url)]
        public string OtherUrl { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "ImagePath")]
        public string ImagePath { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Position")]
        public byte Position { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "UserSelectList")]
        public int UserId { get; set; }

        public IEnumerable<SelectListItem> UserSelectList { get; set; }

        public IEnumerable<SelectListItem> ImagePathSelectList { get; set; }

        public ProjectHelperClass( IEnumerable<SelectListItem> selectList)
        {
            UserSelectList = selectList;
        }

        public ProjectHelperClass()
        {

        }
    }
}