using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_UniversidadEduca {
    public partial class FormularioPrincipal : Form {

        private List<Panel> Paneles;
        public FormularioPrincipal() {

            InitializeComponent();
        }

        private void FormularioPrincipal_Load(object sender, EventArgs e) {
            Paneles = new List<Panel> {
                nuevaSedePanel,
                nuevoProfesorPanel,
                nuevoEstudiantePanel,
                nuevoCursoPanel,
                matricularPanel,
                ingresoPlataformaPanel,
                asignarProfesorPanel,
                registroNotasPanel,
                estudiantesPanel, 
                profesoresPanel
            };

            //Ocultar distintos paneles durante inicialización
            OcultarPaneles();
        }

        private void registrarProfesorBtn_Click(object sender, EventArgs e) {
            OcultarPaneles();
            nuevoProfesorPanel.Visible = true;
        }

        private void registrarEstudianteBtn_Click(object sender, EventArgs e) {
            OcultarPaneles();
            nuevoEstudiantePanel.Visible = true;
        }

        private void registrarSedeBtn_Click(object sender, EventArgs e) {
            OcultarPaneles();
            nuevaSedePanel.Visible = true;
        }

        private void registrarCursoBtn_Click(object sender, EventArgs e) {
            OcultarPaneles();
            nuevoCursoPanel.Visible = true;
        }

        private void registrarNotasBtn_Click(object sender, EventArgs e) {
            OcultarPaneles();
            ingresoPlataformaPanel.Visible = true;
        }
        private void matricularEstudianteBtn_Click(object sender, EventArgs e) {
            OcultarPaneles();
            matricularPanel.Visible = true;
        }

        private void asignarCursoProfesorBtn_Click(object sender, EventArgs e) {
            OcultarPaneles();
            asignarProfesorPanel.Visible = true;
        }

        private void mostrarEstudiantesBtn_Click(object sender, EventArgs e) {
            OcultarPaneles();
            estudiantesPanel.Visible = true;
        }

        private void mostrarProfesoresBtn_Click(object sender, EventArgs e) {
            OcultarPaneles();
            profesoresPanel.Visible = true;
        }

        private void OcultarPaneles() {
            foreach (Panel panel in Paneles) {
                panel.Visible = false;
            }
        }

        private void autenticacionPlataformaBtn_Click(object sender, EventArgs e) {
            OcultarPaneles();
            registroNotasPanel.Visible = true;
        }
    }
}
