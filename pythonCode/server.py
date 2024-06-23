# Created by Youssef Elashry to allow two-way communication between Python3 and Unity to send and receive strings

# Feel free to use this in your individual or commercial projects BUT make sure to reference me as: Two-way communication between Python 3 and Unity (C#) - Y. T. Elashry
# It would be appreciated if you send me how you have used this in your projects (e.g. Machine Learning) at youssef.elashry@gmail.com

# Use at your own risk
# Use under the Apache License 2.0

# Example of a Python UDP server
from GenImage import ImageRequester
import UdpComms as U
import time
from DataProcessor import processData
from GPT import GPT
import os
# Create UDP socket to use for sending (and receiving)
sock = U.UdpComms(udpIP="127.0.0.1", portTX=8000, portRX=8001, enableRX=True, suppressWarnings=True)
ir=ImageRequester()
gpt=GPT()
i = 0
os.system("start chatroom/vivo_chat.exe")
while True:
    data = sock.ReadReceivedData()
    if data != None:
        print(data)
        cmd,prompt=processData(data)
        if cmd=='t2i':
            ir.process(prompt)
            if ir.ready:
                sock.SendData(''+str(ir.url_get))
                print("Url sent!")
        if cmd=='gpt':
            msg=gpt.sync_gpt(prompt)
            if len(msg)>0:
                sock.SendData(''+str(msg))
                print(f'Response:{msg}')
    time.sleep(1)