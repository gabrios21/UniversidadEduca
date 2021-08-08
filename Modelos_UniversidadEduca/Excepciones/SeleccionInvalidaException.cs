using System;

namespace Modelos_UniversidadEduca.Excepciones
{
    public class SeleccionInvalidaException : ApplicationException {
        public SeleccionInvalidaException(string mensaje) : base(mensaje) {

        }
    }
}
