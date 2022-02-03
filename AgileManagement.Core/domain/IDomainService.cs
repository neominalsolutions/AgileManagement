using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Core.domain
{


    /// <summary>
    /// Uygulama içerisindeki Entity logiclerini DomainService ile yöneteceğiz.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDomainService<TEntity> where TEntity:Entity
    {
      
    }
}
