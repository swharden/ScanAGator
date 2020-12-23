
## Theory

A calcium-sensitive fluorophore will increase fluorescence intensity as calcium binds to it. However, raw intensity (**G**) is not always useful because larger structures have more fluorophore (not necessarily a higher concentration of calcium). To normalize to structure size, structures can be filled with a calcium-insensitive fluorophore (**R**), and **G/R** provides a relative (but not absolute) indication of calcium concentration. Note that **G** and **R** are scaled in AFU (arbitrary fluorescence units) which are not particularly useful or comparable across laboratories.

**G/R** can be scaled to the **G/R** of a known calcium concentration (or the **G/R** of a saturating dose) if the desire is to convert **G/R** into absolute calcium concentration. This is often not necessary experimentally, because if experiments demonstrate a significant difference in **G/R** between groups, you know the absolute calcium concentration is different too without having to calculate what it actually is. Note that **G/R** are scaled in relative units, and are constant as long as laser power and PMT gain are also constant. One way to normalize this (even across laboratories) is to adjust baseline PMT gain so both channels yield approximately the same basal fluorescence intensity.

To describe the effect of an action (e.g., electrical stimulation, synaptic release, or action potential) on relative calcium concentration, baseline **G/R** (calculated as the average **G/R** prior to the event) can be subtracted from the total **G/R**. This is the **ΔG/R**. 

Calculations here calculate **ΔG/R** as **Δ(G/R)**. This is necessary because photobleaching reduces the **R** (and it is assumed photobleaching affects both channels similarly). If photobleaching did not occur, **(ΔG)/R** would be a valid calculation. In the literature, most people simply report **ΔF/F**.
