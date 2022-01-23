import socket
import configparser
import w1thermsensor

TEMP_CHECK_PERIOD = 5 # seconds

sensor = w1thermsensor.W1ThermSensor()
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

while True:
    temp = sensor.get_temperature()
    time.sleep(TEMP_CHECK_PERIOD)
