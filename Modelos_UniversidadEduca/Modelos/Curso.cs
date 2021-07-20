namespace UniversidadEduca_Tarea1.Models {
    public class Curso {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Nota { get; set; }

        public Curso(int id, string name, string description, int nota = -1) {
            Id = id;
            Nombre = name;
            Descripcion = description;
            Nota = nota;
        }

        public override string ToString() {
            var cursoEnTexto = $"{Id} - {Nombre}";
            return cursoEnTexto;
        }
    }
}
