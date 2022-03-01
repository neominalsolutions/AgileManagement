using AgileManagement.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Domain
{
    public enum BacklogItemTaskState
    {
        Initial = 0,
        Done  =1
    }

    public class ProductBacklogItemTask:Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Taskın önceliği
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// kaç saatlik bir task olduğu
        /// </summary>
        public int? Hour { get; set; }

        public BacklogItemTaskState State { get; set; } = BacklogItemTaskState.Initial;

        /// <summary>
        /// Task Contributor
        /// </summary>
        public Contributor Contributor { get; set; }


        public ProductBacklogItemTask(string name, string description, int priority)
        {
            Name = name;
            Description = description;
            Priority = priority;

        }


        /// <summary>
        /// Kayıt aşamasında sonra belirlenebilir.
        /// </summary>
        /// <param name="hour"></param>
        public void SetEstimatedWorkHour(int hour)
        {
            Hour = hour;
        }

        /// <summary>
        /// Taskın hangi contributor'e atandığı
        /// </summary>
        /// <param name="contributor"></param>
        public void Assign(Contributor contributor)
        {

        }

    }
}
