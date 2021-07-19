using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Localization;

namespace Volo.Abp.Features
{
    public class FeatureGroupDefinitionConfig
    {
        public string Name { get; set; }

        public Dictionary<string, object> Properties { get; set; }

        public string DisplayName { get; set; }

        public List<FeatureDefinitionConfig> Features { get; set; }

        public FeatureGroupDefinition ConfigFeatureDefinition()
        {
            FeatureGroupDefinition group = new FeatureGroupDefinition(
                Name,
                DisplayName == null? null:new FixedLocalizableString(DisplayName)
                );

            if (!Features.IsNullOrEmpty())
            {
                foreach(var feature in Features)
                {
                    group.AddFeature(feature.ConfigFeatureDefinition());
                }
            }

            return group;
        }
    }
}
