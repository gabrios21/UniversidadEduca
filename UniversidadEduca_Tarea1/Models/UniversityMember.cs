using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversidadEduca_Tarea1.Models {
    class UniversityMember {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public string Apellido { get; private set; }
        public string SegundoApellido { get; private set; }
        public Sede Sede { get; private set; }
        public int ContadorCursosMatriculados { get; set; }
        public Curso[] Cursos { get; private set; }

        public UniversityMember(int id, string name, string lastName, string secondLastName, Sede campus) {
            Id = id;
            Nombre = name; 
            Apellido = lastName;
            SegundoApellido = secondLastName;
            Sede = campus;
            Cursos = new Curso[20];
            ContadorCursosMatriculados = 0;
        }

        public void Enroll(Curso course) {
            if (ContadorCursosMatriculados >= Cursos.Length) {
                throw new IndexOutOfRangeException();
            }
            Cursos[ContadorCursosMatriculados++] = course;
        }
    }
}
