//DashBoard Form

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
using ProjectDummy.BusinessLayer.Entity;
using Popup;
using System.Net;
using System.Net.Sockets;
using System.IO;
using notifypopup;
using System.Xml.Serialization;
using System.Threading;

namespace PLCWinTracker
{
    public partial class Form1 : Form
    {
        Thread _listenerThread;

        public Form1()
        {
            InitializeComponent();

            _listenerThread = new Thread(startListener);
            _listenerThread.IsBackground = true;
            _listenerThread.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            tableLayoutPanel1.Visible = true;
            createpanel();

        }
        public void createpanel()
        {
            DataServiceXmlImpl obj = new DataServiceXmlImpl();
            var plcs = obj.Read();

            this.AutoSize = true;
            tableLayoutPanel1.Controls.Clear();
            //Clear out the existing row and column styles
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.AutoSize = true;

            //Now we will generate the table, setting up the row and column counts first
            tableLayoutPanel1.ColumnCount = 6;
            tableLayoutPanel1.RowCount = plcs.plcList.Count;

            Label lbIpaddress = new Label();
            lbIpaddress.Text = string.Format("IP Address");
            tableLayoutPanel1.Controls.Add(lbIpaddress);


            Label lbOwnername = new Label();
            lbOwnername.Text = string.Format("Owner");
            tableLayoutPanel1.Controls.Add(lbOwnername);

            Label lbStatus = new Label();
            lbStatus.Text = string.Format("Status");
            tableLayoutPanel1.Controls.Add(lbStatus);

            Label lbUsername = new Label();
            lbUsername.Text = string.Format("User");
            tableLayoutPanel1.Controls.Add(lbUsername);


            Label lbAccess = new Label();
            lbAccess.Text = string.Format("Access");
            tableLayoutPanel1.Controls.Add(lbAccess);

            Label lbRelease = new Label();
            lbRelease.Text = string.Format("PLC");
            tableLayoutPanel1.Controls.Add(lbRelease);

            for (int x = 0; x < tableLayoutPanel1.RowCount; x++)
            {
                //First add a column
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));


                //Next, add a row.  Only do this when once, when creating the first column

                // tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                TextBox txtIpaddress = new TextBox();
                //txtIpaddress.Name = x.ToString();
                txtIpaddress.Text = string.Format(plcs.plcList[x].ipAddress.ToString());         //Finally, add the control to the correct location in the table
                tableLayoutPanel1.Controls.Add(txtIpaddress);
                //txtIpaddress.Enabled = false;
                txtIpaddress.ReadOnly = true;
                TextBox txtOwnername = new TextBox();
                txtOwnername.Text = string.Format(plcs.plcList[x].ownerName);         //Finally, add the control to the correct location in the table
                tableLayoutPanel1.Controls.Add(txtOwnername);
                txtOwnername.ReadOnly= true;

                CheckBox cbStatus = new CheckBox();
                cbStatus.Text = string.Format(plcs.plcList[x].status);

                if (plcs.plcList[x].status == "Active")
                    cbStatus.Checked = true;
                else
                    cbStatus.Checked = false;
                tableLayoutPanel1.Controls.Add(cbStatus);
                cbStatus.Enabled = false;


                TextBox txtUsername = new TextBox();
                if (cbStatus.Checked)
                    txtUsername.Text = string.Format(plcs.plcList[x].userName);         //Finally, add the control to the correct location in the table
                else
                    txtUsername.Text = "";
                tableLayoutPanel1.Controls.Add(txtUsername);
                txtUsername.ReadOnly = true;
                Button btnRequest = new Button();
                //btnRequest.Name = x.ToString();
                if (cbStatus.Checked == false)
                { btnRequest.Enabled = true; }
                else
                { btnRequest.Enabled = false; }

                tableLayoutPanel1.Controls.Add(btnRequest);
                btnRequest.Text = string.Format("Request PLC");
                btnRequest.Click += (s, d) =>
                {

                    Form2 newform = new Form2(txtIpaddress.Text);
                    newform.ShowDialog();
                    tableLayoutPanel1.Controls.Clear();
                    //Clear out the existing row and column styles
                    tableLayoutPanel1.ColumnStyles.Clear();
                    tableLayoutPanel1.RowStyles.Clear();
                    createpanel();
                    //Button btn = s as Button;
                    
                    //MessageBox.Show(txtIpaddress.Text);

                };


