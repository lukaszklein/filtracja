using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.Util;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace praca_magisterska
{
    public partial class Form1 : Form
    {
        double PSNRMSE,
               PSNRMSD,
               PSNRMED;
        float Marziliano;

        string[] row = new string[11];

        string numPictureGood,
               numDistortionGood,
            numIntensityGood;

        Image<Bgr, byte> imgToFilter,
                         imgReference;

        Stopwatch timer = new Stopwatch();

        Match sourcePath,
              extensionSourceFile,
              numPicture,
              numDistortion,
            numIntensity;


        public Form1()
        {
            InitializeComponent();
            listViewEval.View = View.Details;
            listViewEval.GridLines = true;
            listViewEval.FullRowSelect = true;
            listViewEval.Columns.Add("Filtr");
            listViewEval.Columns.Add("Rozmiar maski");
            listViewEval.Columns.Add("Sigma");
            listViewEval.Columns.Add("Sigma Color");
            listViewEval.Columns.Add("Sigma Space");
            listViewEval.Columns.Add("Waga maski");
            listViewEval.Columns.Add("PSNR MSE");
            listViewEval.Columns.Add("PSNR MSD");
            listViewEval.Columns.Add("PSNR MED");
            listViewEval.Columns.Add("Marziliano");
            listViewEval.Columns.Add("JNB");
            listViewEval.Columns.Add("Czas wykonania");
        }

        void SaveResults(Image<Bgr, Byte> image, string filter, int size, int sigma, double sigmaColor, 
            double sigmaSpace, float weight, string[] data)
        {
            if (!checkBoxData.Checked)
            {
                image.Save("C://obrazy//" + filter.ToString() + ";" + size.ToString() + ";" + sigma.ToString()
                    + ";" + sigmaColor.ToString() + ";" + sigmaSpace.ToString() + ";" + weight.ToString() + ".bmp");
            }
            StreamWriter File = new StreamWriter("C://obrazy//ocena.txt", true);
            string temp = "Filter name: " + filter.ToString() + "; Mask size: " + size.ToString() + "; Sigma: " + sigma.ToString()
                  + "; Sigma color: " + sigmaColor.ToString() + "; Sigma space: " + sigmaSpace.ToString()
                  + "; Mask weight: " + weight.ToString() + "; MSE: " + data[6].ToString() + "; MSD: " + data[7].ToString() + "; MED: " + data[8].ToString()
                   + "; Marziliano: " + data[9].ToString() + "; Time: " + data[10].ToString();
            temp = temp + "\r\n";
            File.Write(temp);
            File.Close();
        }

        void SaveResults(Image<Bgr, Byte> image, string filter, int size, int sigma, double sigmaColor,
            double sigmaSpace, float weight, string[] data, string imageName)
        {
            if (!checkBoxData.Checked)
            {
                image.Save("C://obrazy//" + imageName + ";" + filter.ToString() + ";" + size.ToString() + ";" + sigma.ToString() + ";"
                     + sigmaColor.ToString() + ";" + sigmaSpace.ToString() + ";" + weight.ToString() + ".bmp");
            }
            StreamWriter File = new StreamWriter("C://obrazy//ocena.txt", true);
            string temp = imageName + ";";
            temp = temp + " Filter name: " + filter.ToString() + "; Mask size: " + size.ToString() + "; Sigma: " + sigma.ToString()
                 + "; Sigma color: " + sigmaColor.ToString() + "; Sigma space: " + sigmaSpace.ToString()
                  + "; Mask weight: " + weight.ToString() + "; MSE: " + data[6].ToString() + "; MSD: " + data[7].ToString() + "; MED: " + data[8].ToString()
                   + "; Marziliano: " + data[9].ToString() + "; Time: " + data[10].ToString();
            temp = temp + "\r\n";
            File.Write(temp);
            File.Close();
        }

        void FilterChoice(Image<Bgr, Byte> imgToFilter, Image<Bgr, Byte> imgReference, string numPictureGood, string numDistortionGood, string numIntensityGood)
        {
            Image<Bgr, Byte> imgFiltered = new Image<Bgr, byte>(imgToFilter.Size);
            string name = numPictureGood + numDistortionGood + "_" + numIntensityGood;

            int sizeMask = (int)numericMinMask.Value;
            if (sizeMask % 2 == 0)
            {
                sizeMask++;
            }

            if (radioAvg.Checked)
            {
                for (int i = sizeMask; i <= (int)numericMaxMask.Value; i=i+2)
                {
                    timer.Reset();
                    timer.Start();
                    CvInvoke.Blur(imgToFilter, imgFiltered, new Size(i, i), new Point(-1, -1));
                    timer.Stop();
                    EvaluationOfFilter(imgReference, imgFiltered);
                    row[0] = "Filtr uśredniający";
                    row[1] = i.ToString();
                    row[2] = "-";
                    row[3] = "-";
                    row[4] = "-";
                    row[5] = "-";
                    row[6] = PSNRMSE.ToString();
                    row[7] = PSNRMSD.ToString();
                    row[8] = PSNRMED.ToString();
                    row[9] = Marziliano.ToString();
                    row[10] = timer.Elapsed.TotalMilliseconds.ToString();
                    ListViewItem listViewItem = new ListViewItem(row);
                    listViewEval.Items.Add(listViewItem);
                    SaveResults(imgFiltered, "Avg", i, 0, 0, 0, 0, row, name);
                }
            }

            if (radioGauss.Checked)
            {
                for (int i = sizeMask; i <= (int)numericMaxMask.Value; i=i+2)
                {
                    for (int j = (int)numericMinSigmaX.Value; j <= (int)numericMaxSigmaX.Value; j=j+5)
                    {
                        timer.Reset();
                        timer.Start();
                        CvInvoke.GaussianBlur(imgToFilter, imgFiltered, new Size(i, i), j);
                        timer.Stop();
                        EvaluationOfFilter(imgReference, imgFiltered);
                        row[0] = "Filtr Gaussa";
                        row[1] = i.ToString();
                        row[2] = j.ToString();
                        row[3] = "-";
                        row[4] = "-";
                        row[5] = "-";
                        row[6] = PSNRMSE.ToString();
                        row[7] = PSNRMSD.ToString();
                        row[8] = PSNRMED.ToString();
                        row[9] = Marziliano.ToString();
                        row[10] = timer.Elapsed.TotalMilliseconds.ToString();
                        ListViewItem listViewItem = new ListViewItem(row);
                        listViewEval.Items.Add(listViewItem);
                        SaveResults(imgFiltered, "Gauss", i, j, 0, 0, 0, row, name);
                    }
                }
            }

            if (radioMedian.Checked)
            {
                for (int i = sizeMask; i <= (int)numericMaxMask.Value; i=i+2)
                {
                    timer.Reset();
                    timer.Start();
                    CvInvoke.MedianBlur(imgToFilter, imgFiltered, i);
                    timer.Stop();
                    EvaluationOfFilter(imgReference, imgFiltered);
                    row[0] = "Filtr medianowy";
                    row[1] = i.ToString();
                    row[2] = "-";
                    row[3] = "-";
                    row[4] = "-";
                    row[5] = "-";
                    row[6] = PSNRMSE.ToString();
                    row[7] = PSNRMSD.ToString();
                    row[8] = PSNRMED.ToString();
                    row[9] = Marziliano.ToString();
                    row[10] = timer.Elapsed.TotalMilliseconds.ToString();
                    ListViewItem listViewItem = new ListViewItem(row);
                    listViewEval.Items.Add(listViewItem);
                    SaveResults(imgFiltered, "Median", i, 0, 0, 0, 0, row, name);
                }
            }

            if (radioBilateral.Checked)
            {
                for (double j = (double)numericMinSigmaColor.Value; j <= (double)numericMaxSigmaColor.Value; j=j+10)
                {
                    for (double k = (double)numericMinSigmaSpace.Value; k <= (double)numericMaxSigmaSpace.Value; k=k+10)
                    {
                        timer.Reset();
                        timer.Start();
                        CvInvoke.BilateralFilter(imgToFilter, imgFiltered, -1, j, k);
                        timer.Stop();
                        EvaluationOfFilter(imgReference, imgFiltered);
                        row[0] = "Filtr bilateralny";
                        row[1] = "-";
                        row[2] = "-";
                        row[3] = j.ToString();
                        row[4] = k.ToString();
                        row[5] = "-";
                        row[6] = PSNRMSE.ToString();
                        row[7] = PSNRMSD.ToString();
                        row[8] = PSNRMED.ToString();
                        row[9] = Marziliano.ToString();
                        row[10] = timer.Elapsed.TotalMilliseconds.ToString();
                        ListViewItem listViewItem = new ListViewItem(row);
                        listViewEval.Items.Add(listViewItem);
                        SaveResults(imgFiltered, "Bilateral", -1, 0, j, k, 0, row, name);
                    }
                }
            }

            if (radioKuwahara.Checked)
            {
                for (int i = sizeMask; i <= (int)numericMaxMask.Value; i=i+2)
                {
                    timer.Reset();
                    timer.Start();
                    imgFiltered = KuwaharaFilter(imgToFilter, i);
                    timer.Stop();
                    EvaluationOfFilter(imgReference, imgFiltered);
                    row[0] = "Filtr Kuwahara";
                    row[1] = i.ToString();
                    row[2] = "-";
                    row[3] = "-";
                    row[4] = "-";
                    row[5] = "-";
                    row[6] = PSNRMSE.ToString();
                    row[7] = PSNRMSD.ToString();
                    row[8] = PSNRMED.ToString();
                    row[9] = Marziliano.ToString();
                    row[10] = timer.Elapsed.TotalMilliseconds.ToString();
                    ListViewItem listViewItem = new ListViewItem(row);
                    listViewEval.Items.Add(listViewItem);
                    SaveResults(imgFiltered, "Kuwahara", i, 0, 0, 0, 0, row, name);
                }
            }

            if (radioUnsharp.Checked)
            {
                for (int i = sizeMask; i <= (int)numericMaxMask.Value; i=i+2)
                {
                    float j = (float)numericUnsharpMaskMin.Value;
                    for (; j <= (float)numericUnsharpMaskMax.Value; j=(float)(j+0.5))
                    {
                        timer.Reset();
                        timer.Start();
                        imgFiltered = UnsharpMasking(imgToFilter, i, j);
                        timer.Stop();
                        EvaluationOfFilter(imgReference, imgFiltered);
                        row[0] = "Unsharp masking";
                        row[1] = i.ToString();
                        row[2] = "-";
                        row[3] = "-";
                        row[4] = "-";
                        row[5] = "-";
                        row[6] = PSNRMSE.ToString();
                        row[7] = PSNRMSD.ToString();
                        row[8] = PSNRMED.ToString();
                        row[9] = Marziliano.ToString();
                        row[10] = timer.Elapsed.TotalMilliseconds.ToString();
                        ListViewItem listViewItem = new ListViewItem(row);
                        listViewEval.Items.Add(listViewItem);
                        SaveResults(imgFiltered, "Unsharp", i, 0, 0, 0, j, row, name);
                    }
                }
            }

            if (radioEqualize.Checked)
            {
                timer.Reset();
                timer.Start();
                imgFiltered = EqualizeHistogram(imgToFilter);
                timer.Stop();
                EvaluationOfFilter(imgReference, imgFiltered);
                row[0] = "Wyrownanie";
                row[1] = "-";
                row[2] = "-";
                row[3] = "-";
                row[4] = "-";
                row[5] = "-";
                row[6] = PSNRMSE.ToString();
                row[7] = PSNRMSD.ToString();
                row[8] = PSNRMED.ToString();
                row[9] = Marziliano.ToString();
                row[10] = timer.Elapsed.TotalMilliseconds.ToString();
                ListViewItem listViewItem = new ListViewItem(row);
                listViewEval.Items.Add(listViewItem);
                SaveResults(imgFiltered, "Eq", 0, 0, 0, 0, 0, row, name);
            }

            if (radioStretch.Checked)
            {
                timer.Reset();
                timer.Start();
                imgFiltered = StretchHistogram(imgToFilter);
                timer.Stop();
                EvaluationOfFilter(imgReference, imgFiltered);
                row[0] = "Rozciągniecie";
                row[1] = "-";
                row[2] = "-";
                row[3] = "-";
                row[4] = "-";
                row[5] = "-";
                row[6] = PSNRMSE.ToString();
                row[7] = PSNRMSD.ToString();
                row[8] = PSNRMED.ToString();
                row[9] = Marziliano.ToString();
                row[10] = timer.Elapsed.TotalMilliseconds.ToString();
                ListViewItem listViewItem = new ListViewItem(row);
                listViewEval.Items.Add(listViewItem);
                SaveResults(imgFiltered, "Str", 0, 0, 0, 0, 0, row, name);
            }

            MessageBox.Show("Ukończono operacje");
        }

        void AllFilters(string path, string numPicture, string extension, Image<Bgr, Byte> imgReference)
        {
            Image<Bgr, Byte> imgFiltered;
            List<int> rejectedDistortions = new List<int> { 2, 3, 5, 8, 9, 11, 13, 15, 16, 18, 19, 21, 24 };

            int sizeMask = (int)numericMinMask.Value;
            if (sizeMask % 2 == 0)
            {
                sizeMask++;
            }

            for (int numImage = 1; numImage <= 24; numImage++)
            {
                while (rejectedDistortions.Contains(numImage))
                {
                    numImage++;
                }


                for (int distortionLvl = 1; distortionLvl <= 5; distortionLvl = distortionLvl + 4)
                {
                    string numPath;

                    if (numImage < 10)
                    {
                        numPath = "0" + numImage.ToString() + "_" + distortionLvl.ToString();
                    }
                    else
                    {
                        numPath = numImage.ToString() + "_" + distortionLvl.ToString();
                    }
                    Image<Bgr, Byte> imgToFilter = new Image<Bgr, byte>(path + numPath + extension);
                    imgFiltered = new Image<Bgr, byte>(imgToFilter.Size);

                    // uśredniający
                    {
                        for (int i = sizeMask; i <= (int)numericMaxMask.Value; i = i + 2)
                        {
                            timer.Reset();
                            timer.Start();
                            CvInvoke.Blur(imgToFilter, imgFiltered, new Size(i, i), new Point(-1, -1));
                            timer.Stop();
                            EvaluationOfFilter(imgReference, imgFiltered);
                            row[0] = "Filtr uśredniający";
                            row[1] = i.ToString();
                            row[2] = "-";
                            row[3] = "-";
                            row[4] = "-";
                            row[5] = "-";
                            row[6] = PSNRMSE.ToString();
                            row[7] = PSNRMSD.ToString();
                            row[8] = PSNRMED.ToString();
                            row[9] = Marziliano.ToString();
                            row[10] = timer.Elapsed.TotalMilliseconds.ToString();
                            ListViewItem listViewItem = new ListViewItem(row);
                            listViewEval.Items.Add(listViewItem);
                            SaveResults(imgFiltered, "Avg", i, 0, 0, 0, 0, row, numPicture + numPath);
                        }
                    }

                    // Gaussa
                    {
                        for (int i = sizeMask; i <= (int)numericMaxMask.Value; i = i + 2)
                        {
                            for (int j = (int)numericMinSigmaX.Value; j <= (int)numericMaxSigmaX.Value; j = j + 5)
                            {
                                timer.Reset();
                                timer.Start();
                                CvInvoke.GaussianBlur(imgToFilter, imgFiltered, new Size(i, i), j);
                                timer.Stop();
                                EvaluationOfFilter(imgReference, imgFiltered);
                                row[0] = "Filtr Gaussa";
                                row[1] = i.ToString();
                                row[2] = j.ToString();
                                row[3] = "-";
                                row[4] = "-";
                                row[5] = "-";
                                row[6] = PSNRMSE.ToString();
                                row[7] = PSNRMSD.ToString();
                                row[8] = PSNRMED.ToString();
                                row[9] = Marziliano.ToString();
                                row[10] = timer.Elapsed.TotalMilliseconds.ToString();
                                ListViewItem listViewItem = new ListViewItem(row);
                                listViewEval.Items.Add(listViewItem);
                                SaveResults(imgFiltered, "Gauss", i, j, 0, 0, 0, row, numPicture + numPath);
                            }
                        }
                    }

                    // medianowy
                    {
                        for (int i = sizeMask; i <= (int)numericMaxMask.Value; i = i + 2)
                        {
                            timer.Reset();
                            timer.Start();
                            CvInvoke.MedianBlur(imgToFilter, imgFiltered, i);
                            timer.Stop();
                            EvaluationOfFilter(imgReference, imgFiltered);
                            row[0] = "Filtr medianowy";
                            row[1] = i.ToString();
                            row[2] = "-";
                            row[3] = "-";
                            row[4] = "-";
                            row[5] = "-";
                            row[6] = PSNRMSE.ToString();
                            row[7] = PSNRMSD.ToString();
                            row[8] = PSNRMED.ToString();
                            row[9] = Marziliano.ToString();
                            row[10] = timer.Elapsed.TotalMilliseconds.ToString();
                            ListViewItem listViewItem = new ListViewItem(row);
                            listViewEval.Items.Add(listViewItem);
                            SaveResults(imgFiltered, "Median", i, 0, 0, 0, 0, row, numPicture + numPath);
                        }
                    }

                    // bilateralny
                    {
                        for (double j = (double)numericMinSigmaColor.Value; j <= (double)numericMaxSigmaColor.Value; j = j + 10)
                        {
                            for (double k = (double)numericMinSigmaSpace.Value; k <= (double)numericMaxSigmaSpace.Value; k = k + 10)
                            {
                                timer.Reset();
                                timer.Start();
                                CvInvoke.BilateralFilter(imgToFilter, imgFiltered, -1, j, k);
                                timer.Stop();
                                EvaluationOfFilter(imgReference, imgFiltered);
                                row[0] = "Filtr bilateralny";
                                row[1] = "-";
                                row[2] = "-";
                                row[3] = j.ToString();
                                row[4] = k.ToString();
                                row[5] = "-";
                                row[6] = PSNRMSE.ToString();
                                row[7] = PSNRMSD.ToString();
                                row[8] = PSNRMED.ToString();
                                row[9] = Marziliano.ToString();
                                row[10] = timer.Elapsed.TotalMilliseconds.ToString();
                                ListViewItem listViewItem = new ListViewItem(row);
                                listViewEval.Items.Add(listViewItem);
                                SaveResults(imgFiltered, "Bilateral", -1, 0, j, k, 0, row, numPicture + numPath);
                            }
                        }
                    }

                    // Kuwahara
                    {
                        for (int i = sizeMask; i <= (int)numericMaxMask.Value; i = i + 2)
                        {
                            timer.Reset();
                            timer.Start();
                            imgFiltered = KuwaharaFilter(imgToFilter, i);
                            timer.Stop();
                            EvaluationOfFilter(imgReference, imgFiltered);
                            row[0] = "Filtr Kuwahara";
                            row[1] = i.ToString();
                            row[2] = "-";
                            row[3] = "-";
                            row[4] = "-";
                            row[5] = "-";
                            row[6] = PSNRMSE.ToString();
                            row[7] = PSNRMSD.ToString();
                            row[8] = PSNRMED.ToString();
                            row[9] = Marziliano.ToString();
                            row[10] = timer.Elapsed.TotalMilliseconds.ToString();
                            ListViewItem listViewItem = new ListViewItem(row);
                            listViewEval.Items.Add(listViewItem);
                            SaveResults(imgFiltered, "Kuwahara", i, 0, 0, 0, 0, row, numPicture + numPath);
                        }
                    }

                    // unsharp masking
                    {
                        for (int i = sizeMask; i <= (int)numericMaxMask.Value; i = i + 2)
                        {
                            float j = (float)numericUnsharpMaskMin.Value;
                            for (; j <= (float)numericUnsharpMaskMax.Value; j = (float)(j + 0.5))
                            {
                                timer.Reset();
                                timer.Start();
                                imgFiltered = UnsharpMasking(imgToFilter, i, j);
                                timer.Stop();
                                EvaluationOfFilter(imgReference, imgFiltered);
                                row[0] = "Unsharp masking";
                                row[1] = i.ToString();
                                row[2] = "-";
                                row[3] = "-";
                                row[4] = "-";
                                row[5] = "-";
                                row[6] = PSNRMSE.ToString();
                                row[7] = PSNRMSD.ToString();
                                row[8] = PSNRMED.ToString();
                                row[9] = Marziliano.ToString();
                                row[10] = timer.Elapsed.TotalMilliseconds.ToString();
                                ListViewItem listViewItem = new ListViewItem(row);
                                listViewEval.Items.Add(listViewItem);
                                SaveResults(imgFiltered, "Unsharp", i, 0, 0, 0, j, row, numPicture + numPath);
                            }
                        }
                    }

                    // wyrówanie histogramu
                    {
                        timer.Reset();
                        timer.Start();
                        imgFiltered = EqualizeHistogram(imgToFilter);
                        timer.Stop();
                        EvaluationOfFilter(imgReference, imgFiltered);
                        row[0] = "Wyrownanie";
                        row[1] = "-";
                        row[2] = "-";
                        row[3] = "-";
                        row[4] = "-";
                        row[5] = "-";
                        row[6] = PSNRMSE.ToString();
                        row[7] = PSNRMSD.ToString();
                        row[8] = PSNRMED.ToString();
                        row[9] = Marziliano.ToString();
                        row[10] = timer.Elapsed.TotalMilliseconds.ToString();
                        ListViewItem listViewItem = new ListViewItem(row);
                        listViewEval.Items.Add(listViewItem);
                        SaveResults(imgFiltered, "Eq", 0, 0, 0, 0, 0, row, numPicture + numPath);
                    }

                    // rozciągnięcie histogramu
                    {
                        timer.Reset();
                        timer.Start();
                        imgFiltered = StretchHistogram(imgToFilter);
                        timer.Stop();
                        EvaluationOfFilter(imgReference, imgFiltered);
                        row[0] = "Rozciągniecie";
                        row[1] = "-";
                        row[2] = "-";
                        row[3] = "-";
                        row[4] = "-";
                        row[5] = "-";
                        row[6] = PSNRMSE.ToString();
                        row[7] = PSNRMSD.ToString();
                        row[8] = PSNRMED.ToString();
                        row[9] = Marziliano.ToString();
                        row[10] = timer.Elapsed.TotalMilliseconds.ToString();
                        ListViewItem listViewItem = new ListViewItem(row);
                        listViewEval.Items.Add(listViewItem);
                        SaveResults(imgFiltered, "Str", 0, 0, 0, 0, 0, row, numPicture + numPath);
                    }
                }
            }
        }
    }
}
