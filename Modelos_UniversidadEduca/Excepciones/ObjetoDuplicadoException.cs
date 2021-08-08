using System;

namespace Modelos_UniversidadEduca.Excepciones
{
    public class ObjetoDuplicadoException : ApplicationException {
        public ObjetoDuplicadoException(string mensaje) : base(mensaje) {

        }
    }
}
