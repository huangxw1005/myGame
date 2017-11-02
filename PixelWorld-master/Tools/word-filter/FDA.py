#!/usr/bin/python2.7    
# -*- coding: utf-8 -*-

import os
import time
import sys

class Node(object):  
    def __init__(self):  
        self.children = None  

# The encode of word is UTF-8  
def add_word(root,word):  
    node = root  
    for i in range(len(word)):  
        if node.children == None:  
            node.children = {}  
            node.children[word[i]] = Node()
        elif word[i] not in node.children: 
            node.children[word[i]] = Node()  
  
        node = node.children[word[i]]  

def init(path):  
    root = Node()  
    fp = open(path,'r')
    for line in fp:  
        line = line[0:-1]
        #print line 
        add_word(root,line)

    fp.close()  
    return root  

# The encode of message is UTF-8  
def is_contain(message, root):
    for i in range(len(message)):
        p = root
        j = i
        while (j<len(message) and p.children!=None and message[j] in p.children):
            p = p.children[message[j]]  
            j = j + 1
  
        if p.children==None:
            print isinstance(message[i:j], unicode)
            print '------found word ->', i, j, str(message[i:j])
            return True
      
    return False
  
  
  
def FDA():  
    root = init('word.txt')  
  
    message = 'tet老母'
 
    start_time = time.time()  
    res = is_contain(message,root)
    print res  
    end_time = time.time()
    print (end_time - start_time) 

def TestCFG():
    print 'TestCFG'
    
    fda = init('word.txt')
    
    fileList = [];
    for root,dirs,files in os.walk(unicode('../cooking/client/Assets/Resources/Cfg', "utf-8")):
        for file in files:
            if file.find('.json') != -1:
                fullPath = os.path.join(root, file)
                fp = open(fullPath,'r')
                print 'test', file
                strs = fp.read()
                if is_contain(strs, fda):
                    print 'found in', file
                    #print strs
if __name__ == '__main__':
    FDA()
