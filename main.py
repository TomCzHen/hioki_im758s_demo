import serial
import logging
import sys
import threading
from serial.tools import list_ports
logging.basicConfig(
    level=logging.DEBUG,
    format='%(asctime)s - %(thread)d - %(name)s - %(levelname)s - %(message)s',
    stream=sys.stdout
)

logger = logging.getLogger(__name__)


def start_analyzer_mode(
        barrier,
        *,
        device_name: str,
        port: str,
        frequency: str,
        num: int,
        scale: str,
        serial_config: dict = None
):
    logger.info(f'{device_name} : Starting analyzer mode')
    if serial_config is None:
        serial_config = {
            "baudrate": 9600,
            "parity": serial.PARITY_NONE,
            "bytesize": serial.EIGHTBITS,
            "stopbits": serial.STOPBITS_ONE
        }
    msg = ""

    with serial.Serial("port", **serial_config) as s:

        s.write(b"*RST\r\n")
        s.write(b":MODE ANALyzer\r\n")
        s.write(b":SWEep:TRIGger SEQuential\r\n")
        s.write(f":LIST:STARt:STOP {frequency},{num},{scale}\r\n")
        s.write(b":PARameter1 Z\r\n")
        s.write(b":PARameter2 OFF\r\n")
        s.write(b":PARameter3 Phase\r\n")
        s.write(b":PARameter4 OFF\r\n")
        s.write(b"*TRG\r\n")
        s.write(b":MEASure?\r\n")

        while True:
            char = s.read()  # 读取一个字节
            if char == b'\n':  # 检查是否是换行符
                break
            elif char == b'\r':  # 检查是否是回车符
                continue
            else:
                msg += char
        logger.info(f'{msg}')
        return msg


def main():
    ports = list_ports.comports()
    if len(ports) == 0:
        raise RuntimeError('No serial port found')

    for port in ports:
        logger.info(f'端口: {port.device}, 描述: {port.description}, 名称: {port.name}')

    device_configs = {
        "im7585_1": {
            "port": "COM1",
            "frequency": "100Hz-120kHz",
            "num": 100,
            "scale": "LOG"
        },
    }
    barrier = threading.Barrier(len(device_configs.items()))
    for i, (k, v) in enumerate(device_configs.items()):
        thread = threading.Thread(
            target=start_analyzer_mode,
            args=(barrier,),
            kwargs={"device_name": k, **v}
        )
        thread.start()


if __name__ == "__main__":
    main()
