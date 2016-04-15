using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace EditableTreeViewer
{
    //Code taken from
    //https://stackoverflow.com/questions/20892114/how-to-serialize-treeview-in-to-xml-and-deserialize-xml-back-to-treeview/20892760#20892760
    //http://www.codeproject.com/Articles/12606/Loading-and-Saving-XML-to-and-from-a-TreeView-Cont
    //
    //Why re-invent the wheel
    public class TreeSaver
    {
        TreeView _treeView1 = null;
        string _filename = string.Empty;

        public TreeSaver(TreeView treeView, string filename)
        {
            _treeView1 = treeView;
            _filename = filename;
        }

        //We use this in the exportToXml2 and the saveNode2 
        //functions, though it's only instantiated once.
        private XmlTextWriter xr;

        public void exportToXml2()
        {
            xr = new XmlTextWriter(_filename, System.Text.Encoding.UTF8);
            xr.WriteStartDocument();
            //Write our root node
            xr.WriteStartElement(_treeView1.Nodes[0].Text);
            foreach (TreeNode node in _treeView1.Nodes)
            {
                saveNode2(node.Nodes);
            }
            //Close the root node
            xr.WriteEndElement();
            xr.Close();
        }

        private void saveNode2(TreeNodeCollection tnc)
        {
            foreach (TreeNode node in tnc)
            {
                //If we have child nodes, we'll write 
                //a parent node, then iterrate through
                //the children
                if (node.Nodes.Count > 0)
                {
                    xr.WriteStartElement(node.Name);
                    xr.WriteStartAttribute("description");
                    xr.WriteString(node.Text);
                    xr.WriteEndAttribute();
                    saveNode2(node.Nodes);
                    xr.WriteEndElement();
                }
                else //No child nodes, so we just write the text
                {
                    xr.WriteStartElement(node.Name);
                    xr.WriteStartAttribute("description");
                    xr.WriteString(node.Text);
                    xr.WriteEndAttribute();
                    xr.WriteString(".");
                    xr.WriteEndElement();
                }
            }
        }
    }
}
