import random


class W1ThermSensor:
    value = 25.0

    def __init__(self):
        print("Mock W1 Therm Sensor created")

    def get_temperature(self) -> float:
        return self.value + random.random() - 0.5
