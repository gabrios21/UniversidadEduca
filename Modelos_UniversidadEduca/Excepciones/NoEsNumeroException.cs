using System;

namespace Modelos_UniversidadEduca.Excepciones
{
    public class NoEsNumeroException : ApplicationException {
        public NoEsNumeroException(string mensaje) : base(mensaje) {

        }
    }
}
