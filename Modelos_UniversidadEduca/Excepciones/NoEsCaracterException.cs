using System;

namespace Modelos_UniversidadEduca.Excepciones
{
    public class NoEsCaracterException : ApplicationException {
        public NoEsCaracterException(string mensaje) : base(mensaje) {

        }
    }
}
