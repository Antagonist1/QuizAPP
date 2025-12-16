# Quiz Buzzer Seri Protokol Dokümantasyonu

## Genel Bilgiler

**Seri Port Ayarları:**
- Baud Rate: 9600
- Data Bits: 8
- Parity: None
- Stop Bits: 1

**Mesaj Formatı:**
- Metin tabanlı (ASCII)
- Her mesaj yeni satır karakteri (\n) ile sonlanır
- Alanlar iki nokta (:) ile ayrılır

## Mesaj Tipleri

### 1. BUZZER Eventi

Bir takım buzzer butonuna bastığında gönderilir.

**Format:**
```
BUZZER:<team>:<mode>:<rank>\n
```

**Parametreler:**

| Alan | Tip | Değerler | Açıklama |
|------|-----|----------|----------|
| team | int | 1, 2 | Takım numarası |
| mode | char | A, B | Oyun modu |
| rank | int | 1-5 | Zorluk/kategori seviyesi |

**Örnekler:**
```
BUZZER:1:A:1\n    # Takım 1, Mode A, Rank 1
BUZZER:2:B:5\n    # Takım 2, Mode B, Rank 5
BUZZER:1:A:3\n    # Takım 1, Mode A, Rank 3
```

**Skorbord Davranışı:**
- Mesajdaki mode, skorbordda seçili mode ile eşleşmelidir
- Mesajdaki rank, skorbordda seçili rank ile eşleşmelidir
- Her iki koşul sağlanırsa ilgili takımın skoru 1 artırılır
- Koşullar sağlanmazsa mesaj göz ardı edilir (event logda gösterilir)

### 2. HOST_PRESS Eventi

Host butonu basıldığında gönderilir.

**Format:**
```
HOST_PRESS:0\n
```

**Parametreler:**

| Alan | Tip | Değerler | Açıklama |
|------|-----|----------|----------|
| duration | int | 0 | Placeholder (her zaman 0) |

**Örnek:**
```
HOST_PRESS:0\n
```

**Skorbord Davranışı:**
- Host butonu basılı olarak işaretlenir
- Basma süresinin sayımı başlatılır
- UI'da "Host: Basılı" gösterilir
- Gerçek zamanlı süre gösterimi aktifleşir

### 3. HOST_RELEASE Eventi

Host butonu bırakıldığında gönderilir.

**Format:**
```
HOST_RELEASE:<duration>\n
```

**Parametreler:**

| Alan | Tip | Değerler | Açıklama |
|------|-----|----------|----------|
| duration | int | 0-∞ | Basma süresi (milisaniye) |

**Örnekler:**
```
HOST_RELEASE:500\n     # 500ms (Kısa basma)
HOST_RELEASE:1500\n    # 1500ms (Uzun basma)
HOST_RELEASE:3000\n    # 3000ms (Uzun basma)
```

**Basma Sınıflandırması:**
- **Kısa Basma:** duration < 1000ms
- **Uzun Basma:** duration ≥ 1000ms

**Skorbord Davranışı:**
- Basma süresi hesaplanır
- Basma tipi (kısa/uzun) belirlenir
- UI'da "Host: Kısa/Uzun basma (Xms)" gösterilir

## Protokol Akış Örnekleri

### Örnek 1: Basit Quiz Oyunu

```
# Oyun başlıyor, host uzun basar (başlat)
HOST_PRESS:0\n
HOST_RELEASE:2000\n

# Takım 1 buzzer'a basar (Mode A, Rank 3)
BUZZER:1:A:3\n

# Cevap doğru, skor güncellendi
# (Skorbord'da Mode A ve Rank 3 seçili olmalı)

# Takım 2 buzzer'a basar (Mode A, Rank 3)
BUZZER:2:A:3\n

# Cevap doğru, skor güncellendi
```

### Örnek 2: Yanlış Mod/Rank

```
# Skorbord: Mode A, Rank 4 seçili

# Takım 1 yanlış mode ile basar
BUZZER:1:B:4\n
# Göz ardı edilir (mode uyuşmuyor)

# Takım 1 yanlış rank ile basar
BUZZER:1:A:3\n
# Göz ardı edilir (rank uyuşmuyor)

# Takım 1 doğru mode ve rank ile basar
BUZZER:1:A:4\n
# Kabul edilir, skor +1
```

### Örnek 3: Host Butonu Kullanımı

```
# Hızlı reset işlemi (kısa basma)
HOST_PRESS:0\n
HOST_RELEASE:500\n

# Menü açma (uzun basma)
HOST_PRESS:0\n
HOST_RELEASE:2500\n

# Çok uzun basma (beklenmeyen durum)
HOST_PRESS:0\n
HOST_RELEASE:10000\n
```

## Arduino Firmware Örneği

### Basit Uygulama

