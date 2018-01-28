from model import Model
import click

class Controller:

    def addObserver(self, model):
        self._model = model

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

            
    def run(self):
        print ("Running controller")
        self.listenAndCall()
        print ("Controller quitting")
