from model import Model
from pynput import keyboard

# https://pypi.python.org/pypi/pynput

class Controller:

    def addObserver(self, model):
        self._model = model

    def run(self):
        print ("Running controller")