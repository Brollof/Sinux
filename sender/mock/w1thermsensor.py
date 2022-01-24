import random


class W1ThermSensor:
    values = list(range(25, 31))

    def __init__(self):
        print("Mock W1 Therm Sensor created")
        self.index = 0

    def get_temperature(self) -> float:
        v = self.values[self.index]
        self.index = (self.index + 1) % len(self.values)
        return v
