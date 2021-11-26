using System.Collections.Generic;
using S = SlowFox.InjectDependenciesAttribute;

namespace SlowFox.Constructors.SampleClient.AB
{
    [S]
    public partial class AliasType
    {
        public List<string> A { get; set; }
    }
}