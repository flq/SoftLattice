using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.Reflection;
using SnarkyMark.Core.Support;

namespace SnarkyMark.Core.Composition
{
    internal class MemberExportConfig
    {
        internal MemberExportConfig(MemberInfo target)
        {
            var targetType = target.UnderlyingMemberType();
            Target = target;
            ContractName = AttributedModelServices.GetContractName(targetType);
            MetadataDict = new Dictionary<string, object>();
            ContractType(targetType);
        }

        internal MemberInfo Target { get; private set; }
        internal string ContractName { get; private set; }
        internal IDictionary<string, object> MetadataDict { get; private set; }

        public MemberExportConfig Contract(Type type)
        {
            ContractType(type);
            return Contract(AttributedModelServices.GetContractName(type));
        }

        public MemberExportConfig Contract(string name)
        {
            ContractName = name;
            return this;
        }

        public MemberExportConfig ContractType(string typeIdentity)
        {
            MetadataDict[CompositionConstants.ExportTypeIdentityMetadataName] = typeIdentity;
            return this;
        }

        public MemberExportConfig ContractType(Type type)
        {
            return ContractType(AttributedModelServices.GetTypeIdentity(type));
        }

        public MemberExportConfig Metadata(string key, object value)
        {
            // TODO: Need to support multiple entries for same key
            MetadataDict[key] = value;
            return this;
        }
    }
}
