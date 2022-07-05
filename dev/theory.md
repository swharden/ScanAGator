
## Theory

A calcium-sensitive fluorophore will increase fluorescence intensity as calcium binds to it. However, raw intensity (**G**) is not always useful because larger structures have more fluorophore (not necessarily a higher concentration of calcium). To normalize to structure size, structures can be filled with a calcium-insensitive fluorophore (**R**), and **G/R** provides a relative (but not absolute) indication of calcium concentration. Note that **G** and **R** are scaled in AFU (arbitrary fluorescence units) which are not particularly useful or comparable across laboratories.

**G/R** can be scaled to the **G/R** of a known calcium concentration (or the **G/R** of a saturating dose) if the desire is to convert **G/R** into absolute calcium concentration. This is often not necessary experimentally, because if experiments demonstrate a significant difference in **G/R** between groups, you know the absolute calcium concentration is different too without having to calculate what it actually is. Note that **G/R** are scaled in relative units, and are constant as long as laser power and PMT gain are also constant. One way to normalize this (even across laboratories) is to adjust baseline PMT gain so both channels yield approximately the same basal fluorescence intensity.

To describe the effect of an action (e.g., electrical stimulation, synaptic release, or action potential) on relative calcium concentration, baseline **G/R** (calculated as the average **G/R** prior to the event) can be subtracted from the total **G/R**. This is the **ΔG/R**. 

Calculations here calculate **ΔG/R** as **Δ(G/R)**. This is necessary because photobleaching reduces the **R** (and it is assumed photobleaching affects both channels similarly). If photobleaching did not occur, **(ΔG)/R** would be a valid calculation. In the literature, most people simply report **ΔF/F**.

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