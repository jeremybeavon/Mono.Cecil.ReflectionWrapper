using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mono.Cecil.ReflectionWrappers
{
    public sealed class CecilEventInfo : EventInfo
    {
        private readonly EventDefinition @event;

        public CecilEventInfo(EventDefinition @event)
        {
            this.@event = @event;
        }

        public override System.Reflection.EventAttributes Attributes
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override Type DeclaringType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override Type ReflectedType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override MethodInfo GetAddMethod(bool nonPublic)
        {
            throw new NotImplementedException();
        }

        public override object[] GetCustomAttributes(bool inherit)
        {
            throw new NotImplementedException();
        }

        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }

        public override MethodInfo GetRaiseMethod(bool nonPublic)
        {
            throw new NotImplementedException();
        }

        public override MethodInfo GetRemoveMethod(bool nonPublic)
        {
            throw new NotImplementedException();
        }

        public override bool IsDefined(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }
    }
}
