# Scan-A-Gator
**ScanAGator** is a Windows application to facilitate analysis of calcium levels from ratiometric line scan images. It was created to rapidly browse and analyze folders created by Prairie View software, but can be easily adapted for other applications. This application makes use of [SciTIF](https://github.com/swharden/SciTIF) and [ScottPlot](https://github.com/swharden/ScottPlot) libraries.

![](src/ScanAGator/screenshot.png)

## Download
A click-to-run Scan-A-Gator (zipped EXE) is available with each release:\
https://github.com/swharden/Scan-A-Gator/releases/latest

## Features
* Can analyze TIFs with data of arbitrary bit depths (e.g., 12-bit data in a 16-bit TIF)
* Semi-automated analysis facilitated by configurable default settings
* Graphs are fully interactive
* Linescan data can be copied to clipboard (TSV) or saved (CSV)
* Analysis parameters for linescans can be saved/recalled as ini files
* Analytical process is fully documented and easy to review in code
* Prairie View version 4 and version 4 XML files are both supported
* Designed to integrate with scientific analysis software (e.g., Matlab and OriginLab)

![](doc/graphics/Graph1making.png)

## Theory


A calcium-sensitive fluorophore will increase fluorescence intensity as calcium binds to it. However, raw intensity (**G**) is not always useful because larger structures have more fluorophore (not necessarily a higher concentration of calcium). To normalize to structure size, structures can be filled with a calcium-insensitive fluorophore (**R**), and **G/R** provides a relative (but not absolute) indication of calcium concentration. Note that **G** and **R** are scaled in AFU (arbitrary fluorescence units) which are not particularly useful or comparable across laboratories.

**G/R** can be scaled to the **G/R** of a known calcium concentration (or the **G/R** of a saturating dose) if the desire is to convert **G/R** into absolute calcium concentration. This is often not necessary experimentally, because if experiments demonstrate a significant difference in **G/R** between groups, you know the absolute calcium concentration is different too without having to calculate what it actually is. Note that **G/R** are scaled in relative units, and are constant as long as laser power and PMT gain are also constant. One way to normalize this (even across laboratories) is to adjust baseline PMT gain so both channels yield approximately the same basal fluorescence intensity.

To describe the effect of an action (e.g., electrical stimulation, synaptic release, or action potential) on relative calcium concentration, baseline **G/R** (calculated as the average **G/R** prior to the event) can be subtracted from the total **G/R**. This is the **ΔG/R**. 

Calculations here calculate **ΔG/R** as **Δ(G/R)**. This is necessary because photobleaching reduces the **R** (and it is assumed photobleaching affects both channels similarly). If photobleaching did not occur, **(ΔG)/R** would be a valid calculation. In the literature, most people simply report **ΔF/F**.

#### Summary of Abbreviations
* **G** - intensity of a calcium-sensitive fluorophore 
* **R** - intensity of a calcium-insensitive fluorophore
* **G/R** - relative calcium level
* **ΔG/R** - change in relative calcium level with time
  * This is just G/R with the average baseline G/R subtracted from it
  * Be mindful that it is actually Δ(G/R) not (ΔG)/R
  

## Analysis Routine
The following sequence of events is performed in software to calculate the curves shown by ScanAGator. Links are provided to relevant source code where applicable. This example is written with a PrairieView LineScan folder in mind.

### Step 1: Acquire Scan Information
* **Scan the folder for important files** - PrairieView LineScan configuration XML file, reference images (TIFs in the References folder), and data images (TIFs in the main folder without "Source" in the filename).
* **Determine number of frames** - Some linescans have multiple repetitions. ScanAGator calls these "frames". Frame count is determined from the number of data images seen in the root folder. Data images are assigned R or G based on filename tags "Ch1" and "Ch2"
* **Determine linescan speed** - the linescan line period is stored in the PrairieView configuration XML file. Unfortunately our data comes from XML files generated with PrairieView 4 and 5 which have very different XML formats. For this reason, string parsing (instead of XML readers) was used to extract the value property from any node with a `scanlinePeriod` (V4) or `scanLinePeriod` (V5) key. This will break if PrairieView changes their XML format again. Relevant code: [PrairieLS.ParseXML()](https://github.com/swharden/Scan-A-Gator/blob/703fa24fa5bf4e1e287558d2b0d1694d66397dc8/src/ScanAGator/PrairieLS.cs#L179-L203)

### Step 2: Load TIFF Images as a floating-point data
The default TIF-reading libraries do not work well because they fail to display 12-bit data stored in 16-bit TIFs (this is why some images look dark in the windows photo viewer). A TIF reading class (modified from [SciTif](https://github.com/swharden/SciTIF)) was created for this purpose ([ImageData.cs](https://github.com/swharden/Scan-A-Gator/blob/703fa24fa5bf4e1e287558d2b0d1694d66397dc8/src/ScanAGator/ImageData.cs)).  

The ImageData class loads TIFs using TiffBitmapDecoder, stores values as floating point (to make math easier later), and has methods for auto-brightness, auto-contrast, and conversion to RGB Bitmap. This way linescan calculations can operate on the raw data values, and an 8-bit RGB bitmap can be acquired at any time for display. 

Methods were also created to collapse the 2D image data horizontally (to produce average intensity per time) or vertically (for automatic structure detection).

### Step 3: Configure the settings

A primary function of the ScanAGator user interface is to provide a way for the user to rapidly set, save, and recall linescan settings. When the Save button is pressed, these settings are stored as an INI file. When a linescan folder is selected, settings from the INI file are automatically loaded.

* `baseline1`, `baseline2` - pixel positions of the baseline region (Y, time)
* `structure1`, `structure2` - pixel positions of the structure to analyze (X, position)
* `filterPx` - size of the gaussian filter (in pixels) to apply to the data in the time domain

#### Default Baseline
When loading a LineScan folder without a save file, the baseline defaults to 0-10% of the LineScan time. This behavior is defined in [PrairieLS.LoadDefaultSettings()](https://github.com/swharden/Scan-A-Gator/blob/703fa24fa5bf4e1e287558d2b0d1694d66397dc8/src/ScanAGator/PrairieLS.cs#L255-L258)

#### Automatic Structure Detection
When loading a LineScan folder without a save file, or when _auto-select structure_ is selected from the settings menu, the following sequence of actions is performed. This behavior is defined by [PrairieLS.StructureAutoDetect()](https://github.com/swharden/Scan-A-Gator/blob/703fa24fa5bf4e1e287558d2b0d1694d66397dc8/src/ScanAGator/PrairieLS.cs#L264-L282)
* Green channel image data is collapsed vertically
* The brightest value (indicating position of the brightest column) is recorded.
* `structure1` and `structure2` set to 3px away from this brightest point

_Possible improvement: Structure detection could improve by performing a 1D Gaussian filter to the collapsed image data to produce a more accurate center-structure bright point._

_Possible improvement: Rather than using a fixed distance from center, the structure bounds could be stepped away from center one pixel at a time, and left in place when the intensity reaches something like half-max. Minimum intensity in this case cannot be considered as zero, but rather the noise floor (perhaps the 10 percentile value)._

### Step 4: Calculate ΔF/F

This sequence of events is used to convert two linescan images into a **ΔF/F** curve. The relevant code is brief and easy to review: [PrairieLS.Analyze()](https://github.com/swharden/Scan-A-Gator/blob/703fa24fa5bf4e1e287558d2b0d1694d66397dc8/src/ScanAGator/PrairieLS.cs#L394-L428)

* Red and green linescan image data are each collapsed (in the space domain) to yield a 1D array of average fluorescence per pixel (in the time domain). Note that the whole image isn't collected and collapsed, but rather just the pixels between `structure1` and `structure2`. These become the raw PMT **R** and **G** arrays.
* **G/R** is calculated for every point in the time domain.
* Baseline **G/R** is calculated (as the average raw **G/R** between `baseline1` and `baseline2`).
* **Δ(G/R)** is created by subtracting the baseline value from every point on the **G/R** curve.
* A 1D Gaussian filter is applied to **Δ(G/R)** and the output saved as a separate filtered curve **f[Δ(G/R)]**.
* Peak **ΔF/F** is calculated as the maximum value of the filtered curve.

_Note: Changing the value of the filter does not affect baseline subtraction, structure detection, or raw ΔG/R calculation - it only affects the shape of the filtered curve, and the peak ΔF/F displayed in the GUI._