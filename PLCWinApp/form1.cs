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

namespace PLCTracker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // creating table layout panel
            createPanel();

        }


        public void createPanel()
        {
            List<properties> list1 = serialization.read();

            //Creating table Layout panel object
            TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
            this.Controls.Add(tableLayoutPanel1);
            this.AutoSize = true;

            //Clear out the existing controls, we are generating a new table layout
            tableLayoutPanel1.Controls.Clear();
            //Clear out the existing row and column styles
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.AutoSize = true;

            //Now we will generate the table, setting up the row and column counts first
            tableLayoutPanel1.ColumnCount = 3;
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


            for (int x = 0; x < tableLayoutPanel1.RowCount; x++)
            {
                //First add a column
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));


                //Next, add a row.  Only do this when once, when creating the first column

                // tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
;
                TextBox tb1 = new TextBox();
                tb1.Text = string.Format(list1[x].ipaddress.ToString());  //Finally, add the control to the correct location in the table
                tableLayoutPanel1.Controls.Add(tb1);
                TextBox tb2 = new TextBox();
                tb2.Text = string.Format(list1[x].owner);         //Finally, add the control to the correct location in the table
                tableLayoutPanel1.Controls.Add(tb2);
                TextBox tb3 = new TextBox();
                tb3.Text = string.Format(list1[x].status);         //Finally, add the control to the correct location in the table
                tableLayoutPanel1.Controls.Add(tb3);

            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
