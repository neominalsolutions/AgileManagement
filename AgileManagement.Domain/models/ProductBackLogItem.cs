using AgileManagement.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Domain
{
    /// <summary>
    /// Müşteriden gelen uygulama isteklerinin tümü bu entityde tutulur
    /// ProductBackLogItem Aggregate Object
    /// </summary>
    public class ProductBackLogItem:Entity
    {
        /// <summary>
        /// Müşteriden gelen iş isteğine verilen isimi
        /// </summary>
        public string Name { get; private set; }  
        /// <summary>
        /// İş isteğinin açıklaması
        /// </summary>
        public string Description { get; private set; }

        private List<ProductBacklogItemTask> backlogItemTasks = new List<ProductBacklogItemTask>();
        public IReadOnlyList<ProductBacklogItemTask> Task => backlogItemTasks;


        public ProductBackLogItem(string name, string description)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// BacklogItem'a task ekleme işlemi
        /// </summary>
        /// <param name="task"></param>
        public void AddTask(ProductBacklogItemTask task)
        {
            backlogItemTasks.Add(task);
        }




    }
}