```cpp
// Pin tanımlamaları
#define TEAM1_BUTTON 2
#define TEAM2_BUTTON 3
#define HOST_BUTTON 4

char currentMode = 'A';  // A veya B
int currentRank = 4;      // 1-5

void setup() {
  Serial.begin(9600);
  pinMode(TEAM1_BUTTON, INPUT_PULLUP);
  pinMode(TEAM2_BUTTON, INPUT_PULLUP);
  pinMode(HOST_BUTTON, INPUT_PULLUP);
}

void sendBuzzer(int team) {
  Serial.print("BUZZER:");
  Serial.print(team);
  Serial.print(":");
  Serial.print(currentMode);
  Serial.print(":");
  Serial.println(currentRank);
}

void loop() {
  // Takım 1 buton
  if (digitalRead(TEAM1_BUTTON) == LOW) {
    sendBuzzer(1);
    delay(500); // Debounce
  }
  
  // Takım 2 buton
  if (digitalRead(TEAM2_BUTTON) == LOW) {
    sendBuzzer(2);
    delay(500); // Debounce
  }
  
  // Host buton
  static unsigned long pressStartTime = 0;
  static bool wasPressed = false;
  
  bool isPressed = (digitalRead(HOST_BUTTON) == LOW);
  
  if (isPressed && !wasPressed) {
    // Yeni basma
    Serial.println("HOST_PRESS:0");
    pressStartTime = millis();
    wasPressed = true;
  } else if (!isPressed && wasPressed) {
    // Bırakma
    unsigned long duration = millis() - pressStartTime;
    Serial.print("HOST_RELEASE:");
    Serial.println(duration);
    wasPressed = false;
  }
}
```

### Gelişmiş Uygulama (Mode/Rank Değiştirme ile)

```cpp
// Pin tanımlamaları
#define TEAM1_BUTTON 2
#define TEAM2_BUTTON 3
#define HOST_BUTTON 4
#define MODE_SWITCH 5      // A/B toggle
#define RANK_UP_BUTTON 6   // Rank artır
#define RANK_DOWN_BUTTON 7 // Rank azalt

char currentMode = 'A';
int currentRank = 4;

void setup() {
  Serial.begin(9600);
  pinMode(TEAM1_BUTTON, INPUT_PULLUP);
  pinMode(TEAM2_BUTTON, INPUT_PULLUP);
  pinMode(HOST_BUTTON, INPUT_PULLUP);
  pinMode(MODE_SWITCH, INPUT_PULLUP);
  pinMode(RANK_UP_BUTTON, INPUT_PULLUP);
  pinMode(RANK_DOWN_BUTTON, INPUT_PULLUP);
}

void sendBuzzer(int team) {
  Serial.print("BUZZER:");
  Serial.print(team);
  Serial.print(":");
  Serial.print(currentMode);
  Serial.print(":");
  Serial.println(currentRank);
}

void loop() {
  // Mode switch
  static bool lastModeState = HIGH;
  bool modeState = digitalRead(MODE_SWITCH);
  if (modeState == LOW && lastModeState == HIGH) {
    currentMode = (currentMode == 'A') ? 'B' : 'A';
    delay(200);
  }
  lastModeState = modeState;
  
  // Rank up/down
  if (digitalRead(RANK_UP_BUTTON) == LOW) {
    if (currentRank < 5) currentRank++;
    delay(200);
  }
  if (digitalRead(RANK_DOWN_BUTTON) == LOW) {
    if (currentRank > 1) currentRank--;
    delay(200);
  }
  
  // Takım butonları
  if (digitalRead(TEAM1_BUTTON) == LOW) {
    sendBuzzer(1);
    delay(500);
  }
  if (digitalRead(TEAM2_BUTTON) == LOW) {
    sendBuzzer(2);
    delay(500);
  }
  
  // Host buton (önceki örnekteki gibi)
  static unsigned long pressStartTime = 0;
  static bool wasPressed = false;
  bool isPressed = (digitalRead(HOST_BUTTON) == LOW);
  
  if (isPressed && !wasPressed) {
    Serial.println("HOST_PRESS:0");
    pressStartTime = millis();
    wasPressed = true;
  } else if (!isPressed && wasPressed) {
    unsigned long duration = millis() - pressStartTime;
    Serial.print("HOST_RELEASE:");
    Serial.println(duration);
    wasPressed = false;
  }
}
```

## Hata Durumları

### Bağlantı Hataları
- Seri port açılamazsa: Kullanıcıya uyarı gösterilir
- Bağlantı koparsa: Otomatik olarak tespit edilir, durum güncellenir

### Geçersiz Mesajlar
- Format hatası olan mesajlar göz ardı edilir
- Log'da gösterilir ancak işlem yapılmaz

### Senkronizasyon
- Mode/Rank eşleşmezliği: Event göz ardı edilir, logda belirtilir
- Kullanıcı manuel olarak ayarları kontrol edebilir

## Test Senaryoları

### Test 1: Temel İletişim
1. Emulator ve Skorbord'u bağla
2. BUZZER:1:A:4 gönder
3. Takım 1 skorunun artığını doğrula

### Test 2: Mode Filtreleme
1. Skorbord'da Mode A seç
2. BUZZER:1:B:4 gönder
3. Skorun değişmediğini doğrula
4. BUZZER:1:A:4 gönder
5. Skorun arttığını doğrula

### Test 3: Rank Filtreleme
1. Skorbord'da Rank 3 seç
2. BUZZER:1:A:1 gönder
3. Skorun değişmediğini doğrula
4. BUZZER:1:A:3 gönder
5. Skorun arttığını doğrula

### Test 4: Host Kısa Basma
1. HOST_PRESS:0 gönder
2. 500ms bekle
3. HOST_RELEASE:500 gönder
4. "Kısa basma" gösterildiğini doğrula

### Test 5: Host Uzun Basma
1. HOST_PRESS:0 gönder
2. 2000ms bekle
3. HOST_RELEASE:2000 gönder
4. "Uzun basma" gösterildiğini doğrula

## Gelecek Geliştirmeler

Protokole eklenebilecek özellikler:
- LED kontrol komutları
- MP3 çalma komutları
- Skor sıfırlama komutu
- Konfigürasyon sorgulama
- Heartbeat/ping mesajları
