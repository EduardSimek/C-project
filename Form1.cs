using System;
using System.Drawing;
using System.Diagnostics;  // stopwatch
using System.Windows.Forms;
using ZedGraph;

using Emgu.CV;
using Emgu.CV.Structure;


using AForge.Imaging.Filters;
using AForge;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Linq;

namespace WindowsFormsApp12
{
    public partial class Form1 : Form
    {
        // The heading * means that both libraries for image processing support the certain filter

        #region Grayscale transforamtion - EmguCV * 

        Image<Gray, byte> img;

        #endregion
        #region Grayscale transformation - AFORGE.NET * 

        Bitmap grayImage;
        Grayscale filter;

        #endregion

        #region HSV - EmguCV

        Image<Hsv, byte> img8;
        Image<Bgr, byte> img9;

        #endregion

        #region LUV - EmguCV

        Image<Luv, byte> img10;
        Image<Bgr, byte> img11;

        #endregion

        #region XYZ - EmguCV

        Image<Xyz, byte> img12;
        Image<Bgr, byte> img13;

        #endregion

        #region LAB - EmguCV

        Image<Lab, byte> img14;
        Image<Bgr, byte> img15;

        #endregion

        #region HLS - EmguCV * 

        Image<Hls, byte> img1;
        Image<Bgr, byte> img2;

        #endregion
        #region HLS - AFORGE.NET * 

        HSLFiltering filter1;
        Bitmap rr1;

        #endregion

        #region Ycc - EmguCV * 

        Image<Ycc, byte> img3;
        Image<Bgr, byte> img4;

        #endregion
        #region YcbCr - AFORGE.NET * 

        YCbCrFiltering filter2;
        Bitmap YCbCr;

        #endregion

        # region Edge detection - EmguCV

        Image<Gray, Byte> gray;
        Image<Gray, Byte> canny;

        #endregion
        #region Edge detection - AFORGE.NET * 

        CannyEdgeDetector filter3;
        Bitmap canny3;
        Bitmap canny4;

        #endregion

        #region Image smoothing - EmguCV * 

        Image<Bgr, Byte> image;
        Image<Bgr, Byte> gauss;

        #endregion
        #region Image smoothing - AFORGE.NET * 

        AdaptiveSmoothing filter4;
        Bitmap r;

        #endregion

        #region Erosion - EmguCV * 
        Image<Bgr, byte> img6;

        #endregion
        #region Erosion - AFORGE.NET * 
        Erosion filter6;
        Bitmap Erosion;

        #endregion 

        #region Dilatation - EmguCV * 

        Image<Bgr, byte> img5;

        #endregion
        #region Dilatation - AFORGE.NET * 

        Dilatation filter5;
        Bitmap Dilatation;

        #endregion 

        #region Binary thresholding - EmguCV * 

        Image<Gray, byte> img7;
        Image<Gray, byte> imgBinarize;

        #endregion
        #region Binary thresholding - AFORGE.NET * 

        Threshold filter7;
        Bitmap c1;
        Bitmap c2;

        #endregion

        int number = 10;
        int second_number = 231;
        int tr_max = 400;
        public Form1()
        {
            InitializeComponent();
        }

        double[] y = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private void hodnoty_y()
        {
            double max = y.Max();
            double min = y.Min();
            double sum = y.Sum();
            double priem = y.Average();

            MessageBox.Show("The highest value of the array is " + max + System.Environment.NewLine +
                                 "the lowest value of the array is " + min + System.Environment.NewLine +
                                 "the sum of the numeric array is " + sum + System.Environment.NewLine +
                                 "the average value of the numeric array is " + priem + " .");

            label16.Text = "The highest value of the array (EmguCV) is " + max + ".";
            label17.Text = "The lowest value of the array (EmguCV) is " + min + ".";
            label18.Text = "The sum of the numeric array (EmguCV) is " + sum + ".";
            label19.Text = "The average value of the numeric array (EmguCV) is " + priem + ".";
        }

