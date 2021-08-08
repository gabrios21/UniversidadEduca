using System;

namespace Modelos_UniversidadEduca.Excepciones
{
    public class NumeroNegativoException : ApplicationException {
        public NumeroNegativoException(string mensaje) : base(mensaje) {

        }
    }
}
