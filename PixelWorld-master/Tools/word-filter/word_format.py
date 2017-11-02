#!/usr/bin/python2.7    
# -*- coding: utf-8 -*-

import time
import re
  
def word_format():  
    fp = open('raw.txt','r')
    strs = fp.read()
    fp.close()

    words = re.split(r'(?:、|,|，|;|；|\s)\s*', strs)


    # write    
    fp_result = open('word.txt','w')
    for word in words:
        if len(word) > 0:
            fp_result.write(word+'\n')
    fp_result.close()

    

if __name__ == '__main__':
    word_format()  