        double[] y2 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private void hodnoty_y2()
        {
            double max = y2.Max();
            double min = y2.Min();
            double sum = y2.Sum();
            double priem = y2.Average();

            MessageBox.Show("The highest value of the array is " + max + System.Environment.NewLine +
                                 "the lowest value of the array is " + min + System.Environment.NewLine +
                                 "the sum of the numeric array is " + sum + System.Environment.NewLine +
                                 "the average value of the numeric array is " + priem + " .");

            label20.Text = "The highest value of the array (AForge.NET) is" + max + ".";
            label21.Text = "The lowest value of the array (AForge.NET) is" + min + ".";
            label22.Text = "The sum of the numeric array (AForge.NET) is " + sum + ".";
            label23.Text = "The average value of the numeric array (AForge.NET) is " + priem + ".";
        }
        private void remove()
        {
            zedGraphControl1.GraphPane.CurveList.Clear();
            zedGraphControl1.GraphPane.GraphObjList.Clear();
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private void falsed()
         {

            trackBar1.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false; 
         }

        private void time_format_emgucv()
        {
            label3.Text = "Image processing time for EmguCV is " + String.Format("{0,00}", abcd.Elapsed) + " s.";
            label4.Text = "Image processing time for EmguCV is  " + String.Format("{0,00}", abcd.ElapsedMilliseconds) + " ms.";

        }

        private void time_format_aforgenet()
        {
            label9.Text = "Image processing time for AForge.NET is " + String.Format("{0,00}", AForge.Elapsed) + " s.";
            label10.Text = "Image processing time for AForge.NET is " + String.Format("{0,00}", AForge.ElapsedMilliseconds) + " ms.";

        }
        private void DrawGraph(double[] y)
        {
            string[] schools = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            var pane = zedGraphControl1.GraphPane;
            pane.Title.Text = "EmguCV vs AForge.NET";
            pane.XAxis.Title.Text = "Number of measurements (-)";
            pane.YAxis.Title.Text = "Processing time (ms)";
            pane.XAxis.Scale.IsVisible = true;
            pane.YAxis.Scale.IsVisible = true;
            pane.XAxis.MajorGrid.IsVisible = true;
            pane.YAxis.MajorGrid.IsVisible = true;
            pane.XAxis.Scale.TextLabels = schools;
            pane.XAxis.Type = AxisType.Text;
            BarItem pointsCurve = pane.AddBar("EmguCV", null, y, Color.Red);
            pointsCurve.IsVisible = true;
            pane.AxisChange();
            zedGraphControl1.Refresh();
            this.Controls.Add(zedGraphControl1);
        }
        private void DrawGraph2(double[] y2)
        {
            string[] schools = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            var pane = zedGraphControl1.GraphPane;
            pane.Title.Text = "EmguCV vs AForge.NET";
            pane.XAxis.Title.Text = "Number of measurements (-)";
            pane.YAxis.Title.Text = "Processing time (ms)";
            pane.XAxis.Scale.IsVisible = true;
            pane.YAxis.Scale.IsVisible = true;
            pane.XAxis.MajorGrid.IsVisible = true;
            pane.YAxis.MajorGrid.IsVisible = true;
            pane.XAxis.Scale.TextLabels = schools;
            pane.XAxis.Type = AxisType.Text;
            BarItem pointsCurve = pane.AddBar("AForge.NET", null, y2, Color.Blue);
            pointsCurve.IsVisible = true;
            pane.AxisChange();
            zedGraphControl1.Refresh();
            this.Controls.Add(zedGraphControl1);
        }
        //loading the image (bmp)
        Bitmap bmp = new Bitmap("c:\\Emgucv2\\configEmguCV\\abc.jpg");
        Stopwatch abcd = new Stopwatch();
        Stopwatch AForge = new Stopwatch();              
        private void button7_Click(object sender, EventArgs e)
        {
            falsed();          
            OpenFileDialog a = new OpenFileDialog();
            a.Filter = "*BMP Image|*.bmp|*JPG Image|*.jpg";

            if (a.ShowDialog() == DialogResult.OK)
            {
                abcd = Stopwatch.StartNew();

                #region Loading the image from the PC (bmp)

                pictureBox1.Image = bmp;
                bmp = new Bitmap(a.FileName);
                pictureBox1.Image = bmp;

                #endregion

                abcd.Stop();

                label7.Text = "Loading time is " + String.Format("{0,00}", abcd.Elapsed) + " s.";
                label8.Text = "Loading time is " + String.Format("{0,00}", abcd.ElapsedMilliseconds) + " ms.";
                label5.Text = ("Image resolution is " + bmp.Width + " x " + bmp.Height);
                comboBox1.FormattingEnabled = true;
                comboBox1.Items.Clear();
                comboBox1.Items.AddRange(new object[]
                    {
                   "0. Grayscale transformation* ",
                   "1. HSV",
                   "2. HLS * ",
                   "3. LUV",
                   "4. Ycc * ",
                   "5. XYZ",
                   "6. LAB",
                   "7. Edge detection * ",
                   "8. Image smoothing* ",
                   "9. Thresholding * ",
                   "10. Dilatation * ",
                   "11. Erosion * "
                    }
                );
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)     
        {           
            // ComboBox Original image ---> Grayscale (EmguCV)
            if (comboBox1.SelectedIndex == 0)
            {
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                remove();

                for (int i = 1; i <= number; i++)
                {

                img = new Image<Gray, byte>(bmp);
                pictureBox2.Image = img.ToBitmap();

                abcd = Stopwatch.StartNew();

                #region EmguCV - Grayscale

                img = new Image<Gray, byte>(bmp);
                pictureBox2.Image = img.ToBitmap();

                #endregion

                abcd.Stop();

                MessageBox.Show("Number of measurement is " + i + "." + " and image processing time is " + String.Format("{0,00}", abcd.ElapsedMilliseconds) + " ms.");

                if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                time_format_emgucv();

                }
                DrawGraph(y);
                hodnoty_y();
            }

            // Combobox Original image ---> Grayscale (AForge.NET)
            if (comboBox1.SelectedIndex == 0)

            {
                falsed();

                for (int i = 1; i <= number; i++)
                {

                filter = new Grayscale(0.2125, 0.7154, 0.0721);
                grayImage = filter.Apply(bmp);

                AForge = Stopwatch.StartNew();

                #region AForge.NET - Grayscale
     
                filter = new Grayscale(0.2125, 0.7154, 0.0721);
                grayImage = filter.Apply(bmp);
                pictureBox3.Image = grayImage;

                #endregion   

                AForge.Stop();

                MessageBox.Show("Number of measurement is " + i + "." + " and image processing time is " + String.Format("{0,00}", AForge.ElapsedMilliseconds) + " ms.");

                if (i > 0) y2[i - 1] = Convert.ToDouble(AForge.Elapsed.TotalMilliseconds);

                time_format_aforgenet();



                }
                DrawGraph2(y2);
                hodnoty_y2();

                
            }

            // Combobox Original image ---> HSV (EmguCV)
            if (comboBox1.SelectedIndex == 1)
            {
                pictureBox3.Image = null;
                label9.Text = " ";
                label10.Text = " ";
                falsed();
                remove();

                for (int i = 1; i <= number; i++)
                {

                img8 = new Image<Hsv, byte>(bmp);
                img9 = new Image<Bgr, byte>(bmp);

                abcd = Stopwatch.StartNew();

                #region EmguCV - HSV

                img8 = new Image<Hsv, byte>(bmp);
                img9 = new Image<Bgr, byte>(bmp);
                img9.Data = img8.Data;
                pictureBox2.Image = img9.Bitmap;

                #endregion

                abcd.Stop();

                MessageBox.Show("Number of measurement is " + i + "." + " and image processing time is " + String.Format("{0,00}", abcd.ElapsedMilliseconds) + " ms.");

                if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                time_format_emgucv();

                }
                DrawGraph(y);
                hodnoty_y();
            }

            // Combobox Original image ---> HLS (EmguCV)
            if (comboBox1.SelectedIndex == 2)
            {
                falsed();
                remove();

                for (int i = 1; i <= number; i++)
                {
                    img1 = new Image<Hls, byte>(bmp);
                    img2 = new Image<Bgr, byte>(bmp);

                    abcd = Stopwatch.StartNew();

                    #region EmguCV - HLS

                    img1 = new Image<Hls, byte>(bmp);
                    img2 = new Image<Bgr, byte>(bmp);
                    img2.Data = img1.Data;
                    pictureBox2.Image = img2.Bitmap;

                    #endregion

                    abcd.Stop();

                    MessageBox.Show("Number of measurement is " + i + "." + " and image processing time is " + String.Format("{0,00}", abcd.ElapsedMilliseconds) + " ms.");

                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                    time_format_emgucv();

                }
                DrawGraph(y);
                hodnoty_y();
            }

            // Combobox Original image ---> HLS (AForge.NET)
            if (comboBox1.SelectedIndex == 2)
            {
                falsed();

                for (int i = 1; i <= number; i++)
                {

                filter1 = new HSLFiltering();
                filter1.Hue = new AForge.IntRange(340, 20);
                filter1.UpdateLuminance = false;
                filter1.UpdateSaturation = false;
                rr1 = filter1.Apply(bmp);

                AForge = Stopwatch.StartNew();

                #region AForge.NET - HLS

                filter1 = new HSLFiltering();
                filter1.Hue = new AForge.IntRange(340, 20);    
                filter1.UpdateLuminance = false;               
                filter1.UpdateSaturation = false;   
                rr1 = filter1.Apply(bmp);            
                pictureBox3.Image = rr1;    
                    
                #endregion

                AForge.Stop();

                    MessageBox.Show("Number of measurement is " + i + "." + " and image processing time is " + String.Format("{0,00}", AForge.ElapsedMilliseconds) + " ms.");

                    if (i > 0) y2[i - 1] = Convert.ToDouble(AForge.Elapsed.TotalMilliseconds);

                time_format_aforgenet();

                }
                DrawGraph2(y2);
                hodnoty_y2();
            }

            // Combobox Original image ---> LUV (EmguCV)
            if (comboBox1.SelectedIndex == 3)
            {
                pictureBox3.Image = null;
                label9.Text = " ";
                label10.Text = " ";
                falsed();
                remove();

                for (int i = 1; i <= number; i++)
                {

                img10 = new Image<Luv, byte>(bmp);
                img11 = new Image<Bgr, byte>(bmp);

                abcd = Stopwatch.StartNew();

                #region EmguCV - LUV

                img10 = new Image<Luv, byte>(bmp);
                img11 = new Image<Bgr, byte>(bmp);
                img11.Data = img10.Data;
                pictureBox2.Image = img11.Bitmap;

                #endregion

                abcd.Stop();

                    MessageBox.Show("Number of measurement is " + i + "." + " and image processing time is " + String.Format("{0,00}", abcd.ElapsedMilliseconds) + " ms.");

                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                time_format_emgucv();

                }
                DrawGraph(y);
                hodnoty_y();
            }

            // Combobox Original image ---> Ycc (EmguCV)
            if (comboBox1.SelectedIndex == 4)
            {
                falsed();
                remove();

                for (int i = 1; i <= number; i++)
                {
                img3 = new Image<Ycc, byte>(bmp);
                img4 = new Image<Bgr, byte>(bmp);

                abcd = Stopwatch.StartNew();

                #region EmguCV - Ycc

                img3 = new Image<Ycc, byte>(bmp);
                img4= new Image<Bgr, byte>(bmp);
                img4.Data = img3.Data;
                pictureBox2.Image = img4.Bitmap;

                #endregion

                abcd.Stop();

                    MessageBox.Show("Number of measurement is " + i + "." + " and image processing time is " + String.Format("{0,00}", abcd.ElapsedMilliseconds) + " ms.");

                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                time_format_emgucv();

                }
                DrawGraph(y);
                hodnoty_y();
            }

            // Combobox Original image ---> YCbCr (AForge.NET)
            if (comboBox1.SelectedIndex == 4)
            {
                falsed();

                for (int i = 1; i <= number; i++)
                {

                    filter2 = new YCbCrFiltering();
                    filter2.Y = new AForge.Range(200, 225);
                    filter2.Cb = new AForge.Range(200, 225);
                    filter2.UpdateCr = false;
                    YCbCr = filter2.Apply(bmp);

                    AForge = Stopwatch.StartNew();

                    #region AFORGE.NET - YCbCr  
                    
                    filter2 = new YCbCrFiltering();
                    filter2.Y = new AForge.Range(200, 225);
                    filter2.Cb = new AForge.Range(200, 225);
                    filter2.UpdateCr = false;
                    YCbCr = filter2.Apply(bmp);
                    pictureBox3.Image = YCbCr;

                    #endregion

                    AForge.Stop();

                    MessageBox.Show("Number of measurement is " + i + "." + " and image processing time is " + String.Format("{0,00}", AForge.ElapsedMilliseconds) + " ms.");
                    if (i > 0) y2[i - 1] = Convert.ToDouble(AForge.Elapsed.TotalMilliseconds);

                    time_format_aforgenet();
                }
                DrawGraph2(y2);
                hodnoty_y2();
            }

            // Combobox Original image ---> XYZ (EmguCV)
            if (comboBox1.SelectedIndex == 5)
            {
                pictureBox3.Image = null;
                label9.Text = " ";
                label10.Text = " ";
                falsed();
                remove();

                for (int i = 1; i <= number; i++)
                {

                    img12 = new Image<Xyz, byte>(bmp);
                    img13 = new Image<Bgr, byte>(bmp);

                    abcd = Stopwatch.StartNew();

                    #region EmguCV - XYZ

                    img12 = new Image<Xyz, byte>(bmp);
                    img13 = new Image<Bgr, byte>(bmp);
                    img13.Data = img12.Data;
                    pictureBox2.Image = img13.Bitmap;

                    #endregion

                    abcd.Stop();

                    MessageBox.Show("Number of measurement is " + i + "." + " and image processing time is " + String.Format("{0,00}", abcd.ElapsedMilliseconds) + " ms.");
                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                    time_format_emgucv();

                }
                DrawGraph(y);
                hodnoty_y();
            }

            // Combobox Original image ---> LAB (EmguCV)
            if (comboBox1.SelectedIndex == 6)
            {
                pictureBox3.Image = null;
                label9.Text = " ";
                label10.Text = " ";
                falsed();
                remove();

                for (int i = 1; i <= number; i++)
                {
                    img14 = new Image<Lab, byte>(bmp);
                    img15 = new Image<Bgr, byte>(bmp);

                    abcd = Stopwatch.StartNew();

                    #region EmguCV - LAB

                    img14 = new Image<Lab, byte>(bmp);
                    img15 = new Image<Bgr, byte>(bmp);
                    img15.Data = img14.Data;
                    pictureBox2.Image = img15.Bitmap;

                    #endregion

                    abcd.Stop();

                    MessageBox.Show("Number of measurement is " + i + "." + " and image processing time is " + String.Format("{0,00}", abcd.ElapsedMilliseconds) + " ms.");

                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                    time_format_emgucv();

                }
                DrawGraph(y);
                hodnoty_y();
            }

            // Combobox Original image---> Edge detection (EmguCV)
            if (comboBox1.SelectedIndex == 7)
            {
                trackBar1.Enabled = true;
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                remove();

                label6.Text = "Edge detection (EmguCV)";

                for (int i = 1; i <= number; i++)
                {
                    
                    gray = new Image<Gray, Byte>(bmp);
                    canny = gray.Canny(11, 30);

                    abcd = Stopwatch.StartNew();
                  
                    #region EmguCV - Edge detection

                    gray = new Image<Gray, Byte>(bmp);
                    canny = gray.Canny(11, 30);
                    pictureBox2.Image = canny.ToBitmap();

                    #endregion

                    abcd.Stop();

                    MessageBox.Show("Number of measurement is " + i + "." + " and image processing time is " + String.Format("{0,00}", abcd.ElapsedMilliseconds) + " ms.");

                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                    time_format_emgucv();

                }
                DrawGraph(y);
                hodnoty_y();
            }

            // Combobox Original image ---> Edge detection   (AForge.NET)
            if (comboBox1.SelectedIndex == 7)
            {
                
                trackBar1.Enabled = true;
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                
                for (int i = 1; i <= number; i++)
                {

                   var bmp16bpp = Grayscale.CommonAlgorithms.BT709.Apply(bmp);
                   filter3 = new CannyEdgeDetector();
                   filter3.HighThreshold = 11;
                   Bitmap canny3 = filter3.Apply(bmp16bpp);

                   AForge = Stopwatch.StartNew();

                   #region AForge.NET - Edge detection

                   var bmp8bpp = Grayscale.CommonAlgorithms.BT709.Apply(bmp);
                   filter3 = new CannyEdgeDetector();
                   filter3.HighThreshold = 11;
                   Bitmap canny4 = filter3.Apply(bmp8bpp);
                   pictureBox3.Image = canny4;

                   #endregion

                   AForge.Stop();

                    MessageBox.Show("Number of measurement is " + i + "." + " and image processing time is " + String.Format("{0,00}", AForge.ElapsedMilliseconds) + " ms.");
                    if (i > 0) y2[i - 1] = Convert.ToDouble(AForge.Elapsed.TotalMilliseconds);

                    time_format_aforgenet();
                }
                DrawGraph2(y2);
                hodnoty_y2();
            }


            // Combobox Original image ---> Image smoothing  (EmguCV)
            if (comboBox1.SelectedIndex == 8)
            {
                trackBar1.Enabled = false;
                groupBox2.Enabled = false;
                groupBox3.Enabled = true;
                remove();

                for (int i = 1; i <= 10; i++)
                {
                    image = new Image<Bgr, byte>(bmp);
                    gauss = image.SmoothGaussian(3, 3, 34.3, 45.3);

                    abcd = Stopwatch.StartNew();

                    #region EmguCV - Image smoothing

                    image = new Image<Bgr, byte>(bmp);
                    Image<Bgr, Byte> blur = image.SmoothBlur(10, 10, true);
                    Image<Bgr, Byte> bilat = image.SmoothBilateral(7, 255, 34);  
                    gauss= image.SmoothGaussian(3, 3, 34.3, 45.3);
                    Image<Bgr, Byte> mediansmooth = image.SmoothMedian(15);
                    pictureBox2.Image = gauss.ToBitmap();

                    #endregion

                    abcd.Stop();

                    MessageBox.Show("Number of measurement is " + i + "." + " and image processing time is " + String.Format("{0,00}", abcd.ElapsedMilliseconds) + " ms.");

                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                    time_format_emgucv();

                }
                DrawGraph(y);
                hodnoty_y();
            }

            // Combobox Original image ---> Image smoothing (AForge.NET)
            if (comboBox1.SelectedIndex == 8)
            {
              img_smoothing = new Image<Bgr, byte>(bmp);            
                for (int i = 1; i <= number; i++)
                {
                    filter4 = new AdaptiveSmoothing();
                    filter4.Factor = 11;
                    r = filter4.Apply(bmp);

                    AForge = Stopwatch.StartNew();

                    #region AForge.NET - Image smoothing
                    
                    filter4 = new AdaptiveSmoothing();
                    filter4.Factor = 11;
                    r = filter4.Apply(bmp);
                    pictureBox3.Image = r;

                    #endregion

                    AForge.Stop();

                    MessageBox.Show("Number of measurement is " + i + "." + " and image processing time is " + String.Format("{0,00}", AForge.ElapsedMilliseconds) + " ms.");

                    if (i > 0) y2[i - 1] = Convert.ToDouble(AForge.Elapsed.TotalMilliseconds);

                    time_format_aforgenet();
                }
                DrawGraph2(y2);
                hodnoty_y2();
                trackBar1.Enabled = true;
                groupBox2.Enabled = false;               
          }

            // Combobox Original image ---> Dilatation (EmguCV)
            if (comboBox1.SelectedIndex == 10)
            {
                groupBox3.Enabled = false;        
                groupBox2.Enabled = false;
                trackBar1.Enabled = true;
                trackBar2.Enabled = false;
                trackBar3.Enabled = false;
                remove();

                for (int i = 1; i <= number; i++)
                {
                    img5 = new Image<Bgr, byte>(bmp);
                    pictureBox2.Image = img5.Dilate(1).Bitmap;

                    abcd = Stopwatch.StartNew();

                    #region EmguCV - Dilatation
                    
                    img5 = new Image<Bgr, byte>(bmp);           
                    pictureBox2.Image = img5.Dilate(1).Bitmap;

                    #endregion

                    abcd.Stop();

                    MessageBox.Show("Number of measurement is " + i + "." + " and image processing time is " + String.Format("{0,00}", abcd.ElapsedMilliseconds) + " ms.");

                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                    time_format_emgucv();
                }
                DrawGraph(y);
                hodnoty_y();
                label6.Text = "Dilatácia";
            }

            // Combobox Original image ---> Dilatation (AForge.NET)
            if (comboBox1.SelectedIndex == 10)
            {
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                trackBar1.Enabled = true;

                for (int i = 1; i <= number; i++)
                {
                    filter5 = new Dilatation();
                    Dilatation = filter5.Apply(bmp);

                    AForge = Stopwatch.StartNew();

                    #region AForge.NET - Dilatation
 
                    filter5 = new Dilatation();
                    Dilatation = filter5.Apply(bmp);
                    pictureBox3.Image = Dilatation;

                    #endregion

                    AForge.Stop();

                    MessageBox.Show("Number of measurement is " + i + "." + " and image processing time is " + String.Format("{0,00}", AForge.ElapsedMilliseconds) + " ms.");
                    if (i > 0) y2[i - 1] = Convert.ToDouble(AForge.Elapsed.TotalMilliseconds);

                    time_format_aforgenet();
                }              
                DrawGraph2(y2);
                hodnoty_y2();
            }

            // Combobox Original image ---> Erosion (EmguCV)
            if (comboBox1.SelectedIndex == 11)
            {
                groupBox3.Enabled = false;            
                groupBox2.Enabled = false;
                remove();

                for (int i = 1; i <= number; i++)
                {
                    img6 = new Image<Bgr, byte>(bmp);     
                    pictureBox2.Image = img6.Erode(1).Bitmap;

                    abcd = Stopwatch.StartNew();

                    #region EmguCV - Erosion

                    img6 = new Image<Bgr, byte>(bmp);          
                    pictureBox2.Image = img6.Erode(1).Bitmap;

                    #endregion

                    abcd.Stop();

                    MessageBox.Show("Number of measurement is " + i + "." + " and image processing time is " + String.Format("{0,00}", abcd.ElapsedMilliseconds) + " ms.");

                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                    time_format_emgucv();

                }
                DrawGraph(y);
                hodnoty_y();
                label6.Text = "Erózia";
            }

            // Combobox Original image ---> Erosion (AForge.NET)
            if (comboBox1.SelectedIndex == 11)
            {
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;

                for (int i = 1; i <= number; i++)
                {
                    filter6 = new Erosion();
                    Erosion = filter6.Apply(bmp);

                    AForge = Stopwatch.StartNew();

                    #region AForge.NET - Erosion

                    filter6 = new Erosion();
                    Erosion = filter6.Apply(bmp);
                    pictureBox3.Image = Erosion;

                    #endregion

                    AForge.Stop();

                    MessageBox.Show("Number of measurement is " + i + "." + " and image processing time is " + String.Format("{0,00}", AForge.ElapsedMilliseconds) + " ms.");

                    if (i > 0) y2[i - 1] = Convert.ToDouble(AForge.Elapsed.TotalMilliseconds);
                    time_format_aforgenet();

                }              
                DrawGraph2(y2);
                hodnoty_y2();
            }

            // Combobox Original image ---> Binary thresholding (EmguCV)
            if (comboBox1.SelectedIndex == 9)
            {
                groupBox2.Enabled = true;
                groupBox3.Enabled = false;
                remove();

                for (int i = 1; i <= number; i++)
                {
                    img7 = new Image<Gray, byte>(bmp);
                    imgBinarize = new Image<Gray, byte>(bmp.Width, bmp.Height, new Gray(0));

                    abcd = Stopwatch.StartNew();

                    #region EmguCV - Binary thresholding
           
                    img7 = new Image<Gray, byte> (bmp);
                    imgBinarize = new Image<Gray, byte>(bmp.Width, bmp.Height, new Gray(0));
                    CvInvoke.Threshold(img7, imgBinarize, 40, 255, Emgu.CV.CvEnum.ThresholdType.Binary);          
                    pictureBox2.Image = imgBinarize.ToBitmap();

                    #endregion

                    abcd.Stop();

                    MessageBox.Show("Number of measurement is " + i + "." + " and image processing time is " + String.Format("{0,00}", abcd.ElapsedMilliseconds) + " ms.");
                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                    time_format_emgucv();

                }           
                DrawGraph(y);
                hodnoty_y();
                label13.Text = "Binary thresholding (EmguCV)";
            }

            //Combobox Original image ---> Binary thresholding (AForge.NET)          
            if (comboBox1.SelectedIndex == 9)
            {
                groupBox3.Enabled = false;
                groupBox2.Enabled = true;
                trackBar1.Enabled = true;

                for (int i = 1; i <= number; i++)
                {
                    var bmp8bpp = Grayscale.CommonAlgorithms.BT709.Apply(bmp);
                    filter7 = new Threshold();
                    filter7.ThresholdValue = 40;
                    Bitmap c1 = filter7.Apply(bmp8bpp);

                    AForge = Stopwatch.StartNew();

                    #region AForge.NET - Binary thresholding

                    var bmp16bpp = Grayscale.CommonAlgorithms.BT709.Apply(bmp);
                    filter7= new Threshold();
                    filter7.ThresholdValue = 40;   
                    Bitmap c2 = filter7.Apply(bmp16bpp);
                    pictureBox3.Image = c2;

                    #endregion

                    AForge.Stop();

                    MessageBox.Show("Number of measurement is " + i + "." + " and image processing time is " + String.Format("{0,00}", AForge.ElapsedMilliseconds) + " ms.");

                    if (i > 0) y2[i - 1] = Convert.ToDouble(AForge.Elapsed.TotalMilliseconds);

                    time_format_aforgenet();
                }
                DrawGraph2(y2);
                hodnoty_y2();
                label6.Text = "Binary thresholding (AForge.NET)";
            }
        }
        private void button11_Click(object sender, EventArgs e)
        {
            label3.Text = String.Format("{0,00}", abcd.Elapsed);
            label4.Text = String.Format("{0,00}", abcd.ElapsedMilliseconds);
            label9.Text = String.Format("{0,00}", AForge.Elapsed);
            label10.Text = String.Format("{0,00}", AForge.ElapsedMilliseconds);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Form2 a = new Form2(label3.Text, label4.Text);
            a.Show(); 
        }

        private void button14_Click(object sender, EventArgs e)
        {
            label3.Text = " ";
            label4.Text = " ";
            label7.Text = " ";
            label8.Text = " ";
            label9.Text = " ";
            label10.Text = " ";
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            remove();
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
        }
        private void zedGraphControl1_Load(object sender, EventArgs e)
        {
        }
        private void button16_Click(object sender, EventArgs e)
        {
        }
        private void label1_Click(object sender, EventArgs e)
        {
        }
        private void label4_Click(object sender, EventArgs e)
        {
        }
        private void button13_Click(object sender, EventArgs e)
        {
            if (aaa.Checked == true)   // Binary hresholding (EmguCV)
            {
                remove();

                for (int i = 1; i <= number; i++)
                { 

                abcd = Stopwatch.StartNew();

                #region EmguCV - Binary thresholding

                Image<Gray, byte> img = new Image<Gray, byte>(bmp);
                imgBinarize = new Image<Gray, byte>(bmp.Width, bmp.Height, new Gray(0));
                CvInvoke.Threshold(img, imgBinarize, trackBar2.Value, 255, Emgu.CV.CvEnum.ThresholdType.Binary);
                pictureBox2.Image = imgBinarize.ToBitmap();

                #endregion 

                abcd.Stop();

                    MessageBox.Show("Number of measurement is " + i + "." + " and image processing time is " + String.Format("{0,00}", abcd.ElapsedMilliseconds) + " ms.");

                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                time_format_emgucv();

                }
                DrawGraph(y);
                hodnoty_y();
            }

            if (radioButton2.Checked == true) // Binary inversion thresholding (EmguCV)
            {
                remove();

                for (int i = 1; i <= number; i++)
                { 
                    abcd = Stopwatch.StartNew();

                    #region EmguCV - Binary inversion threshold

                    Image<Gray, byte> img = new Image<Gray, byte>(bmp);
                    imgBinarize = new Image<Gray, byte>(bmp.Width, bmp.Height, new Gray(0));
                    CvInvoke.Threshold(img, imgBinarize, trackBar2.Value, 255, Emgu.CV.CvEnum.ThresholdType.BinaryInv);
                    pictureBox2.Image = imgBinarize.ToBitmap();

                    #endregion

                    abcd.Stop();

                    MessageBox.Show("Number of measurement is " + i + "." + " and image processing time is " + String.Format("{0,00}", abcd.ElapsedMilliseconds) + " ms.");

                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                    time_format_emgucv();
                }
                DrawGraph(y);
                hodnoty_y();
            }

            if (radioButton3.Checked == true) // Thresholding mask (EmguCV)

            {
                remove();

                for (int i = 1; i <= number; i++)
                {
                    abcd = Stopwatch.StartNew();

                    #region EmguCV - Thresholding mask

                    Image<Gray, byte> img = new Image<Gray, byte>(bmp);
                    imgBinarize = new Image<Gray, byte>(bmp.Width, bmp.Height, new Gray(0));
                    CvInvoke.Threshold(img, imgBinarize, trackBar2.Value, 255, Emgu.CV.CvEnum.ThresholdType.Mask);
                    pictureBox2.Image = imgBinarize.ToBitmap();
                    #endregion

                    abcd.Stop();

                    MessageBox.Show("Number of measurement is " + i + "." + " and image processing time is " + String.Format("{0,00}", abcd.ElapsedMilliseconds) + " ms.");

                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                    time_format_emgucv();
                }
                DrawGraph(y);
                hodnoty_y();
            }

            if (radioButton4.Checked == true) // Threshold value (EmguCV)

            {
                remove();

                for (int i = 1; i <= number; i++)
                {
                    abcd = Stopwatch.StartNew();

                    #region EmguCV - Threshold value

                    Image<Gray, byte> img = new Image<Gray, byte>(bmp);
                    imgBinarize = new Image<Gray, byte>(bmp.Width, bmp.Height, new Gray(0));
                    CvInvoke.Threshold(img, imgBinarize, trackBar2.Value, 255, Emgu.CV.CvEnum.ThresholdType.Otsu);
                    pictureBox2.Image = imgBinarize.ToBitmap();

                    #endregion

                    abcd.Stop();

                    MessageBox.Show("Number of measurement is " + i + "." + " and image processing time is " + String.Format("{0,00}", abcd.ElapsedMilliseconds) + " ms.");

                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                    time_format_emgucv();
                }
                DrawGraph(y);
                hodnoty_y();            
            }

            if (radioButton5.Checked == true) // Zero thresholding (EmguCV)

            remove();
            {
                for (int i = 1; i <= number; i++)
                {
                    abcd = Stopwatch.StartNew();

                    #region EmguCV - Zero thresholding

                    Image<Gray, byte> img = new Image<Gray, byte>(bmp);
                    imgBinarize = new Image<Gray, byte>(bmp.Width, bmp.Height, new Gray(0));
                    CvInvoke.Threshold(img, imgBinarize, trackBar2.Value, 255, Emgu.CV.CvEnum.ThresholdType.ToZero);
                    pictureBox2.Image = imgBinarize.ToBitmap();

                    #endregion

                    abcd.Stop();

                    MessageBox.Show("Number of measurement is " + i + "." + " and image processing time is " + String.Format("{0,00}", abcd.ElapsedMilliseconds) + " ms.");

                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                    time_format_emgucv();
                }              
                DrawGraph(y);
                hodnoty_y();
                }

            if (radioButton6.Checked == true) // Zero inversion thresholding (EmguCV)

                {
                remove();

                for (int i = 1; i <= number; i++)
                {
                    abcd = Stopwatch.StartNew();

                    #region EmguCV - Zero inversion thresholding

                    Image<Gray, byte> img = new Image<Gray, byte>(bmp);
                    imgBinarize = new Image<Gray, byte>(bmp.Width, bmp.Height, new Gray(0));
                    CvInvoke.Threshold(img, imgBinarize, trackBar2.Value, 255, Emgu.CV.CvEnum.ThresholdType.ToZeroInv);
                    pictureBox2.Image = imgBinarize.ToBitmap();

                    #endregion

                    abcd.Stop();

                    MessageBox.Show("Number of measurement is " + i + "." + " and image processing time is " + String.Format("{0,00}", abcd.ElapsedMilliseconds) + " ms.");

                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                    time_format_emgucv();
                }
                DrawGraph(y);
                hodnoty_y();
            }

            if (radioButton7.Checked == true) // Threshold triangle (EmguCV)
            {
                remove();

                for (int i = 1; i <= number; i++)
                {

                    abcd = Stopwatch.StartNew();

                    #region EmguCV - Threshold triangle

                    Image<Gray, byte> img = new Image<Gray, byte>(bmp);
                    imgBinarize = new Image<Gray, byte>(bmp.Width, bmp.Height, new Gray(0));
                    CvInvoke.Threshold(img, imgBinarize, trackBar2.Value, 255, Emgu.CV.CvEnum.ThresholdType.Triangle);
                    pictureBox2.Image = imgBinarize.ToBitmap();

                    #endregion

                    abcd.Stop();

                    MessageBox.Show("Number of measurement is " + i + "." + " and image processing time is " + String.Format("{0,00}", abcd.ElapsedMilliseconds) + " ms.");

                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                    time_format_emgucv();
                }              
                DrawGraph(y);
                hodnoty_y();
            }

            if (radioButton8.Checked == true) // Abbreviated thresholding (EmguCV)

            {
                remove();
                for (int i = 1; i <= number; i++)
                {

                    abcd = Stopwatch.StartNew();

                    #region EmguCV - Abbreviated thresholding

                    Image<Gray, byte> img = new Image<Gray, byte>(bmp);
                    imgBinarize = new Image<Gray, byte>(bmp.Width, bmp.Height, new Gray(0));                    
                    CvInvoke.Threshold(img, imgBinarize, trackBar2.Value, 255, Emgu.CV.CvEnum.ThresholdType.Trunc);
                    pictureBox2.Image = imgBinarize.ToBitmap();

                    #endregion

                    abcd.Stop();

                    MessageBox.Show("Number of measurement is " + i + "." + " and image processing time is " + String.Format("{0,00}", abcd.ElapsedMilliseconds) + " ms.");

                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);
                    time_format_emgucv();
                }
                DrawGraph(y);
                hodnoty_y();  
            }
        }     
        
       Image<Bgr, byte> img_smoothing;     
        private void Form1_Load(object sender, EventArgs e)
        {  
        }
        private void trackBar4_ValueChanged(object sender, EventArgs e)
        {           
        }
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        { 
            if (comboBox1.SelectedIndex == 7) // Edge detection (EmguCV)
            {
                remove();
                groupBox2.Enabled = false;
                label6.Text = "Edge detection (Emgu.CV) - Canny1";
                label13.Text = "Edge detection (Emgu.CV) - Canny2";
                trackBar1.Maximum = tr_max;
                trackBar2.Maximum = tr_max;

                for (int i = 1; i <= number; i++)
                {
                    abcd = Stopwatch.StartNew();

                    #region EmguCV - Edge detection

                    Image<Gray, Byte> gray = new Image<Gray, Byte>(bmp);
                    Image<Gray, Byte> canny = gray.Canny(trackBar1.Value, trackBar2.Value);
                    pictureBox2.Image = canny.ToBitmap();

                    #endregion

                    abcd.Stop();

                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);
                    time_format_emgucv();
                }
                DrawGraph(y);
            }
            
            if (comboBox1.SelectedIndex == 8) // Image smoothing (AForge.NET)
            { 
                trackBar1.Enabled = true;
                label6.Text = "Image smoothing (AForge.NET)";
                remove();

                for (int i = 1; i <= number; i++)
                {
                    AForge = Stopwatch.StartNew();

                    #region AForge.NET - Image smoothing

                    AdaptiveSmoothing filter = new AdaptiveSmoothing();
                    filter.Factor = trackBar1.Value;
                    Bitmap r = filter.Apply(bmp);
                    pictureBox3.Image = r;

                    #endregion

                    AForge.Stop();

                   

                    if (i > 0) y2[i - 1] = Convert.ToDouble(AForge.Elapsed.TotalMilliseconds);

                    time_format_aforgenet();
                }
                DrawGraph2(y2);
                //hodnoty_y2();
            }

            if   (comboBox1.SelectedIndex == 11) // Erosion (EmguCV)
                {
                    groupBox2.Enabled = false;
                    trackBar2.Enabled = false;
                    trackBar3.Enabled = false;
                    label6.Text = "Erosion (EmguCV)";
                    remove();

                for (int i = 1; i <= number; i++)
                {
                    abcd = Stopwatch.StartNew();

                    #region EmguCV - Erosion
                    
                    Image<Bgr, byte> img = new Image<Bgr, byte>(bmp);
                    pictureBox2.Image = img.Erode(trackBar1.Value).Bitmap;

                    #endregion

                    abcd.Stop();

                   

                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                    time_format_emgucv();

                }
                DrawGraph(y);
                hodnoty_y();
                }

            if (comboBox1.SelectedIndex == 10) // Dilatation (EmguCV)
            {
                groupBox2.Enabled = false;
                trackBar2.Enabled = false;
                trackBar3.Enabled = false;
                label6.Text = "Dilatation (EmguCV)";
                remove();

                for (int i = 1; i <= number; i++)
                {
                    abcd = Stopwatch.StartNew();

                    #region EmguCV - Dilatation

                    Image<Bgr, byte> img = new Image<Bgr, byte>(bmp);
                    pictureBox2.Image = img.Dilate(trackBar1.Value).Bitmap;

                    #endregion

                    abcd.Stop();

                    //MessageBox.Show("Číslo merania je " + i + "." + " a Čas spracovania je " + String.Format("{0,00}", abcd.ElapsedMilliseconds) + " ms.");

                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);
                    time_format_emgucv();

                }
                DrawGraph(y);
                hodnoty_y();
            }

