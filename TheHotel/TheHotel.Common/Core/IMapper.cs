using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHotel.Common.Core
{
    public interface IMapper<TSource, TTarget>
    {
        TTarget Map(TSource entity);
        IList<TTarget> Map(IList<TSource> entitites);
    }
}
