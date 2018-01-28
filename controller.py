from model import Model
import click

# Controller class handles all input for the game, and relays information to the model
class Controller:

    # Attach the controller to the model as an observer
    def addObserver(self, model):
        self._model = model

    # Listen for keyboard inputs and relay them to the model
    # Letter 'q' signifies quitting the game, and will end the loop
    def listenAndCall(self):
        while True:
            key = click.getchar()
            if (key == 'q'):
                return
            elif (key == 'a'):
                self._model.playNoteA()
            elif (key == 's'):
                self._model.playNoteB()
            elif (key == 'd'):
                self._model.playNoteC()
            elif (key == 'f'):
                self._model.playNoteD()
    
    # Run the controller
    def run(self):
        print ("Running controller")
        self.listenAndCall()
        print ("Controller quitting")
