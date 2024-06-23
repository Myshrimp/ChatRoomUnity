#!/usr/bin/env python
# encoding: utf-8
import time

import requests
import base64
import json
from auth_util import gen_sign_headers
from submit import submit
import threading
# 请注意替换APP_ID、APP_KEY
APP_ID = '3033144558'
APP_KEY = 'YCXCVrelhjTggDrg'
URI = '/api/v1/task_progress'
DOMAIN = 'api-ai.vivo.com.cn'
METHOD = 'GET'
returned_url=''
def check_progress():
    threading.Timer(3.0,progress).start()
def progress(id):
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
        returned_url=js['result']['images_url']
    return js['result']['finished']

if __name__ == '__main__':
    finished=False
    id=submit()
    while not finished:
        time.sleep(.5)
        try:
            finished=progress(id)
        except:
            print("failed")


