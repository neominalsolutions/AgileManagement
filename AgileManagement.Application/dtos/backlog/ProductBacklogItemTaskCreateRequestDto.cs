using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Application.dtos.backlog
{
    public class ProductBacklogItemTaskCreateRequestDto
    {
        public string ProductBackLogItemId { get; set; }
        public string TaskTitle { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }

    }

    public class ProductBacklogItemTaskCreateResponseDto
    {
        public bool IsSucceded { get; set; }
        public string ProductBackLogItemId { get; set; }
        public string ProductBackLogItemTaskId { get; set; }


    }
}
