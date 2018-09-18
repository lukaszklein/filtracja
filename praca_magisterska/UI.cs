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
        private void Start_Click(object sender, EventArgs e)
        {
            if (imgToFilter == null || imgReference == null)
            {
                MessageBox.Show("Nie wybrano obrazu do filtracji lub obrazu referencyjnego");
            }
            else
            {
                parametersBox.Enabled = false;
                filtersBox.Enabled = false;
                if (checkBoxBatch.Checked)
                {
                    AllFilters(sourcePath.Value, numPictureGood, extensionSourceFile.Value, imgReference);
                }
                else
                {
                    FilterChoice(imgToFilter, imgReference, numPictureGood, numDistortionGood, numIntensityGood);
                }
                parametersBox.Enabled = true;
                filtersBox.Enabled = true;
            }
        }

        private void radioAvg_CheckedChanged(object sender, EventArgs e)
        {
            if (radioAvg.Checked)
            {
                numericMinMask.Visible = true;
                numericMaxMask.Visible = true;
            }
            else if (!radioGauss.Checked && !radioMedian.Checked && !radioKuwahara.Checked && !radioUnsharp.Checked)
            {
                numericMinMask.Visible = false;
                numericMaxMask.Visible = false;
            }
        }

        private void radioGauss_CheckedChanged(object sender, EventArgs e)
        {
            if (radioGauss.Checked)
            {
                numericMinMask.Visible = true;
                numericMaxMask.Visible = true;
                numericMinSigmaX.Visible = true;
                numericMaxSigmaX.Visible = true;
            }
            else if (!radioAvg.Checked && !radioMedian.Checked && !radioKuwahara.Checked && !radioUnsharp.Checked)
            {
                numericMinMask.Visible = false;
                numericMaxMask.Visible = false;
                numericMinSigmaX.Visible = false;
                numericMaxSigmaX.Visible = false;
            }
            else
            {
                numericMinSigmaX.Visible = false;
                numericMaxSigmaX.Visible = false;
            }
        }

        private void radioMedian_CheckedChanged(object sender, EventArgs e)
        {
            if (radioMedian.Checked)
            {
                numericMinMask.Visible = true;
                numericMaxMask.Visible = true;
            }
            else if (!radioGauss.Checked && !radioAvg.Checked && !radioKuwahara.Checked)
            {
                numericMinMask.Visible = false;
                numericMaxMask.Visible = false;
            }
        }

        private void radioBilateral_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBilateral.Checked)
            {
                numericMinSigmaColor.Visible = true;
                numericMaxSigmaColor.Visible = true;
                numericMinSigmaSpace.Visible = true;
                numericMaxSigmaSpace.Visible = true;
            }
            else
            {
                numericMinSigmaColor.Visible = false;
                numericMaxSigmaColor.Visible = false;
                numericMinSigmaSpace.Visible = false;
                numericMaxSigmaSpace.Visible = false;
            }
        }

        private void radioKuwahara_CheckedChanged(object sender, EventArgs e)
        {
            if (radioKuwahara.Checked)
            {
                numericMinMask.Visible = true;
                numericMaxMask.Visible = true;
            }
            else if (!radioGauss.Checked && !radioMedian.Checked && !radioAvg.Checked && !radioUnsharp.Checked)
            {
                numericMinMask.Visible = false;
                numericMaxMask.Visible = false;
            }
        }

        private void radioUnsharp_CheckedChanged(object sender, EventArgs e)
        {
            if (radioUnsharp.Checked)
            {
                numericMinMask.Visible = true;
                numericMaxMask.Visible = true;
                numericUnsharpMaskMin.Visible = true;
                numericUnsharpMaskMax.Visible = true;
            }
            else if (!radioGauss.Checked && !radioMedian.Checked && !radioAvg.Checked && !radioKuwahara.Checked)
            {
                numericMinMask.Visible = false;
                numericMaxMask.Visible = false;
                numericUnsharpMaskMin.Visible = false;
                numericUnsharpMaskMax.Visible = false;
            }
            else
            {
                numericUnsharpMaskMin.Visible = false;
                numericUnsharpMaskMax.Visible = false;
            }
        }        


        private void numericMinMask_ValueChanged(object sender, EventArgs e)
        {
            if (numericMinMask.Value > numericMaxMask.Value)
            {
                numericMinMask.Value = numericMaxMask.Value;
            }
        }

        private void numericMaxMask_ValueChanged(object sender, EventArgs e)
        {
            if (numericMinMask.Value > numericMaxMask.Value)
            {
                numericMaxMask.Value = numericMinMask.Value;
            }
        }

        private void numericMinSigmaX_ValueChanged(object sender, EventArgs e)
        {
            if (numericMinSigmaX.Value > numericMaxSigmaX.Value)
            {
                numericMinSigmaX.Value = numericMaxSigmaX.Value;
            }
        }

        private void numericMaxSigmaX_ValueChanged(object sender, EventArgs e)
        {
            if (numericMinSigmaX.Value > numericMaxSigmaX.Value)
            {
                numericMaxSigmaX.Value = numericMinSigmaX.Value;
            }
        }

        private void numericMinSigmaColor_ValueChanged(object sender, EventArgs e)
        {
            if (numericMinSigmaColor.Value > numericMaxSigmaColor.Value)
            {
                numericMinSigmaColor.Value = numericMaxSigmaColor.Value;
            }
        }

        private void numericMaxSigmaColor_ValueChanged(object sender, EventArgs e)
        {
            if (numericMinSigmaColor.Value > numericMaxSigmaColor.Value)
            {
                numericMaxSigmaColor.Value = numericMinSigmaColor.Value;
            }
        }

        private void numericMinSigmaSpace_ValueChanged(object sender, EventArgs e)
        {
            if (numericMinSigmaSpace.Value > numericMaxSigmaSpace.Value)
            {
                numericMinSigmaSpace.Value = numericMaxSigmaSpace.Value;
            }
        }

        private void numericMaxSigmaSpace_ValueChanged(object sender, EventArgs e)
        {
            if (numericMinSigmaSpace.Value > numericMaxSigmaSpace.Value)
            {
                numericMaxSigmaSpace.Value = numericMinSigmaSpace.Value;
            }
        }

        private void numericUnsharpMaskMin_ValueChanged(object sender, EventArgs e)
        {
            if (numericUnsharpMaskMin.Value > numericUnsharpMaskMax.Value)
            {
                numericUnsharpMaskMin.Value = numericUnsharpMaskMax.Value;
            }
        }

        private void numericUnsharpMaskMax_ValueChanged(object sender, EventArgs e)
        {
            if (numericUnsharpMaskMin.Value > numericUnsharpMaskMax.Value)
            {
                numericUnsharpMaskMax.Value = numericUnsharpMaskMin.Value;
            }
        }

        private void LoadOrgImg_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Grafika|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff";
            DialogResult filedialog = openFileDialog1.ShowDialog();
            if (filedialog != DialogResult.OK || openFileDialog1.FileName == "")
            {
                MessageBox.Show("Nie wybrano pliku");
                return;
            }

            try
            {
                imgToFilter = new Image<Bgr, byte>(openFileDialog1.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            if (imgToFilter == null)
            {
                MessageBox.Show("Nie otworzono pliku");
                return;
            }

            string extensionPattern = @"\..{3}";
            string pathPattern = @"^(.*?)_";
            string numPicturePattern = @"\\i(.{2})";
            string numDistortionPattern = "_(.{2})_";
            string numIntensityPattern = @"[1-5]\.";

            sourcePath = Regex.Match(openFileDialog1.FileName, pathPattern);
            extensionSourceFile = Regex.Match(openFileDialog1.FileName, extensionPattern);
            numPicture = Regex.Match(openFileDialog1.FileName, numPicturePattern);
            numPictureGood = Regex.Replace(numPicture.Value, "[^A-Za-z0-9 ]", "");
            numDistortion = Regex.Match(openFileDialog1.FileName, numDistortionPattern);
            numDistortionGood = Regex.Replace(numDistortion.Value, "[^A-Za-z0-9 ]", "");
            numIntensity = Regex.Match(openFileDialog1.FileName, numIntensityPattern);
            numIntensityGood = Regex.Replace(numIntensity.Value, "[^A-Za-z0-9 ]", "");


            Image<Bgr, byte>  imgToFilterResized = imgToFilter.Resize(imageBoxOriginal.Width, imageBoxFiltered.Height, Inter.Linear);
            imageBoxOriginal.Image = imgToFilterResized;
        }

        private void LoadRefImg_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Grafika|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff";
            DialogResult filedialog = openFileDialog1.ShowDialog();
            if (filedialog != DialogResult.OK || openFileDialog1.FileName == "")
            {
                MessageBox.Show("Nie wybrano pliku");
                return;
            }

            try
            {
                imgReference = new Image<Bgr, byte>(openFileDialog1.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wybrano plik inny niż obraz");
                return;
            }

            if (imgReference == null)
            {
                MessageBox.Show("Nie otworzono pliku");
                return;
            }

            imageBoxFiltered.Image = imgReference;
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            listViewEval.Items.Clear();
        }
    }
}