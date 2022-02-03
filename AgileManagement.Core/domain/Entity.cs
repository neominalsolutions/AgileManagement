using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Core
{
    /// <summary>
    /// Bu sınıftan instance alınamaz ama diğer sınıflar bu sınıftan instance alıp entity özelliği kazanacaklar. Her entity id si olduğu için bu sınıf içerisine koyduk. Entitylerin hepsi event fırlatabilir oluyor. işi bitince eventi fırlatıp içi başka bir nesneye bırakacağız.
    /// </summary>
    public abstract class Entity
    {
        public string Id { get; set; }
        //public abstract void Raise(IDomainEvent @event);
    }

    /// <summary>
    /// Aşağıdaki kodlar eventin kullanım şeklidir.
    /// </summary>
    public class ProductCreated : IDomainEvent
    {
        public string Name { get; set; }
    }

    public class Product: Entity
    {
        public void IncreasePrice(double newPrice)
        {
            DomainEvent.Raise(new ProductCreated());
        }
    }
}
