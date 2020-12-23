# Scan-A-Gator

[![Build Status](https://dev.azure.com/swharden/swharden/_apis/build/status/swharden.Scan-A-Gator?branchName=master)](https://dev.azure.com/swharden/swharden/_build/latest?definitionId=13&branchName=master)

**ScanAGator is a Windows application to facilitate analysis of calcium levels from single-channel or ratiometric fluorescent line scan data.** It was created to browse and analyze data created by _Prairie View_ software, but can be adapted to navigate and analyze data from other sources.

![](src/ScanAGator-FRAMEWORK/screenshot.png)

Analysis results can be displayed and saved in a variety of formats. Interactive plots allow it to be inspected and saved as images. Values can be copied to the clipboard, or saved as CSV files for easy loading into scientific analysis software like OriginLab and MATLAB.

![](doc/graphics/Graph1making.png)

### Download
Scan-A-Gator can be downloaded from the [**releases page**](https://github.com/swharden/Scan-A-Gator/releases).


### Ratiometric Analysis

Fluorescence intensity of ratiometric indicators is  often reported as **ΔG/R**: the change in ratio of green fluorescence (**G**) to red fluorescence (**R**) relative to a baseline period. This metric is preferred because it is largely insensitive to variation in structure size and the effects of photobleaching. Scan-A-Gator calculates this curve using the following sequence:

* Linescan images are loaded into memory. In these 2D images time is on the horizontal axis, space is on the vertical axis. Each channel produces its own image.

* Structure markers are placed in the space domain to outline the structure of interest. Only pixels between these markers are be analyzed.

* Baseline markers are placed in the time domain to indicate the start and end of the baseline time region.

* 2D linescan image data are collapsed in the space domain to yield a 1D arrays for **G** and **R** representing mean pixel intensity for the structure over time.

* The ratio **G/R** is calculated for every point in the time domain.

* Baseline **G/R** is calculated as the average **G/R** between the baseline time points

* **Δ(G/R)** is obtained by subtracting the baseline **G/R** value from every point on the **G/R** curve.

* A 1D Gaussian low-pass filter is applied to **Δ(G/R)** and the filtered curve is refered to as **ΔF/F**. The size of the filter in the time domain is a defined number of milliseconds that is translated into pixel units according to the linescan acquisition rate.