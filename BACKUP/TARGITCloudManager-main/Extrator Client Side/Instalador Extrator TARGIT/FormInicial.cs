using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Instalador_Extrator_TARGIT
{
    public partial class FormInicial : Form
    {
        public static void InstallService(string serviceName, Assembly assembly)
        {
            if (IsServiceInstalled(serviceName))
            {
                return;
            }

            using (AssemblyInstaller installer = GetInstaller(assembly))
            {
                System.Collections.IDictionary state = new System.Collections.Hashtable();
                try
                {
                    installer.Install(state);
                    installer.Commit(state);
                }
                catch
                {
                    try
                    {
                        installer.Rollback(state);
                    }
                    catch { }
                    throw;
                }
            }
        }

        public static bool IsServiceInstalled(string serviceName)
        {
            using (ServiceController controller = new ServiceController(serviceName))
            {
                try
                {
                    ServiceControllerStatus status = controller.Status;
                }
                catch
                {
                    return false;
                }

                return true;
            }
        }

        private static AssemblyInstaller GetInstaller(Assembly assembly)
        {
            AssemblyInstaller installer = new AssemblyInstaller(assembly, null);
            installer.UseNewContext = true;

            return installer;
        }


        public FormInicial()
        {
            InitializeComponent();

            //FormBorderStyle = FormBorderStyle.None;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filePath = "C:\\Users\\rodri\\Documents\\PALatinoamericana\\Produtos\\Desenvolvimento\\Extrator\\Solucao Extrator TARGIT\\Extrator TARGIT\\bin\\Debug\\Extrator TARGIT.exe";
            Assembly assembly = Assembly.LoadFrom(filePath);
            InstallService("Extrator TARGIT", assembly);
        }       
    }
}

