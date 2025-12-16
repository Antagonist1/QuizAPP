# QuizAPP
Arduino Nano tabanlı quiz buzzer sistemi: WPF skorbord (QuizApp), WinForms emulator (ArduinoNanoEmulator) ve seri protokolle çalışan firmware. Mode A/B, rank seçimi, host kıs/uzun basma, LED/MP3 desteği.

## Proje Yapısı

### QuizApp (WPF)
WPF tabanlı quiz skorbord uygulaması. Arduino Nano'dan seri port üzerinden gelen buzzer eventlerini alır ve skorları günceller.

**Özellikler:**
- Seri port bağlantısı ve yönetimi
- Mode A/B seçimi
- Rank (1-5) seçimi
- Host butonu kısa/uzun basma tespiti
- Takım 1 ve Takım 2 için skor gösterimi
- Gerçek zamanlı event takibi
- Skor sıfırlama

### ArduinoNanoEmulator (WinForms)
WinForms tabanlı Arduino Nano emulator uygulaması. QuizApp ile aynı seri protokolü kullanarak test amaçlı kullanılır.

**Özellikler:**
- Seri port bağlantısı
- Takım 1/2 için Mode A/B buzzer simülasyonu
- Rank seçimi (1-5)
- Host butonu simülasyonu (kısa/uzun basma)
- Seri protokol testi

## Seri Protokol Spesifikasyonu

### Buzzer Eventi
```
BUZZER:<team>:<mode>:<rank>
```

**Parametreler:**
- `team`: Takım numarası (1 veya 2)
- `mode`: Mod seçimi (A veya B)
- `rank`: Rank seviyesi (1-5)

**Örnek:**
```
BUZZER:1:A:4
```
Takım 1, Mode A, Rank 4

### Host Butonu Eventi

**Basma (Press):**
```
HOST_PRESS:0
```

**Bırakma (Release):**
```
HOST_RELEASE:<duration>
```

**Parametreler:**
- `duration`: Basma süresi (milisaniye)

**Basma Tipleri:**
- Kısa basma: < 1000ms
- Uzun basma: ≥ 1000ms

**Örnek:**
```
HOST_PRESS:0
HOST_RELEASE:1500
```
Host butonu 1500ms süreyle basıldı (uzun basma)

## Kurulum

### Gereksinimler
- .NET 8.0 SDK veya üzeri
- Windows işletim sistemi (WPF ve WinForms için)

### Derleme

```bash
dotnet build
```

### Çalıştırma

**QuizApp (Skorbord):**
```bash
cd QuizApp
dotnet run
```

**ArduinoNanoEmulator:**
```bash
cd ArduinoNanoEmulator
dotnet run
```

## Kullanım

### Test Senaryosu (Emulator ile)

1. **İki uygulamayı da başlatın**
   - QuizApp (Skorbord)
   - ArduinoNanoEmulator

2. **Sanal Seri Port Oluşturun**
   - Windows için: com0com, VSPE veya benzer bir araç kullanın
   - İki sanal port oluşturun (örn: COM10 ve COM11)

3. **Bağlantıları Kurun**
   - QuizApp'te: Bir portu seçin (örn: COM10) ve "Bağlan"
   - ArduinoNanoEmulator'de: Diğer portu seçin (örn: COM11) ve "Bağlan"

4. **Test Edin**
   - QuizApp'te istediğiniz Mode (A/B) ve Rank'i seçin
   - Emulator'de aynı ayarları yapın
   - Emulator'de buzzer butonlarına basarak event gönderin
   - QuizApp'te skorların güncellendiğini görün

5. **Host Butonunu Test Edin**
   - Emulator'de "HOST BUTON"a tıklayıp basılı tutun
   - 1 saniyeden az bırakın (kısa basma)
   - 1 saniyeden fazla basılı tutun (uzun basma)
   - QuizApp'te basma tipini görün

### Arduino Nano ile Kullanım

Arduino Nano firmware'i aşağıdaki protokolü kullanarak seri port üzerinden mesaj göndermelidir:

1. Buzzer basıldığında: `BUZZER:<team>:<mode>:<rank>\n`
2. Host butonu basıldığında: `HOST_PRESS:0\n`
3. Host butonu bırakıldığında: `HOST_RELEASE:<duration>\n`

**Örnek Arduino Kodu (Taslak):**
```cpp
void sendBuzzer(int team, char mode, int rank) {
  Serial.print("BUZZER:");
  Serial.print(team);
  Serial.print(":");
  Serial.print(mode);
  Serial.print(":");
  Serial.println(rank);
}

void loop() {
  // Buzzer butonları kontrol et
  if (team1ButtonPressed) {
    sendBuzzer(1, currentMode, currentRank);
  }
  
  // Host butonu kontrol et
  if (hostButtonPressed) {
    Serial.println("HOST_PRESS:0");
    unsigned long startTime = millis();
    while (hostButtonStillPressed) {
      // Bekle
    }
    unsigned long duration = millis() - startTime;
    Serial.print("HOST_RELEASE:");
    Serial.println(duration);
  }
}
```

## Özellikler ve Davranış

### Mode Seçimi
- **Mode A**: Belirli bir oyun modu için
- **Mode B**: Alternatif oyun modu için
- Her mode bağımsız olarak çalışır
- Yanlış mode'daki eventler göz ardı edilir

### Rank Sistemi
- 1-5 arası rank seviyeleri
- Her rank farklı zorluk/kategori için kullanılabilir
- Yalnızca seçili rank'teki eventler kabul edilir

### Host Butonu
- Kısa basma (<1000ms): Hızlı kontroller için
- Uzun basma (≥1000ms): Özel işlemler için
- Gerçek zamanlı basma süresi gösterimi

### Skor Takibi
- Her takım için bağımsız skor
- Sıfırlama özelliği (onay ile)
- Görsel geri bildirim

## Geliştirme

### Proje Yapısı
```
QuizAPP/
├── QuizApp/                    # WPF Skorbord
│   ├── MainWindow.xaml         # UI tasarımı
│   ├── MainWindow.xaml.cs      # Uygulama mantığı
│   └── QuizApp.csproj
├── ArduinoNanoEmulator/        # WinForms Emulator
│   ├── Form1.cs                # Emulator mantığı
│   ├── Form1.Designer.cs       # UI tasarımı
│   └── ArduinoNanoEmulator.csproj
└── QuizAPP.sln                 # Visual Studio Solution
```

### Bağımlılıklar
- System.IO.Ports: Seri port iletişimi için

## Lisans
MIT License - Detaylar için LICENSE dosyasına bakın.

## Katkıda Bulunma
Pull request'ler memnuniyetle karşılanır. Büyük değişiklikler için lütfen önce bir issue açın.
