import socket
import configparser
import time
import os
try:
    import w1thermsensor
except:
    import mock.w1thermsensor as w1thermsensor


def get_fullpath(filename):
    script_dir = os.path.dirname(os.path.realpath(__file__))
    return os.path.join(script_dir, filename)


TEMP_CHECK_PERIOD = 1  # seconds
CONFIG_FILE_PATH = get_fullpath('config.ini')

print("Sinux sender app started")

sensor = w1thermsensor.W1ThermSensor()
config = configparser.ConfigParser()
config.read(CONFIG_FILE_PATH)

host = config['DEFAULT']['Host']
port = int(config['DEFAULT']['Port'])

print(f"Receiver: {host}:{port}")

is_connected = False

while True:
    if is_connected is False:
        try:
            sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            sock.connect((host, port))
            print("Connected")
            is_connected = True
        except:
            pass
    else:
        temp = sensor.get_temperature()
        print(temp)
        try:
            sock.send(f"{temp}<EOF>".encode())
        except:
            is_connected = False
            sock.close()

    time.sleep(TEMP_CHECK_PERIOD)
