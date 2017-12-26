***Gadsden reporting engine***
Objective is to create a framework which holds global .net functionality in a 
relatively unchanging and comprehensive package, and executes Iron Python periodically 
(like a lambada or function) during the execution of reports. This allows the user 
to not need to repeat code for periodic services while simultaneously concentrating 
a whole suite of applications that are typically one off's scattered around the company. 
Even the deprecation of services is managed here. 

Things repetatively offered by periodic programs should be offered globally and 
exposed to the embedded python. 

Exceptions should not take down all the reporting functionality. 