using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccessLayer;
using Sendmail;

namespace PLCTrackerApp
{
    public partial class FrmPLCUsageTracker : Form
    {
        public FrmPLCUsageTracker()
        {
            InitializeComponent();
            // creating table layout panel
          

        }

        public string val;
             TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();

        public void createPanel()
        {
            
            List<plcdetails> list1 = serialization.read();
            
            
            //Creating table Layout panel object
            this.Controls.Add(tableLayoutPanel1);
            this.AutoSize = true;

            //Clear out the existing controls, we are generating a new table layout
            tableLayoutPanel1.Controls.Clear();
            //Clear out the existing row and column styles
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.AutoSize = true;

            //Now we will generate the table, setting up the row and column counts first
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.RowCount = list1.Count;

            Label lb1 = new Label();
            lb1.Text = string.Format("IP Address");
            tableLayoutPanel1.Controls.Add(lb1);

            Label lb2 = new Label();
            lb2.Text = string.Format("Owner");
            tableLayoutPanel1.Controls.Add(lb2);

            Label lb3 = new Label();
            lb3.Text = string.Format("Status");
            tableLayoutPanel1.Controls.Add(lb3);

            Label lb4 = new Label();
            lb4.Text = string.Format("User");
            tableLayoutPanel1.Controls.Add(lb4);


            Label lb5 = new Label();
            lb5.Text = string.Format("Access");
            tableLayoutPanel1.Controls.Add(lb5);



            for (int x = 0; x < tableLayoutPanel1.RowCount; )
            {
                //First add a column
                
               


                //if (list1[x].ipaddress.Contains(val))
                //{
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

                    //Next, add a row.  Only do this when once, when creating the first column

                    // tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                    TextBox tb1 = new TextBox();
                    tb1.Text = string.Format(list1[x].ipaddress.ToString());         //Finally, add the control to the correct location in the table
                    tableLayoutPanel1.Controls.Add(tb1);
                    TextBox tb2 = new TextBox();
                    tb2.Text = string.Format(list1[x].owner);         //Finally, add the control to the correct location in the table
                    tableLayoutPanel1.Controls.Add(tb2);
                    CheckBox cb1 = new CheckBox();
                    cb1.Text = string.Format(list1[x].status);
                    cb1.Checked = true;
                    tableLayoutPanel1.Controls.Add(cb1);

                    TextBox tb3 = new TextBox();
                    tb3.Text = string.Format(list1[x].user);         //Finally, add the control to the correct location in the table
                    tableLayoutPanel1.Controls.Add(tb3);

                    Button b1 = new Button();
                    tableLayoutPanel1.Controls.Add(b1);
                    b1.Text = string.Format("Request");
                    b1.Click += (s, e) =>
                    {
                        Notify frm = new Notify();
                        frm.Show();
                    };
                    x++;
                    string TEXT = textBox1.Text;
                    textBox1.TextChanged += (s, e) => 
                    { val=textBox1.Text;
                    reloadPanel(list1);
                    };
                //}
                

            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            createPanel();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    Notify frm = new Notify();
        //    frm.Show();
        //}
        public class Notify : Form
        {
            public Notify()
            {
                Text = "Notify";
                SendMail sendmail = new SendMail();
                sendmail.sendmailto();
                


            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string TEXT= textBox1.Text;
            val = TEXT;


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        public void reloadPanel(List<plcdetails> list1)
        {
            //Creating table Layout panel object
            list1 = list1.FindAll(x => x.ipaddress.Contains(textBox1.Text));
            this.Controls.Add(tableLayoutPanel1);
            this.AutoSize = true;

            //Clear out the existing controls, we are generating a new table layout
            tableLayoutPanel1.Controls.Clear();
            //Clear out the existing row and column styles
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.AutoSize = true;

            //Now we will generate the table, setting up the row and column counts first
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.RowCount = list1.Count;

            Label lb1 = new Label();
            lb1.Text = string.Format("IP Address");
            tableLayoutPanel1.Controls.Add(lb1);

            Label lb2 = new Label();
            lb2.Text = string.Format("Owner");
            tableLayoutPanel1.Controls.Add(lb2);

            Label lb3 = new Label();
            lb3.Text = string.Format("Status");
            tableLayoutPanel1.Controls.Add(lb3);

            Label lb4 = new Label();
            lb4.Text = string.Format("User");
            tableLayoutPanel1.Controls.Add(lb4);


            Label lb5 = new Label();
            lb5.Text = string.Format("Access");
            tableLayoutPanel1.Controls.Add(lb5);



            for (int x = 0; x < tableLayoutPanel1.RowCount; )
            {
                //First add a column




                //if (list1[x].ipaddress.Contains(val))
                //{
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

                //Next, add a row.  Only do this when once, when creating the first column

                // tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                TextBox tb1 = new TextBox();
                tb1.Text = string.Format(list1[x].ipaddress.ToString());         //Finally, add the control to the correct location in the table
                tableLayoutPanel1.Controls.Add(tb1);
                TextBox tb2 = new TextBox();
                tb2.Text = string.Format(list1[x].owner);         //Finally, add the control to the correct location in the table
                tableLayoutPanel1.Controls.Add(tb2);
                CheckBox cb1 = new CheckBox();
                cb1.Text = string.Format(list1[x].status);
                cb1.Checked = true;
                tableLayoutPanel1.Controls.Add(cb1);

                TextBox tb3 = new TextBox();
                tb3.Text = string.Format(list1[x].user);         //Finally, add the control to the correct location in the table
                tableLayoutPanel1.Controls.Add(tb3);

                Button b1 = new Button();
                tableLayoutPanel1.Controls.Add(b1);
                b1.Text = string.Format("Request");
                b1.Click += (s, e) =>
                {
                    Notify frm = new Notify();
                    frm.Show();
                };
                x++;
                //}


            }

        }
    }
}
