import socket
import configparser
import time

try:
    import w1thermsensor
except:
    import mock.w1thermsensor as w1thermsensor

TEMP_CHECK_PERIOD = 5  # seconds

print("Sinux sender app started")

sensor = w1thermsensor.W1ThermSensor()
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
config = configparser.ConfigParser()
config.read('config.ini')

host = config['DEFAULT']['Host']
port = int(config['DEFAULT']['Port'])

print(f"Receiver: {host}:{port}")

sock.connect((host, port))

while True:
    temp = sensor.get_temperature()
    print(temp)
    sock.send(str(temp).encode())
    time.sleep(TEMP_CHECK_PERIOD)
