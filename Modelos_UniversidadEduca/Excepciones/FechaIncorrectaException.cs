using System;

namespace Modelos_UniversidadEduca.Excepciones
{
    public class FechaIncorrectaException : ApplicationException {
        public FechaIncorrectaException(string mensaje) : base(mensaje) {

        }
    }
}
