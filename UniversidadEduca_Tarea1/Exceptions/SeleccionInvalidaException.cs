using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversidadEduca_Tarea1.Exceptions {
    class SeleccionInvalidaException : Exception {
        public SeleccionInvalidaException(string mensaje) : base(mensaje) {

        }
    }
}
