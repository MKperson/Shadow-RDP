
namespace Shadow_RDP
{
    partial class frmShadowRdp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShadowRdp));
            this.lstRdps = new System.Windows.Forms.ListView();
            this.colhCompName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colhUserName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colhSessionName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colhID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colhState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colhIdleTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colhLogonTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtWorkStation = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.lblWorkstation = new System.Windows.Forms.Label();
            this.tabShadowControl = new System.Windows.Forms.TabControl();
            this.tabVDI = new System.Windows.Forms.TabPage();
            this.cbHide = new System.Windows.Forms.CheckBox();
            this.btnRefreshList = new System.Windows.Forms.Button();
            this.tabQuery = new System.Windows.Forms.TabPage();
            this.btnForceRestart = new System.Windows.Forms.Button();
            this.btnClearList = new System.Windows.Forms.Button();
            this.lvQueryList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddtoList = new System.Windows.Forms.Button();
            this.txtQueryBox = new System.Windows.Forms.TextBox();
            this.tabStatus = new System.Windows.Forms.TabPage();
            this.lvVdiStatus = new System.Windows.Forms.ListView();
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabShadowControl.SuspendLayout();
            this.tabVDI.SuspendLayout();
            this.tabQuery.SuspendLayout();
            this.tabStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstRdps
            // 
            this.lstRdps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstRdps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colhCompName,
            this.colhUserName,
            this.colhSessionName,
            this.colhID,
            this.colhState,
            this.colhIdleTime,
            this.colhLogonTime});
            this.lstRdps.FullRowSelect = true;
            this.lstRdps.HideSelection = false;
            this.lstRdps.Location = new System.Drawing.Point(6, 6);
            this.lstRdps.MultiSelect = false;
            this.lstRdps.Name = "lstRdps";
            this.lstRdps.Size = new System.Drawing.Size(488, 492);
            this.lstRdps.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstRdps.TabIndex = 0;
            this.lstRdps.UseCompatibleStateImageBehavior = false;
            this.lstRdps.View = System.Windows.Forms.View.Details;
            this.lstRdps.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.colSort);
            this.lstRdps.ItemActivate += new System.EventHandler(this.lstRdps_ItemActivate);
            // 
            // colhCompName
            // 
            this.colhCompName.Text = "ComputerName";
            this.colhCompName.Width = 100;
            // 
            // colhUserName
            // 
            this.colhUserName.Text = "UserName";
            this.colhUserName.Width = 65;
            // 
            // colhSessionName
            // 
            this.colhSessionName.Text = "SessionName";
            this.colhSessionName.Width = 85;
            // 
            // colhID
            // 
            this.colhID.Text = "ID";
            this.colhID.Width = 25;
            // 
            // colhState
            // 
            this.colhState.Text = "State";
            this.colhState.Width = 45;
            // 
            // colhIdleTime
            // 
            this.colhIdleTime.Text = "Idle Time";
            this.colhIdleTime.Width = 55;
            // 
            // colhLogonTime
            // 
            this.colhLogonTime.Text = "Logon Time";
            this.colhLogonTime.Width = 100;
            // 
            // txtWorkStation
            // 
            this.txtWorkStation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtWorkStation.Location = new System.Drawing.Point(6, 519);
            this.txtWorkStation.Name = "txtWorkStation";
            this.txtWorkStation.Size = new System.Drawing.Size(128, 20);
            this.txtWorkStation.TabIndex = 2;
            this.txtWorkStation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWorkStation_KeyDown);
            // 
            // connectButton
            // 
            this.connectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.connectButton.Location = new System.Drawing.Point(140, 517);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 3;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // lblWorkstation
            // 
            this.lblWorkstation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblWorkstation.AutoSize = true;
            this.lblWorkstation.Location = new System.Drawing.Point(7, 502);
            this.lblWorkstation.Name = "lblWorkstation";
            this.lblWorkstation.Size = new System.Drawing.Size(69, 13);
            this.lblWorkstation.TabIndex = 4;
            this.lblWorkstation.Text = "Work Station";
            // 
            // tabShadowControl
            // 
            this.tabShadowControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabShadowControl.Controls.Add(this.tabVDI);
            this.tabShadowControl.Controls.Add(this.tabQuery);
            this.tabShadowControl.Controls.Add(this.tabStatus);
            this.tabShadowControl.Location = new System.Drawing.Point(12, 12);
            this.tabShadowControl.Name = "tabShadowControl";
            this.tabShadowControl.SelectedIndex = 0;
            this.tabShadowControl.Size = new System.Drawing.Size(508, 571);
            this.tabShadowControl.TabIndex = 5;
            this.tabShadowControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabShadowControl_Selected);
            // 
            // tabVDI
            // 
            this.tabVDI.BackColor = System.Drawing.Color.DarkGray;
            this.tabVDI.Controls.Add(this.cbHide);
            this.tabVDI.Controls.Add(this.btnRefreshList);
            this.tabVDI.Controls.Add(this.lstRdps);
            this.tabVDI.Controls.Add(this.lblWorkstation);
            this.tabVDI.Controls.Add(this.txtWorkStation);
            this.tabVDI.Controls.Add(this.connectButton);
            this.tabVDI.Location = new System.Drawing.Point(4, 22);
            this.tabVDI.Name = "tabVDI";
            this.tabVDI.Padding = new System.Windows.Forms.Padding(3);
            this.tabVDI.Size = new System.Drawing.Size(500, 545);
            this.tabVDI.TabIndex = 0;
            this.tabVDI.Text = "VDI Stations";
            // 
            // cbHide
            // 
            this.cbHide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbHide.AutoSize = true;
            this.cbHide.Checked = true;
            this.cbHide.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbHide.Location = new System.Drawing.Point(372, 521);
            this.cbHide.Name = "cbHide";
            this.cbHide.Size = new System.Drawing.Size(117, 17);
            this.cbHide.TabIndex = 11;
            this.cbHide.Text = "Hide Disconnected";
            this.cbHide.UseVisualStyleBackColor = true;
            // 
            // btnRefreshList
            // 
            this.btnRefreshList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefreshList.Location = new System.Drawing.Point(221, 517);
            this.btnRefreshList.Name = "btnRefreshList";
            this.btnRefreshList.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshList.TabIndex = 10;
            this.btnRefreshList.Text = "Refresh List";
            this.btnRefreshList.UseVisualStyleBackColor = true;
            this.btnRefreshList.Click += new System.EventHandler(this.btnRefreshList_Click);
            // 
            // tabQuery
            // 
            this.tabQuery.BackColor = System.Drawing.Color.DarkGray;
            this.tabQuery.Controls.Add(this.btnForceRestart);
            this.tabQuery.Controls.Add(this.btnClearList);
            this.tabQuery.Controls.Add(this.lvQueryList);
            this.tabQuery.Controls.Add(this.label1);
            this.tabQuery.Controls.Add(this.btnAddtoList);
            this.tabQuery.Controls.Add(this.txtQueryBox);
            this.tabQuery.Location = new System.Drawing.Point(4, 22);
            this.tabQuery.Name = "tabQuery";
            this.tabQuery.Padding = new System.Windows.Forms.Padding(3);
            this.tabQuery.Size = new System.Drawing.Size(500, 545);
            this.tabQuery.TabIndex = 1;
            this.tabQuery.Text = "Query Table";
            // 
            // btnForceRestart
            // 
            this.btnForceRestart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnForceRestart.Location = new System.Drawing.Point(412, 24);
            this.btnForceRestart.Name = "btnForceRestart";
            this.btnForceRestart.Size = new System.Drawing.Size(82, 23);
            this.btnForceRestart.TabIndex = 10;
            this.btnForceRestart.Text = "Force Restart";
            this.btnForceRestart.UseVisualStyleBackColor = true;
            this.btnForceRestart.Click += new System.EventHandler(this.btnForceRestart_Click);
            // 
            // btnClearList
            // 
            this.btnClearList.Location = new System.Drawing.Point(221, 24);
            this.btnClearList.Name = "btnClearList";
            this.btnClearList.Size = new System.Drawing.Size(75, 23);
            this.btnClearList.TabIndex = 9;
            this.btnClearList.Text = "Clear List";
            this.btnClearList.UseVisualStyleBackColor = true;
            this.btnClearList.Click += new System.EventHandler(this.btnClearList_Click);
            // 
            // lvQueryList
            // 
            this.lvQueryList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvQueryList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.lvQueryList.FullRowSelect = true;
            this.lvQueryList.HideSelection = false;
            this.lvQueryList.Location = new System.Drawing.Point(6, 52);
            this.lvQueryList.MultiSelect = false;
            this.lvQueryList.Name = "lvQueryList";
            this.lvQueryList.Size = new System.Drawing.Size(488, 487);
            this.lvQueryList.TabIndex = 8;
            this.lvQueryList.UseCompatibleStateImageBehavior = false;
            this.lvQueryList.View = System.Windows.Forms.View.Details;
            this.lvQueryList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.colSort);
            this.lvQueryList.ItemActivate += new System.EventHandler(this.lvQueryList_ItemActivate);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ComputerName";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "UserName";
            this.columnHeader2.Width = 65;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "SessionName";
            this.columnHeader3.Width = 85;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "ID";
            this.columnHeader4.Width = 25;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "State";
            this.columnHeader5.Width = 45;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Idle Time";
            this.columnHeader6.Width = 55;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Logon Time";
            this.columnHeader7.Width = 100;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Query Work Station";
            // 
            // btnAddtoList
            // 
            this.btnAddtoList.Location = new System.Drawing.Point(140, 24);
            this.btnAddtoList.Name = "btnAddtoList";
            this.btnAddtoList.Size = new System.Drawing.Size(75, 23);
            this.btnAddtoList.TabIndex = 6;
            this.btnAddtoList.Text = "Add To List";
            this.btnAddtoList.UseVisualStyleBackColor = true;
            this.btnAddtoList.Click += new System.EventHandler(this.btnAddtoList_Click);
            // 
            // txtQueryBox
            // 
            this.txtQueryBox.Location = new System.Drawing.Point(6, 26);
            this.txtQueryBox.Name = "txtQueryBox";
            this.txtQueryBox.Size = new System.Drawing.Size(128, 20);
            this.txtQueryBox.TabIndex = 5;
            this.txtQueryBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQueryBox_KeyDown);
            // 
            // tabStatus
            // 
            this.tabStatus.BackColor = System.Drawing.Color.DarkGray;
            this.tabStatus.Controls.Add(this.lvVdiStatus);
            this.tabStatus.Location = new System.Drawing.Point(4, 22);
            this.tabStatus.Name = "tabStatus";
            this.tabStatus.Size = new System.Drawing.Size(500, 545);
            this.tabStatus.TabIndex = 2;
            this.tabStatus.Text = "VDI Status";
            // 
            // lvVdiStatus
            // 
            this.lvVdiStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvVdiStatus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader11,
            this.columnHeader9,
            this.columnHeader12,
            this.columnHeader13});
            this.lvVdiStatus.FullRowSelect = true;
            this.lvVdiStatus.HideSelection = false;
            this.lvVdiStatus.Location = new System.Drawing.Point(3, 3);
            this.lvVdiStatus.MultiSelect = false;
            this.lvVdiStatus.Name = "lvVdiStatus";
            this.lvVdiStatus.Size = new System.Drawing.Size(494, 539);
            this.lvVdiStatus.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvVdiStatus.TabIndex = 1;
            this.lvVdiStatus.UseCompatibleStateImageBehavior = false;
            this.lvVdiStatus.View = System.Windows.Forms.View.Details;
            this.lvVdiStatus.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.colSort);
            this.lvVdiStatus.ItemActivate += new System.EventHandler(this.lvVdiStatus_ItemActivate);
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "ComputerName";
            this.columnHeader8.Width = 100;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "VMID";
            this.columnHeader11.Width = 84;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "NodeId";
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Status";
            this.columnHeader12.Width = 45;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Uptime";
            this.columnHeader13.Width = 146;
            // 
            // frmShadowRdp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(532, 595);
            this.Controls.Add(this.tabShadowControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmShadowRdp";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Agent Shadow RDP";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmShadowRdp_FormClosing);
            this.Load += new System.EventHandler(this.frmShadowRdp_Load);
            this.ResizeEnd += new System.EventHandler(this.frmShadowRdp_ResizeEnd);
            this.Resize += new System.EventHandler(this.frmShadowRdp_Resize);
            this.tabShadowControl.ResumeLayout(false);
            this.tabVDI.ResumeLayout(false);
            this.tabVDI.PerformLayout();
            this.tabQuery.ResumeLayout(false);
            this.tabQuery.PerformLayout();
            this.tabStatus.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstRdps;
        private System.Windows.Forms.ColumnHeader colhUserName;
        private System.Windows.Forms.ColumnHeader colhSessionName;
        private System.Windows.Forms.ColumnHeader colhID;
        private System.Windows.Forms.ColumnHeader colhState;
        private System.Windows.Forms.TextBox txtWorkStation;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Label lblWorkstation;
        private System.Windows.Forms.ColumnHeader colhIdleTime;
        private System.Windows.Forms.ColumnHeader colhLogonTime;
        private System.Windows.Forms.ColumnHeader colhCompName;
        private System.Windows.Forms.TabControl tabShadowControl;
        private System.Windows.Forms.TabPage tabVDI;
        private System.Windows.Forms.TabPage tabQuery;
        private System.Windows.Forms.ListView lvQueryList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddtoList;
        private System.Windows.Forms.TextBox txtQueryBox;
        private System.Windows.Forms.Button btnClearList;
        private System.Windows.Forms.Button btnRefreshList;
        private System.Windows.Forms.CheckBox cbHide;
        private System.Windows.Forms.Button btnForceRestart;
        private System.Windows.Forms.TabPage tabStatus;
        private System.Windows.Forms.ListView lvVdiStatus;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader9;
    }
}

