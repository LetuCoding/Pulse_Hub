using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Pulse
{
    public partial class Form1 : Form
    {
        private Button _btnDS; // Referencia al botón de Nintendo DS


        private void CreateDSButton()
        {
            _btnDS = new Button();
            _btnDS.Location = new Point(50, 50);
            _btnDS.Size = new Size(120, 40);
            _btnDS.Text = "Nintendo DS";
            _btnDS.Click += BtnDS_Click; // Nuevo evento de clic
            this.Controls.Add(_btnDS);
        }

        private void BtnDS_Click(object sender, EventArgs e)
        {
            // Obtener la ruta a la carpeta de ROMs de DS
            string romsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Roms", "DS");

            if (Directory.Exists(romsPath))
            {
                // Obtener la lista de archivos .nds (o la extensión que usen tus ROMs)
                string[] romFiles = Directory.GetFiles(romsPath, "*.nds"); // Ajusta la extensión si es diferente

                if (romFiles.Length > 0)
                {
                    // Crear un nuevo formulario para la lista de ROMs
                    Form romListForm = new Form();
                    romListForm.Text = "Seleccionar ROM de Nintendo DS";
                    romListForm.Size = new Size(300, 400);
                    romListForm.StartPosition = FormStartPosition.CenterParent;
                    romListForm.FormBorderStyle = FormBorderStyle.FixedSingle;
                    romListForm.MaximizeBox = false;
                    romListForm.MinimizeBox = false;

                    ListBox romListBox = new ListBox();
                    romListBox.Dock = DockStyle.Fill;
                    romListBox.DoubleClick += RomListBox_DoubleClick; // Evento al hacer doble clic en una ROM
                    romListForm.Controls.Add(romListBox);

                    // Llenar la ListBox con los nombres de los archivos ROM
                    List<string> romFilePaths = new List<string>();
                    foreach (string file in romFiles)
                    {
                        romListBox.Items.Add(Path.GetFileNameWithoutExtension(file));
                        romFilePaths.Add(file); // Guardar la ruta completa
                    }
                    romListBox.Tag = romFilePaths; // Almacenar la lista de rutas completas en el Tag

                    romListForm.ShowDialog(); // Mostrar el formulario como un diálogo modal
                }
                else
                {
                    MessageBox.Show("No se encontraron ROMs de Nintendo DS en la carpeta especificada.");
                }
            }
            else
            {
                MessageBox.Show($"La carpeta de ROMs de DS no se encontró en la ruta: {romsPath}");
            }
        }

        private void RomListBox_DoubleClick(object sender, EventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            if (listBox.SelectedIndex != -1)
            {
                // Obtener la lista de rutas completas de la propiedad Tag
                List<string> romFilePaths = (List<string>)listBox.Tag;
                // Obtener la ruta completa de la ROM seleccionada
                string selectedRomPath = romFilePaths[listBox.SelectedIndex];
                // Ruta al ejecutable del emulador
                string emuladorPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Emulators", "DeSmuME.exe");

                if (File.Exists(emuladorPath))
                {
                    try
                    {
                        // Lanza el emulador con la ROM como argumento
                        System.Diagnostics.Process.Start(emuladorPath, $"\"{selectedRomPath}\"");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al abrir el emulador con la ROM: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("El archivo del emulador no existe en la ruta especificada.");
                }

                // Cerrar el formulario de la lista de ROMs después de seleccionar una
                ((Form)listBox.Parent).Close();
            }
        }

        private void InitializeComponent()
        {
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form1";
            // La inicialización de componentes generada por el diseñador se mantendrá aquí
        }
    }
}