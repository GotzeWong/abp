using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Volo.Abp.Localization;
using Volo.Abp.Validation.StringValues;

namespace Volo.Abp.Features
{
    public class FeatureDefinitionConfig
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }


        public string Description { get; set; }


        public FeatureDefinitionConfig Parent { get; set; }

        public List<FeatureDefinitionConfig> Children { get; set; }

        public string DefaultValue { get; set; }

        public bool IsVisibleToClients { get; set; } = true;

        public bool IsAvailableToHost { get; set; } = true;

        public List<string> AllowedProviders { get; set; }

        public Dictionary<string, object> Properties { get; set; }

        public string ValueType { get; set; }

        
        public FeatureDefinition ConfigFeatureDefinition()
        {
            FeatureDefinition featureDefinition = new FeatureDefinition(
                Name,
                DefaultValue,
                DisplayName == null? null:new FixedLocalizableString(DisplayName),
                Description == null? null:new FixedLocalizableString(Description),
                GetStringValueType(),
                IsVisibleToClients,
                IsAvailableToHost
                );

            //Add AllowProviders 
            if (!AllowedProviders.IsNullOrEmpty())
            {
                featureDefinition.AllowedProviders.AddRange(AllowedProviders);
            }

            //Add Properties
            if (Properties != null && Properties.Any())
            {
                foreach (var property in Properties)
                {
                    featureDefinition.Properties.Add(property.Key,property.Value);
                }
            }

            if(!Children.IsNullOrEmpty())
            {
                foreach(var child in Children)
                {
                    featureDefinition.AddChild(child.ConfigFeatureDefinition());
                }
            }

            return featureDefinition;
        }

        public IStringValueType GetStringValueType()
        {
            IStringValueType stringValueType = null;

            switch (ValueType)
            {
                case "SELECTION":
                    stringValueType = new SelectionStringValueType();
                    break;
                case "FREE_TEXT":
                    stringValueType = new FreeTextStringValueType();
                    break;
                case "TOGGLE":
                    stringValueType = new ToggleStringValueType();
                    break;
                default:
                    stringValueType = new ToggleStringValueType();
                    break;
            }

            return stringValueType;
        }


    }
}
