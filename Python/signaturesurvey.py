#! /usr/bin/python
import os
import sys
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
	return ("(%s):" % lineCount)

def reset(file):
	file.seek(0)
	
def signature(root, filename):
	print filename,	
	path = absolutePath(root, filename)
	with open(path) as file:
		print lineCount(file),
		print interestingCharacters(file, c_family)
	
def extension(filename):
	return os.path.splitext(filename)[1]
		
def absolutePath(root, filename):
	return os.path.join(root, filename)

def should_skip(filename):
	return isNotACodeFile(filename) or containsGeneratedCode(filename)

def containsGeneratedCode(filename):
	return "g.i.cs" in filename or ".g.cs" in filename

def isNotACodeFile(filename):
	return extension(filename) not in codefiles 

def signature_survey(directory):
	for root,dirs,files in os.walk(directory):
		for filename in files:
			if should_skip(filename): continue 			
			signature(root, filename)

if __name__ == "__main__":
	signature_survey("Okra")