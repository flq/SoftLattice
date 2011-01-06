using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.ReflectionModel;
using System.Linq;
using System.Reflection;

namespace SnarkyMark.Core.Composition
{
    public class Convention
    {
        public virtual ConstructorInfo SelectConstructor(IEnumerable<ConstructorInfo> constructors)
        {
            // very naive solution
            return constructors.Aggregate((a, b) => a.GetParameters().Length < b.GetParameters().Length ? a : b);
        }

        public virtual bool IsImport(FieldInfo field)
        {
            return false;
        }

        public virtual bool IsImport(PropertyInfo property)
        {
            return false;
        }

        public virtual bool IsCollection(Type type)
        {
            return false;
        }

        public virtual bool IsExport(Type type)
        {
            // Experimental
            if (type.IsInterface && !type.FullName.StartsWith("System"))
            {
                return true;
            }

            return false;
        }

        public virtual bool IsExport(FieldInfo field)
        {
            return false;
        }

        public virtual bool IsExport(MethodInfo method)
        {
            return false;
        }

        public virtual bool IsExport(PropertyInfo property)
        {
            return false;
        }

        public virtual ExportDefinition BuildExportDefinition(MemberInfo type, string contract, IDictionary<string, object> metadata)
        {
            return ReflectionModelServices.CreateExportDefinition(
                new LazyMemberInfo(type),
                contract,
                new Lazy<IDictionary<string, object>>(() => metadata),
                null);
        }

        public virtual ExportDefinition BuildExportDefinition(PropertyInfo property)
        {
            return null;
        }

        public virtual ExportDefinition BuildExportDefinition(FieldInfo field)
        {
            return null;
        }

        public virtual ExportDefinition BuildExportDefinition(MethodInfo method)
        {
            return null;
        }

        public virtual ImportDefinition BuildImportDefinition(MemberInfo member,
            string contractName, string requiredTypeIdentity,
            IEnumerable<KeyValuePair<string, Type>> requiredMetadata,
            ImportCardinality cardinality,
            bool isRecomposable,
            CreationPolicy requiredCreationPolicy)
        {
            return ReflectionModelServices.CreateImportDefinition(
                new LazyMemberInfo(member),
                contractName, requiredTypeIdentity,
                requiredMetadata, cardinality,
                isRecomposable, requiredCreationPolicy, null);
        }

        public virtual ImportDefinition BuildImportDefinition(FieldInfo field)
        {
            return null;
        }

        public virtual ImportDefinition BuildImportDefinition(PropertyInfo propertyInfo)
        {
            return null;
        }

        public virtual ImportDefinition BuildImportDefinition(ParameterInfo parameterInfo, 
            string contractName, string requiredTypeIdentity,
            IEnumerable<KeyValuePair<string, Type>> requiredMetadata,
            ImportCardinality cardinality,
            CreationPolicy requiredCreationPolicy)
        {
            // TODO: IsCollection call

            return ReflectionModelServices.CreateImportDefinition(
                new Lazy<ParameterInfo>(() => parameterInfo), 
                contractName, requiredTypeIdentity, 
                requiredMetadata, cardinality, 
                requiredCreationPolicy, null);
        }
    }
}
