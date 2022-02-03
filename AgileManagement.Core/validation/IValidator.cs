using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Core.validation
{
    public interface IValidator<TObject> where TObject:class, new()
    {
        /// <summary>
        /// Validasyon hatlarını gösteririz
        /// </summary>
        List<string> Errors { get; set; }

        /// <summary>
        /// Nesne ilgili kurallara uyup uymadığını gösteriz.
        /// </summary>
        /// <param name="object"></param>
        /// <returns></returns>
        bool IsValid(TObject @object);
    }
}
