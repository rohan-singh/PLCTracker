using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using ProjectDummy.DataService.DataServiceImpl;
using System.Xml.Serialization;
using ProjectDummy.BusinessLayer.Entity;
namespace Popup
{
    public partial class Form2 : Form
    {
        private string _ipAddress = string.Empty;

        string TEXT;

        public Form2(string plcIp)
        {
            InitializeComponent();
            _ipAddress = plcIp;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            TcpClient tcpclnt = new TcpClient();

            try
            {
                tcpclnt.Connect("172.17.90.28", 8001);
                // use the ipaddress as in the server program

                string str = Environment.UserName + ":" + _ipAddress;
                Stream stm = tcpclnt.GetStream();

                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(str);
                //Console.WriteLine("Transmitting.....");

                stm.Write(ba, 0, ba.Length);

                byte[] bb = new byte[100];
                int k = stm.Read(bb, 0, 100);
                string result = System.Text.Encoding.UTF8.GetString(bb);

                //textBox2.Text = result;
                if (result.Contains("Yes"))
                {
                    DataServiceXmlImpl obj = new DataServiceXmlImpl();
                    var plcs = obj.Read();

                    for (int i = 0; i < plcs.plcList.Count; i++)
                    {
                        //string data = plcs.plcList[i].ipAddress.ToString();
                        if (plcs.plcList[i].ipAddress.ToString() == _ipAddress)
                        {
                            plcs.plcList[i].userName = Environment.UserName;
                            plcs.plcList[i].status = "Active";
                        }
                    }
                    XmlSerializer serializer = new XmlSerializer(typeof(plcs));
                    using (TextWriter writer = new StreamWriter(@"D:\InformationPlc.xml"))
                    {
                        serializer.Serialize(writer, plcs);
                        writer.Close();
                    }

                    this.Close();
                }
                else
                {
                    this.lblMessage.Text = "Your request has been declined!";
                    this.lblMessage.ForeColor = Color.Red;
                }

            }

            catch (Exception k)
            {
                //Console.WriteLine("Error..... " + e.StackTrace);
            }
            finally
            {
                tcpclnt.Close();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            TEXT = textBox2.Text;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            this.Text = this.Text + _ipAddress;
            textBox2.Text = Environment.UserName;
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            this.lblMessage.ForeColor = Color.Black;
            lblMessage.Text = "Waiting for a response...";
            lblMessage.Visible = true;
        }
    }
}
