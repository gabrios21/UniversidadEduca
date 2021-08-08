namespace UniversidadEduca_Tarea1.Models {
    public class Curso {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Nota { get; set; }

        public Curso(int id, string name, string description, int nota = 0) {
            Id = id;
            Nombre = name;
            Descripcion = description;
            Nota = nota;
        }

        public override string ToString() {
            var cursoEnTexto = $"{Id} - {Nombre}";
            return cursoEnTexto;
        }

        public override bool Equals(object obj)
        {
            return obj is Curso curso &&
                   Id == curso.Id;
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(Id);
        }
    }
}
