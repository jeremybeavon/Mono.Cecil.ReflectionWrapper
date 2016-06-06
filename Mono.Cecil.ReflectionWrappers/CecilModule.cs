using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mono.Cecil.ReflectionWrappers
{
    public sealed class CecilModule : Module
    {
        private readonly ModuleDefinition module;

        public CecilModule(ModuleDefinition module)
        {
            this.module = module;
        }
    }
}
