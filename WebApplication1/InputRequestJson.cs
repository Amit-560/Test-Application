using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class InputRequestJson
    {
        [Required]
        public string data{ get; set; }
        [Required]
        public IFormFile File { get; set; }

    }
    public class Data
    {
        public string DocumentName { get; set; }
        public string DocumentURI { get; set; }
        public string DateOfUpload { get; set; }
    }
}
