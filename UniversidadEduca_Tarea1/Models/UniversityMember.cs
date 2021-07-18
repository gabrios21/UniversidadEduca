using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversidadEduca_Tarea1.Models {
    class UniversityMember {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string SecondLastName { get; private set; }
        public Sede Campus { get; private set; }
        public int CourseCounter { get; set; }
        public Curso[] Courses { get; private set; }

        public UniversityMember(int id, string name, string lastName, string secondLastName, Sede campus) {
            Id = id;
            Name = name; 
            LastName = lastName;
            SecondLastName = secondLastName;
            Campus = campus;
            Courses = new Curso[20];
            CourseCounter = 0;
        }

        public void Enroll(Curso course) {
            if (CourseCounter >= Courses.Length) {
                throw new IndexOutOfRangeException();
            }
            Courses[CourseCounter++] = course;
        }
    }
}
