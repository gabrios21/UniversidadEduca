using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversidadEduca_Tarea1.Exceptions {
    public class FechaIncorrectaException : ApplicationException {
        public FechaIncorrectaException(string mensaje) : base(mensaje) {

        }
    }
}
