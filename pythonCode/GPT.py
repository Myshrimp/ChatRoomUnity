# encoding: utf-8
import uuid
import time
import requests
from auth_util import gen_sign_headers

# 请替换APP_ID、APP_KEY
APP_ID = '3033144558'
APP_KEY = 'YCXCVrelhjTggDrg'


class GPT():
    def sync_gpt(self,prompt):
        URI = '/vivogpt/completions'
        DOMAIN = 'api-ai.vivo.com.cn'
        METHOD = 'POST'
        params = {
           'requestId': str(uuid.uuid4())
            }
        print('requestId:', params['requestId'])

        data = {
        'prompt': f'{prompt}',
        'model': 'vivo-BlueLM-TB',
        'sessionId': str(uuid.uuid4()),
        'extra': {
            'temperature': 0.9
                }
        }
        headers = gen_sign_headers(APP_ID, APP_KEY, METHOD, URI, params)
        headers['Content-Type'] = 'application/json'

        start_time = time.time()
        url = 'https://{}{}'.format(DOMAIN, URI)
        response = requests.post(url, json=data, headers=headers, params=params)

        if response.status_code == 200:
            res_obj = response.json()
            print(f'response:{res_obj}')
            if res_obj['code'] == 0 and res_obj.get('data'):
                content = res_obj['data']['content']
                print(f'final content:\n{content}')
                return content
        else:
            print(response.status_code, response.text)
        end_time = time.time()
        timecost = end_time - start_time
        print('请求耗时: %.2f秒' % timecost)
        return "Sorry,the vivo-gpt can't respond to you"
    def ai_judge(self,s1,s2):
        return sync_vivogpt(s1,s2)
def sync_vivogpt(s1,s2):
    URI = '/vivogpt/completions'
    DOMAIN = 'api-ai.vivo.com.cn'
    METHOD = 'POST'
    params = {
        'requestId': str(uuid.uuid4())
    }
    print('requestId:', params['requestId'])

    data = {
        'prompt': f'用一个0到1的数字表{s1}和{s2}的相似度',
        'model': 'vivo-BlueLM-TB',
        'sessionId': str(uuid.uuid4()),
        'extra': {
            'temperature': 0.9
        }
    }
    headers = gen_sign_headers(APP_ID, APP_KEY, METHOD, URI, params)
    headers['Content-Type'] = 'application/json'

    start_time = time.time()
    url = 'https://{}{}'.format(DOMAIN, URI)
    response = requests.post(url, json=data, headers=headers, params=params)

    if response.status_code == 200:
        res_obj = response.json()
        print(f'response:{res_obj}')
        if res_obj['code'] == 0 and res_obj.get('data'):
            content = res_obj['data']['content']
            print(f'final content:\n{content}')
            return content
    else:
        print(response.status_code, response.text)
    end_time = time.time()
    timecost = end_time - start_time
    print('请求耗时: %.2f秒' % timecost)
    return "Sorry,the vivo-gpt can't respond to you"


if __name__ == '__main__':
    g=GPT()
    g.sync_gpt('秦王嬴政是怎么死的')