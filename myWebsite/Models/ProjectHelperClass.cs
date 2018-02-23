using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace myWebsite.Models
{
    public class ProjectHelperClass
    {
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