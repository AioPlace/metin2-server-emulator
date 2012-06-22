#region License

//     This file is part of Metin 2 Server Emulator.
// 
//     Metin 2 Server Emulator is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     Metin 2 Server Emulator is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with Metin 2 Server Emulator.  If not, see <http://www.gnu.org/licenses/>

#endregion

using System;
using System.Drawing;
using System.Windows.Forms;
using Metin2PacketDumper.Parsing;
using Metin2ServerEmulatorCommon.Util;
using PacketDotNet;
using SharpPcap;

namespace Metin2PacketDumper
{
    public partial class MainForm : Form
    {
        private readonly CaptureDeviceList _devices = CaptureDeviceList.Instance;
        private ushort _authPort;
        private ICaptureDevice _currentDevice;
        private byte _filterID;
        private ushort _gamePort;
        private bool _isFiltering = false;

        private MetinPacket _mergingPacket;
        private bool _needToMerge = false;

        public MainForm()
        {
            InitializeComponent();
            FormClosing += MainForm_FormClosing;
            PacketList.SelectedIndexChanged += PacketList_SelectedIndexChanged;

            CheckForIllegalCrossThreadCalls = false;
            foreach (var dev in _devices)
            {
                DeviceList.Items.Add(dev.Description);
            }
            if (DeviceList.Items.Count > 0)
                DeviceList.SelectedIndex = 0;
        }

        private void PacketList_SelectedIndexChanged(object sender, EventArgs e)
        {
            PacketDetailTxt.Clear();
            if (PacketList.SelectedItem == null)
                return;

            MetinPacket p = (MetinPacket) PacketList.SelectedItem;

            if (p.Merged)
                PacketDetailTxt.AppendText("Merged Packet\n");

            PacketDetailTxt.AppendText("ID:\t\t\t0x" + p.Id.ToString("X2") + "\n");
            PacketDetailTxt.AppendText("ID:\t\t\t0x" + p.Id + "\n");
            PacketDetailTxt.AppendText("PSH:\t\t\t" + p.PSH + "\n");
            PacketDetailTxt.AppendText("Length:\t\t\t" + p.Length + "\n");
            PacketDetailTxt.AppendText("Route:\t\t\t" + p.Route + "\n");
            PacketDetailTxt.AppendText("Crypted:\t\t" + p.Crypted + "\r\n\r\n");
            PacketDetailTxt.AppendText("Data:\t\t\t" + ByteSupport.ByteArrayToHexString(p.Data, true) + "\r\n\r\n");
            if (p.Crypted)
                PacketDetailTxt.AppendText("Decrypted Data:\t\t" +
                                           ByteSupport.ByteArrayToHexString(p.DecryptedData, true) + "\r\n\r\n");

            PacketDetailTxt.AppendText("ASCII Data:\t\t" + ByteSupport.ByteArrayToAsciiString(p.Data) + "\r\n\r\n");

            if (p.Crypted)
                PacketDetailTxt.AppendText("ASCII Decrypted Data:\t" +
                                           ByteSupport.ByteArrayToAsciiString(p.DecryptedData) + "\r\n\r\n");
        }

        private void MainForm_FormClosing(object sender, EventArgs e)
        {
            Stop();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            SettingsButton.Checked = !SettingsButton.Checked;
            MainSplitContainer.Panel1Collapsed = !SettingsButton.Checked;
        }

        private void ListenButton_Click(object sender, EventArgs e)
        {
            if (_currentDevice == null)
                return;

            ListenButton.Checked = !ListenButton.Checked;

            if (ListenButton.Checked)
                Start();
            else
                Stop();
        }

        private void Start()
        {
            _authPort = Convert.ToUInt16(AuthPortTxt.Text);
            _gamePort = Convert.ToUInt16(GamePortTxt.Text);
            _currentDevice.Open();
            _currentDevice.Filter = string.Format("(dst host {0} || src host {0}) && ip proto \\tcp", ServerIPTxt.Text);
            _currentDevice.OnPacketArrival += CurrentDevice_OnPacketArrival;
            _currentDevice.StartCapture();
        }

        private void Stop()
        {
            _currentDevice.StopCapture();
            _currentDevice.Close();
            _currentDevice.OnPacketArrival -= CurrentDevice_OnPacketArrival;
        }

        private void CurrentDevice_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            MetinPacket p =
                new MetinPacket(TcpPacket.GetEncapsulated(Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data)),
                                _authPort, _gamePort, ProcessTxt.Text);


            if (_needToMerge)
            {
                if (p.Length > 0 && (p.SourcePort == _mergingPacket.SourcePort))
                    _mergingPacket.MergePacket(p);
                else
                {
                    if (!(!p.PSH && pshChk.Checked))
                        PacketList.Items.Add(p);
                    return;
                }

                if (p.Length != 1452)
                    _needToMerge = false;

                return;
            }

            if (p.Length == 1452)
            {
                _needToMerge = true;
                _mergingPacket = p;
                return;
            }

            if (_mergingPacket != null && p.Length != 0)
            {
                PacketList.Items.Add(_mergingPacket);
                _mergingPacket = null;
            }

            if (!(!p.PSH && pshChk.Checked))
                PacketList.Items.Add(p);
        }


        private void ServerIPTxt_Enter(object sender, EventArgs e)
        {
            if (ServerIPTxt.Text == "Server IP")
            {
                ServerIPTxt.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
                ServerIPTxt.Clear();
            }
        }

        private void GamePortTxt_Enter(object sender, EventArgs e)
        {
            if (GamePortTxt.Text == "Game Port")
            {
                GamePortTxt.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
                GamePortTxt.Clear();
            }
        }

        private void AuthPortTxt_Enter(object sender, EventArgs e)
        {
            if (AuthPortTxt.Text == "Auth Port")
            {
                AuthPortTxt.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
                AuthPortTxt.Clear();
            }
        }

        private void ChooseDeviceBtn_Click(object sender, EventArgs e)
        {
            _currentDevice = _devices[DeviceList.SelectedIndex];
// ReSharper disable LocalizableElement
            CurrentDeviceLbl.Text = "Current: " + _currentDevice.Description;
// ReSharper restore LocalizableElement
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            PacketList.Items.Clear();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ActiveFilterBtn.Checked = !ActiveFilterBtn.Checked;

            _filterID = ByteSupport.HexStringToByteArray(FilterBox.Text)[0];
            _isFiltering = !_isFiltering;
        }
    }
}