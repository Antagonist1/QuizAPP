using System.IO.Ports;

namespace ArduinoNanoEmulator;

public partial class Form1 : Form
{
    private SerialPort? _serialPort;
    private System.Windows.Forms.Timer? _hostPressTimer;
    private DateTime _hostPressStartTime;
    private bool _isHostPressed = false;

    public Form1()
    {
        InitializeComponent();
        LoadSerialPorts();
        
        // Initialize host press timer
        _hostPressTimer = new System.Windows.Forms.Timer();
        _hostPressTimer.Interval = 100;
        _hostPressTimer.Tick += HostPressTimer_Tick;
    }

    private void LoadSerialPorts()
    {
        cmbSerialPorts.Items.Clear();
        string[] ports = SerialPort.GetPortNames();
        foreach (string port in ports)
        {
            cmbSerialPorts.Items.Add(port);
        }
        if (cmbSerialPorts.Items.Count > 0)
        {
            cmbSerialPorts.SelectedIndex = 0;
        }
    }

    private void BtnConnect_Click(object? sender, EventArgs e)
    {
        if (cmbSerialPorts.SelectedItem == null)
        {
            MessageBox.Show("Lütfen bir seri port seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            _serialPort = new SerialPort
            {
                PortName = cmbSerialPorts.SelectedItem.ToString() ?? "COM1",
                BaudRate = 9600,
                DataBits = 8,
                Parity = Parity.None,
                StopBits = StopBits.One
            };

            _serialPort.Open();

            btnConnect.Enabled = false;
            btnDisconnect.Enabled = true;
            cmbSerialPorts.Enabled = false;
            grpBuzzers.Enabled = true;
            grpHost.Enabled = true;
            txtStatus.Text = $"Bağlandı: {_serialPort.PortName}";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Bağlantı hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnDisconnect_Click(object? sender, EventArgs e)
    {
        DisconnectSerialPort();
    }

    private void DisconnectSerialPort()
    {
        if (_serialPort != null && _serialPort.IsOpen)
        {
            _serialPort.Close();
            _serialPort.Dispose();
            _serialPort = null;
        }

        btnConnect.Enabled = true;
        btnDisconnect.Enabled = false;
        cmbSerialPorts.Enabled = true;
        grpBuzzers.Enabled = false;
        grpHost.Enabled = false;
        txtStatus.Text = "Bağlantı kesildi";
    }

    private void SendBuzzerEvent(int team, string mode, int rank)
    {
        if (_serialPort != null && _serialPort.IsOpen)
        {
            string message = $"BUZZER:{team}:{mode}:{rank}\n";
            _serialPort.WriteLine(message);
            txtStatus.Text = $"Gönderildi: {message.Trim()}";
        }
    }

    private void BtnTeam1ModeA_Click(object? sender, EventArgs e)
    {
        int rank = (int)numRank.Value;
        SendBuzzerEvent(1, "A", rank);
    }

    private void BtnTeam1ModeB_Click(object? sender, EventArgs e)
    {
        int rank = (int)numRank.Value;
        SendBuzzerEvent(1, "B", rank);
    }

    private void BtnTeam2ModeA_Click(object? sender, EventArgs e)
    {
        int rank = (int)numRank.Value;
        SendBuzzerEvent(2, "A", rank);
    }

    private void BtnTeam2ModeB_Click(object? sender, EventArgs e)
    {
        int rank = (int)numRank.Value;
        SendBuzzerEvent(2, "B", rank);
    }

    private void BtnHostPress_MouseDown(object? sender, MouseEventArgs e)
    {
        if (_serialPort != null && _serialPort.IsOpen)
        {
            _isHostPressed = true;
            _hostPressStartTime = DateTime.Now;
            _hostPressTimer?.Start();
            _serialPort.WriteLine("HOST_PRESS:0\n");
            btnHostPress.BackColor = Color.LightGreen;
        }
    }

    private void BtnHostPress_MouseUp(object? sender, MouseEventArgs e)
    {
        if (_serialPort != null && _serialPort.IsOpen && _isHostPressed)
        {
            _isHostPressed = false;
            _hostPressTimer?.Stop();
            int duration = (int)(DateTime.Now - _hostPressStartTime).TotalMilliseconds;
            _serialPort.WriteLine($"HOST_RELEASE:{duration}\n");
            btnHostPress.BackColor = SystemColors.Control;
            string pressType = duration > 1000 ? "Uzun" : "Kısa";
            txtStatus.Text = $"Host {pressType} basma gönderildi ({duration}ms)";
        }
    }

    private void HostPressTimer_Tick(object? sender, EventArgs e)
    {
        if (_isHostPressed)
        {
            int duration = (int)(DateTime.Now - _hostPressStartTime).TotalMilliseconds;
            txtStatus.Text = $"Host butonu basılı ({duration}ms)";
        }
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        DisconnectSerialPort();
        _hostPressTimer?.Stop();
        _hostPressTimer?.Dispose();
        base.OnFormClosing(e);
    }
}
