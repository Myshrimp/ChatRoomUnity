#!/usr/bin/env python
# encoding: utf-8
import time

import requests
import base64
import json
from auth_util import gen_sign_headers
from submit import submit
import threading

class ImageRequester():
    def __init__(self):
        self.url_get=''
        self.ready=False
    def check_progress(self):
        threading.Timer(3.0,self.progress).start()
    def progress(self,id):
        APP_ID = '3033144558'
        APP_KEY = 'YCXCVrelhjTggDrg'
        URI = '/api/v1/task_progress'
        DOMAIN = 'api-ai.vivo.com.cn'
        METHOD = 'GET'
        returned_url = ''
        print("check progress")
        params = {
            'task_id':id
        }
        headers = gen_sign_headers(APP_ID, APP_KEY, METHOD, URI, params)

        uri_params = ''
        for key, value in params.items():
            uri_params = uri_params + key + '=' + value + '&'
        uri_params = uri_params[:-1]

        url = 'http://{}{}?{}'.format(DOMAIN, URI, uri_params)
        print('url:', url)
        response = requests.get(url, headers=headers)
        js=response.json()
        if response.status_code == 200:
            print(response.json())
        else:
            print(response.status_code, response.text)
        if js['result']['finished']:
            self.ready=True
            self.url_get=js['result']['images_url']
        return js['result']['finished']

    def submit(self,prompt):
        APP_ID = '3033144558'
        APP_KEY = 'YCXCVrelhjTggDrg'
        URI = '/api/v1/task_submit'
        DOMAIN = 'api-ai.vivo.com.cn'
        METHOD = 'POST'
        params = {}
        data = {
            'height': 768,
            'width': 576,
            'prompt': prompt,
            'styleConfig': '55c682d5eeca50d4806fd1cba3628781'
        }

        headers = gen_sign_headers(APP_ID, APP_KEY, METHOD, URI, params)
        headers['Content-Type'] = 'application/json'

        url = 'http://{}{}'.format(DOMAIN, URI)
        response = requests.post(url, data=json.dumps(data), headers=headers)
        js = response.json()
        id = js['result']['task_id']
        if response.status_code == 200:
            print(response.json())
        else:
            print(response.status_code, response.text)
        return id

    def process(self,prompt):
        finished = False
        id = self.submit(prompt)
        while not finished:
            time.sleep(.5)
            try:
                finished = self.progress(id)
            except:
                print("failed,trying again")

        print('Is message ready to go? ',self.ready)

if __name__ == '__main__':
    ir=ImageRequester()
    prompt='一条狗追着人咬'
    ir.process(prompt)


