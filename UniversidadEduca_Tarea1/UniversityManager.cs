using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversidadEduca_Tarea1.Exceptions;
using UniversidadEduca_Tarea1.Models;

namespace UniversidadEduca_Tarea1 {
    class UniversityManager {
        readonly int arrayMaxLength = 20;
        public Sede[] Campuses { get; private set; }
        public int CampusCounter { get; private set; }
        public Profesor[] Faculty { get; private set; }
        public int ProfessorsCounter { get; private set; }
        public Estudiante[] Students { get; private set; }
        public int StudentsCounter { get; private set; }
        public Curso[] Curriculum { get; private set; }
        public int CoursesCounter { get; private set; }

        public UniversityManager() {
            Campuses = new Sede[arrayMaxLength];
            CampusCounter = 0;
            Faculty = new Profesor[arrayMaxLength];
            ProfessorsCounter = 0;
            Students = new Estudiante[arrayMaxLength];
            StudentsCounter = 0;
            Curriculum = new Curso[arrayMaxLength];
            CoursesCounter = 0;
        }

        public void AddCampus(int campusId, string description) {
            if (CampusCounter >= arrayMaxLength) {
                throw new IndexOutOfRangeException();
            }

            if (CampusExists(campusId, out _)) {
                throw new DuplicateObjectException("El Id seleccionado ya existe. Seleccione otro Id para agregar una sede nueva");
            }

            Sede newCampus = new Sede(campusId, description);
            Campuses[CampusCounter++] = newCampus;
        }

        internal void AddProfessor(int professorId, string professorName, string professorLastName, string professorSecLastName, decimal salary, string userName, string password, Sede campus) {
            if (ProfessorsCounter >= arrayMaxLength) {
                throw new IndexOutOfRangeException();
            }

            if (ProfessorExists(professorId, out _)) {
                throw new DuplicateObjectException("El profesor digitado ya existe");
            }

            LogIn login = new LogIn(userName, password);

            Profesor newProfessor = new Profesor(professorId, professorName, professorLastName, professorSecLastName, salary, campus, login);
            Faculty[ProfessorsCounter++] = newProfessor;
        }

        internal void AddStudent(int studentId, string studentName, string studentLastName, string studentSecLastName, DateTime dob, char gender, Sede campus) {
            if (StudentsCounter >= arrayMaxLength) {
                throw new IndexOutOfRangeException();
            }

            if (StudentExists(studentId, out _)) {
                throw new DuplicateObjectException("El estudiante digitado ya existe");
            }

            Estudiante newStudent = new Estudiante(studentId, studentName, studentLastName, studentSecLastName, dob, gender, campus);
            Students[StudentsCounter++] = newStudent;
        }

        internal void AddCourse(int courseId, string courseName, string courseDescription) {
            if (CoursesCounter >= arrayMaxLength) {
                throw new IndexOutOfRangeException();
            }

            if (CourseExists(courseId, out _)) {
                throw new DuplicateObjectException("El código seleccionado ya existe. Seleccione otro código para agregar un curso nuevo");
            }

            Curso newCourse = new Curso(courseId, courseName, courseDescription);
            Curriculum[CoursesCounter++] = newCourse;
        }

        public void Enroll(UniversityMember member, Curso newCourse) {
            if (member.CourseCounter >= arrayMaxLength) {
                throw new IndexOutOfRangeException();
            }
            foreach (Curso course in member.Courses) {
                if (course != null && course.Id == newCourse.Id) {
                    throw new DuplicateObjectException("Ya se encuentra matriculado en el curso seleccionado");
                }
            }
            member.Courses[member.CourseCounter++] = newCourse;
        }

        internal bool ProfessorExists(int newProfId, out Profesor professorFound) {
            bool exists = false;
            professorFound = null;
            foreach (Profesor professor in Faculty) {
                if (professor != null && professor.Id == newProfId) {
                    exists = true;
                    professorFound = professor;
                } 
            }
            return exists;
        }

        internal bool StudentExists(int newStudentId, out Estudiante studentFound) {
            bool exists = false;
            studentFound = null;
            foreach (Estudiante student in Students) {
                if (student != null && student.Id == newStudentId) {
                    exists = true;
                    studentFound = student;
                }
            }
            return exists;
        }

        internal bool CourseExists(int newCourseId, out Curso courseFound) {
            bool exists = false;
            courseFound = null;
            foreach (Curso course in Curriculum) {
                if (course != null && course.Id == newCourseId) {
                    exists = true;
                    courseFound = course;
                }
            }
            return exists;
        }

        internal bool CampusExists(int newCampusId, out Sede campusFound) {
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
