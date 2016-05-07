using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHotel.Common.Core
{
    public interface IModelFactory<TViewModel, TModel>
    {
        TViewModel Create(params object[] parameters);
        TViewModel Create(TModel model);
    }
}
