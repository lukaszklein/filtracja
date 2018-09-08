namespace praca_magisterska
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.imageBoxOriginal = new Emgu.CV.UI.ImageBox();
            this.filtersBox = new System.Windows.Forms.GroupBox();
            this.radioStretch = new System.Windows.Forms.RadioButton();
            this.radioEqualize = new System.Windows.Forms.RadioButton();
            this.radioUnsharp = new System.Windows.Forms.RadioButton();
            this.radioKuwahara = new System.Windows.Forms.RadioButton();
            this.radioBilateral = new System.Windows.Forms.RadioButton();
            this.radioMedian = new System.Windows.Forms.RadioButton();
            this.radioGauss = new System.Windows.Forms.RadioButton();
            this.radioAvg = new System.Windows.Forms.RadioButton();
            this.radioNone = new System.Windows.Forms.RadioButton();
            this.Start = new System.Windows.Forms.Button();
            this.imageBoxFiltered = new Emgu.CV.UI.ImageBox();
            this.listViewEval = new System.Windows.Forms.ListView();
            this.numericMinMask = new System.Windows.Forms.NumericUpDown();
            this.numericMaxMask = new System.Windows.Forms.NumericUpDown();
            this.numericMinSigmaX = new System.Windows.Forms.NumericUpDown();
            this.numericMinSigmaY = new System.Windows.Forms.NumericUpDown();
            this.numericMaxSigmaX = new System.Windows.Forms.NumericUpDown();
            this.numericMaxSigmaY = new System.Windows.Forms.NumericUpDown();
            this.numericMinSigmaColor = new System.Windows.Forms.NumericUpDown();
            this.numericMaxSigmaColor = new System.Windows.Forms.NumericUpDown();
            this.numericMaxSigmaSpace = new System.Windows.Forms.NumericUpDown();
            this.numericMinSigmaSpace = new System.Windows.Forms.NumericUpDown();
            this.numericUnsharpMaskMin = new System.Windows.Forms.NumericUpDown();
            this.numericUnsharpMaskMax = new System.Windows.Forms.NumericUpDown();
            this.parametersBox = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Clear = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.LoadRefImg = new System.Windows.Forms.Button();
            this.LoadOrgImg = new System.Windows.Forms.Button();
            this.checkBoxBatch = new System.Windows.Forms.CheckBox();
            this.checkBoxData = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxOriginal)).BeginInit();
            this.filtersBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxFiltered)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMinMask)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxMask)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMinSigmaX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMinSigmaY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxSigmaX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxSigmaY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMinSigmaColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxSigmaColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxSigmaSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMinSigmaSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUnsharpMaskMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUnsharpMaskMax)).BeginInit();
            this.parametersBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageBoxOriginal
            // 
            this.imageBoxOriginal.Location = new System.Drawing.Point(763, 23);
            this.imageBoxOriginal.Name = "imageBoxOriginal";
            this.imageBoxOriginal.Size = new System.Drawing.Size(255, 255);
            this.imageBoxOriginal.TabIndex = 2;
            this.imageBoxOriginal.TabStop = false;
            // 
            // filtersBox
            // 
            this.filtersBox.Controls.Add(this.radioStretch);
            this.filtersBox.Controls.Add(this.radioEqualize);
            this.filtersBox.Controls.Add(this.radioUnsharp);
            this.filtersBox.Controls.Add(this.radioKuwahara);
            this.filtersBox.Controls.Add(this.radioBilateral);
            this.filtersBox.Controls.Add(this.radioMedian);
            this.filtersBox.Controls.Add(this.radioGauss);
            this.filtersBox.Controls.Add(this.radioAvg);
            this.filtersBox.Controls.Add(this.radioNone);
            this.filtersBox.Location = new System.Drawing.Point(47, 23);
            this.filtersBox.Name = "filtersBox";
            this.filtersBox.Size = new System.Drawing.Size(200, 235);
            this.filtersBox.TabIndex = 3;
            this.filtersBox.TabStop = false;
            this.filtersBox.Text = "Operacje";
            // 
            // radioStretch
            // 
            this.radioStretch.AutoSize = true;
            this.radioStretch.Location = new System.Drawing.Point(6, 204);
            this.radioStretch.Name = "radioStretch";
            this.radioStretch.Size = new System.Drawing.Size(146, 17);
            this.radioStretch.TabIndex = 29;
            this.radioStretch.TabStop = true;
            this.radioStretch.Text = "Rozciągnięcie histogramu";
            this.radioStretch.UseVisualStyleBackColor = true;
            this.radioStretch.Click += new System.EventHandler(this.radioStretch_Click);
            // 
            // radioEqualize
            // 
            this.radioEqualize.AutoSize = true;
            this.radioEqualize.Location = new System.Drawing.Point(6, 181);
            this.radioEqualize.Name = "radioEqualize";
            this.radioEqualize.Size = new System.Drawing.Size(138, 17);
            this.radioEqualize.TabIndex = 28;
            this.radioEqualize.TabStop = true;
            this.radioEqualize.Text = "Wyrównanie histogramu";
            this.radioEqualize.UseVisualStyleBackColor = true;
            this.radioEqualize.Click += new System.EventHandler(this.radioEqualize_Click);
            // 
            // radioUnsharp
            // 
            this.radioUnsharp.AutoSize = true;
            this.radioUnsharp.Location = new System.Drawing.Point(6, 158);
            this.radioUnsharp.Name = "radioUnsharp";
            this.radioUnsharp.Size = new System.Drawing.Size(107, 17);
            this.radioUnsharp.TabIndex = 9;
            this.radioUnsharp.TabStop = true;
            this.radioUnsharp.Text = "Unsharp masking";
            this.radioUnsharp.UseVisualStyleBackColor = true;
            this.radioUnsharp.Click += new System.EventHandler(this.radioUnsharp_Click);
            // 
            // radioKuwahara
            // 
            this.radioKuwahara.AutoSize = true;
            this.radioKuwahara.Location = new System.Drawing.Point(6, 135);
            this.radioKuwahara.Name = "radioKuwahara";
            this.radioKuwahara.Size = new System.Drawing.Size(92, 17);
            this.radioKuwahara.TabIndex = 8;
            this.radioKuwahara.TabStop = true;
            this.radioKuwahara.Text = "Filtr Kuwahara";
            this.radioKuwahara.UseVisualStyleBackColor = true;
            this.radioKuwahara.Click += new System.EventHandler(this.radioKuwahara_Click);
            // 
            // radioBilateral
            // 
            this.radioBilateral.AutoSize = true;
            this.radioBilateral.Location = new System.Drawing.Point(6, 112);
            this.radioBilateral.Name = "radioBilateral";
            this.radioBilateral.Size = new System.Drawing.Size(91, 17);
            this.radioBilateral.TabIndex = 7;
            this.radioBilateral.TabStop = true;
            this.radioBilateral.Text = "Filtr bilateralny";
            this.radioBilateral.UseVisualStyleBackColor = true;
            this.radioBilateral.Click += new System.EventHandler(this.radioBilateral_Click);
            // 
            // radioMedian
            // 
            this.radioMedian.AutoSize = true;
            this.radioMedian.Location = new System.Drawing.Point(6, 89);
            this.radioMedian.Name = "radioMedian";
            this.radioMedian.Size = new System.Drawing.Size(97, 17);
            this.radioMedian.TabIndex = 6;
            this.radioMedian.TabStop = true;
            this.radioMedian.Text = "Filtr medianowy";
            this.radioMedian.UseVisualStyleBackColor = true;
            this.radioMedian.Click += new System.EventHandler(this.radioMedian_Click);
            // 
            // radioGauss
            // 
            this.radioGauss.AutoSize = true;
            this.radioGauss.Location = new System.Drawing.Point(6, 66);
            this.radioGauss.Name = "radioGauss";
            this.radioGauss.Size = new System.Drawing.Size(80, 17);
            this.radioGauss.TabIndex = 5;
            this.radioGauss.TabStop = true;
            this.radioGauss.Text = "Filtr Gaussa";
            this.radioGauss.UseVisualStyleBackColor = true;
            this.radioGauss.Click += new System.EventHandler(this.radioGauss_Click);
            // 
            // radioAvg
            // 
            this.radioAvg.AutoSize = true;
            this.radioAvg.Location = new System.Drawing.Point(6, 42);
            this.radioAvg.Name = "radioAvg";
            this.radioAvg.Size = new System.Drawing.Size(103, 17);
            this.radioAvg.TabIndex = 4;
            this.radioAvg.Text = "Filtr uśredniający";
            this.radioAvg.UseVisualStyleBackColor = true;
            this.radioAvg.CheckedChanged += new System.EventHandler(this.radioAvg_CheckedChanged);
            // 
            // radioNone
            // 
            this.radioNone.AutoSize = true;
            this.radioNone.Checked = true;
            this.radioNone.Location = new System.Drawing.Point(6, 19);
            this.radioNone.Name = "radioNone";
            this.radioNone.Size = new System.Drawing.Size(69, 17);
            this.radioNone.TabIndex = 4;
            this.radioNone.TabStop = true;
            this.radioNone.Text = "Brak filtru";
            this.radioNone.UseVisualStyleBackColor = true;
            this.radioNone.Click += new System.EventHandler(this.radioNone_Click);
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(85, 282);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(75, 23);
            this.Start.TabIndex = 4;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // imageBoxFiltered
            // 
            this.imageBoxFiltered.Location = new System.Drawing.Point(763, 282);
            this.imageBoxFiltered.Name = "imageBoxFiltered";
            this.imageBoxFiltered.Size = new System.Drawing.Size(277, 235);
            this.imageBoxFiltered.TabIndex = 5;
            this.imageBoxFiltered.TabStop = false;
            // 
            // listViewEval
            // 
            this.listViewEval.Location = new System.Drawing.Point(32, 350);
            this.listViewEval.Name = "listViewEval";
            this.listViewEval.Size = new System.Drawing.Size(685, 97);
            this.listViewEval.TabIndex = 15;
            this.listViewEval.UseCompatibleStateImageBehavior = false;
            // 
            // numericMinMask
            // 
            this.numericMinMask.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericMinMask.Location = new System.Drawing.Point(218, 23);
            this.numericMinMask.Maximum = new decimal(new int[] {
            101,
            0,
            0,
            0});
            this.numericMinMask.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericMinMask.Name = "numericMinMask";
            this.numericMinMask.Size = new System.Drawing.Size(40, 20);
            this.numericMinMask.TabIndex = 16;
            this.numericMinMask.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericMinMask.Visible = false;
            this.numericMinMask.ValueChanged += new System.EventHandler(this.numericMinMask_ValueChanged);
            // 
            // numericMaxMask
            // 
            this.numericMaxMask.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericMaxMask.Location = new System.Drawing.Point(264, 23);
            this.numericMaxMask.Maximum = new decimal(new int[] {
            101,
            0,
            0,
            0});
            this.numericMaxMask.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericMaxMask.Name = "numericMaxMask";
            this.numericMaxMask.Size = new System.Drawing.Size(40, 20);
            this.numericMaxMask.TabIndex = 17;
            this.numericMaxMask.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericMaxMask.Visible = false;
            this.numericMaxMask.ValueChanged += new System.EventHandler(this.numericMaxMask_ValueChanged);
            // 
            // numericMinSigmaX
            // 
            this.numericMinSigmaX.Increment = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericMinSigmaX.Location = new System.Drawing.Point(218, 49);
            this.numericMinSigmaX.Maximum = new decimal(new int[] {
            201,
            0,
            0,
            0});
            this.numericMinSigmaX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMinSigmaX.Name = "numericMinSigmaX";
            this.numericMinSigmaX.Size = new System.Drawing.Size(40, 20);
            this.numericMinSigmaX.TabIndex = 18;
            this.numericMinSigmaX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMinSigmaX.Visible = false;
            this.numericMinSigmaX.ValueChanged += new System.EventHandler(this.numericMinSigmaX_ValueChanged);
            // 
            // numericMinSigmaY
            // 
            this.numericMinSigmaY.Increment = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericMinSigmaY.Location = new System.Drawing.Point(218, 75);
            this.numericMinSigmaY.Maximum = new decimal(new int[] {
            201,
            0,
            0,
            0});
            this.numericMinSigmaY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMinSigmaY.Name = "numericMinSigmaY";
            this.numericMinSigmaY.Size = new System.Drawing.Size(40, 20);
            this.numericMinSigmaY.TabIndex = 19;
            this.numericMinSigmaY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMinSigmaY.Visible = false;
            this.numericMinSigmaY.ValueChanged += new System.EventHandler(this.numericMinSigmaY_ValueChanged);
            // 
            // numericMaxSigmaX
            // 
            this.numericMaxSigmaX.Increment = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericMaxSigmaX.Location = new System.Drawing.Point(264, 49);
            this.numericMaxSigmaX.Maximum = new decimal(new int[] {
            201,
            0,
            0,
            0});
            this.numericMaxSigmaX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMaxSigmaX.Name = "numericMaxSigmaX";
            this.numericMaxSigmaX.Size = new System.Drawing.Size(40, 20);
            this.numericMaxSigmaX.TabIndex = 20;
            this.numericMaxSigmaX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMaxSigmaX.Visible = false;
            this.numericMaxSigmaX.ValueChanged += new System.EventHandler(this.numericMaxSigmaX_ValueChanged);
            // 
            // numericMaxSigmaY
            // 
            this.numericMaxSigmaY.Increment = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericMaxSigmaY.Location = new System.Drawing.Point(264, 75);
            this.numericMaxSigmaY.Maximum = new decimal(new int[] {
            201,
            0,
            0,
            0});
            this.numericMaxSigmaY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMaxSigmaY.Name = "numericMaxSigmaY";
            this.numericMaxSigmaY.Size = new System.Drawing.Size(40, 20);
            this.numericMaxSigmaY.TabIndex = 21;
            this.numericMaxSigmaY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMaxSigmaY.Visible = false;
            this.numericMaxSigmaY.ValueChanged += new System.EventHandler(this.numericMaxSigmaY_ValueChanged);
            // 
            // numericMinSigmaColor
            // 
            this.numericMinSigmaColor.Increment = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericMinSigmaColor.Location = new System.Drawing.Point(218, 101);
            this.numericMinSigmaColor.Maximum = new decimal(new int[] {
            202,
            0,
            0,
            0});
            this.numericMinSigmaColor.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericMinSigmaColor.Name = "numericMinSigmaColor";
            this.numericMinSigmaColor.Size = new System.Drawing.Size(40, 20);
            this.numericMinSigmaColor.TabIndex = 22;
            this.numericMinSigmaColor.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericMinSigmaColor.Visible = false;
            this.numericMinSigmaColor.ValueChanged += new System.EventHandler(this.numericMinSigmaColor_ValueChanged);
            // 
            // numericMaxSigmaColor
            // 
            this.numericMaxSigmaColor.Increment = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericMaxSigmaColor.Location = new System.Drawing.Point(264, 101);
            this.numericMaxSigmaColor.Maximum = new decimal(new int[] {
            202,
            0,
            0,
            0});
            this.numericMaxSigmaColor.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericMaxSigmaColor.Name = "numericMaxSigmaColor";
            this.numericMaxSigmaColor.Size = new System.Drawing.Size(40, 20);
            this.numericMaxSigmaColor.TabIndex = 23;
            this.numericMaxSigmaColor.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericMaxSigmaColor.Visible = false;
            this.numericMaxSigmaColor.ValueChanged += new System.EventHandler(this.numericMaxSigmaColor_ValueChanged);
            // 
            // numericMaxSigmaSpace
            // 
            this.numericMaxSigmaSpace.Increment = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericMaxSigmaSpace.Location = new System.Drawing.Point(264, 127);
            this.numericMaxSigmaSpace.Maximum = new decimal(new int[] {
            202,
            0,
            0,
            0});
            this.numericMaxSigmaSpace.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericMaxSigmaSpace.Name = "numericMaxSigmaSpace";
            this.numericMaxSigmaSpace.Size = new System.Drawing.Size(40, 20);
            this.numericMaxSigmaSpace.TabIndex = 25;
            this.numericMaxSigmaSpace.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericMaxSigmaSpace.Visible = false;
            this.numericMaxSigmaSpace.ValueChanged += new System.EventHandler(this.numericMaxSigmaSpace_ValueChanged);
            // 
            // numericMinSigmaSpace
            // 
            this.numericMinSigmaSpace.Increment = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericMinSigmaSpace.Location = new System.Drawing.Point(218, 127);
            this.numericMinSigmaSpace.Maximum = new decimal(new int[] {
            202,
            0,
            0,
            0});
            this.numericMinSigmaSpace.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericMinSigmaSpace.Name = "numericMinSigmaSpace";
            this.numericMinSigmaSpace.Size = new System.Drawing.Size(40, 20);
            this.numericMinSigmaSpace.TabIndex = 24;
            this.numericMinSigmaSpace.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericMinSigmaSpace.Visible = false;
            this.numericMinSigmaSpace.ValueChanged += new System.EventHandler(this.numericMinSigmaSpace_ValueChanged);
            // 
            // numericUnsharpMaskMin
            // 
            this.numericUnsharpMaskMin.DecimalPlaces = 1;
            this.numericUnsharpMaskMin.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUnsharpMaskMin.Location = new System.Drawing.Point(218, 153);
            this.numericUnsharpMaskMin.Maximum = new decimal(new int[] {
            101,
            0,
            0,
            65536});
            this.numericUnsharpMaskMin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUnsharpMaskMin.Name = "numericUnsharpMaskMin";
            this.numericUnsharpMaskMin.Size = new System.Drawing.Size(40, 20);
            this.numericUnsharpMaskMin.TabIndex = 26;
            this.numericUnsharpMaskMin.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUnsharpMaskMin.Visible = false;
            this.numericUnsharpMaskMin.ValueChanged += new System.EventHandler(this.numericUnsharpMaskMin_ValueChanged);
            // 
            // numericUnsharpMaskMax
            // 
            this.numericUnsharpMaskMax.DecimalPlaces = 1;
            this.numericUnsharpMaskMax.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUnsharpMaskMax.Location = new System.Drawing.Point(264, 153);
            this.numericUnsharpMaskMax.Maximum = new decimal(new int[] {
            101,
            0,
            0,
            65536});
            this.numericUnsharpMaskMax.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUnsharpMaskMax.Name = "numericUnsharpMaskMax";
            this.numericUnsharpMaskMax.Size = new System.Drawing.Size(40, 20);
            this.numericUnsharpMaskMax.TabIndex = 27;
            this.numericUnsharpMaskMax.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUnsharpMaskMax.Visible = false;
            this.numericUnsharpMaskMax.ValueChanged += new System.EventHandler(this.numericUnsharpMaskMax_ValueChanged);
            // 
            // parametersBox
            // 
            this.parametersBox.Controls.Add(this.label6);
            this.parametersBox.Controls.Add(this.label5);
            this.parametersBox.Controls.Add(this.label4);
            this.parametersBox.Controls.Add(this.label3);
            this.parametersBox.Controls.Add(this.label2);
            this.parametersBox.Controls.Add(this.label1);
            this.parametersBox.Controls.Add(this.numericMinMask);
            this.parametersBox.Controls.Add(this.numericMaxMask);
            this.parametersBox.Controls.Add(this.numericUnsharpMaskMax);
            this.parametersBox.Controls.Add(this.numericMinSigmaX);
            this.parametersBox.Controls.Add(this.numericUnsharpMaskMin);
            this.parametersBox.Controls.Add(this.numericMaxSigmaX);
            this.parametersBox.Controls.Add(this.numericMaxSigmaSpace);
            this.parametersBox.Controls.Add(this.numericMinSigmaY);
            this.parametersBox.Controls.Add(this.numericMinSigmaSpace);
            this.parametersBox.Controls.Add(this.numericMaxSigmaY);
            this.parametersBox.Controls.Add(this.numericMaxSigmaColor);
            this.parametersBox.Controls.Add(this.numericMinSigmaColor);
            this.parametersBox.Location = new System.Drawing.Point(264, 23);
            this.parametersBox.Name = "parametersBox";
            this.parametersBox.Size = new System.Drawing.Size(310, 198);
            this.parametersBox.TabIndex = 29;
            this.parametersBox.TabStop = false;
            this.parametersBox.Text = "Parametry";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 155);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 33;
            this.label6.Text = "Waga maski";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Odchylenie w przestrzeni";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Odchylenie w kolorze";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(166, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "Odchylenie standardowe w pionie";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Odchylenie standardowe w poziomie";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Rozmiar maski";
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(528, 282);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(75, 23);
            this.Clear.TabIndex = 30;
            this.Clear.Text = "Wyczyść";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // LoadRefImg
            // 
            this.LoadRefImg.Location = new System.Drawing.Point(172, 311);
            this.LoadRefImg.Name = "LoadRefImg";
            this.LoadRefImg.Size = new System.Drawing.Size(144, 23);
            this.LoadRefImg.TabIndex = 31;
            this.LoadRefImg.Text = "Wczytaj obraz referencyjny\r\n\r\n";
            this.LoadRefImg.UseVisualStyleBackColor = true;
            this.LoadRefImg.Click += new System.EventHandler(this.LoadRefImg_Click);
            // 
            // LoadOrgImg
            // 
            this.LoadOrgImg.Location = new System.Drawing.Point(172, 282);
            this.LoadOrgImg.Name = "LoadOrgImg";
            this.LoadOrgImg.Size = new System.Drawing.Size(144, 23);
            this.LoadOrgImg.TabIndex = 33;
            this.LoadOrgImg.Text = "Wczytaj obraz do filtracji";
            this.LoadOrgImg.UseVisualStyleBackColor = true;
            this.LoadOrgImg.Click += new System.EventHandler(this.LoadOrgImg_Click);
            // 
            // checkBoxBatch
            // 
            this.checkBoxBatch.AutoSize = true;
            this.checkBoxBatch.Location = new System.Drawing.Point(53, 315);
            this.checkBoxBatch.Name = "checkBoxBatch";
            this.checkBoxBatch.Size = new System.Drawing.Size(111, 17);
            this.checkBoxBatch.TabIndex = 34;
            this.checkBoxBatch.Text = "Filtracja serii zdjęć";
            this.checkBoxBatch.UseVisualStyleBackColor = true;
            this.checkBoxBatch.CheckedChanged += new System.EventHandler(this.checkBoxBatch_CheckedChanged);
            // 
            // checkBoxData
            // 
            this.checkBoxData.AutoSize = true;
            this.checkBoxData.Location = new System.Drawing.Point(338, 228);
            this.checkBoxData.Name = "checkBoxData";
            this.checkBoxData.Size = new System.Drawing.Size(117, 17);
            this.checkBoxData.TabIndex = 35;
            this.checkBoxData.Text = "Zapisuj tylko wyniki";
            this.checkBoxData.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 748);
            this.Controls.Add(this.checkBoxData);
            this.Controls.Add(this.checkBoxBatch);
            this.Controls.Add(this.LoadOrgImg);
            this.Controls.Add(this.LoadRefImg);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.parametersBox);
            this.Controls.Add(this.listViewEval);
            this.Controls.Add(this.imageBoxFiltered);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.filtersBox);
            this.Controls.Add(this.imageBoxOriginal);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxOriginal)).EndInit();
            this.filtersBox.ResumeLayout(false);
            this.filtersBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxFiltered)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMinMask)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxMask)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMinSigmaX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMinSigmaY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxSigmaX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxSigmaY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMinSigmaColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxSigmaColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxSigmaSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMinSigmaSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUnsharpMaskMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUnsharpMaskMax)).EndInit();
            this.parametersBox.ResumeLayout(false);
            this.parametersBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox imageBoxOriginal;
        private System.Windows.Forms.GroupBox filtersBox;
        private System.Windows.Forms.RadioButton radioAvg;
        private System.Windows.Forms.RadioButton radioNone;
        private System.Windows.Forms.Button Start;
        private Emgu.CV.UI.ImageBox imageBoxFiltered;
        private System.Windows.Forms.RadioButton radioBilateral;
        private System.Windows.Forms.RadioButton radioMedian;
        private System.Windows.Forms.RadioButton radioGauss;
        private System.Windows.Forms.ListView listViewEval;
        private System.Windows.Forms.NumericUpDown numericMinMask;
        private System.Windows.Forms.NumericUpDown numericMaxMask;
        private System.Windows.Forms.NumericUpDown numericMinSigmaX;
        private System.Windows.Forms.NumericUpDown numericMinSigmaY;
        private System.Windows.Forms.NumericUpDown numericMaxSigmaX;
        private System.Windows.Forms.NumericUpDown numericMaxSigmaY;
        private System.Windows.Forms.NumericUpDown numericMinSigmaColor;
        private System.Windows.Forms.NumericUpDown numericMaxSigmaColor;
        private System.Windows.Forms.NumericUpDown numericMaxSigmaSpace;
        private System.Windows.Forms.NumericUpDown numericMinSigmaSpace;
        private System.Windows.Forms.RadioButton radioKuwahara;
        private System.Windows.Forms.RadioButton radioUnsharp;
        private System.Windows.Forms.NumericUpDown numericUnsharpMaskMin;
        private System.Windows.Forms.NumericUpDown numericUnsharpMaskMax;
        private System.Windows.Forms.RadioButton radioEqualize;
        private System.Windows.Forms.RadioButton radioStretch;
        private System.Windows.Forms.GroupBox parametersBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button LoadRefImg;
        private System.Windows.Forms.Button LoadOrgImg;
        private System.Windows.Forms.CheckBox checkBoxBatch;
        private System.Windows.Forms.CheckBox checkBoxData;
    }
}

