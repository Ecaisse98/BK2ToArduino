using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace View
{
    public partial class MainUI : Form
    {
        IViewController ViewController { get; set; }
        public MainUI(IViewController viewController)
        {
            InitializeComponent();
            this.ViewController = viewController;
            changeLabel(LabelState.None);
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;


            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "bk2 files (*.bk2)|*.bk2";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();
                }
            }

            if(filePath != string.Empty)
            {

                txtFilePath.Text = filePath;
            }

            changeLabel(LabelState.FileLoaded);
        }

        private void btnSendFile_Click(object sender, EventArgs e)
        {
            changeLabel(LabelState.FileSent);
            ViewController.SendDataToComm(txtFilePath.Text);
            changeLabel(LabelState.Finished);
        }

        private void cbCommPort_Click(object sender, EventArgs e)
        {
            foreach(string item in ViewController.GetCommPortList())
            {
                if(!cbCommPort.Items.Contains(item))
                    cbCommPort.Items.Add(item);
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ViewController.ConnectToCommPort(cbCommPort.SelectedItem.ToString());
            changeLabel(LabelState.Connected);
        }

        private void changeLabel(LabelState lblState)
        {
            lblStatus.ForeColor = Color.Green;
            switch (lblState)
            {
                case LabelState.None:
                    lblStatus.ForeColor = Color.Red;
                    lblStatus.Text = "Waiting for connection...";
                    btnOpenFile.Enabled = false;
                    btnSendFile.Enabled = false;
                    break;
                case LabelState.Connected:
                    lblStatus.Text = "Connected!";
                    btnOpenFile.Enabled = true;
                    btnSendFile.Enabled = false;
                    break;
                case LabelState.FileLoaded:
                    lblStatus.Text = "File Loaded";
                    btnOpenFile.Enabled = true;
                    btnSendFile.Enabled = true;
                    break;
                case LabelState.FileSent:
                    lblStatus.Text = "File Sent";
                    btnConnect.Enabled = false;
                    btnOpenFile.Enabled = false;
                    btnSendFile.Enabled = false;
                    break;
                case LabelState.Finished:
                    lblStatus.Text = "Finished";
                    btnConnect.Enabled = true;
                    btnOpenFile.Enabled = false;
                    btnSendFile.Enabled = false;
                    break;

            }


            

        }


    }
}
