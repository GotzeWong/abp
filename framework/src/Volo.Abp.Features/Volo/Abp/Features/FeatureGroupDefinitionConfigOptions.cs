using System;
using System.Collections.Generic;
using System.Text;

namespace Volo.Abp.Features
{
    /// <summary>
    /// For reading configuration of FeatureGroupDefinition
    /// </summary>
    public class FeatureGroupDefinitionConfigOptions
    {
        public Dictionary<string, FeatureGroupDefinitionConfig> FeatureGroupDefinitions { get; set; }
    }
}
