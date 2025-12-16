# Hızlı Başlangıç Kılavuzu

## Windows'ta Test Etme (Sanal Seri Port ile)

### 1. Sanal Seri Port Kurulumu

**com0com (Önerilen - Ücretsiz)**
1. https://sourceforge.net/projects/com0com/ adresinden indirin
2. Kurulum yapın
3. Setup Command Prompt'u yönetici olarak çalıştırın
4. İki sanal port oluşturun:
   ```
   install PortName=COM10 PortName=COM11
   ```

**Alternatif: VSPE (Virtual Serial Port Emulator)**
1. http://www.eterlogic.com/Products.VSPE.html adresinden indirin
2. İki sanal port pair oluşturun

### 2. Uygulamaları Çalıştırma

**Terminal 1 - QuizApp (Skorbord):**
```bash
cd QuizApp
dotnet run
```

**Terminal 2 - ArduinoNanoEmulator:**
```bash
cd ArduinoNanoEmulator
dotnet run
```

### 3. Bağlantı Kurma

1. **QuizApp'te:**
   - Seri Port: COM10 seçin
   - "Bağlan" butonuna tıklayın
   - Mod: A veya B seçin
   - Rank: 1-5 arası seçin (varsayılan: 4)

2. **ArduinoNanoEmulator'de:**
   - Seri Port: COM11 seçin
   - "Bağlan" butonuna tıklayın
   - Rank: QuizApp ile aynı değeri seçin (varsayılan: 4)

### 4. Test Senaryoları

#### Senaryo 1: Basit Skor Testi
1. QuizApp'te Mode A ve Rank 4 seçili olsun
2. Emulator'de Rank 4 seçili olsun
3. Takım 1 > Mod A butonuna tıklayın
4. QuizApp'te Takım 1 skorunun 1 arttığını görün
5. Takım 2 > Mod A butonuna tıklayın
6. QuizApp'te Takım 2 skorunun 1 arttığını görün

#### Senaryo 2: Mode Filtreleme
1. QuizApp'te Mode A seçin
2. Emulator'de Takım 1 > Mod B butonuna tıklayın
3. QuizApp'te skorun değişmediğini, "Mod uyuşmuyor" mesajı göründüğünü görün
4. Emulator'de Takım 1 > Mod A butonuna tıklayın
5. QuizApp'te skorun arttığını görün

#### Senaryo 3: Rank Filtreleme
1. QuizApp'te Rank 4 seçin
2. Emulator'de Rank'ı 2 yapın
3. Takım 1 > Mod A butonuna tıklayın
4. QuizApp'te skorun değişmediğini, "Rank uyuşmuyor" mesajı göründüğünü görün
5. Emulator'de Rank'ı 4 yapın
6. Takım 1 > Mod A butonuna tıklayın
7. QuizApp'te skorun arttığını görün

#### Senaryo 4: Host Butonu - Kısa Basma
1. Emulator'de "HOST BUTON"a tıklayın
2. Hemen bırakın (1 saniyeden az)
3. QuizApp'te "Host: Kısa basma (Xms)" mesajını görün

#### Senaryo 5: Host Butonu - Uzun Basma
1. Emulator'de "HOST BUTON"a tıklayın
2. 2-3 saniye basılı tutun
3. QuizApp'te basma süresinin gerçek zamanlı güncellendiğini görün
4. Bırakın
5. "Host: Uzun basma (Xms)" mesajını görün

#### Senaryo 6: Skor Sıfırlama
1. Skorları birkaç puan artırın
2. QuizApp'te "Skorları Sıfırla" butonuna tıklayın
3. Onay mesajında "Evet" seçin
4. Her iki takımın skorunun 0 olduğunu görün

### 5. Sorun Giderme

**Bağlantı Hatası:**
- Seri portların doğru seçildiğinden emin olun
- Sanal port pair'lerinin doğru kurulduğunu kontrol edin
- Başka bir uygulama portları kullanıyor olabilir, kapatın

**Veri Alınamıyor:**
- Her iki uygulamanın da bağlı olduğundan emin olun
- com0com'da port pair'lerinin doğru eşlendiğini kontrol edin
- Uygulamaları yeniden başlatın

**Skorlar Güncellenmiyor:**
- Mode (A/B) ayarlarının eşleştiğinden emin olun
- Rank ayarlarının eşleştiğinden emin olun
- "Son olay" mesajını kontrol edin, filtreleme nedeni gösterilir

## Arduino Nano ile Kullanım

### Donanım Bağlantısı
1. Arduino Nano'yu USB kablosu ile bilgisayara bağlayın
2. Arduino IDE veya benzeri araçla firmware'i yükleyin
3. QuizApp'i başlatın
4. Seri port olarak Arduino'nun bağlı olduğu portu seçin (örn: COM3)
5. "Bağlan" butonuna tıklayın

### Firmware Gereksinimleri
- Baud rate: 9600
- Protokol: PROTOCOL.md dosyasına bakın
- Örnek kod: PROTOCOL.md içinde Arduino örnekleri mevcut

## Visual Studio ile Geliştirme

### Projeyi Açma
1. Visual Studio 2022 veya üzerini açın
2. QuizAPP.sln dosyasını açın
3. Her iki proje de solution'da görünecek

### Debugging
1. Startup Projects ayarını "Multiple startup projects" yapın
2. Her iki projeyi de "Start" olarak işaretleyin
3. F5 ile debugging başlatın

### Build
```bash
# Tüm solution'ı build et
dotnet build

# Release build
dotnet build -c Release

# Sadece QuizApp
dotnet build QuizApp/QuizApp.csproj

# Sadece Emulator
dotnet build ArduinoNanoEmulator/ArduinoNanoEmulator.csproj
```

## İpuçları

- **Tam Ekran Skorbord**: QuizApp açıldığında otomatik olarak maximize olur
- **Hızlı Test**: Emulator ile hızlıca farklı senaryoları test edebilirsiniz
- **Log Takibi**: Her iki uygulamada da durum barında son eventler gösterilir
- **Gerçek Zamanlı Host**: Host butonuna basılı tutarken süre gerçek zamanlı güncellenir

## Güvenlik Notları

- Seri port bağlantıları başarısız olabilir, her zaman try-catch ile korunmuştur
- Bağlantı koptuğunda uygulamalar graceful olarak kapatılır
- Geçersiz mesajlar göz ardı edilir, uygulama crash olmaz
