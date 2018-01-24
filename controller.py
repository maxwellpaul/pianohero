from model import Model

class Controller:

    def addObserver(self, model):
        self._model = model

    def run(self):
        print ("Running controller")