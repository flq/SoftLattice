using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SnarkyMark.Core.Composition
{
    internal abstract class BasePartRegistration<TDERIVED> : PartRegistration where TDERIVED : class
    {
        protected BasePartRegistration()
        {
            ExportConfigs = new List<MemberExportConfig>();
            ImportConfigs = new List<MemberImportConfig>();
            ExportEvalConfigs = new List<Func<Type, MemberExportConfig>>();
            PropertiesToImportConfig = new List<Tuple<Predicate<PropertyInfo>,Action<MemberImportConfig,PropertyInfo>>>();
            FieldsToImportConfig = new List<Tuple<Predicate<FieldInfo>, Action<MemberImportConfig, FieldInfo>>>();
        }

        internal protected List<MemberExportConfig> ExportConfigs { get; private set; }
        internal protected List<MemberImportConfig> ImportConfigs { get; private set; }
        internal protected List<Func<Type, MemberExportConfig>> ExportEvalConfigs { get; private set; }
        internal protected List<Tuple<Predicate<PropertyInfo>, Action<MemberImportConfig, PropertyInfo>>> PropertiesToImportConfig { get; private set; }
        internal protected List<Tuple<Predicate<FieldInfo>, Action<MemberImportConfig, FieldInfo>>> FieldsToImportConfig { get; private set; }

        public TDERIVED ExportingSelf()
        {
            ExportEvalConfigs.Add(CreateSelfMemberExportConfig);

            return GetMostDerived();
        }

        public TDERIVED Exporting(Action<MemberExportConfig, Type> expExp)
        {
            ExportEvalConfigs.Add(t =>
            {
                var cfg = CreateSelfMemberExportConfig(t); 
                expExp(cfg, t);
                return cfg;
            });

            return GetMostDerived();
        }

        #region More

        internal protected CreationPolicy? DefaultCreationPolicy { get; private set; }

        public TDERIVED WithCreationPolicy(CreationPolicy creationPolicy)
        {
            DefaultCreationPolicy = creationPolicy;
            return GetMostDerived();
        }

        protected Dictionary<string, object> PartMetadataDict = new Dictionary<string, object>();

        public TDERIVED PartMetadata(string key, object value)
        {
            PartMetadataDict[key] = value;
            return GetMostDerived();
        }

        public TDERIVED ImportingFields(Predicate<FieldInfo> selector, Action<MemberImportConfig, FieldInfo> config)
        {
            FieldsToImportConfig.Add(
                new Tuple<Predicate<FieldInfo>, Action<MemberImportConfig, FieldInfo>>(selector, config));

            return GetMostDerived();
        }

        public TDERIVED ImportingProperties(Predicate<PropertyInfo> selector, Action<MemberImportConfig, PropertyInfo> config)
        {
            PropertiesToImportConfig.Add(
                new Tuple<Predicate<PropertyInfo>, Action<MemberImportConfig, PropertyInfo>>(selector, config));

            return GetMostDerived();
        }

        public TDERIVED ImportingProperties(Predicate<PropertyInfo> selector)
        {
            Action<MemberImportConfig, PropertyInfo> config = (c, p) => { };

            PropertiesToImportConfig.Add(
                new Tuple<Predicate<PropertyInfo>, Action<MemberImportConfig, PropertyInfo>>(selector, config));

            return GetMostDerived();
        }

        #endregion

        internal protected MemberExportConfig CreateSelfMemberExportConfig(Type type)
        {
            return CreateMemberExportConfig(type, type);
        }

        internal protected MemberExportConfig CreateMemberExportConfig(Type exporting, Type contractType)
        {
            var config = new MemberExportConfig(exporting);
            config.Contract(contractType).ContractType(contractType);
            return config;
        }

        internal protected MemberImportConfig CreateMemberImportConfig(Expression propertyOrField)
        {
            MemberInfo member = GetMemberFromExpression(propertyOrField);

            return new MemberImportConfig(member);
        }

        internal protected void CreateExportDefinitions(Type type, Convention convention, List<ExportDefinition> exports)
        {
            var interfacesImpl = new HashSet<Type>(type.GetInterfaces());

            exports.AddRange(
                ExportEvalConfigs
                .Select(exportCfgFunc => exportCfgFunc(type))
                .Select(exportCfg => convention.BuildExportDefinition(exportCfg.Target, exportCfg.ContractName, exportCfg.MetadataDict)));

            foreach (var exportCfg in ExportConfigs)
            {
                var exportDef = convention.BuildExportDefinition(
                   exportCfg.Target, exportCfg.ContractName, exportCfg.MetadataDict);

                // TODO: remove from interfacesImpl if exported

                exports.Add(exportDef);
            }

            foreach (var interType in interfacesImpl)
            {
                if (convention.IsExport(interType))
                {
                    var exportCfg = new MemberExportConfig(type);
                    exportCfg.Contract(interType);

                    var exportDef = convention.BuildExportDefinition(
                       exportCfg.Target, exportCfg.ContractName, exportCfg.MetadataDict);

                    exports.Add(exportDef);
                }
            }
        }

        internal protected void CreateImportDefinitions(Type type, Convention convention, List<ImportDefinition> imports)
        {
            var members = type.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            var cis = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public);

            var selectedConstructor = convention.SelectConstructor(cis);

            foreach(var paramInfo in selectedConstructor.GetParameters())
            {
                var import = new MemberImportConfig(paramInfo);

                var importDef = convention.BuildImportDefinition(paramInfo, 
                    import.ContractName, import.RequiredTypeIdentity, 
                    import.RequiredMetadata, import.Cardinality, 
                    import.RequiredCreationPolicy);

                imports.Add(importDef);
            }

            var importConfigForThisType = new List<MemberImportConfig>(ImportConfigs);

            foreach (var member in members)
            {
                switch (member.MemberType)
                {
                    case MemberTypes.Field:

                        var field = member as FieldInfo;

                        foreach (var tuple in FieldsToImportConfig)
                        {
                            var selector = tuple.Item1;

                            if (selector(field))
                            {
                                var cfg = tuple.Item2;
                                var importConfig = new MemberImportConfig(member);
                                cfg(importConfig, field);

                                importConfigForThisType.Add(importConfig);
                            }
                        }

                        break;

                    case MemberTypes.Property:

                        var property = member as PropertyInfo;
                        bool imported = false;

                        foreach (var tuple in PropertiesToImportConfig)
                        {
                            var selector = tuple.Item1;

                            if (selector(property))
                            {
                                var cfg = tuple.Item2;
                                var importConfig = new MemberImportConfig(member);
                                cfg(importConfig, property);

                                importConfigForThisType.Add(importConfig);
                                imported = true;
                            }
                        }

                        if (!imported)
                        {
                            if (convention.IsImport(property))
                            {
                                var importDef = convention.BuildImportDefinition(property);

                                if (importDef != null)
                                {
                                    imports.Add(importDef);
                                }
                            }
                        }

                        break;
                }
            }

            foreach (var importCfg in importConfigForThisType)
            {
                var importDef = convention.BuildImportDefinition(
                    importCfg.Target,
                    importCfg.ContractName,
                    importCfg.RequiredTypeIdentity,
                    importCfg.RequiredMetadata,
                    importCfg.Cardinality,
                    importCfg.IsRecomposable,
                    importCfg.RequiredCreationPolicy);

                imports.Add(importDef);
            }
        }

        internal abstract TDERIVED GetMostDerived();

        private static MemberInfo GetMemberFromExpression(Expression exp)
        {
            var member = GetMemberFromLambda(exp);
            return (member != null) ? member.Member : null;
        }

        private static MemberExpression GetMemberFromLambda(Expression exp)
        {
            var lambda = exp as LambdaExpression;

            if (lambda == null)
            {
                throw new Exception("Configuration should be limited to lambda expressions");
            }

            return (MemberExpression) lambda.Body;
        }
    }
}
