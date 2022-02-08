using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgileManagement.Mvc.Models
{
    public class ProjectCreateInputModel
    {
        [Required(ErrorMessage ="Proje ismi boş geçilemez")]
        public string Name { get; set; }

        public string Description { get; set; }



    }
}
