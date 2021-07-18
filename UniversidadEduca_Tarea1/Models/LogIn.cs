namespace UniversidadEduca_Tarea1.Models {
    class LogIn {
        public string User { get; private set; }
        public string Password { get; private set; }

        public LogIn(string user, string password) {
            User = user;
            Password = password;
        }
    }
}