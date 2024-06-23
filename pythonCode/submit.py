#!/usr/bin/env python
# encoding: utf-8

import requests
import base64
import json
from auth_util import gen_sign_headers

# 请注意替换APP_ID、APP_KEY


def submit():
    APP_ID = '3033144558'
    APP_KEY = 'YCXCVrelhjTggDrg'
    URI = '/api/v1/task_submit'
    DOMAIN = 'api-ai.vivo.com.cn'
    METHOD = 'POST'
    params = {}
    data = {
     'height': 768,
     'width': 576,
     'prompt': '一条踢足球的狗',
     'styleConfig': '55c682d5eeca50d4806fd1cba3628781'
     }

    headers = gen_sign_headers(APP_ID, APP_KEY, METHOD, URI, params)
    headers['Content-Type'] = 'application/json'

    url = 'http://{}{}'.format(DOMAIN, URI)
    response = requests.post(url, data=json.dumps(data), headers=headers)
    js=response.json()
    id=js['result']['task_id']
    if response.status_code == 200:
        print(response.json())
    else:
        print(response.status_code, response.text)
    return id

if __name__ == '__main__':
   submit()