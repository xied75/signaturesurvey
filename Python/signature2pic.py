#! /usr/bin/python
import signaturesurvey
import sys
import Image

def createImageSignature(signature):
        indent = 0
        loc = 0
        codes = []
        code = ""
        previous = ' '
        for char in signature:

            if (char == '{' or char == "}") and loc != 0:
                code = code + ("L%s" % (loc))
                codes.append(loc + 256)
                loc = 0
                
            if char == '{':
                indent = indent + 1
                previous = char
                continue
            
            if char == '}':
                if previous == '{':
                    code = code + ("I%s" % (indent))
                    codes.append(indent)
                    previous = ''
                indent = indent - 1
                
                continue
            
            if char == ';':
                if loc == 0 and indent != 0:
                    code = code + ("I%s" % (indent))
                    codes.append(indent)
                loc = loc+1
                continue
        if loc != 0:
            code = code + ("L%s" % (loc))
            codes.append(loc + 256)
        return codes

def createPic(imgLines, maxLen):
        blowUp = 8
        newImage = Image.new("RGB", (maxLen * blowUp, len(imgLines) * blowUp), "#FFFFFF")
        heatmap = Image.open("./heatmaps/pbj.png")
        
        
        for x in range(len(imgLines)):
                imgLine = imgLines[x]
                for y in range(len(imgLine)):
                        #print x, y
                        if (imgLine[y] > 255):
                                val = 255- (imgLine[y] - 256) - 100
                                #print val
                                for x1 in range(x * blowUp, (x+1) * blowUp):
                                        for y1 in range(y * blowUp, (y+1) * blowUp):
                                                newImage.putpixel((y1, x1), (val, val, val))
                        else:
                                #print imgLine[y]
                                for x1 in range(x * blowUp, (x+1) * blowUp):
                                        for y1 in range(y * blowUp, (y+1) * blowUp):
                                                newImage.putpixel((y1,x1), heatmap.getpixel((1, max(0, max(imgLine[y] * 10), 255))))
    
        newImage.save("../../out.png")
    

def sig2pic(directory):
        signatures = signaturesurvey.signature_survey(directory)
        imgLines = []
        maxLen = 0
        for sig in signatures:
                #print sig
                img = createImageSignature(sig.Signature)
                imgLines.append(img)
                maxLen = max(maxLen, len(img))
                #print img
        #print maxLen
        createPic(imgLines, maxLen)
                
if __name__ == "__main__":
        sig2pic(sys.argv[1])
