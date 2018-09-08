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
        Image<Bgr, Byte> KuwaharaFilter(Image<Bgr, Byte> imgToFilter, int size)
        {
            Image<Ycc, Byte> imgYccToFilter = new Image<Ycc, byte>(imgToFilter.Size),
                             imgYccFiltered = new Image<Ycc, byte>(imgToFilter.Size);
            Image<Bgr, Byte> imgFiltered = new Image<Bgr, byte>(imgToFilter.Size);

            CvInvoke.CvtColor(imgToFilter, imgYccToFilter, ColorConversion.Bgr2YCrCb);

            int[] apetureMinX = { -(size / 2), 0, -(size / 2), 0 },
                  apetureMaxX = { 0, (size / 2), 0, (size / 2) },
                  apetureMinY = { -(size / 2), -(size / 2), 0, 0 },
                  apetureMaxY = { 0, 0, (size / 2), (size / 2) };

            for (int x = 0; x < imgYccToFilter.Height; ++x)
            {
                for (int y = 0; y < imgYccToFilter.Width; ++y)
                {
                    double[] yValues = { 0, 0, 0, 0 };
                    int[] numPixels = { 0, 0, 0, 0 },
                          maxYValue = { 0, 0, 0, 0 },
                          minYValue = { 255, 255, 255, 255 };

                    for (int i = 0; i < 4; ++i)
                    {
                        for (int x2 = apetureMinX[i]; x2 < apetureMaxX[i]; ++x2)
                        {
                            int tempX = x + x2;
                            if (tempX >= 0 && tempX < imgYccToFilter.Height)
                            {
                                for (int y2 = apetureMinY[i]; y2 < apetureMaxY[i]; ++y2)
                                {
                                    int tempY = y + y2;
                                    if (tempY >= 0 && tempY < imgYccToFilter.Width)
                                    {
                                        Ycc tempColor = imgYccToFilter[tempX, tempY];
                                        yValues[i] += tempColor.Y;

                                        if (tempColor.Y > maxYValue[i])
                                        {
                                            maxYValue[i] = (int)tempColor.Y;
                                        }
                                        else if (tempColor.Y < minYValue[i])
                                        {
                                            minYValue[i] = (int)tempColor.Y;
                                        }

                                        ++numPixels[i];
                                    }
                                }
                            }
                        }
                    }

                    int j = 0;
                    int minDifference = 10000;

                    for (int i = 0; i < 4; ++i)
                    {
                        int currentDifference = maxYValue[i] - minYValue[i];
                        if (currentDifference < minDifference && numPixels[i] > 0)
                        {
                            j = i;
                            minDifference = currentDifference;
                        }
                    }

                    Ycc filteredPixel = new Ycc(yValues[j] / numPixels[j], imgYccToFilter[x, y].Cr, imgYccToFilter[x, y].Cb);
                    imgYccFiltered[x, y] = filteredPixel;
                }
            }

            CvInvoke.CvtColor(imgYccFiltered, imgFiltered, ColorConversion.YCrCb2Bgr);
            imageBoxFiltered.Image = imgFiltered;

            return imgFiltered;
        }

        Image<Bgr, Byte> UnsharpMasking(Image<Bgr, Byte> imgToFilter, int size, float maskSize)
        {
            Image<Bgr, Byte> imgBlurredToFilter = new Image<Bgr, Byte>(imgToFilter.Size),
                             imgMask = new Image<Bgr, Byte>(imgToFilter.Size),
                             imgFiltered = new Image<Bgr, Byte>(imgToFilter.Size);

            CvInvoke.GaussianBlur(imgToFilter, imgBlurredToFilter, new Size(size, size), 1, 1);

            imgMask = imgToFilter - imgBlurredToFilter;
            imgMask = imgMask * maskSize;
            imgFiltered = imgToFilter + imgMask;

            return imgFiltered;
        }

        Image<Bgr, Byte> EqualizeHistogram(Image<Bgr, Byte> imgToFilter)
        {
            Image<Ycc, byte> imgYccToFilter = new Image<Ycc, byte>(imgToFilter.Size),
                             imgYccFiltered = new Image<Ycc, byte>(imgToFilter.Size);
            Image<Bgr, Byte> imgFiltered = new Image<Bgr, byte>(imgToFilter.Size);

            CvInvoke.CvtColor(imgToFilter, imgYccToFilter, ColorConversion.Bgr2YCrCb);

            Image<Gray, Byte> imgY = imgYccToFilter[0],
                              imgCr = imgYccToFilter[1],
                              imgCb = imgYccToFilter[2];

            imgY._EqualizeHist();
            CvInvoke.Merge(new VectorOfMat(imgY.Mat, imgCr.Mat, imgCb.Mat), imgYccFiltered);
            CvInvoke.CvtColor(imgYccFiltered, imgFiltered, ColorConversion.YCrCb2Bgr);

            return imgFiltered;
        }

        Image<Bgr, Byte> StretchHistogram(Image<Bgr, Byte> imgToFilter)
        {
            Image<Ycc, byte> imgYccToFilter = new Image<Ycc, byte>(imgToFilter.Size);
            Image<Bgr, Byte> imgFiltered = new Image<Bgr, byte>(imgToFilter.Size);
            CvInvoke.CvtColor(imgToFilter, imgYccToFilter, ColorConversion.Bgr2YCrCb);
            Ycc yccColor;

            //lowest and highest Y value
            double minY = 0,
                   maxY = 0,
                   temp,
                   newMaxY = 255, 
                   newMinY = 0, 
                   newY = 0;

            for (int x = 0; x < imgYccToFilter.Height; x++)
            {
                for (int y = 0; y < imgYccToFilter.Width; y++)
                {
                    yccColor = imgYccToFilter[x, y];
                    if (x == 0 && y == 0)
                    {
                        minY = yccColor.Y;
                    }
                    else if (x == 0 && y == 1)
                    {
                        maxY = yccColor.Y;
                        if (maxY < minY)
                        {
                            temp = minY;
                            minY = maxY;
                            maxY = minY;
                            temp = 0;
                        }
                    }
                    else
                    {
                        if (yccColor.Y < minY)
                        {
                            minY = yccColor.Y;
                        }
                        else if (yccColor.Y > maxY)
                        {
                            maxY = yccColor.Y;
                        }
                    }

                }

            }

            for (int x = 0; x < imgYccToFilter.Height; x++)
            {
                for (int y = 0; y < imgYccToFilter.Width; y++)
                {
                    yccColor = imgYccToFilter[x, y];
                    yccColor.Y = (yccColor.Y - minY) * (newMaxY - newMinY) / (maxY - minY) + newMinY;
                    imgYccToFilter[x, y] = yccColor;
                }

            }

            CvInvoke.CvtColor(imgYccToFilter, imgFiltered, ColorConversion.YCrCb2Bgr);

            return imgFiltered;
        }
    }
}