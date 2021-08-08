using System;

namespace Modelos_UniversidadEduca.Excepciones {
    public class ObjetoNoExisteException : ApplicationException {
        public ObjetoNoExisteException(string mensaje) : base(mensaje) {

        }
    }
}
