using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversidadEduca_Tarea1.Exceptions {
    class NumeroNegativoException : Exception {
        public NumeroNegativoException(string mensaje) : base(mensaje) {

        }
    }
}
