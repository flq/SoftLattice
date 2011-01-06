using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using SnarkyMark.Core.Support;
using SnarkyMark.Core.Support.Microsoft.ComponentModel.Composition.Extensions;

namespace SnarkyMark.Core.Composition
{
    internal class MemberImportConfig
    {
        private MemberImportConfig(Type memberType)
        {
            var elementType = memberType.GetCollectionElementType();

            if (elementType != null)
            {
                ContractName = AttributedModelServices.GetContractName(elementType);
                RequiredTypeIdentity = AttributedModelServices.GetTypeIdentity(elementType);
            }
            else
            {
                ContractName = AttributedModelServices.GetContractName(memberType);
                RequiredTypeIdentity = AttributedModelServices.GetTypeIdentity(memberType);
            }

            RequiredMetadata = new List<KeyValuePair<string, Type>>();
        }

        internal MemberImportConfig(ParameterInfo paramInfo)
            : this(paramInfo.ParameterType)
        {
            Target = paramInfo.Member;
        }

        internal MemberImportConfig(MemberInfo member)
            : this(member.UnderlyingMemberType())
        {
            Target = member;
        }

        internal MemberInfo Target { get; private set; }
        internal string ContractName { get; private set; }
        internal string RequiredTypeIdentity { get; private set; }
        internal List<KeyValuePair<string,Type>> RequiredMetadata { get; private set; }
        internal ImportCardinality Cardinality { get; private set; }
        internal CreationPolicy RequiredCreationPolicy { get; private set; }
        internal bool IsRecomposable { get; private set; }

    	public MemberImportConfig Contract(MemberInfo member)
        {
            Type memberType = member.UnderlyingMemberType();
            Contract(AttributedModelServices.GetContractName(memberType));
            return this;
        }

    	public MemberImportConfig Contract(string name)
        {
            ContractName = name;
            return this;
        }

        public MemberImportConfig ContractType(Type type)
        {
            RequiredTypeIdentity = AttributedModelServices.GetTypeIdentity(type);
            return this;
        }

    	public MemberImportConfig RequiresMetadata(string key, Type metadataType)
        {
            RequiredMetadata.Add(new KeyValuePair<string, Type>(key, metadataType));
            return this;
        }

    	public MemberImportConfig RequiresCreationPolicy(CreationPolicy cp)
        {
            RequiredCreationPolicy = cp;
            return this;
        }

    	public MemberImportConfig Recomposable()
        {
            IsRecomposable = true;
            return this;
        }

        public MemberImportConfig Required()
        {
            Cardinality = ImportCardinality.ExactlyOne;
            return this;
        }

        public MemberImportConfig Optional()
        {
            Cardinality = ImportCardinality.ZeroOrOne;
            return this;
        }

        public MemberImportConfig Many()
        {
            Cardinality = ImportCardinality.ZeroOrMore;
            return this;
        }
    }
}
