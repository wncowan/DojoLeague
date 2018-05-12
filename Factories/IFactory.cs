using DojoLeague.Models;
using System.Collections.Generic;

namespace DojoLeague.Factory {

    public interface IFactory<T> where T: BaseEntity {}
}