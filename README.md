# Scan-A-Gator
**Scan-A-Gator** is a Windows application to facilitate analysis of calcium levels from ratiometric line scan images. It was created to rapidly browse and analyze folders created by Prairie View software, but can be easily adapted for other applications. This application makes use of [SciTIF](https://github.com/swharden/SciTIF) and [ScottPlot](https://github.com/swharden/ScottPlot) libraries.

**Project Status:** This project is very early in the development cycle, and the current program lacks important functionality. Code is stored here for backup, reference, and educational use only.

## Features
* Can analyze TIFs with data of arbitrary bit depths (e.g., 12-bit data in a 16-bit TIF)
* Semi-automated analysis facilitated by configurable default settings
* Graphs are fully interactive
* Linescan data is saved in CSV format
* Analysis parameters are saved as XML
* Analytical routines are encapsulated and easy to review or modify

## Screenshots
![](src/ScanAGator/screenshot.png)
