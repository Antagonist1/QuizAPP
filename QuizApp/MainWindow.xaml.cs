using System.IO.Ports;
using System.Windows;
using System.Windows.Threading;

namespace QuizApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private SerialPort? _serialPort;
    private int _team1Score = 0;
    private int _team2Score = 0;
    private DispatcherTimer? _hostPressTimer;
    private DateTime _hostPressStartTime;
    private bool _isHostPressed = false;

    public MainWindow()
    {
        InitializeComponent();
        LoadSerialPorts();
        
        // Initialize host press timer
        _hostPressTimer = new DispatcherTimer();
        _hostPressTimer.Interval = TimeSpan.FromMilliseconds(100);
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

    private void BtnConnect_Click(object sender, RoutedEventArgs e)
    {
        if (cmbSerialPorts.SelectedItem == null)
        {
            MessageBox.Show("Lütfen bir seri port seçin.", "Hata", MessageBoxButton.OK, MessageBoxImage.Warning);
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

            _serialPort.DataReceived += SerialPort_DataReceived;
            _serialPort.Open();

            btnConnect.IsEnabled = false;
            btnDisconnect.IsEnabled = true;
            cmbSerialPorts.IsEnabled = false;
            txtStatus.Text = $"Bağlandı: {_serialPort.PortName}";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Bağlantı hatası: {ex.Message}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void BtnDisconnect_Click(object sender, RoutedEventArgs e)
    {
        DisconnectSerialPort();
    }

    private void DisconnectSerialPort()
    {
        if (_serialPort != null && _serialPort.IsOpen)
        {
            _serialPort.DataReceived -= SerialPort_DataReceived;
            _serialPort.Close();
            _serialPort.Dispose();
            _serialPort = null;
        }

        btnConnect.IsEnabled = true;
        btnDisconnect.IsEnabled = false;
        cmbSerialPorts.IsEnabled = true;
        txtStatus.Text = "Bağlantı kesildi";
    }

    private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        if (_serialPort == null || !_serialPort.IsOpen) return;

        try
        {
            string data = _serialPort.ReadLine().Trim();
            Dispatcher.Invoke(() => ProcessSerialData(data));
        }
        catch (Exception ex)
        {
            Dispatcher.Invoke(() => txtStatus.Text = $"Veri okuma hatası: {ex.Message}");
        }
    }

    private void ProcessSerialData(string data)
    {
        // Protocol: BUZZER:<team>:<mode>:<rank>
        // Example: BUZZER:1:A:4 (Team 1, Mode A, Rank 4)
        // HOST_PRESS:<duration> (Host button press)
        // HOST_RELEASE:<duration> (Host button release)

        if (string.IsNullOrWhiteSpace(data)) return;

        txtLastEvent.Text = $"Son olay: {data} ({DateTime.Now:HH:mm:ss})";

        string[] parts = data.Split(':');
        
        if (parts.Length < 1) return;

        switch (parts[0])
        {
            case "BUZZER":
                if (parts.Length >= 4)
                {
                    ProcessBuzzerEvent(parts[1], parts[2], parts[3]);
                }
                break;

            case "HOST_PRESS":
                _isHostPressed = true;
                _hostPressStartTime = DateTime.Now;
                _hostPressTimer?.Start();
                txtHostPress.Text = "Host: Basılı";
                break;

            case "HOST_RELEASE":
                _isHostPressed = false;
                _hostPressTimer?.Stop();
                if (parts.Length >= 2 && int.TryParse(parts[1], out int duration))
                {
                    string pressType = duration > 1000 ? "Uzun" : "Kısa";
                    txtHostPress.Text = $"Host: {pressType} basma ({duration}ms)";
                }
                break;
        }
    }

    private void ProcessBuzzerEvent(string team, string mode, string rank)
    {
        // Check if mode matches
        bool isModeA = rbModeA.IsChecked == true;
        string currentMode = isModeA ? "A" : "B";
        
        if (mode != currentMode)
        {
            txtLastEvent.Text += " (Mod uyuşmuyor, göz ardı edildi)";
            return;
        }

        // Get selected rank
        int selectedRank = cmbRank.SelectedIndex + 1;
        if (!int.TryParse(rank, out int eventRank) || eventRank != selectedRank)
        {
            txtLastEvent.Text += " (Rank uyuşmuyor, göz ardı edildi)";
            return;
        }

        // Update score
        if (team == "1")
        {
            _team1Score++;
            txtTeam1Score.Text = _team1Score.ToString();
            txtLastEvent.Text += " ✓ Takım 1 puan kazandı!";
        }
        else if (team == "2")
        {
            _team2Score++;
            txtTeam2Score.Text = _team2Score.ToString();
            txtLastEvent.Text += " ✓ Takım 2 puan kazandı!";
        }
    }

    private void HostPressTimer_Tick(object? sender, EventArgs e)
    {
        if (_isHostPressed)
        {
            int duration = (int)(DateTime.Now - _hostPressStartTime).TotalMilliseconds;
            txtHostPress.Text = $"Host: Basılı ({duration}ms)";
        }
    }

    private void BtnReset_Click(object sender, RoutedEventArgs e)
    {
        var result = MessageBox.Show("Skorları sıfırlamak istediğinizden emin misiniz?", 
            "Onay", MessageBoxButton.YesNo, MessageBoxImage.Question);
        
        if (result == MessageBoxResult.Yes)
        {
            _team1Score = 0;
            _team2Score = 0;
            txtTeam1Score.Text = "0";
            txtTeam2Score.Text = "0";
            txtLastEvent.Text = "Skorlar sıfırlandı";
        }
    }

    protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
    {
        DisconnectSerialPort();
        _hostPressTimer?.Stop();
        base.OnClosing(e);
    }
}