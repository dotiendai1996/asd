namespace TestDeviceAbriDoor
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxDeviceIP = new System.Windows.Forms.TextBox();
            this.tbxPort = new System.Windows.Forms.TextBox();
            this.btnPingDevice = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxMachineNumber = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblDeviceInfo = new System.Windows.Forms.Label();
            this.dgvRecords = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.dateProcessDateStart = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dateProcessDateEnd = new System.Windows.Forms.DateTimePicker();
            this.btnPullData = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.dgvRecords)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (236)))), ((int) (((byte) (236)))), ((int) (((byte) (236)))));
            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Controls.Add(this.label1);
            this.pnlHeader.Controls.Add(this.tbxDeviceIP);
            this.pnlHeader.Controls.Add(this.tbxPort);
            this.pnlHeader.Controls.Add(this.btnPingDevice);
            this.pnlHeader.Controls.Add(this.label2);
            this.pnlHeader.Controls.Add(this.btnConnect);
            this.pnlHeader.Controls.Add(this.label3);
            this.pnlHeader.Controls.Add(this.tbxMachineNumber);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(800, 37);
            this.pnlHeader.TabIndex = 714;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int) (((byte) (116)))), ((int) (((byte) (116)))), ((int) (((byte) (116)))));
            this.lblHeader.Location = new System.Drawing.Point(12, 9);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(80, 19);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "GGROUP";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(223, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Device IP";
            // 
            // tbxDeviceIP
            // 
            this.tbxDeviceIP.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxDeviceIP.Location = new System.Drawing.Point(281, 9);
            this.tbxDeviceIP.Name = "tbxDeviceIP";
            this.tbxDeviceIP.Size = new System.Drawing.Size(99, 20);
            this.tbxDeviceIP.TabIndex = 0;
            this.tbxDeviceIP.Text = "192.168.1.201";
            // 
            // tbxPort
            // 
            this.tbxPort.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxPort.Location = new System.Drawing.Point(418, 9);
            this.tbxPort.MaxLength = 6;
            this.tbxPort.Name = "tbxPort";
            this.tbxPort.Size = new System.Drawing.Size(56, 20);
            this.tbxPort.TabIndex = 2;
            this.tbxPort.Text = "4370";
            // 
            // btnPingDevice
            // 
            this.btnPingDevice.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPingDevice.Location = new System.Drawing.Point(705, 9);
            this.btnPingDevice.Name = "btnPingDevice";
            this.btnPingDevice.Size = new System.Drawing.Size(75, 23);
            this.btnPingDevice.TabIndex = 5;
            this.btnPingDevice.Text = "Ping Device";
            this.btnPingDevice.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(386, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port";
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Location = new System.Drawing.Point(624, 9);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(480, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Machine Number";
            // 
            // tbxMachineNumber
            // 
            this.tbxMachineNumber.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxMachineNumber.Location = new System.Drawing.Point(581, 9);
            this.tbxMachineNumber.MaxLength = 3;
            this.tbxMachineNumber.Name = "tbxMachineNumber";
            this.tbxMachineNumber.Size = new System.Drawing.Size(37, 20);
            this.tbxMachineNumber.TabIndex = 8;
            this.tbxMachineNumber.Text = "1";
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.Location = new System.Drawing.Point(0, 425);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.lblStatus.Size = new System.Drawing.Size(800, 25);
            this.lblStatus.TabIndex = 713;
            this.lblStatus.Text = "label3";
            // 
            // lblDeviceInfo
            // 
            this.lblDeviceInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDeviceInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.lblDeviceInfo.Location = new System.Drawing.Point(0, 37);
            this.lblDeviceInfo.Name = "lblDeviceInfo";
            this.lblDeviceInfo.Size = new System.Drawing.Size(800, 28);
            this.lblDeviceInfo.TabIndex = 893;
            this.lblDeviceInfo.Text = "Device Info : --";
            this.lblDeviceInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvRecords
            // 
            this.dgvRecords.AllowUserToAddRows = false;
            this.dgvRecords.AllowUserToDeleteRows = false;
            this.dgvRecords.AllowUserToOrderColumns = true;
            this.dgvRecords.AllowUserToResizeColumns = false;
            this.dgvRecords.AllowUserToResizeRows = false;
            this.dgvRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecords.Location = new System.Drawing.Point(0, 119);
            this.dgvRecords.Name = "dgvRecords";
            this.dgvRecords.Size = new System.Drawing.Size(800, 306);
            this.dgvRecords.TabIndex = 895;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.dateProcessDateStart);
            this.flowLayoutPanel1.Controls.Add(this.label4);
            this.flowLayoutPanel1.Controls.Add(this.dateProcessDateEnd);
            this.flowLayoutPanel1.Controls.Add(this.btnPullData);
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 65);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(800, 54);
            this.flowLayoutPanel1.TabIndex = 894;
            // 
            // dateProcessDateStart
            // 
            this.dateProcessDateStart.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dateProcessDateStart.Dock = System.Windows.Forms.DockStyle.Left;
            this.dateProcessDateStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateProcessDateStart.Location = new System.Drawing.Point(3, 3);
            this.dateProcessDateStart.Name = "dateProcessDateStart";
            this.dateProcessDateStart.Size = new System.Drawing.Size(160, 20);
            this.dateProcessDateStart.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(169, 5);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 49);
            this.label4.TabIndex = 12;
            this.label4.Text = "đến";
            // 
            // dateProcessDateEnd
            // 
            this.dateProcessDateEnd.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dateProcessDateEnd.Dock = System.Windows.Forms.DockStyle.Left;
            this.dateProcessDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateProcessDateEnd.Location = new System.Drawing.Point(209, 3);
            this.dateProcessDateEnd.Name = "dateProcessDateEnd";
            this.dateProcessDateEnd.Size = new System.Drawing.Size(160, 20);
            this.dateProcessDateEnd.TabIndex = 13;
            // 
            // btnPullData
            // 
            this.btnPullData.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.btnPullData.Location = new System.Drawing.Point(375, 3);
            this.btnPullData.Name = "btnPullData";
            this.btnPullData.Size = new System.Drawing.Size(99, 48);
            this.btnPullData.TabIndex = 14;
            this.btnPullData.Text = "Get Data Chấm Công";
            this.btnPullData.UseVisualStyleBackColor = true;
            this.btnPullData.Click += new System.EventHandler(this.btnPullData_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.button1.ForeColor = System.Drawing.Color.Green;
            this.button1.Location = new System.Drawing.Point(480, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(166, 48);
            this.button1.TabIndex = 15;
            this.button1.Text = "Cập nhật dữ liệu chấm công lên máy chủ QLNS";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvRecords);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.lblDeviceInfo);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.lblStatus);
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thu thập dữ liệu từ đầu đọc thẻ lên máy chủ QLNS";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize) (this.dgvRecords)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnPingDevice;
        private System.Windows.Forms.Button btnPullData;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker dateProcessDateEnd;
        private System.Windows.Forms.DateTimePicker dateProcessDateStart;
        private System.Windows.Forms.DataGridView dgvRecords;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblDeviceInfo;
        public System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.TextBox tbxDeviceIP;
        private System.Windows.Forms.TextBox tbxMachineNumber;
        private System.Windows.Forms.TextBox tbxPort;

        #endregion
    }
}

