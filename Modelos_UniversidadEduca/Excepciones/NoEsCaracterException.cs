using System;

namespace UniversidadEduca_Tarea1.Exceptions {
    public class NoEsCaracterException : ApplicationException {
        public NoEsCaracterException(string mensaje) : base(mensaje) {

        }
    }
}
