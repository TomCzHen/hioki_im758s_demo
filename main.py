import logging
import sys
import threading

import serial
from serial.tools import list_ports

logging.basicConfig(
    level=logging.DEBUG,
    format='%(asctime)s - %(thread)d - %(name)s - %(levelname)s - %(message)s',
    stream=sys.stdout
)

logger = logging.getLogger(__name__)


def start_analyzer_mode(
        *,
        device_name: str,
        serial_port: serial.Serial,
        frequency: str,
        num: int,
        scale: str,
        thread_barrier: threading.Barrier | None = None,
):
    if thread_barrier:
        thread_barrier.wait()
    logger.info(f'{device_name} : Starting analyzer mode')

    serial_port.write(b"*RST\r\n")
    serial_port.write(b":MODE ANALyzer\r\n")
    serial_port.write(b":SWEep:TRIGger SEQuential\r\n")
    serial_port.write(f":LIST:STARt:STOP {frequency},{num},{scale}\r\n".encode("ascii"))
    serial_port.write(b":PARameter1 Z\r\n")
    serial_port.write(b":PARameter2 OFF\r\n")
    serial_port.write(b":PARameter3 Phase\r\n")
    serial_port.write(b":PARameter4 OFF\r\n")
    serial_port.write(b"*TRG\r\n")
    serial_port.write(b":MEASure?\r\n")

    msg = b""

    while True:
        char = serial_port.read()
        if char == b'\r':
            break
        msg += char

    logger.info(f"{msg.decode('ascii')}")
    return msg


def main():
    ports = list_ports.comports()
    if len(ports) == 0:
        raise RuntimeError('No serial port found')

    for port in ports:
        logger.info(f'端口: {port.device}, 描述: {port.description}, 名称: {port.name}')

    device_configs = {
        "im7585_1": {
            "control_config": {
                "frequency": "1000000",
                "num": 100,
                "scale": "LOG",
            },
            "serial_config": {
                "port": "COM1",
                "baudrate": 9600,
                "parity": serial.PARITY_NONE,
                "bytesize": serial.EIGHTBITS,
                "stopbits": serial.STOPBITS_ONE
            }
        },
    }
    thread_barrier = threading.Barrier(len(device_configs.items()))

    for i, (k, v) in enumerate(device_configs.items()):
        serial_port = serial.Serial(**v['serial_config'])
        thread = threading.Thread(
            target=start_analyzer_mode,
            kwargs={
                "device_name": k,
                "serial_port": serial_port,
                **v["control_config"],
                "thread_barrier": thread_barrier
            }
        )
        thread.start()


if __name__ == "__main__":
    main()
