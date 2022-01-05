using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class imagemodal
    {
        [Key]
        public int imageid { get; set; }

        [Column(TypeName="nvarchar(50)")]
        public string title { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [DisplayName("Image Name")]
        public string imagename { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile imagefile { get; set; }
    }
}
