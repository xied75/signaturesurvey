Signature Survey
================

This is an adoption of Ward Cunningham's [Signature Survey](http://c2.com/doc/SignatureSurvey/).I am using it as a kata whenever I wan't to learn a new programming language. 

A signature survey is a print of all Statements (;) and Blocks ({}) of a program that was written in a C-Family language (Java, CSharp, C, Cpp, etc. ). It is supposed to be a method to browse the source code of a large and unfamiliar software. Although it is meant to generate browsable, nested reports, I found that it does not have to be browsable to be useful. The 'signature' of a program is the simplest form of visualization one can create and can be interpreted as a simple form of code metric. Rather than installing NDepend or a large measurement solution on a foreign machine, you can easily checkout or download the 50 LOC python script and run it. It gives you immeadiate feedback on the health of a solution. Look out for patterns, repetitions and other things. You can quickly identify unhealthy spots.

Use
===
For a C# program, the signature might look like this:

``LogOff.cs (21): ;{{{}{}{;;}}}
MainViewModel.cs (103): ;;;;;;{{;;;;;;;;;{;;;}{{;}{;}}{{;}{;}}{{;}{;}}
MainWindow.xaml.cs (42): ;;;;;{{;{;;;}{{;}}{;;;}{;;}}}
MenuItemExtension.cs (17): ;;;{{{{;}}}}
MouseWheelGesture.cs (56): ;{{{}{}{;;}{{{};}}{{{};}}{{{};}}{{{};}}{;;;{
MutuallyExclusiveCheckBoxes.cs (42): ;;{{{;}{;}{;;;;}{;}{;}{;}}}``

Each Line shows the signature of a single file like this:

``<Name> (Lines Of Code): ;;;; {;;} {;;}``

Where ;; are statements and { } are code blocks (Namespaces, Classes, Methods, If-Blocks and so on)

What to look out for
====================
In different languages different patterns emerge. 

``MainWindow.xaml.cs (42): ;;;;;{{;{;;;}{{;}}{;;;}{;;}}}``

Imports (Usings) 
----------------
This file is quite short (42) lines of code. The first couple of semicolons in a C# file are the usings. In Java these might be the imports. The first opening curly brace is usually the namespace, the second opening curly brace just after that is the class in that file. For the same most files end with }} (closing curly of the class and namespace). 

If the file starts with a myriad of semicolons you can tell without ever visiting the file, that the class inside must be doing a lot, since it has so many different namespaces to include. 

Constructors and Fields
-----------------------
Depending on the author's programming style, the first opening curly brace inside the class is the constructor. A big constructor is a bad thing. You want it to be short. Also look out for everything inbetween the constuctor and the namespace/class declaration, these are usually data fields. Too many data fields indicate a data structure, rather than an object and could show violations of the OO paradigm from a high level perspective.

Properties
----------
Especially auto properties in C#, which look like this:

``public string Name { get; set; }``

Result in a pattern like this {;;}. Just like a lot of data fields, too many properties could show that the class publishes data, rather than hiding it. 


Repetitions
-----------
Apart from the code-related patterns above, you should look out for anything that might look suspicious. The overall look of the report will indicate whether the solution is healthy. Many short files are good, too many big files are bad. Long streams of curlybraces indicate deep nesting (bad), long streams of semicolons indicate too many statements or too long methods (also bad, at least for OO code). The overall differences count. If you find something like this in a report, you might wan't to check it out:

``
MainViewModel.cs (180): ;;;;;;{{;;;;;;;;;{;;;}{{;}{;}}{{;}{;}}{{;}{;}}{{;}{;}}{;;;;;}{;;}{;}{;{;;}}{;}{;;}{;;;;}}}{;;}{;}{;{;;}}{;}{;;}{;;;;}}}{;;}{;}{;{;;}}{;}{;;}{;;;;}}}
MainWindow.xaml.cs (42): ;;;;;{{;{;;;}{{;}}{;;;}{;;}}}
``

Look out for averages. If the overall solution contains hundreds of files with an average LOC count of 50 and suddenly there is a file with LOC > 3000, you might want to check out exactly that file. Maybe some time I will actually make this browsable with hyperlinks, but so far it's already a really helpful tool to get a good overview for a random solution. Thanks, Ward! 