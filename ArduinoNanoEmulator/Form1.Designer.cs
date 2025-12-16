namespace ArduinoNanoEmulator;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        grpConnection = new GroupBox();
        btnDisconnect = new Button();
        btnConnect = new Button();
        cmbSerialPorts = new ComboBox();
        lblSerialPort = new Label();
        grpBuzzers = new GroupBox();
        numRank = new NumericUpDown();
        lblRank = new Label();
        grpTeam2 = new GroupBox();
        btnTeam2ModeB = new Button();
        btnTeam2ModeA = new Button();
        grpTeam1 = new GroupBox();
        btnTeam1ModeB = new Button();
        btnTeam1ModeA = new Button();
        grpHost = new GroupBox();
        lblHostInfo = new Label();
        btnHostPress = new Button();
        txtStatus = new TextBox();
        grpConnection.SuspendLayout();
        grpBuzzers.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numRank).BeginInit();
        grpTeam2.SuspendLayout();
        grpTeam1.SuspendLayout();
        grpHost.SuspendLayout();
        SuspendLayout();
        // 
        // grpConnection
        // 
        grpConnection.Controls.Add(btnDisconnect);
        grpConnection.Controls.Add(btnConnect);
        grpConnection.Controls.Add(cmbSerialPorts);
        grpConnection.Controls.Add(lblSerialPort);
        grpConnection.Location = new Point(12, 12);
        grpConnection.Name = "grpConnection";
        grpConnection.Size = new Size(760, 80);
        grpConnection.TabIndex = 0;
        grpConnection.TabStop = false;
        grpConnection.Text = "Seri Port Bağlantısı";
        // 
        // btnDisconnect
        // 
        btnDisconnect.Enabled = false;
        btnDisconnect.Location = new Point(420, 30);
        btnDisconnect.Name = "btnDisconnect";
        btnDisconnect.Size = new Size(120, 30);
        btnDisconnect.TabIndex = 3;
        btnDisconnect.Text = "Bağlantıyı Kes";
        btnDisconnect.UseVisualStyleBackColor = true;
        btnDisconnect.Click += BtnDisconnect_Click;
        // 
        // btnConnect
        // 
        btnConnect.Location = new Point(294, 30);
        btnConnect.Name = "btnConnect";
        btnConnect.Size = new Size(120, 30);
        btnConnect.TabIndex = 2;
        btnConnect.Text = "Bağlan";
        btnConnect.UseVisualStyleBackColor = true;
        btnConnect.Click += BtnConnect_Click;
        // 
        // cmbSerialPorts
        // 
        cmbSerialPorts.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbSerialPorts.FormattingEnabled = true;
        cmbSerialPorts.Location = new Point(100, 32);
        cmbSerialPorts.Name = "cmbSerialPorts";
        cmbSerialPorts.Size = new Size(180, 28);
        cmbSerialPorts.TabIndex = 1;
        // 
        // lblSerialPort
        // 
        lblSerialPort.AutoSize = true;
        lblSerialPort.Location = new Point(20, 35);
        lblSerialPort.Name = "lblSerialPort";
        lblSerialPort.Size = new Size(74, 20);
        lblSerialPort.TabIndex = 0;
        lblSerialPort.Text = "Seri Port:";
        // 
        // grpBuzzers
        // 
        grpBuzzers.Controls.Add(numRank);
        grpBuzzers.Controls.Add(lblRank);
        grpBuzzers.Controls.Add(grpTeam2);
        grpBuzzers.Controls.Add(grpTeam1);
        grpBuzzers.Enabled = false;
        grpBuzzers.Location = new Point(12, 98);
        grpBuzzers.Name = "grpBuzzers";
        grpBuzzers.Size = new Size(760, 220);
        grpBuzzers.TabIndex = 1;
        grpBuzzers.TabStop = false;
        grpBuzzers.Text = "Quiz Buzzerlar";
        // 
        // numRank
        // 
        numRank.Location = new Point(100, 30);
        numRank.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
        numRank.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        numRank.Name = "numRank";
        numRank.Size = new Size(80, 27);
        numRank.TabIndex = 3;
        numRank.Value = new decimal(new int[] { 4, 0, 0, 0 });
        // 
        // lblRank
        // 
        lblRank.AutoSize = true;
        lblRank.Location = new Point(20, 32);
        lblRank.Name = "lblRank";
        lblRank.Size = new Size(47, 20);
        lblRank.TabIndex = 2;
        lblRank.Text = "Rank:";
        // 
        // grpTeam2
        // 
        grpTeam2.Controls.Add(btnTeam2ModeB);
        grpTeam2.Controls.Add(btnTeam2ModeA);
        grpTeam2.Location = new Point(390, 70);
        grpTeam2.Name = "grpTeam2";
        grpTeam2.Size = new Size(350, 130);
        grpTeam2.TabIndex = 1;
        grpTeam2.TabStop = false;
        grpTeam2.Text = "Takım 2";
        // 
        // btnTeam2ModeB
        // 
        btnTeam2ModeB.BackColor = Color.LightCoral;
        btnTeam2ModeB.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        btnTeam2ModeB.Location = new Point(180, 40);
        btnTeam2ModeB.Name = "btnTeam2ModeB";
        btnTeam2ModeB.Size = new Size(150, 70);
        btnTeam2ModeB.TabIndex = 1;
        btnTeam2ModeB.Text = "Mod B";
        btnTeam2ModeB.UseVisualStyleBackColor = false;
        btnTeam2ModeB.Click += BtnTeam2ModeB_Click;
        // 
        // btnTeam2ModeA
        // 
        btnTeam2ModeA.BackColor = Color.LightCoral;
        btnTeam2ModeA.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        btnTeam2ModeA.Location = new Point(20, 40);
        btnTeam2ModeA.Name = "btnTeam2ModeA";
        btnTeam2ModeA.Size = new Size(150, 70);
        btnTeam2ModeA.TabIndex = 0;
        btnTeam2ModeA.Text = "Mod A";
        btnTeam2ModeA.UseVisualStyleBackColor = false;
        btnTeam2ModeA.Click += BtnTeam2ModeA_Click;
        // 
        // grpTeam1
        // 
        grpTeam1.Controls.Add(btnTeam1ModeB);
        grpTeam1.Controls.Add(btnTeam1ModeA);
        grpTeam1.Location = new Point(20, 70);
        grpTeam1.Name = "grpTeam1";
        grpTeam1.Size = new Size(350, 130);
        grpTeam1.TabIndex = 0;
        grpTeam1.TabStop = false;
        grpTeam1.Text = "Takım 1";
        // 
        // btnTeam1ModeB
        // 
        btnTeam1ModeB.BackColor = Color.LightBlue;
        btnTeam1ModeB.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        btnTeam1ModeB.Location = new Point(180, 40);
        btnTeam1ModeB.Name = "btnTeam1ModeB";
        btnTeam1ModeB.Size = new Size(150, 70);
        btnTeam1ModeB.TabIndex = 1;
        btnTeam1ModeB.Text = "Mod B";
        btnTeam1ModeB.UseVisualStyleBackColor = false;
        btnTeam1ModeB.Click += BtnTeam1ModeB_Click;
        // 
        // btnTeam1ModeA
        // 
        btnTeam1ModeA.BackColor = Color.LightBlue;
        btnTeam1ModeA.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        btnTeam1ModeA.Location = new Point(20, 40);
        btnTeam1ModeA.Name = "btnTeam1ModeA";
        btnTeam1ModeA.Size = new Size(150, 70);
        btnTeam1ModeA.TabIndex = 0;
        btnTeam1ModeA.Text = "Mod A";
        btnTeam1ModeA.UseVisualStyleBackColor = false;
        btnTeam1ModeA.Click += BtnTeam1ModeA_Click;
        // 
        // grpHost
        // 
        grpHost.Controls.Add(lblHostInfo);
        grpHost.Controls.Add(btnHostPress);
        grpHost.Enabled = false;
        grpHost.Location = new Point(12, 324);
        grpHost.Name = "grpHost";
        grpHost.Size = new Size(760, 120);
        grpHost.TabIndex = 2;
        grpHost.TabStop = false;
        grpHost.Text = "Host Butonu";
        // 
        // lblHostInfo
        // 
        lblHostInfo.AutoSize = true;
        lblHostInfo.Location = new Point(350, 50);
        lblHostInfo.Name = "lblHostInfo";
        lblHostInfo.Size = new Size(385, 20);
        lblHostInfo.TabIndex = 1;
        lblHostInfo.Text = "Butonu basılı tutun (>1000ms = uzun basma, <1000ms = kısa)";
        // 
        // btnHostPress
        // 
        btnHostPress.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        btnHostPress.Location = new Point(20, 30);
        btnHostPress.Name = "btnHostPress";
        btnHostPress.Size = new Size(300, 70);
        btnHostPress.TabIndex = 0;
        btnHostPress.Text = "HOST BUTON";
        btnHostPress.UseVisualStyleBackColor = true;
        btnHostPress.MouseDown += BtnHostPress_MouseDown;
        btnHostPress.MouseUp += BtnHostPress_MouseUp;
        // 
        // txtStatus
        // 
        txtStatus.BackColor = SystemColors.Info;
        txtStatus.Location = new Point(12, 450);
        txtStatus.Multiline = true;
        txtStatus.Name = "txtStatus";
        txtStatus.ReadOnly = true;
        txtStatus.Size = new Size(760, 60);
        txtStatus.TabIndex = 3;
        txtStatus.Text = "Bağlantı bekleniyor...";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(784, 521);
        Controls.Add(txtStatus);
        Controls.Add(grpHost);
        Controls.Add(grpBuzzers);
        Controls.Add(grpConnection);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Arduino Nano Emulator - Quiz Buzzer";
        grpConnection.ResumeLayout(false);
        grpConnection.PerformLayout();
        grpBuzzers.ResumeLayout(false);
        grpBuzzers.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numRank).EndInit();
        grpTeam2.ResumeLayout(false);
        grpTeam1.ResumeLayout(false);
        grpHost.ResumeLayout(false);
        grpHost.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private GroupBox grpConnection;
    private Label lblSerialPort;
    private ComboBox cmbSerialPorts;
    private Button btnConnect;
    private Button btnDisconnect;
    private GroupBox grpBuzzers;
    private GroupBox grpTeam1;
    private Button btnTeam1ModeA;
    private Button btnTeam1ModeB;
    private GroupBox grpTeam2;
    private Button btnTeam2ModeB;
    private Button btnTeam2ModeA;
    private Label lblRank;
    private NumericUpDown numRank;
    private GroupBox grpHost;
    private Button btnHostPress;
    private Label lblHostInfo;
    private TextBox txtStatus;
}
