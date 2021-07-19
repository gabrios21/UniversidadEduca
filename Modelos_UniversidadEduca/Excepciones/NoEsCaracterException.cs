using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversidadEduca_Tarea1.Exceptions {
    class NoEsCaracterException : Exception {
        public NoEsCaracterException(string mensaje) : base(mensaje) {

        }
    }
}
