using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversidadEduca_Tarea1.Exceptions;
using UniversidadEduca_Tarea1.Models;

namespace UniversidadEduca_Tarea1 {
    class GestorUniversidad {
        readonly int arrayMaxLength = 20;
        public Sede[] Campuses { get; private set; }
        public int ContadorSedes { get; private set; }
        public Profesor[] Profesorado { get; private set; }
        public int ProfessorsCounter { get; private set; }
        public Estudiante[] Estudiante { get; private set; }
        public int StudentsCounter { get; private set; }
        public Curso[] Curriculo { get; private set; }
        public int ContadorCursos { get; private set; }

        public GestorUniversidad() {
            Campuses = new Sede[arrayMaxLength];
            ContadorSedes = 0;
            Profesorado = new Profesor[arrayMaxLength];
            ProfessorsCounter = 0;
            Estudiante = new Estudiante[arrayMaxLength];
            StudentsCounter = 0;
            Curriculo = new Curso[arrayMaxLength];
            ContadorCursos = 0;
        }

        public void AgregarCampus(int campusId, string description) {
            if (ContadorSedes >= arrayMaxLength) {
                throw new IndexOutOfRangeException();
            }

            if (SedeExiste(campusId, out _)) {
                throw new ObjectoDuplicadoException("El Id seleccionado ya existe. Seleccione otro Id para agregar una sede nueva");
            }

            Sede newCampus = new Sede(campusId, description);
            Campuses[ContadorSedes++] = newCampus;
        }

        internal void AgregarProfesor(int professorId, string professorName, string professorLastName, string professorSecLastName, decimal salary, string userName, string password, Sede campus) {
            if (ProfessorsCounter >= arrayMaxLength) {
                throw new IndexOutOfRangeException();
            }

            if (ProfesorExiste(professorId, out _)) {
                throw new ObjectoDuplicadoException("El profesor digitado ya existe");
            }

            LogIn login = new LogIn(userName, password);

            Profesor newProfessor = new Profesor(professorId, professorName, professorLastName, professorSecLastName, salary, campus, login);
            Profesorado[ProfessorsCounter++] = newProfessor;
        }

        internal void AgregarEstudiante(int studentId, string studentName, string studentLastName, string studentSecLastName, DateTime dob, char gender, Sede campus) {
            if (StudentsCounter >= arrayMaxLength) {
                throw new IndexOutOfRangeException();
            }

            if (EstudianteExiste(studentId, out _)) {
                throw new ObjectoDuplicadoException("El estudiante digitado ya existe");
            }

            Estudiante newStudent = new Estudiante(studentId, studentName, studentLastName, studentSecLastName, dob, gender, campus);
            Estudiante[StudentsCounter++] = newStudent;
        }

        internal void AgregarCurso(int courseId, string courseName, string courseDescription) {
            if (ContadorCursos >= arrayMaxLength) {
                throw new IndexOutOfRangeException();
            }

            if (CursoExiste(courseId, out _)) {
                throw new ObjectoDuplicadoException("El código seleccionado ya existe. Seleccione otro código para agregar un curso nuevo");
            }

            Curso newCourse = new Curso(courseId, courseName, courseDescription);
            Curriculo[ContadorCursos++] = newCourse;
        }

        public void Matricular(UniversityMember member, Curso newCourse) {
            if (member.ContadorCursosMatriculados >= arrayMaxLength) {
                throw new IndexOutOfRangeException();
            }
            foreach (Curso course in member.Cursos) {
                if (course != null && course.Id == newCourse.Id) {
                    throw new ObjectoDuplicadoException("Ya se encuentra matriculado en el curso seleccionado");
                }
            }
            member.Cursos[member.ContadorCursosMatriculados++] = newCourse;
        }

        internal bool ProfesorExiste(int newProfId, out Profesor professorFound) {
            bool exists = false;
            professorFound = null;
            foreach (Profesor professor in Profesorado) {
                if (professor != null && professor.Id == newProfId) {
                    exists = true;
                    professorFound = professor;
                } 
            }
            return exists;
        }

        internal bool EstudianteExiste(int newStudentId, out Estudiante studentFound) {
            bool exists = false;
            studentFound = null;
            foreach (Estudiante student in Estudiante) {
                if (student != null && student.Id == newStudentId) {
                    exists = true;
                    studentFound = student;
                }
            }
            return exists;
        }

        internal bool CursoExiste(int newCourseId, out Curso courseFound) {
            bool exists = false;
            courseFound = null;
            foreach (Curso course in Curriculo) {
                if (course != null && course.Id == newCourseId) {
                    exists = true;
                    courseFound = course;
                }
            }
            return exists;
        }

        internal bool SedeExiste(int newCampusId, out Sede campusFound) {
            bool exists = false;
            campusFound = null;
            foreach (Sede campus in Campuses) {
                if (campus != null && campus.Id == newCampusId) {
                    exists = true;
                    campusFound = campus;
                }
            }
            return exists;
        }

    }
}
