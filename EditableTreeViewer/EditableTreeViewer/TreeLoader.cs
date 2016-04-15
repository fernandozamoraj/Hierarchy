using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;

namespace EditableTreeViewer
{
    public class TreeLoader
    {
        string _fileName = string.Empty;
        TreeView _treeView1 = null;

        public TreeLoader(TreeView treeView, string fileName)
        {
            _fileName = fileName;
            _treeView1 = treeView;
        }

        //Open the XML file, and start to populate the treeview
        public void populateTreeview()
        {
            
            try
            {
                //First, we'll load the Xml document
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(_fileName);
                //Now, clear out the treeview, 
                //and add the first (root) node
                _treeView1.Nodes.Clear();
                _treeView1.Nodes.Add(new
                    TreeNode(xDoc.DocumentElement.Name));
                TreeNode tNode = new TreeNode();
                tNode = (TreeNode)_treeView1.Nodes[0];
                //We make a call to addTreeNode, 
                //where we'll add all of our nodes
                addTreeNode(xDoc.DocumentElement, tNode);
                //Expand the treeview to show all nodes
                _treeView1.ExpandAll();
            }
            catch (XmlException xExc)
            //Exception is thrown is there is an error in the Xml
            {
                MessageBox.Show(xExc.Message);
            }
            catch (Exception ex) //General exception
            {
                MessageBox.Show(ex.Message);
            }
        }
        //This function is called recursively until all nodes are loaded
        private void addTreeNode(XmlNode xmlNode, TreeNode treeNode)
        {
            XmlNode xNode;
            TreeNode tNode;
            XmlNodeList xNodeList;
            if (xmlNode.HasChildNodes) //The current node has children
            {
                xNodeList = xmlNode.ChildNodes;
                for (int x = 0; x <= xNodeList.Count - 1; x++)
                //Loop through the child nodes
                {
                    xNode = xmlNode.ChildNodes[x];
                    string description = "";

                    if (xNode.Attributes != null && xNode.Attributes["description"] != null)
                    {
                        description = xNode.Attributes["description"].InnerText;
                    }
                    treeNode.Nodes.Add(xNode.Name, description);
                    tNode = treeNode.Nodes[x];
                    addTreeNode(xNode, tNode);
                }
            }
            else //No children, so add the outer xml (trimming off whitespace)
                treeNode.Text = xmlNode.OuterXml.Trim();
        }
    }
}
