using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mono.Cecil.ReflectionWrappers
{
    internal static class Filters
    {
        public static IEnumerable<TItem> FindAll<TItem>(
            this IEnumerable<TItem> items,
            Func<TItem, string> toStringFunc,
            string name,
            BindingFlags flags)
        {
            return items.FindAll(toStringFunc, name, flags.HasFlag(BindingFlags.IgnoreCase));
        }

        public static IEnumerable<TItem> FindAll<TItem>(
            this IEnumerable<TItem> items,
            Func<TItem, string> toStringFunc,
            string name,
            bool ignoreCase)
        {
            StringComparison comparison = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            return items.Where(item => name.Equals(toStringFunc(item), comparison));
        }

        public static TItem Find<TItem>(
            this IEnumerable<TItem> items,
            Func<TItem, string> toStringFunc,
            string name,
            BindingFlags flags)
        {
            return items.FindAll(toStringFunc, name, flags).FirstOrDefault();
        }

        public static TItem Find<TItem>(
            this IEnumerable<TItem> items,
            Func<TItem, string> toStringFunc,
            string name,
            bool ignoreCase)
        {
            return items.FindAll(toStringFunc, name, ignoreCase).FirstOrDefault();
        }

        public static IEnumerable<MethodDefinition> FilterConstructors(
            this IEnumerable<MethodDefinition> methods,
            BindingFlags flags)
        {
            return Filter(methods.Where(method => method.IsConstructor), flags);
        }

        public static IEnumerable<EventDefinition> FilterEvents(this IEnumerable<EventDefinition> events, BindingFlags flags)
        {
            if (flags == BindingFlags.Default)
            {
                flags = BindingFlags.Public | BindingFlags.Instance;
            }

            return events.Where(@event => IsMatch(@event, flags));
        }

        public static IEnumerable<FieldDefinition> FilterFields(this IEnumerable<FieldDefinition> fields, BindingFlags flags)
        {
            if (flags == BindingFlags.Default)
            {
                flags = BindingFlags.Public | BindingFlags.Instance;
            }

            return fields.Where(field => IsMatch(field, flags));
        }

        public static IEnumerable<MethodDefinition> FilterMethods(
            this IEnumerable<MethodDefinition> methods,
            BindingFlags flags)
        {
            return Filter(methods.Where(method => !method.IsConstructor), flags);
        }

        public static IEnumerable<TypeDefinition> FilterNestedTypes(
            this IEnumerable<TypeDefinition> nestedTypes,
            BindingFlags flags)
        {
            if (flags == BindingFlags.Default)
            {
                flags = BindingFlags.Public | BindingFlags.Instance;
            }

            return nestedTypes.Where(nestedType => IsMatch(nestedType, flags));
        }

        public static IEnumerable<PropertyDefinition> FilterProperties(
            this IEnumerable<PropertyDefinition> properties,
            BindingFlags flags)
        {
            if (flags == BindingFlags.Default)
            {
                flags = BindingFlags.Public | BindingFlags.Instance;
            }

            return properties.Where(property => IsMatch(property.GetMethod ?? property.SetMethod, flags));
        }

        private static IEnumerable<MethodDefinition> Filter(IEnumerable<MethodDefinition> methods, BindingFlags flags)
        {
            if (flags == BindingFlags.Default)
            {
                flags = BindingFlags.Public | BindingFlags.Instance;
            }

            return methods.Where(method => IsMatch(method, flags));
        }

        private static bool IsMatch(MethodDefinition method, BindingFlags flags)
        {
            return (flags.HasFlag(BindingFlags.Public | BindingFlags.NonPublic) ||
                (flags.HasFlag(BindingFlags.Public) && method.IsPublic) ||
                (flags.HasFlag(BindingFlags.NonPublic) && !method.IsPublic)) &&
                (flags.HasFlag(BindingFlags.Instance | BindingFlags.Static) ||
                (flags.HasFlag(BindingFlags.Instance) && !method.IsStatic) ||
                (flags.HasFlag(BindingFlags.Static) && method.IsStatic));
        }

        private static bool IsMatch(EventDefinition @event, BindingFlags flags)
        {
            return IsMatch(@event.AddMethod, flags);
        }

        private static bool IsMatch(FieldDefinition field, BindingFlags flags)
        {
            return (flags.HasFlag(BindingFlags.Public | BindingFlags.NonPublic) ||
                (flags.HasFlag(BindingFlags.Public) && field.IsPublic) ||
                (flags.HasFlag(BindingFlags.NonPublic) && !field.IsPublic)) &&
                (flags.HasFlag(BindingFlags.Instance | BindingFlags.Static) ||
                (flags.HasFlag(BindingFlags.Instance) && !field.IsStatic) ||
                (flags.HasFlag(BindingFlags.Static) && field.IsStatic));
        }

        private static bool IsMatch(TypeDefinition type, BindingFlags flags)
        {
            return flags.HasFlag(BindingFlags.Public | BindingFlags.NonPublic) ||
                (flags.HasFlag(BindingFlags.Public) && type.IsPublic) ||
                (flags.HasFlag(BindingFlags.NonPublic) && !type.IsPublic);
        }
    }
}
