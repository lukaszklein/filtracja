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
                    FilterChoice(imgToFilter, imgReference);
                }
                parametersBox.Enabled = true;
                filtersBox.Enabled = true;
            }
        }

        private void checkBoxBatch_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxBatch.Checked)
            {
                filtersBox.Enabled = false;
                numericMinMask.Visible = true;
                numericMaxMask.Visible = true;
                numericMinSigmaX.Visible = true;
                numericMaxSigmaX.Visible = true;
                numericMinSigmaY.Visible = true;
                numericMaxSigmaY.Visible = true;
                numericMinSigmaColor.Visible = true;
                numericMaxSigmaColor.Visible = true;
                numericMinSigmaSpace.Visible = true;
                numericMaxSigmaSpace.Visible = true;
                numericUnsharpMaskMin.Visible = true;
                numericUnsharpMaskMax.Visible = true;
            }
            else
            {
                filtersBox.Enabled = true;
                numericMinMask.Visible = false;
                numericMaxMask.Visible = false;
                numericMinSigmaX.Visible = false;
                numericMaxSigmaX.Visible = false;
                numericMinSigmaY.Visible = false;
                numericMaxSigmaY.Visible = false;
                numericMinSigmaColor.Visible = false;
                numericMaxSigmaColor.Visible = false;
                numericMinSigmaSpace.Visible = false;
                numericMaxSigmaSpace.Visible = false;
                numericUnsharpMaskMin.Visible = false;
                numericUnsharpMaskMax.Visible = false;
            }
        }

        private void radioAvg_CheckedChanged(object sender, EventArgs e)
        {
            numericMinMask.Visible = true;
            numericMaxMask.Visible = true;
            numericMinSigmaX.Visible = false;
            numericMaxSigmaX.Visible = false;
            numericMinSigmaY.Visible = false;
            numericMaxSigmaY.Visible = false;
            numericMinSigmaColor.Visible = false;
            numericMaxSigmaColor.Visible = false;
            numericMinSigmaSpace.Visible = false;
            numericMaxSigmaSpace.Visible = false;
            numericUnsharpMaskMin.Visible = false;
            numericUnsharpMaskMax.Visible = false;
        }

        private void radioGauss_Click(object sender, EventArgs e)
        {
            numericMinMask.Visible = true;
            numericMaxMask.Visible = true;
            numericMinSigmaX.Visible = true;
            numericMaxSigmaX.Visible = true;
            numericMinSigmaY.Visible = true;
            numericMaxSigmaY.Visible = true;
            numericMinSigmaColor.Visible = false;
            numericMaxSigmaColor.Visible = false;
            numericMinSigmaSpace.Visible = false;
            numericMaxSigmaSpace.Visible = false;
            numericUnsharpMaskMin.Visible = false;
            numericUnsharpMaskMax.Visible = false;
        }

        private void radioNone_Click(object sender, EventArgs e)
        {
            numericMinMask.Visible = false;
            numericMaxMask.Visible = false;
            numericMinSigmaX.Visible = false;
            numericMaxSigmaX.Visible = false;
            numericMinSigmaY.Visible = false;
            numericMaxSigmaY.Visible = false;
            numericMinSigmaColor.Visible = false;
            numericMaxSigmaColor.Visible = false;
            numericMinSigmaSpace.Visible = false;
            numericMaxSigmaSpace.Visible = false;
            numericUnsharpMaskMin.Visible = false;
            numericUnsharpMaskMax.Visible = false;
        }

        private void radioMedian_Click(object sender, EventArgs e)
        {
            numericMinMask.Visible = true;
            numericMaxMask.Visible = true;
            numericMinSigmaX.Visible = false;
            numericMaxSigmaX.Visible = false;
            numericMinSigmaY.Visible = false;
            numericMaxSigmaY.Visible = false;
            numericMinSigmaColor.Visible = false;
            numericMaxSigmaColor.Visible = false;
            numericMinSigmaSpace.Visible = false;
            numericMaxSigmaSpace.Visible = false;
            numericUnsharpMaskMin.Visible = false;
            numericUnsharpMaskMax.Visible = false;
        }

        private void radioBilateral_Click(object sender, EventArgs e)
        {
            numericMinMask.Visible = false;
            numericMaxMask.Visible = false;
            numericMinSigmaX.Visible = false;
            numericMaxSigmaX.Visible = false;
            numericMinSigmaY.Visible = false;
            numericMaxSigmaY.Visible = false;
            numericMinSigmaColor.Visible = true;
            numericMaxSigmaColor.Visible = true;
            numericMinSigmaSpace.Visible = true;
            numericMaxSigmaSpace.Visible = true;
            numericUnsharpMaskMin.Visible = false;
            numericUnsharpMaskMax.Visible = false;
        }

        private void radioKuwahara_Click(object sender, EventArgs e)
        {
            numericMinMask.Visible = true;
            numericMaxMask.Visible = true;
            numericMinSigmaX.Visible = false;
            numericMaxSigmaX.Visible = false;
            numericMinSigmaY.Visible = false;
            numericMaxSigmaY.Visible = false;
            numericMinSigmaColor.Visible = false;
            numericMaxSigmaColor.Visible = false;
            numericMinSigmaSpace.Visible = false;
            numericMaxSigmaSpace.Visible = false;
            numericUnsharpMaskMin.Visible = false;
            numericUnsharpMaskMax.Visible = false;
        }

        private void radioUnsharp_Click(object sender, EventArgs e)
        {
            numericMinMask.Visible = true;
            numericMaxMask.Visible = true;
            numericMinSigmaX.Visible = false;
            numericMaxSigmaX.Visible = false;
            numericMinSigmaY.Visible = false;
            numericMaxSigmaY.Visible = false;
            numericMinSigmaColor.Visible = false;
            numericMaxSigmaColor.Visible = false;
            numericMinSigmaSpace.Visible = false;
            numericMaxSigmaSpace.Visible = false;
            numericUnsharpMaskMin.Visible = true;
            numericUnsharpMaskMax.Visible = true;
        }

        private void radioEqualize_Click(object sender, EventArgs e)
        {
            numericMinMask.Visible = false;
            numericMaxMask.Visible = false;
            numericMinSigmaX.Visible = false;
            numericMaxSigmaX.Visible = false;
            numericMinSigmaY.Visible = false;
            numericMaxSigmaY.Visible = false;
            numericMinSigmaColor.Visible = false;
            numericMaxSigmaColor.Visible = false;
            numericMinSigmaSpace.Visible = false;
            numericMaxSigmaSpace.Visible = false;
            numericUnsharpMaskMin.Visible = false;
            numericUnsharpMaskMax.Visible = false;
        }

        private void radioStretch_Click(object sender, EventArgs e)
        {
            numericMinMask.Visible = false;
            numericMaxMask.Visible = false;
            numericMinSigmaX.Visible = false;
            numericMaxSigmaX.Visible = false;
            numericMinSigmaY.Visible = false;
            numericMaxSigmaY.Visible = false;
            numericMinSigmaColor.Visible = false;
            numericMaxSigmaColor.Visible = false;
            numericMinSigmaSpace.Visible = false;
            numericMaxSigmaSpace.Visible = false;
            numericUnsharpMaskMin.Visible = false;
            numericUnsharpMaskMax.Visible = false;
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

        private void numericMinSigmaY_ValueChanged(object sender, EventArgs e)
        {
            if (numericMinSigmaY.Value > numericMaxSigmaY.Value)
            {
                numericMinSigmaY.Value = numericMaxSigmaY.Value;
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

        private void numericMaxSigmaY_ValueChanged(object sender, EventArgs e)
        {
            if (numericMinSigmaY.Value > numericMaxSigmaY.Value)
            {
                numericMaxSigmaY.Value = numericMinSigmaY.Value;
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

            if (checkBoxBatch.Checked)
            {
                string extensionPattern = @"\..{3}";
                string pathPattern = @"^(.*?)_";
                string numPicturePattern = @"\\i(.{2})";
                sourcePath = Regex.Match(openFileDialog1.FileName, pathPattern);
                extensionSourceFile = Regex.Match(openFileDialog1.FileName, extensionPattern);
                numPicture = Regex.Match(openFileDialog1.FileName, numPicturePattern);
                numPictureGood = Regex.Replace(numPicture.Value, "[^A-Za-z0-9 ]", "");
            }

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