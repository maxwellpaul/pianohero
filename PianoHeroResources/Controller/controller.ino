#include <Keyboard.h>

const int button1Pin = 3;
const int button2Pin = 4;
const int button3Pin = 5;
const int button4Pin = 6;

boolean pressed1 = false;
boolean pressed2 = false;
boolean pressed3 = false;
boolean pressed4 = false;


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

  //A
  if(!pressed1 && button1State == HIGH){
      pressed1 = true;
      clickA();
  }
  else if(pressed1 && button1State == LOW){
    pressed1 = false;
  }

  //S
  if(!pressed2 && button2State == HIGH){
      pressed2 = true;
      clickS();
  }
  else if(pressed2 && button2State == LOW){
    pressed2 = false;
  }

  //D
  if(!pressed3 && button3State == HIGH){
      pressed3 = true;
      clickD();
  }
  else if(pressed3 && button3State == LOW){
    pressed3 = false;
  }

  //F
  if(!pressed4 && button4State == HIGH){
      pressed4 = true;
      clickF();
  }
  else if(pressed4 && button4State == LOW){
    pressed4 = false;
  }
}

void clickA(){
    Keyboard.write('A');
}
void clickS(){
    Keyboard.write('S');
}

void clickD(){
    Keyboard.write('D');
}

void clickF(){
    Keyboard.write('F');
}
