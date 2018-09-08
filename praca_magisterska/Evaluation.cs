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

namespace praca_magisterska
{
    public partial class Form1 : Form
    {
        void EvaluationOfFilter(Image<Bgr, Byte> imgReference, Image<Bgr, Byte> imgFiltered)
        {
            Bgr colorFiltered, colorOriginal;
            Gray filteredCannyBlue, filteredCannyGreen, filteredCannyRed;
            Gray originalCannyBlue, originalCannyGreen, originalCannyRed;
            double temp = 0;
            double blueSum = 0;
            double greenSum = 0;
            double redSum = 0;

            //Measurement of Error
            {
                for (int i = 0; i < imgReference.Height; i++)
                {
                    for (int j = 0; j < imgReference.Width; j++)
                    {
                        colorFiltered = imgFiltered[i, j];//im większa wartość, tym większy wkłąd kanału w notacji BGR
                        colorOriginal = imgReference[i, j];
                        temp = Math.Pow(colorOriginal.Blue - colorFiltered.Blue, 2);
                        blueSum += temp;
                        temp = 0;
                        temp = Math.Pow(colorOriginal.Green - colorFiltered.Green, 2);
                        greenSum += temp;
                        temp = 0;
                        temp = Math.Pow(colorOriginal.Red - colorFiltered.Red, 2);
                        redSum += temp;
                        temp = 0;
                    }
                }

                double blueMSE = blueSum / (imgFiltered.Width * imgFiltered.Height);
                double greenMSE = greenSum / (imgFiltered.Width * imgFiltered.Height);
                double redMSE = redSum / (imgFiltered.Width * imgFiltered.Height);
                PSNRMSE = 10 * Math.Log10(3 / (blueMSE + greenMSE + redMSE));//im większa liczba tym bardziej podobne
            }

            //Structural distortion
            {
                int PixelsOver25Height = imgReference.Height % 25;
                int PixelsOver25Width = imgReference.Width % 25;
                int AddHeight = 0;
                int AddWidth = 0;

                if (PixelsOver25Height != 0)
                {
                    AddHeight = 25 - PixelsOver25Height;
                }
                if (PixelsOver25Width != 0)
                {
                    AddWidth = 25 - PixelsOver25Width;
                }

                Image<Bgr, byte> OriginalResize = new Image<Bgr, byte>(imgReference.Width + AddWidth, imgReference.Height + AddHeight);
                Image<Bgr, byte> FilteredResize = new Image<Bgr, byte>(imgReference.Width + AddWidth, imgReference.Height + AddHeight);

                CvInvoke.CopyMakeBorder(imgReference, OriginalResize, 0, AddHeight, 0, AddWidth, 0, new MCvScalar(0));
                CvInvoke.CopyMakeBorder(imgFiltered, FilteredResize, 0, AddHeight, 0, AddWidth, 0, new MCvScalar(0));

                int LengthOfRegionsEdge = 25;
                int tempwidth, tempheight;
                tempwidth = OriginalResize.Width;
                tempheight = OriginalResize.Height;
                int NumberOfRegionsHorizontally = tempwidth / LengthOfRegionsEdge;
                int NumberOfRegionsVertically = tempheight / LengthOfRegionsEdge;

                double tempFiltered = 0;
                double tempOriginal = 0;

                double blueMeanOriginal = 0;
                double blueMinOriginal = 0;
                double blueMaxOriginal = 0;
                double blueTempSumOriginal = 0;

                double blueMeanFiltered = 0;
                double blueMinFiltered = 0;
                double blueMaxFiltered = 0;
                double blueTempSumFiltered = 0;
                double blueStruct = 0;


                double greenMeanOriginal = 0;
                double greenMinOriginal = 0;
                double greenMaxOriginal = 0;
                double greenTempSumOriginal = 0;

                double greenMeanFiltered = 0;
                double greenMinFiltered = 0;
                double greenMaxFiltered = 0;
                double greenTempSumFiltered = 0;
                double greenStruct = 0;


                double redMeanOriginal = 0;
                double redMinOriginal = 0;
                double redMaxOriginal = 0;
                double redTempSumOriginal = 0;

                double redMeanFiltered = 0;
                double redMinFiltered = 0;
                double redMaxFiltered = 0;
                double redTempSumFiltered = 0;
                double redStruct = 0;


                for (int i = 0; i < NumberOfRegionsVertically; i++)
                {
                    for (int j = 0; j < NumberOfRegionsHorizontally; j++)
                    {
                        for (int x = 0; x < LengthOfRegionsEdge; x++)
                        {
                            for (int y = 0; y < LengthOfRegionsEdge; y++)
                            {
                                colorFiltered = FilteredResize[x + 25 * i, y + 25 * j];
                                colorOriginal = OriginalResize[x + 25 * i, y + 25 * j];

                                blueTempSumFiltered += colorFiltered.Blue;
                                blueTempSumOriginal += colorOriginal.Blue;

                                if (blueMinFiltered > colorFiltered.Blue)
                                {
                                    blueMinFiltered = colorFiltered.Blue;
                                }
                                else if (blueMinOriginal > colorOriginal.Blue)
                                {
                                    blueMinOriginal = colorOriginal.Blue;
                                }

                                if (blueMaxFiltered > colorFiltered.Blue)
                                {
                                    blueMaxFiltered = colorFiltered.Blue;
                                }
                                else if (blueMaxOriginal > colorOriginal.Blue)
                                {
                                    blueMaxOriginal = colorOriginal.Blue;
                                }


                                greenTempSumFiltered += colorFiltered.Green;
                                greenTempSumOriginal += colorOriginal.Green;

                                if (greenMinFiltered > colorFiltered.Green)
                                {
                                    greenMinFiltered = colorFiltered.Green;
                                }
                                else if (greenMinOriginal > colorOriginal.Green)
                                {
                                    greenMinOriginal = colorOriginal.Green;
                                }

                                if (greenMaxFiltered > colorFiltered.Green)
                                {
                                    greenMaxFiltered = colorFiltered.Green;
                                }
                                else if (greenMaxOriginal > colorOriginal.Green)
                                {
                                    greenMaxOriginal = colorOriginal.Green;
                                }


                                redTempSumFiltered += colorFiltered.Red;
                                redTempSumOriginal += colorOriginal.Red;

                                if (redMinFiltered > colorFiltered.Red)
                                {
                                    redMinFiltered = colorFiltered.Red;
                                }
                                else if (redMinOriginal > colorOriginal.Red)
                                {
                                    redMinOriginal = colorOriginal.Red;
                                }

                                if (redMaxFiltered > colorFiltered.Red)
                                {
                                    redMaxFiltered = colorFiltered.Red;
                                }
                                else if (redMaxOriginal > colorOriginal.Red)
                                {
                                    redMaxOriginal = colorOriginal.Red;
                                }
                            }
                        }

                        blueMeanFiltered = blueTempSumFiltered / (Math.Pow(LengthOfRegionsEdge, 2));
                        blueMeanOriginal = blueTempSumOriginal / (Math.Pow(LengthOfRegionsEdge, 2));

                        blueStruct += 0.5 * Math.Pow(blueMeanOriginal - blueMeanFiltered, 2) + 0.25 * Math.Pow(blueMaxOriginal - blueMaxFiltered, 2) +
                            0.25 * Math.Pow(blueMinOriginal - blueMinFiltered, 2);


                        greenMeanFiltered = greenTempSumFiltered / (Math.Pow(LengthOfRegionsEdge, 2));
                        greenMeanOriginal = greenTempSumOriginal / (Math.Pow(LengthOfRegionsEdge, 2));

                        greenStruct += 0.5 * Math.Pow(greenMeanOriginal - greenMeanFiltered, 2) + 0.25 * Math.Pow(greenMaxOriginal - greenMaxFiltered, 2) +
                            0.25 * Math.Pow(greenMinOriginal - greenMinFiltered, 2);


                        redMeanFiltered = redTempSumFiltered / (Math.Pow(LengthOfRegionsEdge, 2));
                        redMeanOriginal = redTempSumOriginal / (Math.Pow(LengthOfRegionsEdge, 2));

                        redStruct += 0.5 * Math.Pow(redMeanOriginal - redMeanFiltered, 2) + 0.25 * Math.Pow(redMaxOriginal - redMaxFiltered, 2) +
                            0.25 * Math.Pow(redMinOriginal - redMinFiltered, 2);

                        blueTempSumFiltered = 0;
                        blueTempSumOriginal = 0;
                        blueMinFiltered = 0;
                        blueMinOriginal = 0;
                        blueMaxFiltered = 0;
                        blueMaxOriginal = 0;
                        blueMeanFiltered = 0;
                        blueMeanOriginal = 0;


                        greenTempSumFiltered = 0;
                        greenTempSumOriginal = 0;
                        greenMinFiltered = 0;
                        greenMinOriginal = 0;
                        greenMaxFiltered = 0;
                        greenMaxOriginal = 0;
                        greenMeanFiltered = 0;
                        greenMeanOriginal = 0;


                        redTempSumFiltered = 0;
                        redTempSumOriginal = 0;
                        redMinFiltered = 0;
                        redMinOriginal = 0;
                        redMaxFiltered = 0;
                        redMaxOriginal = 0;
                        redMeanFiltered = 0;
                        redMeanOriginal = 0;
                    }
                }

                blueStruct = blueStruct / (NumberOfRegionsHorizontally * NumberOfRegionsVertically);
                greenStruct = greenStruct / (NumberOfRegionsHorizontally * NumberOfRegionsVertically);
                redStruct = redStruct / (NumberOfRegionsHorizontally * NumberOfRegionsVertically);
                PSNRMSD = 10 * Math.Log10(3 / (blueStruct + greenStruct + redStruct));
            }

            //Edge distortion
            {
                Image<Gray, byte> imgOriginalCannyBlue = imgReference[0].Canny(120, 50);
                Image<Gray, byte> imgOriginalCannyGreen = imgReference[1].Canny(120, 50);
                Image<Gray, byte> imgOriginalCannyRed = imgReference[2].Canny(120, 50);
                Image<Gray, byte> imgFilteredCannyBlue = imgFiltered[0].Canny(120, 50);
                Image<Gray, byte> imgFilteredCannyGreen = imgFiltered[1].Canny(120, 50);
                Image<Gray, byte> imgFilteredCannyRed = imgFiltered[2].Canny(120, 50);

                for (int i = 0; i < imgReference.Height; i++)
                {
                    for (int j = 0; j < imgReference.Width; j++)
                    {
                        filteredCannyBlue = imgFilteredCannyBlue[i, j];
                        filteredCannyGreen = imgFilteredCannyGreen[i, j];
                        filteredCannyRed = imgFilteredCannyRed[i, j];
                        originalCannyBlue = imgOriginalCannyBlue[i, j];
                        originalCannyGreen = imgOriginalCannyGreen[i, j];
                        originalCannyRed = imgOriginalCannyRed[i, j];

                        temp = Math.Pow(originalCannyBlue.Intensity - filteredCannyBlue.Intensity, 2);
                        blueSum += temp;
                        temp = 0;
                        temp = Math.Pow(originalCannyGreen.Intensity - filteredCannyGreen.Intensity, 2);
                        greenSum += temp;
                        temp = 0;
                        temp = Math.Pow(originalCannyRed.Intensity - filteredCannyRed.Intensity, 2);
                        redSum += temp;
                        temp = 0;
                    }
                }

                double blueMED = blueSum / (imgFiltered.Width * imgFiltered.Height);
                double greenMED = greenSum / (imgFiltered.Width * imgFiltered.Height);
                double redMED = redSum / (imgFiltered.Width * imgFiltered.Height);
                PSNRMED = 10 * Math.Log10(3 / (blueMED + greenMED + redMED));//im większa liczba tym bardziej podobne
            }

            //Marziliano metric
            {
                Image<Ycc, byte> YccImage = new Image<Ycc, byte>(imgFiltered.Size);
                CvInvoke.CvtColor(imgFiltered, YccImage, ColorConversion.Bgr2YCrCb);
                Image<Gray, byte> Luminance = new Image<Gray, byte>(YccImage.Size);
                Image<Gray, float> Sobelx = new Image<Gray, float>(YccImage.Size);
                Image<Gray, byte> SobelxToByte = new Image<Gray, byte>(YccImage.Size);
                Image<Gray, byte> ThreshImage = new Image<Gray, byte>(YccImage.Size);
                Luminance = YccImage[0];
                Sobelx = Luminance.Sobel(1, 0, 3);
                SobelxToByte = Sobelx.Convert<Gray, byte>();
                //CvInvoke.Threshold(SobelxToByte, ThreshImage, 100, 255, 0);
                CvInvoke.AdaptiveThreshold(SobelxToByte, ThreshImage, 255, AdaptiveThresholdType.GaussianC, 0, 3, 3);
                float sumadlugosci = 0;
                float liczbakrawedzi = 0;
                for (int x = 0; x < ThreshImage.Height; x++)
                {
                    int[] wiersz = new int[ThreshImage.Width];
                    for (int i = 0; i < ThreshImage.Width; i++)
                    {
                        wiersz[i] = (int)SobelxToByte[x, i].Intensity;
                    }

                    for (int y = 0; y < SobelxToByte.Width; y++)
                    {
                        if (ThreshImage[x, y].Intensity != 255)
                        {
                            int krawedz = y;
                            int rp = 0, r = 0, m2;
                            int lewo = 0, prawo = 0, dlugosc;
                            List<int> ekstrema = new List<int>();
                            for (int m1 = 0; m1 < wiersz.Length - 1; m1++)
                            {
                                m2 = m1 + 1;
                                r = wiersz[m2] - wiersz[m1];
                                if (Math.Sign(r) != Math.Sign(rp) && (r != 0 && rp != 0))
                                {
                                    ekstrema.Add(m1);
                                }
                                rp = r;
                            }
                            if (wiersz[0] > wiersz[1] || wiersz[0] < wiersz[1])
                            {
                                ekstrema.Add(0);
                            }
                            if (wiersz[wiersz.Length - 2] > wiersz[wiersz.Length - 1] || wiersz[wiersz.Length - 2] < wiersz[wiersz.Length - 1])
                            {
                                ekstrema.Add(wiersz.Length - 1);
                            }
                            ekstrema.Sort();
                            for (int i = 0; i < ekstrema.Count - 1; i++)
                            {
                                if (krawedz >= ekstrema.ElementAt(i) && krawedz < ekstrema.ElementAt(i + 1))
                                {
                                    lewo = ekstrema.ElementAt(i);
                                    prawo = ekstrema.ElementAt(i + 1);
                                    break;
                                }
                            }
                            for (int i = lewo; i < krawedz; i++)
                            {
                                if (wiersz[i] == wiersz[i + 1])
                                {
                                    lewo = i + 1;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (int i = prawo; i > krawedz + 1; i--)
                            {
                                if (wiersz[i] == wiersz[i - 1])
                                {
                                    prawo = i - 1;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            dlugosc = prawo - lewo;
                            if (dlugosc <= 0)
                            {
                                dlugosc = 1;
                            }
                            sumadlugosci = sumadlugosci + dlugosc;
                            liczbakrawedzi = liczbakrawedzi + 1;
                        }
                    }
                }
                Marziliano = sumadlugosci / liczbakrawedzi;
            }

            //JNB
            {
                Image<Ycc, byte> YccImage = new Image<Ycc, byte>(imgReference.Size);
                YccImage = imgFiltered.Convert<Ycc, byte>();
                Image<Gray, byte> luminance = new Image<Gray, byte>(imgReference.Size);
                Image<Gray, float> sobelx = new Image<Gray, float>(imgReference.Size);
                Image<Gray, byte> byteimage = new Image<Gray, byte>(imgReference.Size);
                Image<Gray, byte> thresh = new Image<Gray, byte>(imgReference.Size);
                luminance = YccImage[0];
                sobelx = luminance.Sobel(1, 0, 3);
                byteimage = sobelx.Convert<Gray, byte>();
                CvInvoke.AdaptiveThreshold(byteimage, thresh, 255, AdaptiveThresholdType.GaussianC, 0, 3, 3);
                //CvInvoke.Threshold(byteimage, thresh, 100, 255, 0);
                float sumadlugosci = 0;
                float liczbakrawedzi = 0;
                //byteimage[wiersz, kolumna];
                for (int x = 0; x < thresh.Height; x++)
                {
                    int[] wiersz = new int[thresh.Width];
                    for (int i = 0; i < thresh.Width; i++)
                    {
                        wiersz[i] = (int)luminance[x, i].Intensity;
                    }

                    for (int y = 0; y < byteimage.Width; y++)
                    {
                        if (thresh[x, y].Intensity != 255)
                        {
                            int krawedz = y;
                            int rp = 0, r = 0, m2;
                            int lewo = 0, prawo = 0, dlugosc;
                            List<int> ekstrema = new List<int>();
                            for (int m1 = 0; m1 < wiersz.Length - 1; m1++)
                            {
                                m2 = m1 + 1;
                                r = wiersz[m2] - wiersz[m1];
                                if (Math.Sign(r) != Math.Sign(rp) && (r != 0 && rp != 0))
                                {
                                    ekstrema.Add(m1);
                                }
                                rp = r;
                            }
                            if (wiersz[0] > wiersz[1] || wiersz[0] < wiersz[1])
                            {
                                ekstrema.Add(0);
                            }
                            if (wiersz[wiersz.Length - 2] > wiersz[wiersz.Length - 1] || wiersz[wiersz.Length - 2] < wiersz[wiersz.Length - 1])
                            {
                                ekstrema.Add(wiersz.Length - 1);
                            }
                            ekstrema.Sort();
                            for (int i = 0; i < ekstrema.Count - 1; i++)
                            {
                                if (krawedz >= ekstrema.ElementAt(i) && krawedz < ekstrema.ElementAt(i + 1))
                                {
                                    lewo = ekstrema.ElementAt(i);
                                    prawo = ekstrema.ElementAt(i + 1);
                                    break;
                                }
                            }
                            for (int i = lewo; i < krawedz; i++)
                            {
                                if (wiersz[i] == wiersz[i + 1])
                                {
                                    lewo = i + 1;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (int i = prawo; i > krawedz + 1; i--)
                            {
                                if (wiersz[i] == wiersz[i - 1])
                                {
                                    prawo = i - 1;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            dlugosc = prawo - lewo;
                            if (dlugosc <= 0)
                            {
                                dlugosc = 1;
                            }
                            sumadlugosci = sumadlugosci + dlugosc;
                            liczbakrawedzi = liczbakrawedzi + 1;
                        }
                    }
                }
                JNB = sumadlugosci / liczbakrawedzi;
            }

            //CPBD
            {
                Image<Ycc, Byte> YccOriginal = new Image<Ycc, Byte>(imgReference.Size);
                Image<Gray, byte> Luminance = new Image<Gray, byte>(imgReference.Size);
                Image<Gray, byte> Canny = new Image<Gray, byte>(imgReference.Size);
                Image<Bgr, float> Sobel = new Image<Bgr, float>(imgReference.Size);
                Image<Gray, byte> ByteSobel = new Image<Gray, byte>(imgReference.Size);
                Image<Gray, byte> ThreshSobel = new Image<Gray, byte>(imgReference.Size);
                YccOriginal = imgFiltered.Convert<Ycc, byte>();
                Luminance = YccOriginal[0];
                CvInvoke.Canny(imgFiltered, Canny, 50, 100);
                Sobel = imgFiltered.Sobel(1, 1, 3);


                ByteSobel = Sobel.Convert<Gray, byte>();
                //CvInvoke.Threshold(ByteSobel, ThreshSobel, 120, 255, 0);
                CvInvoke.AdaptiveThreshold(ByteSobel, ThreshSobel, 255, AdaptiveThresholdType.GaussianC, 0, 3, 3);

                int PixelsOver64Height = imgReference.Height % 64;
                int PixelsOver64Width = imgReference.Width % 64;
                int AddHeight = 0;
                int AddWidth = 0;

                if (PixelsOver64Height != 0)
                {
                    AddHeight = 64 - PixelsOver64Height;
                }
                if (PixelsOver64Width != 0)
                {
                    AddWidth = 64 - PixelsOver64Width;
                }

                Image<Gray, byte> SobelResize = new Image<Gray, byte>(imgReference.Width + AddWidth, imgReference.Height + AddHeight);
                Image<Gray, byte> CannyResize = new Image<Gray, byte>(imgReference.Width + AddWidth, imgReference.Height + AddHeight);
                Image<Gray, byte> LuminanceResize = new Image<Gray, byte>(imgReference.Width + AddWidth, imgReference.Height + AddHeight);

                CvInvoke.CopyMakeBorder(ByteSobel, SobelResize, 0, AddHeight, 0, AddWidth, 0, new MCvScalar(255));
                CvInvoke.CopyMakeBorder(Canny, CannyResize, 0, AddHeight, 0, AddWidth, 0, new MCvScalar(0));
                CvInvoke.CopyMakeBorder(Luminance, LuminanceResize, 0, AddHeight, 0, AddWidth, 0, new MCvScalar(0));

                int RegionsVertically = SobelResize.Height / 64;
                int RegionsHorizontally = SobelResize.Width / 64;
                int Edges = 0;
                int RegionsProcessed = 0;
                List<double> prawdopodobienstwacpbd = new List<double>();
                int alledges = 0;
                for (int i = 0; i < RegionsHorizontally; i++)
                {
                    for (int j = 0; j < RegionsVertically; j++)
                    {
                        List<int> dlugoscikrawedzi = new List<int>();
                        List<int> jasnosc = new List<int>();
                        Edges = 0;
                        for (int x = 0; x < 64; x++)
                        {
                            int[] wiersz = new int[SobelResize.Width];
                            for (int z = 0; z < SobelResize.Width; z++)
                            {
                                wiersz[z] = (int)SobelResize[x + 25 * j, z].Intensity;
                            }

                            for (int y = 0; y < 64; y++)
                            {
                                jasnosc.Add((int)Luminance[x + 25 * j, y + 25 * i].Intensity);
                                if (Canny[x + 25 * j, y + 25 * i].Intensity != 0)
                                {
                                    Edges++;
                                    alledges++;
                                    int krawedzCanny = y + 25 * i;
                                    //edge length
                                    {
                                        int rp = 0, r = 0, m2;
                                        int lewo = 0, prawo = 0, dlugosc;
                                        List<int> ekstrema = new List<int>();
                                        for (int m1 = 0; m1 < wiersz.Length - 1; m1++)
                                        {
                                            m2 = m1 + 1;
                                            r = wiersz[m2] - wiersz[m1];
                                            if (Math.Sign(r) != Math.Sign(rp) && (r != 0 && rp != 0))
                                            {
                                                ekstrema.Add(m1);
                                            }
                                            rp = r;
                                        }
                                        if (wiersz[0] > wiersz[1] || wiersz[0] < wiersz[1])
                                        {
                                            ekstrema.Add(0);
                                        }
                                        if (wiersz[wiersz.Length - 2] > wiersz[wiersz.Length - 1] || wiersz[wiersz.Length - 2] < wiersz[wiersz.Length - 1])
                                        {
                                            ekstrema.Add(wiersz.Length - 1);
                                        }
                                        ekstrema.Sort();
                                        for (int a = 0; a < ekstrema.Count - 1; a++)
                                        {
                                            if (krawedzCanny >= ekstrema.ElementAt(i) && krawedzCanny < ekstrema.ElementAt(a + 1))
                                            {
                                                lewo = ekstrema.ElementAt(a);
                                                prawo = ekstrema.ElementAt(a + 1);
                                                break;
                                            }
                                        }
                                        for (int a = lewo; a < krawedzCanny; a++)
                                        {
                                            if (wiersz[a] == wiersz[a + 1])
                                            {
                                                lewo = a + 1;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        for (int a = prawo; a > krawedzCanny + 1; a--)
                                        {
                                            if (wiersz[a] == wiersz[a - 1])
                                            {
                                                prawo = a - 1;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        dlugosc = prawo - lewo;
                                        if (dlugosc <= 0)
                                        {
                                            dlugosc = 1;
                                        }
                                        dlugoscikrawedzi.Add(dlugosc);
                                    }
                                }
                            }
                        }
                        if (Edges > Math.Pow(64, 2) * 0.002)
                        {
                            RegionsProcessed++;
                            jasnosc.Sort();
                            int kontrast = jasnosc.ElementAt(jasnosc.Count - 1) - jasnosc.ElementAt(0);
                            int wspolczynnikkontrastu;
                            double ocenablokujnb;
                            double sumablokujnb = 0;
                            double ocenablokujnd;
                            double sumablokujnd = 0;
                            if (kontrast <= 50)
                            {
                                wspolczynnikkontrastu = 5;
                            }
                            else
                            {
                                wspolczynnikkontrastu = 3;
                            }
                            foreach (var dlugosc in dlugoscikrawedzi)
                            {
                                double wnetrze = -Math.Pow(Math.Abs(dlugosc / wspolczynnikkontrastu), 3.6);
                                prawdopodobienstwacpbd.Add(1 - Math.Pow(Math.E, wnetrze));
                            }
                        }
                        jasnosc.Clear();
                    }
                }
                double sumaocencpbd = 0;
                double ocenacaloscijcpbd;
                foreach (var ocena in prawdopodobienstwacpbd)
                {
                    sumaocencpbd = sumaocencpbd + ocena;
                }
                CPBD = sumaocencpbd / alledges;
            }
        }
    }
}