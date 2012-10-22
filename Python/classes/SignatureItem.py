#! /usr/bin/python

class SignatureItem:
        Name = ""
        LOC = 0
        Signature = ""
        
        def __init__(self, name, loc, signature):
                self.Name= name
                self.LOC = loc
                self.Signature = signature

        def __str__(self):
                return "%s (%s): %s" % (self.Name, self.LOC, self.Signature)
