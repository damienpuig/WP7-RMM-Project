WP7-RMM-Project
===============

A Windows Phone Banking Accounts Manager. The application is obviously developed in C#.
The project has been done for a Windows Phone Hackathon in Paris.


The project is composed of 3 major layers:
- Data Layer (Model)
- Business Layer (Business services)
- Phone Layer (Which is Interface implementation)



Several issues has been revealed developing this type of application:
- Strong influence of the code on battery and therefore didn't have to use object implementation when not needed.
- Heavy implementation using Services (And Simple IoC).
- No presence of Object Mapper on WP7 (Automapper using Reflection, mapping has been written using extensions..)


