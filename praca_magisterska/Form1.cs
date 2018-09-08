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
               PSNRMED,
               CPBD;
        float Marziliano,
              JNB;

        string[] row = new string[14];

        string numPictureGood;

        Image<Bgr, byte> imgToFilter,
                         imgReference;

        Stopwatch timer = new Stopwatch();

        Match sourcePath,
              extensionSourceFile,
              numPicture;

        public Form1()
        {
            InitializeComponent();
            listViewEval.View = View.Details;
            listViewEval.GridLines = true;
            listViewEval.FullRowSelect = true;
            listViewEval.Columns.Add("Filtr");
            listViewEval.Columns.Add("Rozmiar maski");
            listViewEval.Columns.Add("Sigma X");
            listViewEval.Columns.Add("Sigma Y");
            listViewEval.Columns.Add("Sigma Color");
            listViewEval.Columns.Add("Sigma Space");
            listViewEval.Columns.Add("Waga maski");
            listViewEval.Columns.Add("PSNR MSE");
            listViewEval.Columns.Add("PSNR MSD");
            listViewEval.Columns.Add("PSNR MED");
            listViewEval.Columns.Add("Marziliano");
            listViewEval.Columns.Add("JNB");
            listViewEval.Columns.Add("CPBD");
            listViewEval.Columns.Add("Czas wykonania");
        }

        void SaveResults(Image<Bgr, Byte> image, string filter, int size, int sigmaX, int sigmaY, double sigmaColor, 
            double sigmaSpace, float weight, string[] data)
        {
            if (!checkBoxData.Checked)
            {
                image.Save("C://obrazy//" + filter.ToString() + size.ToString() + sigmaX.ToString() + sigmaY.ToString() + sigmaColor.ToString()
                + sigmaSpace.ToString() + weight.ToString() + ".bmp");
            }
            StreamWriter File = new StreamWriter("C://obrazy//ocena.txt", true);
            string temp = "Filter name: " + filter.ToString() + ";Mask size: " + size.ToString() + ";SigmaX: " + sigmaX.ToString()
                 + ";SigmaY: " + sigmaY.ToString() + ";Sigma color: " + sigmaColor.ToString() + ";Sigma space: " + sigmaSpace.ToString()
                  + ";Mask weight: " + weight.ToString() + ";MSE: " + data[7].ToString() + ";MSD: " + data[8].ToString() + ";MED: " + data[9].ToString()
                   + ";Marziliano: " + data[10].ToString() + ";JNB: " + data[11].ToString() + ";CPBD: " + data[12].ToString() + ";Time: " + data[13].ToString();
            temp = temp + "\r\n";
            File.Write(temp);
            File.Close();
        }

        void SaveResults(Image<Bgr, Byte> image, string filter, int size, int sigmaX, int sigmaY, double sigmaColor,
            double sigmaSpace, float weight, string[] data, string imageName)
        {
            if (!checkBoxData.Checked)
            {
                image.Save("C://obrazy//" + imageName + ";" + filter.ToString() + size.ToString() + sigmaX.ToString() + sigmaY.ToString() + sigmaColor.ToString()
                + sigmaSpace.ToString() + weight.ToString() + ".bmp");
            }
            StreamWriter File = new StreamWriter("C://obrazy//ocena.txt", true);
            string temp = imageName + ";";
            temp = temp + "Filter name: " + filter.ToString() + ";Mask size: " + size.ToString() + ";SigmaX: " + sigmaX.ToString()
                 + ";SigmaY: " + sigmaY.ToString() + ";Sigma color: " + sigmaColor.ToString() + ";Sigma space: " + sigmaSpace.ToString()
                  + ";Mask weight: " + weight.ToString() + ";MSE: " + data[7].ToString() + ";MSD: " + data[8].ToString() + ";MED: " + data[9].ToString()
                   + ";Marziliano: " + data[10].ToString() + ";JNB: " + data[11].ToString() + ";CPBD: " + data[12].ToString() + ";Time: " + data[13].ToString();
            temp = temp + "\r\n";
            File.Write(temp);
            File.Close();
        }

        void FilterChoice(Image<Bgr, Byte> imgToFilter, Image<Bgr, Byte> imgReference)
        {
            Image<Bgr, Byte> imgFiltered = new Image<Bgr, byte>(imgToFilter.Size);

            int i = (int)numericMinMask.Value;
            if (i % 2 == 0)
            {
                i++;
            }

            if (radioAvg.Checked)
            {
                for (; i <= (int)numericMaxMask.Value; i=i+2)
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
                    row[6] = "-";
                    row[7] = PSNRMSE.ToString();
                    row[8] = PSNRMSD.ToString();
                    row[9] = PSNRMED.ToString();
                    row[10] = Marziliano.ToString();
                    row[11] = JNB.ToString();
                    row[12] = CPBD.ToString();
                    row[13] = timer.Elapsed.TotalMilliseconds.ToString();
                    ListViewItem listViewItem = new ListViewItem(row);
                    listViewEval.Items.Add(listViewItem);
                    SaveResults(imgFiltered, "Avg", i, 0, 0, 0, 0, 0, row);
                }
            }
            else if (radioGauss.Checked)
            {
                for (; i <= (int)numericMaxMask.Value; i=i+2)
                {
                    for (int j = (int)numericMinSigmaX.Value; j <= (int)numericMaxSigmaX.Value; j++)
                    {
                        for (int k = (int)numericMinSigmaY.Value; k <= (int)numericMaxSigmaY.Value; k++)
                        {
                            timer.Reset();
                            timer.Start();
                            CvInvoke.GaussianBlur(imgToFilter, imgFiltered, new Size(i,i), j, k);
                            timer.Stop();
                            EvaluationOfFilter(imgReference, imgFiltered);
                            row[0] = "Filtr Gaussa";
                            row[1] = i.ToString();
                            row[2] = j.ToString();
                            row[3] = k.ToString();
                            row[4] = "-";
                            row[5] = "-";
                            row[6] = "-";
                            row[7] = PSNRMSE.ToString();
                            row[8] = PSNRMSD.ToString();
                            row[9] = PSNRMED.ToString();
                            row[10] = Marziliano.ToString();
                            row[11] = JNB.ToString();
                            row[12] = CPBD.ToString();
                            row[13] = timer.Elapsed.TotalMilliseconds.ToString();
                            ListViewItem listViewItem = new ListViewItem(row);
                            listViewEval.Items.Add(listViewItem);
                            SaveResults(imgFiltered, "Gauss", i, j, k, 0, 0, 0, row);
                        }
                    }
                }
            }
            else if (radioMedian.Checked)
            {
                for (; i <= (int)numericMaxMask.Value; i=i+2)
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
                    row[6] = "-";
                    row[7] = PSNRMSE.ToString();
                    row[8] = PSNRMSD.ToString();
                    row[9] = PSNRMED.ToString();
                    row[10] = Marziliano.ToString();
                    row[11] = JNB.ToString();
                    row[12] = CPBD.ToString();
                    row[13] = timer.Elapsed.TotalMilliseconds.ToString();
                    ListViewItem listViewItem = new ListViewItem(row);
                    listViewEval.Items.Add(listViewItem);
                    SaveResults(imgFiltered, "Median", i, 0, 0, 0, 0, 0, row);
                }
            }
            else if (radioBilateral.Checked)
            {
                for (double j = (double)numericMinSigmaColor.Value; j <= (double)numericMaxSigmaColor.Value; j++)
                {
                    for (double k = (double)numericMinSigmaSpace.Value; k <= (double)numericMaxSigmaSpace.Value; k++)
                    {
                        timer.Reset();
                        timer.Start();
                        CvInvoke.BilateralFilter(imgToFilter, imgFiltered, -1, j, k);
                        timer.Stop();
                        EvaluationOfFilter(imgReference, imgFiltered);
                        row[0] = "Filtr bilateralny";
                        row[1] = "-";
                        row[2] = "-";
                        row[3] = "-";
                        row[4] = j.ToString();
                        row[5] = k.ToString();
                        row[6] = "-";
                        row[7] = PSNRMSE.ToString();
                        row[8] = PSNRMSD.ToString();
                        row[9] = PSNRMED.ToString();
                        row[10] = Marziliano.ToString();
                        row[11] = JNB.ToString();
                        row[12] = CPBD.ToString();
                        row[13] = timer.Elapsed.TotalMilliseconds.ToString();
                        ListViewItem listViewItem = new ListViewItem(row);
                        listViewEval.Items.Add(listViewItem);
                        SaveResults(imgFiltered, "Bilateral", -1, 0, 0, j, k, 0, row);
                    }
                }
            }
            else if (radioKuwahara.Checked)
            {
                for (; i <= (int)numericMaxMask.Value; i=i+2)
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
                    row[6] = "-";
                    row[7] = PSNRMSE.ToString();
                    row[8] = PSNRMSD.ToString();
                    row[9] = PSNRMED.ToString();
                    row[10] = Marziliano.ToString();
                    row[11] = JNB.ToString();
                    row[12] = CPBD.ToString();
                    row[13] = timer.Elapsed.TotalMilliseconds.ToString();
                    ListViewItem listViewItem = new ListViewItem(row);
                    listViewEval.Items.Add(listViewItem);
                    SaveResults(imgFiltered, "Kuwahara", i, 0, 0, 0, 0, 0, row);
                }
            }
            else if (radioUnsharp.Checked)
            {
                for (; i <= (int)numericMaxMask.Value; i=i+2)
                {
                    float j = (float)numericUnsharpMaskMin.Value;
                    for (; j <= (float)numericUnsharpMaskMax.Value; j=(float)(j+0.05))
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
                        row[6] = j.ToString();
                        row[7] = PSNRMSE.ToString();
                        row[8] = PSNRMSD.ToString();
                        row[9] = PSNRMED.ToString();
                        row[10] = Marziliano.ToString();
                        row[11] = JNB.ToString();
                        row[12] = CPBD.ToString();
                        row[13] = timer.Elapsed.TotalMilliseconds.ToString();
                        ListViewItem listViewItem = new ListViewItem(row);
                        listViewEval.Items.Add(listViewItem);
                        SaveResults(imgFiltered, "Unsharp", i, 0, 0, 0, 0, j, row);
                    }
                }
            }
            else if (radioEqualize.Checked)
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
                row[6] = "-";
                row[7] = PSNRMSE.ToString();
                row[8] = PSNRMSD.ToString();
                row[9] = PSNRMED.ToString();
                row[10] = Marziliano.ToString();
                row[11] = JNB.ToString();
                row[12] = CPBD.ToString();
                row[13] = timer.Elapsed.TotalMilliseconds.ToString();
                ListViewItem listViewItem = new ListViewItem(row);
                listViewEval.Items.Add(listViewItem);
                SaveResults(imgFiltered, "Eq", 0, 0, 0, 0, 0, 0, row);
            }
            else if (radioStretch.Checked)
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
                row[6] = "-";
                row[7] = PSNRMSE.ToString();
                row[8] = PSNRMSD.ToString();
                row[9] = PSNRMED.ToString();
                row[10] = Marziliano.ToString();
                row[11] = JNB.ToString();
                row[12] = CPBD.ToString();
                row[13] = timer.Elapsed.TotalMilliseconds.ToString();
                ListViewItem listViewItem = new ListViewItem(row);
                listViewEval.Items.Add(listViewItem);
                SaveResults(imgFiltered, "Str", 0, 0, 0, 0, 0, 0, row);
            }

            MessageBox.Show("Ukończono operacje");
        }

        void AllFilters(string path, string numPicture, string extension, Image<Bgr, Byte> imgReference)
        {
            Image<Bgr, Byte> imgFiltered;

            int sizeMask = (int)numericMinMask.Value;
            if (sizeMask % 2 == 0)
            {
                sizeMask++;
            }

            for (int numImage = 1; numImage <= 24; numImage++)
            {
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
                    Image<Bgr,Byte> imgToFilter = new Image<Bgr, byte>(path + numPath + extension);
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
                            row[6] = "-";
                            row[7] = PSNRMSE.ToString();
                            row[8] = PSNRMSD.ToString();
                            row[9] = PSNRMED.ToString();
                            row[10] = Marziliano.ToString();
                            row[11] = JNB.ToString();
                            row[12] = CPBD.ToString();
                            row[13] = timer.Elapsed.TotalMilliseconds.ToString();
                            ListViewItem listViewItem = new ListViewItem(row);
                            listViewEval.Items.Add(listViewItem);
                            SaveResults(imgFiltered, "Avg", i, 0, 0, 0, 0, 0, row, numPicture+numPath);
                        }
                    }

                    // Gaussa
                    {
                        for (int i = sizeMask; i <= (int)numericMaxMask.Value; i = i + 2)
                        {
                            for (int j = (int)numericMinSigmaX.Value; j <= (int)numericMaxSigmaX.Value; j++)
                            {
                                for (int k = (int)numericMinSigmaY.Value; k <= (int)numericMaxSigmaY.Value; k++)
                                {
                                    timer.Reset();
                                    timer.Start();
                                    CvInvoke.GaussianBlur(imgToFilter, imgFiltered, new Size(i, i), j, k);
                                    timer.Stop();
                                    EvaluationOfFilter(imgReference, imgFiltered);
                                    row[0] = "Filtr Gaussa";
                                    row[1] = i.ToString();
                                    row[2] = j.ToString();
                                    row[3] = k.ToString();
                                    row[4] = "-";
                                    row[5] = "-";
                                    row[6] = "-";
                                    row[7] = PSNRMSE.ToString();
                                    row[8] = PSNRMSD.ToString();
                                    row[9] = PSNRMED.ToString();
                                    row[10] = Marziliano.ToString();
                                    row[11] = JNB.ToString();
                                    row[12] = CPBD.ToString();
                                    row[13] = timer.Elapsed.TotalMilliseconds.ToString();
                                    ListViewItem listViewItem = new ListViewItem(row);
                                    listViewEval.Items.Add(listViewItem);
                                    SaveResults(imgFiltered, "Gauss", i, j, k, 0, 0, 0, row, numPicture + numPath);
                                }
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
                            row[6] = "-";
                            row[7] = PSNRMSE.ToString();
                            row[8] = PSNRMSD.ToString();
                            row[9] = PSNRMED.ToString();
                            row[10] = Marziliano.ToString();
                            row[11] = JNB.ToString();
                            row[12] = CPBD.ToString();
                            row[13] = timer.Elapsed.TotalMilliseconds.ToString();
                            ListViewItem listViewItem = new ListViewItem(row);
                            listViewEval.Items.Add(listViewItem);
                            SaveResults(imgFiltered, "Median", i, 0, 0, 0, 0, 0, row, numPicture + numPath);
                        }
                    }

                    // bilateralny
                    {
                        for (double j = (double)numericMinSigmaColor.Value; j <= (double)numericMaxSigmaColor.Value; j++)
                        {
                            for (double k = (double)numericMinSigmaSpace.Value; k <= (double)numericMaxSigmaSpace.Value; k++)
                            {
                                timer.Reset();
                                timer.Start();
                                CvInvoke.BilateralFilter(imgToFilter, imgFiltered, -1, j, k);
                                timer.Stop();
                                EvaluationOfFilter(imgReference, imgFiltered);
                                row[0] = "Filtr bilateralny";
                                row[1] = "-";
                                row[2] = "-";
                                row[3] = "-";
                                row[4] = j.ToString();
                                row[5] = k.ToString();
                                row[6] = "-";
                                row[7] = PSNRMSE.ToString();
                                row[8] = PSNRMSD.ToString();
                                row[9] = PSNRMED.ToString();
                                row[10] = Marziliano.ToString();
                                row[11] = JNB.ToString();
                                row[12] = CPBD.ToString();
                                row[13] = timer.Elapsed.TotalMilliseconds.ToString();
                                ListViewItem listViewItem = new ListViewItem(row);
                                listViewEval.Items.Add(listViewItem);
                                SaveResults(imgFiltered, "Bilateral", -1, 0, 0, j, k, 0, row, numPicture + numPath);
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
                            row[6] = "-";
                            row[7] = PSNRMSE.ToString();
                            row[8] = PSNRMSD.ToString();
                            row[9] = PSNRMED.ToString();
                            row[10] = Marziliano.ToString();
                            row[11] = JNB.ToString();
                            row[12] = CPBD.ToString();
                            row[13] = timer.Elapsed.TotalMilliseconds.ToString();
                            ListViewItem listViewItem = new ListViewItem(row);
                            listViewEval.Items.Add(listViewItem);
                            SaveResults(imgFiltered, "Kuwahara", i, 0, 0, 0, 0, 0, row, numPicture + numPath);
                        }
                    }

                    // unsharp masking
                    {
                        for (int i = sizeMask; i <= (int)numericMaxMask.Value; i = i + 2)
                        {
                            float j = (float)numericUnsharpMaskMin.Value;
                            for (; j <= (float)numericUnsharpMaskMax.Value; j = (float)(j + 0.05))
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
                                row[6] = j.ToString();
                                row[7] = PSNRMSE.ToString();
                                row[8] = PSNRMSD.ToString();
                                row[9] = PSNRMED.ToString();
                                row[10] = Marziliano.ToString();
                                row[11] = JNB.ToString();
                                row[12] = CPBD.ToString();
                                row[13] = timer.Elapsed.TotalMilliseconds.ToString();
                                ListViewItem listViewItem = new ListViewItem(row);
                                listViewEval.Items.Add(listViewItem);
                                SaveResults(imgFiltered, "Unsharp", i, 0, 0, 0, 0, j, row, numPicture + numPath);
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
                        row[6] = "-";
                        row[7] = PSNRMSE.ToString();
                        row[8] = PSNRMSD.ToString();
                        row[9] = PSNRMED.ToString();
                        row[10] = Marziliano.ToString();
                        row[11] = JNB.ToString();
                        row[12] = CPBD.ToString();
                        row[13] = timer.Elapsed.TotalMilliseconds.ToString();
                        ListViewItem listViewItem = new ListViewItem(row);
                        listViewEval.Items.Add(listViewItem);
                        SaveResults(imgFiltered, "Eq", 0, 0, 0, 0, 0, 0, row, numPicture + numPath);
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
                        row[6] = "-";
                        row[7] = PSNRMSE.ToString();
                        row[8] = PSNRMSD.ToString();
                        row[9] = PSNRMED.ToString();
                        row[10] = Marziliano.ToString();
                        row[11] = JNB.ToString();
                        row[12] = CPBD.ToString();
                        row[13] = timer.Elapsed.TotalMilliseconds.ToString();
                        ListViewItem listViewItem = new ListViewItem(row);
                        listViewEval.Items.Add(listViewItem);
                        SaveResults(imgFiltered, "Str", 0, 0, 0, 0, 0, 0, row, numPicture + numPath);
                    }
                }
            }
        }
    }
}
