using System.Collections.Generic;

namespace Favesrus.Core.Common
{
    public interface ILinkContaining
    {
        List<Link> Links { get; set; }
        void AddLink(Link link);
    }
}
