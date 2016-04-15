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
    public partial class AddForm : Form
    {

        private string _nodeDescription = "";



        public string NodeDescription
        {
            get { return _nodeDescription; }
            set { _nodeDescription = value; }
        }

        public AddForm()
        {
            InitializeComponent();
            txtDescription.Text = _nodeDescription;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _nodeDescription = txtDescription.Text;
   
            this.DialogResult = DialogResult.OK;
        }
    }
}
