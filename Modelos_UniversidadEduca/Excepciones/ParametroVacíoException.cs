using System;

namespace Modelos_UniversidadEduca.Excepciones {
    public class ParametroVacíoException : ApplicationException {
        public ParametroVacíoException(string mensaje) : base(mensaje) {

        }
    }
}
