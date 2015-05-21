using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public partial class Form2 : Form
    {
        string name, username;
        public Form2(string name, string username)
        {
            InitializeComponent();
            label1.Text = name;
            this.username = username;
            this.name = name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (title.Text == ""||title.Text=="Title")
            {
                MessageBox.Show("Please Enter the Title of Message");
            }
            else
            if (message.Text == "" || message.Text == "Type your message here")
            {
                MessageBox.Show("Enter Message");
            }
            else
                if (comboBox1.Text == "")
                {
                    MessageBox.Show("Please select Year");
                }
                else
                {
                    string tag = "post";
                    string content = "title=" + title.Text + "&message=" + message.Text + "&year=" 
                        + comboBox1.Text + "&admin=" + username;

                    string URI = "http://dcetech.com/sagnik/social_network/desktop/data_activity.php";

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URI);
                    request.Method = "POST";
                    byte[] byteArray = Encoding.UTF8.GetBytes(content);
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = byteArray.Length;
                    Stream dataStream = request.GetRequestStream();

                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();

                    WebResponse response = null;

                    try
                    {
                        response = request.GetResponse();
                    }
                    catch (WebException w)
                    {
                        MessageBox.Show("Server is down! Please try again later" + w);
                    }

                    dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = HttpUtility.UrlDecode(reader.ReadToEnd()).Split('<')[0];
                    MessageBox.Show(responseFromServer);
                }
        }
        
    }
}
