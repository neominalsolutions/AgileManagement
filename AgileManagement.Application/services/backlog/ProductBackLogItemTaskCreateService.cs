using AgileManagement.Application.dtos.backlog;
using AgileManagement.Core;
using AgileManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Application.services.backlog
{
    public class ProductBackLogItemTaskCreateService : IApplicationService<ProductBacklogItemTaskCreateRequestDto, ProductBacklogItemTaskCreateResponseDto>
    {
        private readonly IProductBackLogItemRepository _productBackLogItemRepository;

            public ProductBackLogItemTaskCreateService(IProductBackLogItemRepository productBackLogItemRepository)
        {
            _productBackLogItemRepository = productBackLogItemRepository;
        }
        /// <summary>
        /// BacklogItem task ekleme işlemi yapılır
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ProductBacklogItemTaskCreateResponseDto OnProcess(ProductBacklogItemTaskCreateRequestDto request = null)
        {
            try
            {
                var backlogItem = _productBackLogItemRepository.Find(request.ProductBackLogItemId);

                if (backlogItem == null)
                {
                    throw new Exception("ProductBacklogItem bulunamadı!");
                }


                var task = new ProductBacklogItemTask(name: request.TaskTitle, description: request.Description, priority: request.Priority);
                backlogItem.AddTask(task);

                _productBackLogItemRepository.Save();

                return new ProductBacklogItemTaskCreateResponseDto
                {
                    IsSucceded = true,
                    ProductBackLogItemId = request.ProductBackLogItemId,
                    ProductBackLogItemTaskId = task.Id
                };

            }
            catch (Exception)
            {


                return new ProductBacklogItemTaskCreateResponseDto
                {
                    IsSucceded = false
                };
            }

        }
    }
}