            if (comboBox1.SelectedIndex == 9) // Binary thresholding (AForge.NET)
            {
                remove();
                trackBar1.Maximum = second_number;
                label6.Text = "Binary thresholding (AForge.NET)";

                for (int i = 1; i <= number; i++)
                {
                    AForge = Stopwatch.StartNew();

                    #region AFORGE.NET - Binary thresholding

                    var bmp16bpp = Grayscale.CommonAlgorithms.BT709.Apply(bmp);
                    Threshold filter = new Threshold();
                    filter.ThresholdValue = trackBar1.Value;   // trackBar2.Value;
                    Bitmap c = filter.Apply(bmp16bpp);
                    pictureBox3.Image = c;

                    #endregion

                    AForge.Stop();

                    //MessageBox.Show("Číslo merania je " + i + "." + " a Čas spracovania je " + String.Format("{0,00}", AForge.ElapsedMilliseconds) + " ms.");

                    if (i > 0) y2[i - 1] = Convert.ToDouble(AForge.Elapsed.TotalMilliseconds);

                    time_format_aforgenet();
                }
                DrawGraph2(y2);
                hodnoty_y2();
            }          
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label11.Text = trackBar1.Value.ToString();
        }
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label12.Text = trackBar2.Value.ToString();
        }
        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            label15.Text = trackBar3.Value.ToString();
        }
        private void trackBar4_Scroll(object sender, EventArgs e)
        {           
        }
        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 7)
            {
                label14.Text = "AForge.NET - Gaussian";
                trackBar1.Enabled = true;
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
             
                for (int i = 1; i <= number; i++)
                {

                    AForge = Stopwatch.StartNew();

                    #region AFORGE.NET - Gaussian

                    var bmp8bpp = Grayscale.CommonAlgorithms.BT709.Apply(bmp);
                    CannyEdgeDetector filter = new CannyEdgeDetector();
                    filter.GaussianSize = trackBar3.Value;
                    Bitmap canny = filter.Apply(bmp8bpp);
                    pictureBox3.Image = canny;

                    #endregion AFORGE.NET

                    AForge.Stop();
                   // MessageBox.Show("Číslo merania je " + i + "." + " a Čas spracovania je " + String.Format("{0,00}", AForge.ElapsedMilliseconds) + " ms.");
                    if (i > 0) y2[i - 1] = Convert.ToDouble(AForge.Elapsed.TotalMilliseconds);

                    time_format_aforgenet();
                }
                DrawGraph2(y2);
                hodnoty_y2();
            }
        }
        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 7) // Edge detection (EmguCV)
            {
                remove();
                groupBox2.Enabled = false;
                label6.Text = "Edge detection (EmguCV) - Canny1";
                label13.Text = "Edge detection (EmguCV) - Canny2";
                trackBar1.Maximum = tr_max;
                trackBar2.Maximum = tr_max;

                for (int i = 1; i <= number; i++)
                {
                    abcd = Stopwatch.StartNew();

                    #region EmguCV - Edge detection

                    Image<Gray, Byte> gray = new Image<Gray, Byte>(bmp);
                    Image<Gray, Byte> canny = gray.Canny(trackBar1.Value, trackBar2.Value);
                    pictureBox2.Image = canny.ToBitmap();

                    #endregion

                    abcd.Stop();

                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                    time_format_emgucv();
                }
                DrawGraph(y);
            }

            if (comboBox1.SelectedIndex == 9) // Binary thresholding (EmguCV)

                remove();
                {                 
                label13.Text = "Binary thresholding (EmguCV)";
                trackBar2.Enabled = true;

                for (int i = 1; i <= number; i++)
                {
                    abcd = Stopwatch.StartNew();

                    #region EmguCV - Binary thresholding

                    Image<Gray, byte> img = new Image<Gray, byte>(bmp);
                    imgBinarize = new Image<Gray, byte>(bmp.Width, bmp.Height, new Gray(0));
                    CvInvoke.Threshold(img, imgBinarize, trackBar2.Value, 255, Emgu.CV.CvEnum.ThresholdType.Trunc);

                    #endregion

                    abcd.Stop();

                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                    time_format_emgucv();
                }
                DrawGraph(y);
                hodnoty_y();
                }
        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {        
        }
        private void button13_Click_2(object sender, EventArgs e)
        {
        }
        private void button13_Click_3(object sender, EventArgs e)
        {
            if (aaa.Checked == true) // Binary thresholding (EmguCV)
            {
                remove();
                label13.Text = "Binary thresholding (EmguCV)";
                trackBar2.Maximum = second_number;

                for (int i = 1; i <= number; i++)
                {
                    abcd = Stopwatch.StartNew();

                    #region EmguCV - Binary thresholding

                    Image<Gray, byte> img = new Image<Gray, byte>(bmp);
                    imgBinarize = new Image<Gray, byte>(bmp.Width, bmp.Height, new Gray(0));
                    CvInvoke.Threshold(img, imgBinarize, trackBar2.Value, 255, Emgu.CV.CvEnum.ThresholdType.Binary);
                    pictureBox2.Image = imgBinarize.ToBitmap();

                    #endregion

                    abcd.Stop();

                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                    time_format_emgucv();
                }
                DrawGraph(y);
                hodnoty_y();
            }

            if (radioButton2.Checked == true) // Binary inversion thresholding (EmguCV)
            {
                remove();
                label13.Text = "Binary inversion thresholding (EmguCV)";
                trackBar2.Maximum = second_number;

                for (int i = 1; i <= number; i++)
                {
                    abcd = Stopwatch.StartNew();

                    #region EmguCV - Binary inversion thresholding 

                    Image<Gray, byte> img = new Image<Gray, byte>(bmp);
                    imgBinarize = new Image<Gray, byte>(bmp.Width, bmp.Height, new Gray(0));
                    CvInvoke.Threshold(img, imgBinarize, trackBar2.Value, 255, Emgu.CV.CvEnum.ThresholdType.BinaryInv);
                    pictureBox2.Image = imgBinarize.ToBitmap();

                    #endregion 

                    abcd.Stop();

                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                    time_format_emgucv();
                }
                DrawGraph(y);
                hodnoty_y();
            }

            if (radioButton3.Checked == true) // Threshold mask (EmguCV)
            {
                remove();
                label13.Text = "Threshold mask (EmguCV)";
                trackBar2.Maximum = second_number;

                for (int i = 1; i <= number; i++)
                {
                    abcd = Stopwatch.StartNew();

                    #region EmguCV - Threshold mask

                    Image<Gray, byte> img = new Image<Gray, byte>(bmp);
                    imgBinarize = new Image<Gray, byte>(bmp.Width, bmp.Height, new Gray(0));
                    CvInvoke.Threshold(img, imgBinarize, trackBar2.Value, 255, Emgu.CV.CvEnum.ThresholdType.Mask);
                    pictureBox2.Image = imgBinarize.ToBitmap();

                    #endregion

                    abcd.Stop();

                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                    time_format_emgucv();
                }
                DrawGraph(y);
                hodnoty_y();
            }
            if (radioButton4.Checked == true) // Threshold value (EmguCV)
            {
                remove();
                label13.Text = "Threshold value (EmguCV)";
                trackBar2.Maximum = second_number;

                for (int i = 1; i <= number; i++)
                {
                    abcd = Stopwatch.StartNew();

                    #region EmguCV - Threshold value

                    Image<Gray, byte> img = new Image<Gray, byte>(bmp);
                    imgBinarize = new Image<Gray, byte>(bmp.Width, bmp.Height, new Gray(0));
                    CvInvoke.Threshold(img, imgBinarize, trackBar2.Value, 255, Emgu.CV.CvEnum.ThresholdType.Otsu);
                    pictureBox2.Image = imgBinarize.ToBitmap();

                    #endregion

                    abcd.Stop();
                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                    time_format_emgucv();
                }
                DrawGraph(y);
                hodnoty_y();
            }

            if (radioButton5.Checked == true) // Zero thresholding (EmguCV)

            {
                remove();
                label13.Text = "Zero thresholding (EmguCV)";
                trackBar2.Maximum = second_number;

                for (int i = 1; i <= number; i++)
                {
                    abcd = Stopwatch.StartNew();

                    #region EmguCV - Zero thresholding

                    Image<Gray, byte> img = new Image<Gray, byte>(bmp);
                    imgBinarize = new Image<Gray, byte>(bmp.Width, bmp.Height, new Gray(0));
                    CvInvoke.Threshold(img, imgBinarize, trackBar2.Value, 255, Emgu.CV.CvEnum.ThresholdType.ToZero);
                    pictureBox2.Image = imgBinarize.ToBitmap();

                    #endregion 

                    abcd.Stop();
                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                    time_format_emgucv();
                }
                DrawGraph(y);
                hodnoty_y();
            }
            if (radioButton6.Checked == true) // Zero inversion thresholding (EmguCV)
            {
                remove();
                label13.Text = "Zero inversion thresholding (EmguCV)";
                trackBar2.Maximum = second_number;

                for (int i = 1; i <= number; i++)
                {
                    abcd = Stopwatch.StartNew();

                    #region EmguCV - Zero inversion thresholding 

                    Image<Gray, byte> img = new Image<Gray, byte>(bmp);
                    imgBinarize = new Image<Gray, byte>(bmp.Width, bmp.Height, new Gray(0));
                    CvInvoke.Threshold(img, imgBinarize, trackBar2.Value, 255, Emgu.CV.CvEnum.ThresholdType.ToZeroInv);
                    pictureBox2.Image = imgBinarize.ToBitmap();

                    #endregion

                    abcd.Stop();
                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);
                    time_format_emgucv();

                }
                DrawGraph(y);
                hodnoty_y();
            }
            if (radioButton7.Checked == true) // Threshold triangle (EmguCV)
            {
                remove();
                label13.Text = "Threshold triangle  (EmguCV)";
                trackBar2.Maximum = second_number;

                for (int i = 1; i <= number; i++)
                {
                    abcd = Stopwatch.StartNew();

                    #region EmguCV - Threshold triangle 

                    Image<Gray, byte> img = new Image<Gray, byte>(bmp);
                    imgBinarize = new Image<Gray, byte>(bmp.Width, bmp.Height, new Gray(0));
                    CvInvoke.Threshold(img, imgBinarize, trackBar2.Value, 255, Emgu.CV.CvEnum.ThresholdType.Triangle);
                    pictureBox2.Image = imgBinarize.ToBitmap();

                    #endregion

                    abcd.Stop();
                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                    time_format_emgucv();
                }
                DrawGraph(y);
                hodnoty_y();
            }
            if (radioButton8.Checked == true) // Abbreviated thresholding (EmguCV)
            {
                remove();
                label13.Text = "Abbreviated thresholding (EmguCV)";
                trackBar2.Maximum = second_number;

                for (int i = 1; i <= number; i++)
                {
                    abcd = Stopwatch.StartNew();

                    #region EmguCV - Abbreviated thresholding 

                    Image<Gray, byte> img = new Image<Gray, byte>(bmp);
                    imgBinarize = new Image<Gray, byte>(bmp.Width, bmp.Height, new Gray(0));
                    CvInvoke.Threshold(img, imgBinarize, trackBar2.Value, 255, Emgu.CV.CvEnum.ThresholdType.Trunc);
                    pictureBox2.Image = imgBinarize.ToBitmap();

                    #endregion

                    abcd.Stop();
                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                    time_format_emgucv();
                }
                DrawGraph(y);
                hodnoty_y();
            }
        }
        private void button16_Click_1(object sender, EventArgs e)
        {          
        }
        private void aaa_CheckedChanged(object sender, EventArgs e)
        {
        }
        private void button8_Click(object sender, EventArgs e)
        {
            trackBar2.Enabled = true;
            trackBar3.Enabled = true;

            if (radioButton1.Checked == true) // Bilateral image smoothing (EmguCV)
            {


                label13.Text = "Bilateral image smoothing  - core size (EmguCV)";
                label14.Text = "Bilateral image smoothing - color sigma (EmguCV)";
                remove();

                for (int i = 1; i <= number; i++)

                { 

                abcd = Stopwatch.StartNew();

                #region EmguCV - Bilateral image smoothing 

                Image<Bgr, Byte> image = new Image<Bgr, byte>(bmp);
                Image<Bgr, Byte> bilat = image.SmoothBilateral(trackBar2.Value, trackBar3.Value, 34);    
                pictureBox2.Image = bilat.ToBitmap();

                #endregion

                abcd.Stop();

                if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                time_format_emgucv();

                }
                DrawGraph(y);
                hodnoty_y();
            }

            if (radioButton9.Checked == true)   // Image smoothing - blur  (EmguCV)
            {
                trackBar2.Minimum = 1;
                trackBar3.Minimum = 1;
                remove();
                label13.Text = "Image smoothing - blur - width (EmguCV)";
                label14.Text = "Image smoothing - blur - height (EmguCV)";

                for (int i = 1; i <= number; i++)

                {
                    abcd = Stopwatch.StartNew();

                    #region EmguCV - Image smoothing - blur

                    Image<Bgr, Byte> image = new Image<Bgr, byte>(bmp);
                    Image<Bgr, Byte> blur = image.SmoothBlur(trackBar3.Value, trackBar2.Value, true);  
                    pictureBox2.Image = blur.ToBitmap();

                    #endregion

                    abcd.Stop();

                    time_format_emgucv();
                }
                DrawGraph(y);
                hodnoty_y();
            }

            if (radioButton10.Checked == true) // Image smoothing - center blur (EmguCV)
            {
                remove();
                label13.Text = "Image smoothing - center blur (EmguCV)";

                for (int i = 1; i <= number; i++)
                    {

                    abcd = Stopwatch.StartNew();

                    #region EmguCV - Image smoothing - center blur

                    Image<Bgr, Byte> image = new Image<Bgr, byte>(bmp);
                    Image<Bgr, Byte> mediansmooth = image.SmoothMedian(15); 
                    pictureBox2.Image = mediansmooth.ToBitmap();

                    #endregion

                    abcd.Stop();

                    if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                    time_format_emgucv();
                }            
                DrawGraph(y);
                hodnoty_y();
            }

            if (radioButton11.Checked == true) // Gaussian image smoothing (EmguCV)
            {
                remove();
                label13.Text = "Gaussian image smoothing (EmguCV)";

                for (int i = 1; i <= number; i++)
                        {
                            abcd = Stopwatch.StartNew();

                            #region EmguCV - Gaussian image smoothing 

                            Image<Bgr, Byte> image = new Image<Bgr, byte>(bmp);
                            Image<Bgr, Byte> gauss = image.SmoothGaussian(1, 3, 34.3, 45.3);  
                            pictureBox2.Image = gauss.ToBitmap();

                            #endregion

                            abcd.Stop();

                          

                            if (i > 0) y[i - 1] = Convert.ToDouble(abcd.Elapsed.TotalMilliseconds);

                             time_format_emgucv();
                }  
                DrawGraph(y);
                hodnoty_y();
            }
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
        }
        private void label12_Click(object sender, EventArgs e)
        {
        }
        private void label6_Click(object sender, EventArgs e)
        {
        }
    }
    internal class bmp8bpp
    {
    }
}

    

    


  