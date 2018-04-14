using System.Collections.Generic;

namespace Core.Data
{
    public class AssetsModel
    {
        public Pager Pager { get; set; }
        public IEnumerable<Asset> Assets { get; set; }
    }
}