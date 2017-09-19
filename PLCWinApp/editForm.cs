using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjectDummy.DataService.DataServiceImpl;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using ProjectDummy.BusinessLayer.Entity;
using PLCWinTracker;
namespace PLCWinTracker
{
    public partial class 
        Edit : Form
    {
            public Edit()
            {
                InitializeComponent();
            }

             public void editsubmit_Click_1(object sender, EventArgs e)
             {
              

             }

             private void tabControl1_Click(object sender, EventArgs e)
             {
                
                 DataServiceXmlImpl objj = new DataServiceXmlImpl();
                 var plcss = objj.Read();

             }

             private void tabPage1_Click(object sender, EventArgs e)
             {
               // groupBox1.Visible = false;
             }

            

             private void Edit_Load(object sender, EventArgs e)
             {
                 createpanel();
             }
             public void createpanel()
             {
                 DataServiceXmlImpl objj = new DataServiceXmlImpl();
                 var plcss = objj.Read();

                 for (int j = 0; j < plcss.plcList.Count; j++)
                 {
                     cb1.Items.Add(plcss.plcList[j].ipAddress);
                 }

                 cb1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                 cb1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                 cb1.AutoCompleteSource = AutoCompleteSource.ListItems;
             }



             private void newowner_Click(object sender, EventArgs e)
             {

             }

             private void button2_Click(object sender, EventArgs e)
             {
                 DataServiceXmlImpl objj = new DataServiceXmlImpl();
                 var plcss = objj.Read();

                 {
                     for (int i = 0; i < plcss.plcList.Count; i++)
                     {
                         //string data = plcs.plcList[i].ipAddress.ToString();
                         if (plcss.plcList[i].ipAddress.ToString() == cb1.Text)
                         {
                             plcss.plcList[i].ownerName = txtnewowner.Text;

                             XmlSerializer serializer = new XmlSerializer(typeof(plcs));
                             using (TextWriter writer = new StreamWriter(@"D:\InformationPlc.xml"))
                             {
                                 serializer.Serialize(writer, plcss);
                                 writer.Close();
                             }
                         }
                     }
                    
                     MessageBox.Show("Owner details is edited");

                     txtCurrentOwner.Text = "";
                     txtnewowner.Text = "";
                     groupBox1.Visible = false;

                 }

             }

             private void Save_Click(object sender, EventArgs e)
             {
                 DataServiceXmlImpl objj = new DataServiceXmlImpl();
                 var addip = objj.Read();

                 plcDevice obj2 = new plcDevice();
                 obj2.ipAddress = addtxtip.Text;
                 obj2.status = " ";
                 obj2.userName = " ";
                 obj2.ownerName = addtxtname.Text;
                 addip.plcList.Add(obj2);
                 XmlSerializer serializer = new XmlSerializer(typeof(plcs));
                 using (TextWriter writer = new StreamWriter(@"D:\InformationPlc.xml"))
                 {
                     serializer.Serialize(writer, addip);
                     writer.Close();
                    
                 }
                
                 MessageBox.Show("Data has been added");
             }

             private void cb1_SelectedIndexChanged(object sender, EventArgs e)
             {
                 groupBox1.Enabled = true;
                 groupBox1.Visible = true;

                 string ed1 = cb1.Text;
                 DataServiceXmlImpl obj = new DataServiceXmlImpl();
                 var plcs = obj.Read();

                 for (int i = 0; i < plcs.plcList.Count; i++)
                 {
                     //string data = plcs.plcList[i].ipAddress.ToString();
                     if (plcs.plcList[i].ipAddress.ToString() == cb1.Text)
                     {
                         txtCurrentOwner.Text = String.Format(plcs.plcList[i].ownerName.ToString());
                     }

                 }
             } 
    }
}
