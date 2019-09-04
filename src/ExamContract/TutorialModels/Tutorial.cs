﻿using ExamContract;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamContract.TutorialModels
{
    public class Tutorial : Entity
    {       
        [MaxLength(100)]
        [Required]
        [Display(Name = "Tutorial name")]
        public string Name { get; set; }
        [Display(Name = "File")]
        public byte[] File { get; set; }

        [Display(Name = "File Type")]
        public string FileType { get; set; }

        [Display(Name = "Content")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Description is required!")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        //[NotMapped]
        //public IFormFile FormFile { get; set; }

    }
}



