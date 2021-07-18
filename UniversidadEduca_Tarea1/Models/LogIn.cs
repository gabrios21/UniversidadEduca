namespace UniversidadEduca_Tarea1.Models {
    class LogIn {
        public string Usuario { get; private set; }
        public string Contrasena { get; private set; }

        public LogIn(string user, string password) {
            Usuario = user;
            Contrasena = password;
        }
    }
}