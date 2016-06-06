using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mono.Cecil.ReflectionWrappers
{
    public sealed class CecilAssembly : Assembly
    {
        private readonly AssemblyDefinition assembly;

        public CecilAssembly(AssemblyDefinition assembly)
        {
            this.assembly = assembly;
        }
    }
}
