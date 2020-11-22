Software application for time-lapse microscopy for Olympus C-series cameras for taking sequence of photos with different exposure time.

See Time-lapse microscopy http://en.wikipedia.org/wiki/Time-lapse_microscopy

"Program for capture of microscope images. It is designed for capturing of long image series, more detailed analysis of information content and processes occurring in the microscopy sample. In comparison to classical software tools, it has better possibilities in exposition setup, possibility to capture images with several camera setup at one time. One of typical applications is the possibility to capture several images with different exposition time and in this way to overcome the dynamic limit of camera. In the current version Olympus cameras are supported."
Tested on
Olympus C-7070 Wide Zoom
Olympus SP-500 Ultra Zoom



Computes Shannon entropy and Contrast measurement number. So it can be changed to compute something else for taken pictures.

http://en.wikipedia.org/wiki/Entropy_(information_theory)

http://en.wikipedia.org/wiki/Autofocus#Contrast_measurement

Enabled taking sequences with different focus position and exposure time.
Enabled set up of initial camera configuration.

Usage of current release
timelapseexp20090721working alpha usage (youtube)
https://www.youtube.com/watch?v=RZprcBNV41I


Rationale

Development of Control Software for Time-lapse Microscopy 2008 final
http://www.scribd.com/doc/17312181/Development-of-Control-Software-for-Timelapse-Microscopy-2008-final

Development of Software for Time-lapse Microscopy 2009 final
http://www.scribd.com/doc/17647294/Development-of-Software-for-Timelapse-Microscopy-2009-Final

Works on
Windows XP SP3 and .NET Framework Client Profile 3.5SP1
Depens on
NLog, AForge Framework, Visifire, Microsoft.Contracts
Roadmap
May be will be developed by me next summer.
Possibly would be done or I'd like to do:

    Using State pattern and something like FSM, WF. WPF interface(write WPF target for NLog) and MVVM. Taking away many UI bugs.
    Store and load experiment setups. Improved logging and feed back of data to user.
    Creating stub camera.
    .NET 4.0 usage.
    Using MEF,System.AddIn, Mono.Addins to support different calculations and cameras.
    Unit tests and tests of different usage scenarios. May be using White, NUnit, mock library.
    Translate part/all code to F#
    Support of E-series camera