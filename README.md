XMLGenerator
============

A WinForms tool . Worked in C# , the purpose of the project was to compare the parts of XML files that should be identical and detect the differences
Some part of the code is experimental, it doesn't really contribute to the goal of the project. This work was done during my internship at RickCo in Holland.
The idea of the tool is to compare the RRP and A&O XML files. We are noticing the differences in both clcin and clcout files for various pension packets that are used by A&O Services. The files follow this format: 
personID;associationID;pensionpacket.clcin (or clcout)
In situation where a person doesn’t have a partner ( or child) or the packet itself is not appropriate, the associationID is given value unknown. 
The data we have from A&O is  not always in a xml format, which complicates the things a bit as we need to get the information needed from a database and transform it to an excel files which we then compare with the xml from RRP. 
The tool have several sections 
•	Form for clcin and clcout comparison for all the pension packets for the ES and GBS phase
•	Form for clcout comparison for all the pension packets for mutatieverloop GBS-ES
•	Form for generation of excel files with person data  from a huge A&O database for ES, GBS and BS phase
