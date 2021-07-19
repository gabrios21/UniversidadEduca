﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversidadEduca_Tarea1.Models {
    public class Sede {
        public int Id { get; private set; }
        public string Descripcion { get; private set; }

        public Sede(int id, string description) {
            Id = id;
            Descripcion = description;
        }
    }
}