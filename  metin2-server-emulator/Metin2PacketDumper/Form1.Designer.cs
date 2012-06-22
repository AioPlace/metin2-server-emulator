namespace Metin2PacketDumper
{
    partial class MainForm
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Liberare le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pshChk = new System.Windows.Forms.CheckBox();
            this.NetworkSettingsBox = new System.Windows.Forms.GroupBox();
            this.GamePortTxt = new System.Windows.Forms.TextBox();
            this.AuthPortTxt = new System.Windows.Forms.TextBox();
            this.ServerIPTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ServerListBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.XTEASettingsBox = new System.Windows.Forms.GroupBox();
            this.XTEAKey2Lbl = new System.Windows.Forms.Label();
            this.XTEAKey1Lbl = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.ProcessTxt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ListeninDeviceBox = new System.Windows.Forms.GroupBox();
            this.DeviceList = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ChooseDeviceBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CurrentDeviceLbl = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.PacketList = new System.Windows.Forms.ListBox();
            this.PacketDetailTxt = new System.Windows.Forms.TextBox();
            this.MainToolStrip = new System.Windows.Forms.ToolStrip();
            this.ListenButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SettingsButton = new System.Windows.Forms.ToolStripButton();
            this.SaveBtn = new System.Windows.Forms.ToolStripButton();
            this.ClearBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.FilterBox = new System.Windows.Forms.ToolStripTextBox();
            this.ActiveFilterBtn = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
            this.MainSplitContainer.Panel1.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.NetworkSettingsBox.SuspendLayout();
            this.XTEASettingsBox.SuspendLayout();
            this.ListeninDeviceBox.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.MainToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainSplitContainer
            // 
            this.MainSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.MainSplitContainer.IsSplitterFixed = true;
            this.MainSplitContainer.Location = new System.Drawing.Point(0, 25);
            this.MainSplitContainer.Name = "MainSplitContainer";
            this.MainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // MainSplitContainer.Panel1
            // 
            this.MainSplitContainer.Panel1.Controls.Add(this.groupBox1);
            this.MainSplitContainer.Panel1.Controls.Add(this.NetworkSettingsBox);
            this.MainSplitContainer.Panel1.Controls.Add(this.XTEASettingsBox);
            this.MainSplitContainer.Panel1.Controls.Add(this.ListeninDeviceBox);
            // 
            // MainSplitContainer.Panel2
            // 
            this.MainSplitContainer.Panel2.Controls.Add(this.splitContainer1);
            this.MainSplitContainer.Size = new System.Drawing.Size(929, 586);
            this.MainSplitContainer.SplitterDistance = 252;
            this.MainSplitContainer.SplitterWidth = 1;
            this.MainSplitContainer.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.pshChk);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(580, 114);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(335, 125);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Packets settings";
            // 
            // pshChk
            // 
            this.pshChk.AutoSize = true;
            this.pshChk.Checked = true;
            this.pshChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.pshChk.Location = new System.Drawing.Point(9, 20);
            this.pshChk.Name = "pshChk";
            this.pshChk.Size = new System.Drawing.Size(115, 19);
            this.pshChk.TabIndex = 0;
            this.pshChk.Text = "Ignore PSH: 0";
            this.pshChk.UseVisualStyleBackColor = true;
            // 
            // NetworkSettingsBox
            // 
            this.NetworkSettingsBox.Controls.Add(this.GamePortTxt);
            this.NetworkSettingsBox.Controls.Add(this.AuthPortTxt);
            this.NetworkSettingsBox.Controls.Add(this.ServerIPTxt);
            this.NetworkSettingsBox.Controls.Add(this.label2);
            this.NetworkSettingsBox.Controls.Add(this.ServerListBox);
            this.NetworkSettingsBox.Controls.Add(this.label1);
            this.NetworkSettingsBox.Controls.Add(this.label3);
            this.NetworkSettingsBox.Controls.Add(this.label4);
            this.NetworkSettingsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NetworkSettingsBox.Location = new System.Drawing.Point(14, 3);
            this.NetworkSettingsBox.Name = "NetworkSettingsBox";
            this.NetworkSettingsBox.Size = new System.Drawing.Size(366, 112);
            this.NetworkSettingsBox.TabIndex = 5;
            this.NetworkSettingsBox.TabStop = false;
            this.NetworkSettingsBox.Text = "Server";
            // 
            // GamePortTxt
            // 
            this.GamePortTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GamePortTxt.Location = new System.Drawing.Point(281, 79);
            this.GamePortTxt.Name = "GamePortTxt";
            this.GamePortTxt.Size = new System.Drawing.Size(76, 21);
            this.GamePortTxt.TabIndex = 4;
            this.GamePortTxt.Text = "Game Port";
            this.GamePortTxt.Enter += new System.EventHandler(this.GamePortTxt_Enter);
            // 
            // AuthPortTxt
            // 
            this.AuthPortTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AuthPortTxt.Location = new System.Drawing.Point(205, 79);
            this.AuthPortTxt.Name = "AuthPortTxt";
            this.AuthPortTxt.Size = new System.Drawing.Size(70, 21);
            this.AuthPortTxt.TabIndex = 3;
            this.AuthPortTxt.Text = "Auth Port";
            this.AuthPortTxt.Enter += new System.EventHandler(this.AuthPortTxt_Enter);
            // 
            // ServerIPTxt
            // 
            this.ServerIPTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServerIPTxt.Location = new System.Drawing.Point(9, 79);
            this.ServerIPTxt.Name = "ServerIPTxt";
            this.ServerIPTxt.Size = new System.Drawing.Size(190, 21);
            this.ServerIPTxt.TabIndex = 2;
            this.ServerIPTxt.Text = "Server IP";
            this.ServerIPTxt.Enter += new System.EventHandler(this.ServerIPTxt_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "...or manually enter one";
            // 
            // ServerListBox
            // 
            this.ServerListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ServerListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServerListBox.FormattingEnabled = true;
            this.ServerListBox.Location = new System.Drawing.Point(9, 35);
            this.ServerListBox.Name = "ServerListBox";
            this.ServerListBox.Size = new System.Drawing.Size(348, 23);
            this.ServerListBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pick a server from the list...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(198, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(10, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = ":";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(273, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "/";
            // 
            // XTEASettingsBox
            // 
            this.XTEASettingsBox.Controls.Add(this.XTEAKey2Lbl);
            this.XTEASettingsBox.Controls.Add(this.XTEAKey1Lbl);
            this.XTEASettingsBox.Controls.Add(this.button2);
            this.XTEASettingsBox.Controls.Add(this.label7);
            this.XTEASettingsBox.Controls.Add(this.button1);
            this.XTEASettingsBox.Controls.Add(this.ProcessTxt);
            this.XTEASettingsBox.Controls.Add(this.label6);
            this.XTEASettingsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.XTEASettingsBox.Location = new System.Drawing.Point(14, 114);
            this.XTEASettingsBox.Name = "XTEASettingsBox";
            this.XTEASettingsBox.Size = new System.Drawing.Size(366, 125);
            this.XTEASettingsBox.TabIndex = 7;
            this.XTEASettingsBox.TabStop = false;
            this.XTEASettingsBox.Text = "XTEA";
            // 
            // XTEAKey2Lbl
            // 
            this.XTEAKey2Lbl.AutoSize = true;
            this.XTEAKey2Lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.XTEAKey2Lbl.Location = new System.Drawing.Point(77, 84);
            this.XTEAKey2Lbl.Name = "XTEAKey2Lbl";
            this.XTEAKey2Lbl.Size = new System.Drawing.Size(279, 15);
            this.XTEAKey2Lbl.TabIndex = 6;
            this.XTEAKey2Lbl.Text = "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ";
            // 
            // XTEAKey1Lbl
            // 
            this.XTEAKey1Lbl.AutoSize = true;
            this.XTEAKey1Lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.XTEAKey1Lbl.Location = new System.Drawing.Point(77, 65);
            this.XTEAKey1Lbl.Name = "XTEAKey1Lbl";
            this.XTEAKey1Lbl.Size = new System.Drawing.Size(279, 15);
            this.XTEAKey1Lbl.TabIndex = 5;
            this.XTEAKey1Lbl.Text = "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(9, 84);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(61, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Clear";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(286, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 15);
            this.label7.TabIndex = 3;
            this.label7.Text = ".exe";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(9, 61);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(61, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Update";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // ProcessTxt
            // 
            this.ProcessTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcessTxt.Location = new System.Drawing.Point(9, 35);
            this.ProcessTxt.Name = "ProcessTxt";
            this.ProcessTxt.Size = new System.Drawing.Size(274, 21);
            this.ProcessTxt.TabIndex = 1;
            this.ProcessTxt.Text = "Launcher";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(255, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "From which process should I pick XTEA keys?";
            // 
            // ListeninDeviceBox
            // 
            this.ListeninDeviceBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ListeninDeviceBox.Controls.Add(this.DeviceList);
            this.ListeninDeviceBox.Controls.Add(this.label5);
            this.ListeninDeviceBox.Controls.Add(this.ChooseDeviceBtn);
            this.ListeninDeviceBox.Controls.Add(this.panel1);
            this.ListeninDeviceBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListeninDeviceBox.Location = new System.Drawing.Point(580, 3);
            this.ListeninDeviceBox.Name = "ListeninDeviceBox";
            this.ListeninDeviceBox.Size = new System.Drawing.Size(335, 112);
            this.ListeninDeviceBox.TabIndex = 6;
            this.ListeninDeviceBox.TabStop = false;
            this.ListeninDeviceBox.Text = "Listening device";
            // 
            // DeviceList
            // 
            this.DeviceList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeviceList.FormattingEnabled = true;
            this.DeviceList.ItemHeight = 15;
            this.DeviceList.Location = new System.Drawing.Point(9, 35);
            this.DeviceList.Name = "DeviceList";
            this.DeviceList.Size = new System.Drawing.Size(318, 49);
            this.DeviceList.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "Choose your listening device";
            // 
            // ChooseDeviceBtn
            // 
            this.ChooseDeviceBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChooseDeviceBtn.Location = new System.Drawing.Point(8, 80);
            this.ChooseDeviceBtn.Name = "ChooseDeviceBtn";
            this.ChooseDeviceBtn.Size = new System.Drawing.Size(75, 25);
            this.ChooseDeviceBtn.TabIndex = 9;
            this.ChooseDeviceBtn.Text = "Choose";
            this.ChooseDeviceBtn.UseVisualStyleBackColor = true;
            this.ChooseDeviceBtn.Click += new System.EventHandler(this.ChooseDeviceBtn_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.CurrentDeviceLbl);
            this.panel1.Location = new System.Drawing.Point(72, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(255, 66);
            this.panel1.TabIndex = 10;
            // 
            // CurrentDeviceLbl
            // 
            this.CurrentDeviceLbl.AutoSize = true;
            this.CurrentDeviceLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentDeviceLbl.Location = new System.Drawing.Point(8, 47);
            this.CurrentDeviceLbl.Name = "CurrentDeviceLbl";
            this.CurrentDeviceLbl.Size = new System.Drawing.Size(83, 15);
            this.CurrentDeviceLbl.TabIndex = 0;
            this.CurrentDeviceLbl.Text = "Current: None";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.PacketList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.PacketDetailTxt);
            this.splitContainer1.Size = new System.Drawing.Size(929, 333);
            this.splitContainer1.SplitterDistance = 168;
            this.splitContainer1.TabIndex = 0;
            // 
            // PacketList
            // 
            this.PacketList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PacketList.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PacketList.FormattingEnabled = true;
            this.PacketList.ItemHeight = 12;
            this.PacketList.Location = new System.Drawing.Point(0, 0);
            this.PacketList.Name = "PacketList";
            this.PacketList.Size = new System.Drawing.Size(929, 168);
            this.PacketList.TabIndex = 0;
            // 
            // PacketDetailTxt
            // 
            this.PacketDetailTxt.BackColor = System.Drawing.Color.White;
            this.PacketDetailTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PacketDetailTxt.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PacketDetailTxt.Location = new System.Drawing.Point(0, 0);
            this.PacketDetailTxt.Multiline = true;
            this.PacketDetailTxt.Name = "PacketDetailTxt";
            this.PacketDetailTxt.ReadOnly = true;
            this.PacketDetailTxt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.PacketDetailTxt.Size = new System.Drawing.Size(929, 161);
            this.PacketDetailTxt.TabIndex = 0;
            // 
            // MainToolStrip
            // 
            this.MainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.MainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ListenButton,
            this.toolStripSeparator1,
            this.SettingsButton,
            this.SaveBtn,
            this.ClearBtn,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.FilterBox,
            this.ActiveFilterBtn});
            this.MainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.MainToolStrip.Name = "MainToolStrip";
            this.MainToolStrip.Size = new System.Drawing.Size(935, 25);
            this.MainToolStrip.TabIndex = 6;
            this.MainToolStrip.Text = "toolStrip1";
            // 
            // ListenButton
            // 
            this.ListenButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ListenButton.Image = ((System.Drawing.Image)(resources.GetObject("ListenButton.Image")));
            this.ListenButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ListenButton.Name = "ListenButton";
            this.ListenButton.Size = new System.Drawing.Size(42, 22);
            this.ListenButton.Text = "Listen";
            this.ListenButton.Click += new System.EventHandler(this.ListenButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // SettingsButton
            // 
            this.SettingsButton.Checked = true;
            this.SettingsButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SettingsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.SettingsButton.Image = ((System.Drawing.Image)(resources.GetObject("SettingsButton.Image")));
            this.SettingsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.Size = new System.Drawing.Size(53, 22);
            this.SettingsButton.Text = "Settings";
            this.SettingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.SaveBtn.Image = ((System.Drawing.Image)(resources.GetObject("SaveBtn.Image")));
            this.SaveBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(35, 22);
            this.SaveBtn.Text = "Save";
            // 
            // ClearBtn
            // 
            this.ClearBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ClearBtn.Image = ((System.Drawing.Image)(resources.GetObject("ClearBtn.Image")));
            this.ClearBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ClearBtn.Name = "ClearBtn";
            this.ClearBtn.Size = new System.Drawing.Size(38, 22);
            this.ClearBtn.Text = "Clear";
            this.ClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(66, 22);
            this.toolStripLabel1.Text = "Filter by ID:";
            // 
            // FilterBox
            // 
            this.FilterBox.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilterBox.MaxLength = 2;
            this.FilterBox.Name = "FilterBox";
            this.FilterBox.Size = new System.Drawing.Size(20, 25);
            this.FilterBox.Text = "FF";
            // 
            // ActiveFilterBtn
            // 
            this.ActiveFilterBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ActiveFilterBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ActiveFilterBtn.Name = "ActiveFilterBtn";
            this.ActiveFilterBtn.Size = new System.Drawing.Size(44, 22);
            this.ActiveFilterBtn.Text = "Active";
            this.ActiveFilterBtn.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 620);
            this.Controls.Add(this.MainToolStrip);
            this.Controls.Add(this.MainSplitContainer);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(751, 650);
            this.Name = "MainForm";
            this.Text = "Metin2 Packet Dumper (New)";
            this.MainSplitContainer.Panel1.ResumeLayout(false);
            this.MainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).EndInit();
            this.MainSplitContainer.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.NetworkSettingsBox.ResumeLayout(false);
            this.NetworkSettingsBox.PerformLayout();
            this.XTEASettingsBox.ResumeLayout(false);
            this.XTEASettingsBox.PerformLayout();
            this.ListeninDeviceBox.ResumeLayout(false);
            this.ListeninDeviceBox.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.MainToolStrip.ResumeLayout(false);
            this.MainToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer MainSplitContainer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox XTEASettingsBox;
        private System.Windows.Forms.Label XTEAKey2Lbl;
        private System.Windows.Forms.Label XTEAKey1Lbl;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox ProcessTxt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox ListeninDeviceBox;
        private System.Windows.Forms.ListBox DeviceList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button ChooseDeviceBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label CurrentDeviceLbl;
        private System.Windows.Forms.GroupBox NetworkSettingsBox;
        private System.Windows.Forms.TextBox GamePortTxt;
        private System.Windows.Forms.TextBox AuthPortTxt;
        private System.Windows.Forms.TextBox ServerIPTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ServerListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox PacketList;
        private System.Windows.Forms.TextBox PacketDetailTxt;
        private System.Windows.Forms.ToolStrip MainToolStrip;
        private System.Windows.Forms.ToolStripButton ListenButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton SettingsButton;
        private System.Windows.Forms.ToolStripButton SaveBtn;
        private System.Windows.Forms.CheckBox pshChk;
        private System.Windows.Forms.ToolStripButton ClearBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox FilterBox;
        private System.Windows.Forms.ToolStripButton ActiveFilterBtn;

    }
}

