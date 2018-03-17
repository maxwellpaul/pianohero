#include <Keyboard.h>

const int button1Pin = 3;
const int button2Pin = 4;
const int button3Pin = 5;
const int button4Pin = 6;

void setup() {
  // Set All pins to be inputs
  pinMode(button1Pin, INPUT);

  pinMode(button2Pin, INPUT);

  pinMode(button3Pin, INPUT);

  pinMode(button4Pin, INPUT);
}

void loop() {
  // Read the state of each button
  int button1State = digitalRead(button1Pin);
  int button2State = digitalRead(button2Pin); 
  int button3State = digitalRead(button3Pin); 
  int button4State = digitalRead(button4Pin);

  //Click the appropriate keys on keyboard
  if(button1State == HIGH){
      clickA();
  }

  if(button2State == HIGH){
      clickS();
  }

  if(button3State == HIGH){
      clickD();
  }

  if(button4State == HIGH){
      clickF();
  }
  
}

void clickA(){
    Keyboard.write('A');
    Serial.print("A");
}

void clickS(){
    Keyboard.write('S');
    Serial.print("S");
}

void clickD(){
    Keyboard.write('D');
    Serial.print("D");
}

void clickF(){
    Keyboard.write('F');
    Serial.print("F");
}