                Button btmRelease = new Button();
                if (plcs.plcList[x].status == "Active")
                    btmRelease.Enabled = true;
                else
                    btmRelease.Enabled = false;
                tableLayoutPanel1.Controls.Add(btmRelease);
                btmRelease.Text = string.Format("Release PLC");

                btmRelease.Click += (s, d) =>
                {
                    DataServiceXmlImpl _obj = new DataServiceXmlImpl();
                    var _plcs = _obj.Read();
                    for (int i = 0; i < _plcs.plcList.Count; i++)
                    {
                         //string data = plcs.plcList[i].ipAddress.ToString();
                         if (_plcs.plcList[i].ipAddress.ToString()==txtIpaddress.Text)
                        {
                            _plcs.plcList[i].status = "InActive";
                         }
                    }
                    XmlSerializer serializer = new XmlSerializer(typeof(plcs));
                    TextWriter writer = new StreamWriter(@"E:\InformationPlc.xml");
                    
                        serializer.Serialize(writer, _plcs);
                        writer.Close();
                        tableLayoutPanel1.Controls.Clear();
                        //Clear out the existing row and column styles
                        tableLayoutPanel1.ColumnStyles.Clear();
                        tableLayoutPanel1.RowStyles.Clear();
                        createpanel();
                        
                        
                };
                

                textBox1.TextChanged += (s, k) =>
                {
                    reloadPanel(plcs.plcList);
                };


            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
            Edit e1 = new Edit();
            e1.Show();
            e1.FormClosed += (s, x) =>
            {
                tableLayoutPanel1.Controls.Clear();
                //Clear out the existing row and column styles
                tableLayoutPanel1.ColumnStyles.Clear();
                tableLayoutPanel1.RowStyles.Clear();
                createpanel(); };
            
            
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Edit e1 = new Edit();
            e1.StartPosition = FormStartPosition.CenterScreen;
            e1.ShowDialog();
            e1.FormClosed += (s, x) =>
            {
                tableLayoutPanel1.Controls.Clear();
                tableLayoutPanel1.Controls.Clear();
                //Clear out the existing row and column styles
                tableLayoutPanel1.ColumnStyles.Clear();
                tableLayoutPanel1.RowStyles.Clear();
                createpanel();
            };
        }





        public void reloadPanel(List<plcDevice> list)
        {
            tableLayoutPanel1.Visible = false;

            List<plcDevice> list1 = list.FindAll(x=>x.ipAddress.ToString().Contains(textBox1.Text));
            tableLayoutPanel1.Controls.Clear();
            //Clear out the existing row and column styles
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.AutoSize = true;
 
            //Now we will generate the table, setting up the row and column counts first
            tableLayoutPanel1.ColumnCount = 6;
            tableLayoutPanel1.RowCount = list1.Count;

            Label lbIpaddress = new Label();
            lbIpaddress.Text = string.Format("IP Address");
            tableLayoutPanel1.Controls.Add(lbIpaddress);


            Label lbOwnername = new Label();
            lbOwnername.Text = string.Format("Owner");
            tableLayoutPanel1.Controls.Add(lbOwnername);

            Label lbStatus = new Label();
            lbStatus.Text = string.Format("Status");
            tableLayoutPanel1.Controls.Add(lbStatus);

            Label lbUsername = new Label();
            lbUsername.Text = string.Format("User");
            tableLayoutPanel1.Controls.Add(lbUsername);


            Label lbAccess = new Label();
            lbAccess.Text = string.Format("Access");
            tableLayoutPanel1.Controls.Add(lbAccess);

            Label lbRelease = new Label();
            lbRelease.Text = string.Format("PLC");
            tableLayoutPanel1.Controls.Add(lbRelease);

            for (int x = 0; x < tableLayoutPanel1.RowCount; x++)
            {
                //First add a column
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));


                //Next, add a row.  Only do this when once, when creating the first column

                // tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                TextBox txtIpaddress = new TextBox();
                //txtIpaddress.Name = x.ToString();
                txtIpaddress.Text = string.Format(list1[x].ipAddress.ToString());         //Finally, add the control to the correct location in the table
                tableLayoutPanel1.Controls.Add(txtIpaddress);
                //txtIpaddress.Enabled = false;
                txtIpaddress.ReadOnly = true;
                TextBox txtOwnername = new TextBox();
                txtOwnername.Text = string.Format(list1[x].ownerName);         //Finally, add the control to the correct location in the table
                tableLayoutPanel1.Controls.Add(txtOwnername);
                txtOwnername.ReadOnly = true;

                CheckBox cbStatus = new CheckBox();
                cbStatus.Text = string.Format(list1[x].status);

