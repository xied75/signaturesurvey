#! /usr/bin/python
import os
import sys
from classes.SignatureItem import SignatureItem
write = sys.stdout.write

codefiles = ['.java','.cs','.c','.cpp','.h']
c_family = [';','{','}']

def interestingCharacters(file, interesting_characters):
	signature = [
		character 
		for character in file.read() 
		if character in interesting_characters
	]
	return ''.join(signature)

def lineCount(file):
	lineCount = len(file.readlines())
	reset(file)
	return lineCount

def reset(file):
	file.seek(0)
	
def signature(root, filename):
	fName = filename
	path = absolutePath(root, filename)
	with open(path) as file:
		loc = lineCount(file)
		signature = interestingCharacters(file, c_family)
		return SignatureItem(fName, loc, signature)
	
def extension(filename):
	return os.path.splitext(filename)[1]
		
def absolutePath(root, filename):
	return os.path.join(root, filename)

def should_skip(filename):
	return isNotACodeFile(filename) or containsGeneratedCode(filename) or belongsToResharper(filename)

def belongsToResharper(filename):
	return "ReSharper" in filename

def containsGeneratedCode(filename):
	return "g.i.cs" in filename or ".g.cs" in filename

def isNotACodeFile(filename):
	return extension(filename) not in codefiles 

def signature_survey(directory):
	items = []
	for root,dirs,files in os.walk(directory):
		for filename in files:
			if should_skip(filename): continue 			
			items.append(signature(root, filename))
	return items

if __name__ == "__main__":
	items = signature_survey(sys.argv[1])
	for item in items:
                print(item)
	
