const int PINS[] = { 2,3,4,5,6,7,8,9,10,11,12,13 };
//00001111 00001111
//UDLRsSBY XALR0000 
int input;

void setup() {
  Serial.begin(345600);
  int pin = 0;
  for(pin = 0; pin < sizeof(PINS); pin++){
    pinMode(PINS[pin], OUTPUT);  
  }

  input = 0;
}


void loop() {
  clearPins();
  if(Serial.available() > 1){
  input = readSerial();
  writeInput(input);
  }
  
}

int readSerial(){
  byte bytes[2];
  int i = 0;
    // read the incoming byte:
    bytes[0] = Serial.read();
    bytes[1] = Serial.read();
    i = bytes[1] << 8 | (bytes[0] & 0xFF);
    return i;
}

void writeInput(int input){
  int pin = 0;
  for(pin = 4; pin < 16; pin++){
       if(input >> pin & 0x01){
          digitalWrite(PINS[pin-4], HIGH);
       }
  }
}

void clearPins(){
  int pin = 0;
  for(pin = 0; pin < sizeof(PINS); pin++){
    digitalWrite(PINS[pin], LOW);  
  }
}
