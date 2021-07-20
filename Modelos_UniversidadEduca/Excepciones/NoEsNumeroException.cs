using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversidadEduca_Tarea1.Exceptions {
    public class NoEsNumeroException : ApplicationException {
        public NoEsNumeroException(string mensaje) : base(mensaje) {

        }
    }
}
