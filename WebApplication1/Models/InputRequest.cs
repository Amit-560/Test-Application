using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class InputRequest
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string DocumentName { get; set; }
        [Required]
        public string DocumentURI { get; set; }
        [Required]
        public DateTime DateOfUpload { get; set; }
       

    }
}
