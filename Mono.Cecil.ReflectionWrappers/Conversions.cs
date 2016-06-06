using System;
using System.Reflection;

namespace Mono.Cecil.ReflectionWrappers
{
    internal static class Conversions
    {
        public static Type ToType(this TypeReference type)
        {
            return type == null ? null : type.Resolve().ToType();
        }

        public static Type ToType(this TypeDefinition type)
        {
            return type == null ? null : new CecilType(type);
        }

        public static ConstructorInfo ToConstructorInfo(this MethodDefinition method)
        {
            return method == null ? null : new CecilConstructorInfo(method);
        }

        public static EventInfo ToEventInfo(this EventDefinition @event)
        {
            return @event == null ? null : new CecilEventInfo(@event);
        }

        public static FieldInfo ToFieldInfo(this FieldDefinition field)
        {
            return field == null ? null : new CecilFieldInfo(field);
        }

        public static MethodInfo ToMethodInfo(this MethodDefinition method)
        {
            return method == null ? null : new CecilMethodInfo(method);
        }

        public static PropertyInfo ToPropertyInfo(this PropertyDefinition property)
        {
            return property == null ? null : new CecilPropertyInfo(property);
        }
    }
}