                if (list1[x].status == "Active")
                    cbStatus.Checked = true;
                else
                    cbStatus.Checked = false;
                tableLayoutPanel1.Controls.Add(cbStatus);
                cbStatus.Enabled = false;


                TextBox txtUsername = new TextBox();
                if (cbStatus.Checked)
                    txtUsername.Text = string.Format(list1[x].userName);         //Finally, add the control to the correct location in the table
                else
                    txtUsername.Text = "";
                tableLayoutPanel1.Controls.Add(txtUsername);
                txtUsername.ReadOnly = true;
                Button btnRequest = new Button();
                //btnRequest.Name = x.ToString();
                if (cbStatus.Checked == false)
                { btnRequest.Enabled = true; }
                else
                { btnRequest.Enabled = false; }

                tableLayoutPanel1.Controls.Add(btnRequest);
                btnRequest.Text = string.Format("Request PLC");
                btnRequest.Click += (s, d) =>
                {

                    Form2 newform = new Form2(txtIpaddress.Text);
                    newform.ShowDialog();
                    tableLayoutPanel1.Controls.Clear();
                    //Clear out the existing row and column styles
                    tableLayoutPanel1.ColumnStyles.Clear();
                    tableLayoutPanel1.RowStyles.Clear();
                    createpanel();

                };


                Button btmRelease = new Button();
                if (list1[x].status == "Active")
                    btnRequest.Enabled = true;
                else
                    btnRequest.Enabled = false;
                tableLayoutPanel1.Controls.Add(btmRelease);
                btmRelease.Text = string.Format("Release PLC");

                btmRelease.Click += (s, d) =>
                {
                    DataServiceXmlImpl _obj = new DataServiceXmlImpl();
                    var _plcs = _obj.Read();
                    for (int i = 0; i < _plcs.plcList.Count; i++)
                    {
                        //string data = plcs.plcList[i].ipAddress.ToString();
                        if (_plcs.plcList[i].ipAddress.ToString() == txtIpaddress.Text)
                        {
                            _plcs.plcList[i].status = "InActive";
                        }
                    }
                    XmlSerializer serializer = new XmlSerializer(typeof(plcs));
                    TextWriter writer = new StreamWriter(@"E:\InformationPlc.xml");

                    serializer.Serialize(writer, _plcs);
                    writer.Close();
                    tableLayoutPanel1.Controls.Clear();
                    //Clear out the existing row and column styles
                    tableLayoutPanel1.ColumnStyles.Clear();
                    tableLayoutPanel1.RowStyles.Clear();
                    createpanel();


                };
            }

            tableLayoutPanel1.Visible = true;
        }

        public void startListener()
        {
            while (true)
            {
                try
                {
                    IPAddress ipAd = IPAddress.Parse("172.17.90.38");
                    // use local m/c IP address, and 
                    // use the same in the client

                    /* Initializes the Listener */
                    TcpListener myList = new TcpListener(ipAd, 8001);

                    /* Start Listeneting at the specified port */
                    myList.Start();

                    Socket s = myList.AcceptSocket();


                    byte[] b = new byte[100];
                    int k = s.Receive(b);
                    string result = System.Text.Encoding.UTF8.GetString(b);
                    AccessForm newform = new AccessForm() { msg = result };


                    newform.button1.Click += (sen, eve) =>
                    {
                        ASCIIEncoding asen = new ASCIIEncoding();
                        //s.Send(asen.GetBytes("The string was recieved by the server."));
                        s.Send(asen.GetBytes("Yes"));
                        //Console.WriteLine("\nSent Acknowledgement");
                        /* clean up */
                        s.Close();
                        myList.Stop();

                        this.Close();

                    };

                    newform.button2.Click += (sen, eve) =>
                    {
                        ASCIIEncoding asen = new ASCIIEncoding();
                        //s.Send(asen.GetBytes("The string was recieved by the server."));
                        s.Send(asen.GetBytes("No"));
                        //Console.WriteLine("\nSent Acknowledgement");
                        /* clean up */
                        s.Close();
                        myList.Stop();

                        this.Close();
                    };

                    newform.ShowDialog();
                }
                catch (Exception k)
                {
                    Console.WriteLine("Error..... " + k.StackTrace);
                }
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Edit e1 = new Edit();
            e1.StartPosition = FormStartPosition.CenterScreen;
            e1.ShowDialog();
            tableLayoutPanel1.Controls.Clear();
            //Clear out the existing row and column styles
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            createpanel();



        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
