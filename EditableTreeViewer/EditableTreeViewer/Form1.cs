using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditableTreeViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            treeView1.Nodes.Add("Root", "Root");

        }

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!ThereIsANodeSelected())
            {
                return;
            }

            AddForm addForm = new AddForm();
            addForm.ShowDialog();
            if (DialogResult.OK == addForm.DialogResult)
            {
                string description = addForm.NodeDescription;
                string name = ScrubName(description);

                treeView1.SelectedNode.Nodes.Add(name, description);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!ThereIsANodeSelected())
            {
                return;
            }

            AddForm addForm = new AddForm();
            addForm.ShowDialog();

            if (DialogResult.OK == addForm.DialogResult)
            {
                TreeNode node = treeView1.SelectedNode;
                string name = ScrubName(addForm.NodeDescription);
                node.Name = name;
                node.Text = addForm.NodeDescription;
            }
        }

        private string ScrubName(string name)
        {
            string cleanName = "";

            char[] rawChars = name.ToCharArray();

            foreach (var c in rawChars)
            {
                if(char.IsLetterOrDigit(c))
                {
                    cleanName += c;    
                }

            }

            return cleanName;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ThereIsANodeSelected())
                return;

            if (SelectedNodeIsRoot())
            {
                MessageBox.Show("You cannot delete the root node");
                return;
            }

            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete this node: " + treeView1.SelectedNode.Text, "Delete Warning!", MessageBoxButtons.YesNo))
            {
                treeView1.Nodes.Remove(treeView1.SelectedNode);
            }
        }

        private bool SelectedNodeIsRoot()
        {
            return treeView1.SelectedNode != null && treeView1.SelectedNode.Parent == null;
        }

        private bool ThereIsANodeSelected()
        {
            return treeView1.SelectedNode != null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.Title = "Save XML Document";
            saveFileDialog.Filter = "All Files (*.*)|*.*|XML Files (*.xml)|*.xml";

            if (DialogResult.OK == saveFileDialog.ShowDialog())
            {
                TreeSaver treeSaver = new TreeSaver(treeView1, saveFileDialog.FileName);

                treeSaver.exportToXml2();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Open XML Document";
            dlg.Filter = "All Files (*.*)|*.*|XML Files (*.xml)|*.xml";
            dlg.FileName = Application.StartupPath + "example.xml";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                TreeLoader treeLoader = new TreeLoader(treeView1, dlg.FileName);
                treeLoader.populateTreeview();
            }
        }
    }
}
