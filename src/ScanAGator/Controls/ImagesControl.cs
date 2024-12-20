﻿using ScanAGator.Imaging;
using ScanAGator.Prairie;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ScanAGator.Controls;

public partial class ImagesControl : UserControl
{
    Prairie.ParirieXmlFile? PvXml = null;
    List<string> ImagePaths = [];

    private StructureRange _StructureRange;
    public StructureRange StructureRange

    {
        get => _StructureRange;
        set
        {
            _StructureRange = value;
            LoadSelectedImage();
        }
    }

    public ImagesControl()
    {
        InitializeComponent();
        hScrollBar1.ValueChanged += (s, e) => LoadSelectedImage();
        trackBar1.ValueChanged += (s, e) => LoadSelectedImage();
        pictureBox1.MouseClick += PictureBox1_MouseClick;
    }

    private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
        {
            ContextMenu context = new();

            MenuItem mi1 = new() { Text = $"Show in Explorer" };
            mi1.Click += (s, e) =>
            {
                System.Diagnostics.Process.Start("explorer.exe", $"/select, \"{ImagePaths[hScrollBar1.Value]}\"");
            };
            context.MenuItems.Add(mi1);

            MenuItem mi2 = new() { Text = $"Copy Image" };
            mi2.Click += (s, e) =>
            {
                Clipboard.SetImage(pictureBox1.Image);
            };
            context.MenuItems.Add(mi2);

            context.Show(this, e.Location);
        }
    }

    public void LoadSelectedImage()
    {
        if (!ImagePaths.Any())
        {
            pictureBox1.Image = null;
            label1.Text = "no valid images";
            return;
        }

        string imagePath = ImagePaths[hScrollBar1.Value];
        double brightness = (double)trackBar1.Value / 100;
        Bitmap bmp = ImageDataTools.ReadTif_ST(imagePath, brightness);
        ImageDrawing.DrawLinescan(bmp, PvXml, StructureRange);
        Image oldImage = pictureBox1.Image;
        pictureBox1.Image = bmp;
        oldImage?.Dispose();

        string title = Path.GetFileName(imagePath);
        if (title.StartsWith("LineScan-") && title.Contains("-Cycle"))
            title = "Cycle" + title.Split(new string[] { "Cycle" }, StringSplitOptions.None)[1];
        label1.Text = title;
    }

    public void SelelctBestImage()
    {
        for (int i = 0; i < ImagePaths.Count; i++)
        {
            string path = ImagePaths[i];
            string filename = Path.GetFileName(path);
            if (filename.Contains("-8bit-") && filename.Contains("-Ch1-"))
            {
                SelectImage(i);
                return;
            }
        }

        SelectImage(0);
    }

    private void SelectImage(int newValue)
    {
        int oldValue = hScrollBar1.Value;
        hScrollBar1.Value = newValue;
        if (newValue == oldValue)
            LoadSelectedImage();
    }

    public void SetLinescanFolder(string? linescanFolder)
    {
        if (linescanFolder is null)
            return;

        FolderContents contents = new(linescanFolder);
        PvXml = new ParirieXmlFile(contents.XmlFilePath);

        List<string> imagePaths = [];

        string referenceFolder = Path.Combine(linescanFolder, "References");
        if (Directory.Exists(referenceFolder))
        {
            imagePaths.AddRange(Directory.GetFiles(referenceFolder, "*.tif"));
            imagePaths.AddRange(Directory.GetFiles(referenceFolder, "*.jpg"));
            imagePaths.AddRange(Directory.GetFiles(referenceFolder, "*.bmp"));
            imagePaths.AddRange(Directory.GetFiles(referenceFolder, "*.png"));
        }

        ImagePaths.Clear();
        ImagePaths.AddRange(imagePaths.Where(x => File.Exists(x)));

        if (!ImagePaths.Any())
        {
            hScrollBar1.Value = 0;
            hScrollBar1.Maximum = 0;
            LoadSelectedImage();
            return;
        }

        hScrollBar1.Value = Math.Min(hScrollBar1.Value, ImagePaths.Count - 1);
        hScrollBar1.Maximum = ImagePaths.Count - 1;
        SelelctBestImage();
    }
}
