using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using ReflectionTypeAttributes = System.Reflection.TypeAttributes;

namespace Mono.Cecil.ReflectionWrappers
{
    public class CecilType : Type
    {
        private readonly TypeDefinition type;
        private Assembly assembly;
        private Module module;

        public CecilType(TypeDefinition type)
        {
            this.type = type;
        }

        internal CecilType(TypeDefinition type, Assembly assembly)
            : this(type)
        {
            this.assembly = assembly;
        }

        public override Assembly Assembly
        {
            get
            {
                if (assembly == null)
                {
                    assembly = new CecilAssembly(type.Module.Assembly);
                }

                return assembly;
            }
        }

        public override string AssemblyQualifiedName
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override Type BaseType
        {
            get { return type.BaseType.ToType(); }
        }

        public override bool ContainsGenericParameters
        {
            get { return type.GenericParameters.Count != 0; }
        }

        public override IEnumerable<CustomAttributeData> CustomAttributes
        {
            get { throw new NotSupportedException(); }
        }

        public override Type DeclaringType
        {
            get { return type.DeclaringType.ToType(); }
        }

        public override string FullName
        {
            get { return type.FullName; }
        }

        public override System.Reflection.GenericParameterAttributes GenericParameterAttributes
        {
            get
            {
                return base.GenericParameterAttributes;
            }
        }

        public override Guid GUID
        {
            get { throw new NotSupportedException(); }
        }

        public override Module Module
        {
            get
            {
                if (module == null)
                {
                    module = new CecilModule(type.Module);
                }

                return module;
            }
        }

        public override string Name
        {
            get { return type.Name; }
        }

        public override string Namespace
        {
            get { return type.Namespace; }
        }

        public override Type UnderlyingSystemType
        {
            get { throw new NotSupportedException(); }
        }

        public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
        {
            return type.Methods.FilterConstructors(bindingAttr).Select(Conversions.ToConstructorInfo).ToArray();
        }

        public override object[] GetCustomAttributes(bool inherit)
        {
            throw new NotImplementedException();
        }

        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }

        public override Type GetElementType()
        {
            return type.GetElementType().ToType();
        }

        public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
        {
            return type.Events.FilterEvents(bindingAttr).Find(@event => @event.Name, name, bindingAttr).ToEventInfo();
        }

        public override EventInfo[] GetEvents(BindingFlags bindingAttr)
        {
            return type.Events.FilterEvents(bindingAttr).Select(Conversions.ToEventInfo).ToArray();
        }

        public override FieldInfo GetField(string name, BindingFlags bindingAttr)
        {
            return type.Fields.FilterFields(bindingAttr).Find(field => field.Name, name, bindingAttr).ToFieldInfo();
        }

        public override FieldInfo[] GetFields(BindingFlags bindingAttr)
        {
            return type.Fields.FilterFields(bindingAttr).Select(Conversions.ToFieldInfo).ToArray();
        }

        public override Type GetInterface(string name, bool ignoreCase)
        {
            return type.Interfaces.Find(@interface => @interface.Name, name, ignoreCase).ToType();
        }

        public override Type[] GetInterfaces()
        {
            return type.Interfaces.Select(Conversions.ToType).ToArray();
        }

        public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
        {
            return GetConstructors(bindingAttr).Cast<MemberInfo>()
                .Concat(GetEvents(bindingAttr))
                .Concat(GetFields(bindingAttr))
                .Concat(GetMethods(bindingAttr))
                .Concat(GetProperties(bindingAttr))
                .ToArray();
        }

        public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
        {
            return type.Methods.FilterMethods(bindingAttr).Select(Conversions.ToMethodInfo).ToArray();
        }

        public override Type GetNestedType(string name, BindingFlags bindingAttr)
        {
            return type.NestedTypes.FilterNestedTypes(bindingAttr).Find(nestedType => nestedType.Name, name, bindingAttr).ToType();
        }

        public override Type[] GetNestedTypes(BindingFlags bindingAttr)
        {
            return type.NestedTypes.FilterNestedTypes(bindingAttr).Select(Conversions.ToType).ToArray();
        }

        public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
        {
            return type.Properties.FilterProperties(bindingAttr).Select(Conversions.ToPropertyInfo).ToArray();
        }

        public override object InvokeMember(
            string name,
            BindingFlags invokeAttr,
            Binder binder,
            object target,
            object[] args,
            ParameterModifier[] modifiers,
            CultureInfo culture,
            string[] namedParameters)
        {
            throw new NotImplementedException();
        }

        public override bool IsDefined(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }

        protected override ReflectionTypeAttributes GetAttributeFlagsImpl()
        {
            throw new NotImplementedException();
        }

        protected override ConstructorInfo GetConstructorImpl(
            BindingFlags bindingAttr,
            Binder binder,
            CallingConventions callConvention,
            Type[] types,
            ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }

        protected override MethodInfo GetMethodImpl(
            string name,
            BindingFlags bindingAttr,
            Binder binder,
            CallingConventions callConvention,
            Type[] types,
            ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }

        protected override PropertyInfo GetPropertyImpl(
            string name,
            BindingFlags bindingAttr,
            Binder binder,
            Type returnType,
            Type[] types,
            ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }

        protected override bool HasElementTypeImpl()
        {
            return type.GetElementType() != null;
        }

        protected override bool IsArrayImpl()
        {
            return type.IsArray;
        }

        protected override bool IsByRefImpl()
        {
            return type.IsByReference;
        }

        protected override bool IsCOMObjectImpl()
        {
            throw new NotSupportedException();
        }

        protected override bool IsPointerImpl()
        {
            return type.IsPointer;
        }

        protected override bool IsPrimitiveImpl()
        {
            return type.IsPrimitive;
        }
    }
}
