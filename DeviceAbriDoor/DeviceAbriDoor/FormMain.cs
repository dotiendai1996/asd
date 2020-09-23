using DeviceAbriDoor.Models;
using DeviceAbriDoor.RestSharp;
using DeviceAbriDoor.Utils;
using ScheduledService.Schedules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TestDeviceAbriDoor
{
    public partial class FormMain : Form
    {
        private AbriDoorSDK aTK100;

        private IList<CreateOrEditDataChamCongDto> dataChamCongList;

        public FormMain()
        {
            InitializeComponent();
            aTK100 = new AbriDoorSDK();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            dateProcessDateStart.Value = DateTime.Now.Date;
            dateProcessDateEnd.Value = DateTime.Now.Date.AddDays(1).AddTicks(-1);

            flowLayoutPanel1.Enabled = false;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            SchedulerUtils.Instance.ShutdownAsync();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            aTK100.Connect_Net(tbxDeviceIP.Text, Convert.ToInt32(tbxPort.Text));

            if (aTK100.IsConnected)
            {
                string deviceInfo = string.Empty;
                if (aTK100.GetSerialNumber(out deviceInfo))
                    lblDeviceInfo.Text = $"Device Info : {deviceInfo}";

                if (aTK100.GetFirmwareVersion(ref deviceInfo))
                    lblDeviceInfo.Text += $" - Firmware Version : {deviceInfo}";

                if (aTK100.GetVendor(ref deviceInfo))
                    lblDeviceInfo.Text += $" - Vendor : {deviceInfo}";

                if (aTK100.GetSDKVersion(ref deviceInfo))
                    lblDeviceInfo.Text += $" - SDK Version : {deviceInfo}";

                flowLayoutPanel1.Enabled = true;
            }
            aTK100.Disconnect();

            Cursor.Current = Cursors.Default;
        }

        private void btnPullData_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            dataChamCongList = DeviceUtils.Instance.GetDataChamCongAll();
            //dataChamCongList = DeviceUtils.Instance.GetDataChamCongByDate(dateProcessDateStart.Value, dateProcessDateEnd.Value);
            BindToGridView(dataChamCongList);

            Cursor.Current = Cursors.Default;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataChamCongList != null && dataChamCongList.Count > 0)
            {
                Cursor.Current = Cursors.WaitCursor;

                foreach (var chamCong in dataChamCongList)
                {
                    WebApiHelper.Instance.Post(WebApiConstant.ADMIN_DATACHAMCONG_CREATEOREDIT, chamCong);
                }

                Cursor.Current = Cursors.Default;
            }
        }

        private void ClearGrid()
        {
            if (dgvRecords.Controls.Count > 2)
            { dgvRecords.Controls.RemoveAt(2); }

            dgvRecords.DataSource = null;
            dgvRecords.Controls.Clear();
            dgvRecords.Rows.Clear();
            dgvRecords.Columns.Clear();
        }
        private void BindToGridView(IList<CreateOrEditDataChamCongDto> list)
        {
            ClearGrid();
            var bindingList = new BindingList<CreateOrEditDataChamCongDto>(list);
            var source = new BindingSource(bindingList, null);
            dgvRecords.DataSource = source;
            dgvRecords.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ChangeGridProperties(dgvRecords);

            dgvRecords.Update();
            dgvRecords.Refresh();
        }

        private void DisplayListOutput(string message)
        {
            if (dgvRecords.Controls.Count > 2)
            { dgvRecords.Controls.RemoveAt(2); }

            ShowStatusBar(message, false);
        }

        public void ShowStatusBar(string message, bool type)
        {
            if (message.Trim() == string.Empty)
            {
                lblStatus.Visible = false;
                return;
            }

            lblStatus.Visible = true;
            lblStatus.Text = message;
            lblStatus.ForeColor = Color.White;

            if (type)
                lblStatus.BackColor = Color.FromArgb(79, 208, 154);
            else
                lblStatus.BackColor = Color.FromArgb(230, 112, 134);
        }

        public static void ChangeGridProperties(DataGridView dgvMaster)
        {
            dgvMaster.DefaultCellStyle.Font = new Font("Segoe UI", 8F);
            dgvMaster.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMaster.RowHeadersWidth = 10;
            dgvMaster.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvMaster.RowHeadersVisible = false;
            dgvMaster.RowTemplate.DefaultCellStyle.Padding = new Padding(5, 0, 10, 0);
            foreach (DataGridViewColumn c in dgvMaster.Columns)
            {
                c.Resizable = DataGridViewTriState.False;
                c.ReadOnly = true;
            }
            dgvMaster.AllowUserToOrderColumns = true;
            dgvMaster.BackgroundColor = SystemColors.Control;
            dgvMaster.BorderStyle = BorderStyle.Fixed3D;
        }
    }
}
