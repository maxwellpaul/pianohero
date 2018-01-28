from model import Model
from view import View
from controller import Controller

# run the game, setup and run
model = Model()
view = View()
controller = Controller()

controller.addObserver(model)

model.run()
view.run()
controller.run()