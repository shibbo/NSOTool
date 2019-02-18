using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSOTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = "";

            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                name = dialog.FileName;
            }
            else
            {
                MessageBox.Show("File opening failed.");
                return;
            }

            mNSO = new NSO(name);

            radioButton1.Select();
            radioButton1.Checked = true;
            fileOffsetBox.Text = "0x" + mNSO.mTextSeg.fileOffset.ToString("X");
            memoryOffsetBox.Text = "0x" + mNSO.mTextSeg.memoryOffset.ToString("X");
            decompSizeBox.Text = "0x" + mNSO.mTextSeg.decompressedSize.ToString("X");
            hashBox.Text = BitConverter.ToString(mNSO.mTextSHA).Replace("-", "");

            // checkboxes
            checkBox1.Checked = (mNSO.mFlags & 0x1) != 0;
            checkBox2.Checked = ((mNSO.mFlags >> 1) & 0x1) != 0;
            checkBox3.Checked = ((mNSO.mFlags >> 2) & 0x1) != 0;

            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            radioButton3.Enabled = true;
        }

        NSO mNSO;

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            fileOffsetBox.Text = "0x" + mNSO.mRODataSeg.fileOffset.ToString("X");
            memoryOffsetBox.Text = "0x" + mNSO.mRODataSeg.memoryOffset.ToString("X");
            decompSizeBox.Text = "0x" + mNSO.mRODataSeg.decompressedSize.ToString("X");
            hashBox.Text = BitConverter.ToString(mNSO.mRODataSHA).Replace("-", "");
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            fileOffsetBox.Text = "0x" + mNSO.mDataSeg.fileOffset.ToString("X");
            memoryOffsetBox.Text = "0x" + mNSO.mDataSeg.memoryOffset.ToString("X");
            decompSizeBox.Text = "0x" + mNSO.mDataSeg.decompressedSize.ToString("X");
            hashBox.Text = BitConverter.ToString(mNSO.mDataSHA).Replace("-", "");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            fileOffsetBox.Text = "0x" + mNSO.mTextSeg.fileOffset.ToString("X");
            memoryOffsetBox.Text = "0x" + mNSO.mTextSeg.memoryOffset.ToString("X");
            decompSizeBox.Text = "0x" + mNSO.mTextSeg.decompressedSize.ToString("X");
            hashBox.Text = BitConverter.ToString(mNSO.mTextSHA).Replace("-", "");
        }
    }
}
