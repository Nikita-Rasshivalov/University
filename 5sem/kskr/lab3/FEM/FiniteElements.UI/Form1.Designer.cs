
namespace FiniteElements.UI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.fem2D = new System.Windows.Forms.TabPage();
            this.meshPanel = new System.Windows.Forms.Panel();
            this.showYDeformationsButton = new System.Windows.Forms.Button();
            this.computeButton = new System.Windows.Forms.Button();
            this.showXDeformationsButton = new System.Windows.Forms.Button();
            this.clearNodesButton = new System.Windows.Forms.Button();
            this.showStressesButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.coefsBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dataPathBox = new System.Windows.Forms.TextBox();
            this.loadMeshButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.elementsBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.nodesBox = new System.Windows.Forms.ComboBox();
            this.showDisplacements = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.nodeTypeBox = new System.Windows.Forms.ComboBox();
            this.forcePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.forceValueBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.forceAngleBox = new System.Windows.Forms.TextBox();
            this.angleTypeBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mainControl = new System.Windows.Forms.TabControl();
            this.fem2D.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.forcePanel.SuspendLayout();
            this.mainControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // fem2D
            // 
            this.fem2D.BackColor = System.Drawing.SystemColors.Highlight;
            this.fem2D.Controls.Add(this.meshPanel);
            this.fem2D.Controls.Add(this.showYDeformationsButton);
            this.fem2D.Controls.Add(this.computeButton);
            this.fem2D.Controls.Add(this.showXDeformationsButton);
            this.fem2D.Controls.Add(this.clearNodesButton);
            this.fem2D.Controls.Add(this.showStressesButton);
            this.fem2D.Controls.Add(this.panel1);
            this.fem2D.Controls.Add(this.showDisplacements);
            this.fem2D.Controls.Add(this.panel2);
            this.fem2D.Location = new System.Drawing.Point(4, 29);
            this.fem2D.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fem2D.Name = "fem2D";
            this.fem2D.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fem2D.Size = new System.Drawing.Size(1089, 939);
            this.fem2D.TabIndex = 0;
            this.fem2D.Text = "2D";
            this.fem2D.Click += new System.EventHandler(this.fem2D_Click);
            // 
            // meshPanel
            // 
            this.meshPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.meshPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.meshPanel.Location = new System.Drawing.Point(15, 19);
            this.meshPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.meshPanel.Name = "meshPanel";
            this.meshPanel.Size = new System.Drawing.Size(706, 797);
            this.meshPanel.TabIndex = 0;
            this.meshPanel.Click += new System.EventHandler(this.panel1_Click);
            this.meshPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // showYDeformationsButton
            // 
            this.showYDeformationsButton.BackColor = System.Drawing.SystemColors.Info;
            this.showYDeformationsButton.Location = new System.Drawing.Point(925, 837);
            this.showYDeformationsButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.showYDeformationsButton.Name = "showYDeformationsButton";
            this.showYDeformationsButton.Size = new System.Drawing.Size(144, 55);
            this.showYDeformationsButton.TabIndex = 24;
            this.showYDeformationsButton.Text = "Показать деформации по Y";
            this.showYDeformationsButton.UseVisualStyleBackColor = false;
            this.showYDeformationsButton.Click += new System.EventHandler(this.showYDeformationsButton_Click);
            // 
            // computeButton
            // 
            this.computeButton.BackColor = System.Drawing.Color.AntiqueWhite;
            this.computeButton.Location = new System.Drawing.Point(15, 837);
            this.computeButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.computeButton.Name = "computeButton";
            this.computeButton.Size = new System.Drawing.Size(706, 55);
            this.computeButton.TabIndex = 3;
            this.computeButton.Text = "Вычислить";
            this.computeButton.UseVisualStyleBackColor = false;
            this.computeButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // showXDeformationsButton
            // 
            this.showXDeformationsButton.BackColor = System.Drawing.SystemColors.Info;
            this.showXDeformationsButton.Location = new System.Drawing.Point(761, 837);
            this.showXDeformationsButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.showXDeformationsButton.Name = "showXDeformationsButton";
            this.showXDeformationsButton.Size = new System.Drawing.Size(144, 55);
            this.showXDeformationsButton.TabIndex = 23;
            this.showXDeformationsButton.Text = "Показать деформации по X";
            this.showXDeformationsButton.UseVisualStyleBackColor = false;
            this.showXDeformationsButton.Click += new System.EventHandler(this.showXDeformationsButton_Click);
            // 
            // clearNodesButton
            // 
            this.clearNodesButton.BackColor = System.Drawing.SystemColors.Info;
            this.clearNodesButton.Location = new System.Drawing.Point(761, 700);
            this.clearNodesButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.clearNodesButton.Name = "clearNodesButton";
            this.clearNodesButton.Size = new System.Drawing.Size(307, 43);
            this.clearNodesButton.TabIndex = 8;
            this.clearNodesButton.Text = "Обнулить значения узлов";
            this.clearNodesButton.UseVisualStyleBackColor = false;
            this.clearNodesButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // showStressesButton
            // 
            this.showStressesButton.BackColor = System.Drawing.SystemColors.Info;
            this.showStressesButton.Location = new System.Drawing.Point(925, 763);
            this.showStressesButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.showStressesButton.Name = "showStressesButton";
            this.showStressesButton.Size = new System.Drawing.Size(144, 55);
            this.showStressesButton.TabIndex = 22;
            this.showStressesButton.Text = "Показать напряжения";
            this.showStressesButton.UseVisualStyleBackColor = false;
            this.showStressesButton.Click += new System.EventHandler(this.showStressesButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Info;
            this.panel1.Controls.Add(this.coefsBox);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.dataPathBox);
            this.panel1.Controls.Add(this.loadMeshButton);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.elementsBox);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.nodesBox);
            this.panel1.Location = new System.Drawing.Point(761, 272);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(307, 405);
            this.panel1.TabIndex = 19;
            // 
            // coefsBox
            // 
            this.coefsBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.coefsBox.FormattingEnabled = true;
            this.coefsBox.Location = new System.Drawing.Point(22, 279);
            this.coefsBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.coefsBox.Name = "coefsBox";
            this.coefsBox.Size = new System.Drawing.Size(267, 28);
            this.coefsBox.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 255);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(253, 20);
            this.label6.TabIndex = 19;
            this.label6.Text = "Выберите файл с коэффициентами";
            // 
            // dataPathBox
            // 
            this.dataPathBox.AllowDrop = true;
            this.dataPathBox.Location = new System.Drawing.Point(25, 35);
            this.dataPathBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataPathBox.Name = "dataPathBox";
            this.dataPathBox.ReadOnly = true;
            this.dataPathBox.Size = new System.Drawing.Size(263, 27);
            this.dataPathBox.TabIndex = 9;
            this.dataPathBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataPathBox_DragDrop);
            this.dataPathBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.dataPathBox_DragEnter);
            this.dataPathBox.DragLeave += new System.EventHandler(this.dataPathBox_DragLeave);
            // 
            // loadMeshButton
            // 
            this.loadMeshButton.Location = new System.Drawing.Point(22, 341);
            this.loadMeshButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.loadMeshButton.Name = "loadMeshButton";
            this.loadMeshButton.Size = new System.Drawing.Size(267, 47);
            this.loadMeshButton.TabIndex = 18;
            this.loadMeshButton.Text = "Загрузить сетку";
            this.loadMeshButton.UseVisualStyleBackColor = true;
            this.loadMeshButton.Click += new System.EventHandler(this.loadMeshButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(257, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "Перетащите сюда папку с данными";
            // 
            // elementsBox
            // 
            this.elementsBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.elementsBox.FormattingEnabled = true;
            this.elementsBox.Location = new System.Drawing.Point(22, 197);
            this.elementsBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.elementsBox.Name = "elementsBox";
            this.elementsBox.Size = new System.Drawing.Size(267, 28);
            this.elementsBox.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(182, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "Выберите файл с узлами";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 173);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(217, 20);
            this.label8.TabIndex = 16;
            this.label8.Text = "Выберите файл с элементами";
            // 
            // nodesBox
            // 
            this.nodesBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nodesBox.FormattingEnabled = true;
            this.nodesBox.Location = new System.Drawing.Point(22, 117);
            this.nodesBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nodesBox.Name = "nodesBox";
            this.nodesBox.Size = new System.Drawing.Size(267, 28);
            this.nodesBox.TabIndex = 15;
            // 
            // showDisplacements
            // 
            this.showDisplacements.BackColor = System.Drawing.SystemColors.Info;
            this.showDisplacements.Location = new System.Drawing.Point(761, 763);
            this.showDisplacements.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.showDisplacements.Name = "showDisplacements";
            this.showDisplacements.Size = new System.Drawing.Size(144, 55);
            this.showDisplacements.TabIndex = 21;
            this.showDisplacements.Text = "Показать перемещения";
            this.showDisplacements.UseVisualStyleBackColor = false;
            this.showDisplacements.Click += new System.EventHandler(this.showDisplacements_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Info;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.nodeTypeBox);
            this.panel2.Controls.Add(this.forcePanel);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(761, 19);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(307, 223);
            this.panel2.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(65, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(208, 28);
            this.label2.TabIndex = 3;
            this.label2.Text = "Задать значение узлу";
            // 
            // nodeTypeBox
            // 
            this.nodeTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nodeTypeBox.FormattingEnabled = true;
            this.nodeTypeBox.Items.AddRange(new object[] {
            "Закрепление",
            "Сила",
            "Без условия"});
            this.nodeTypeBox.Location = new System.Drawing.Point(114, 68);
            this.nodeTypeBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nodeTypeBox.Name = "nodeTypeBox";
            this.nodeTypeBox.Size = new System.Drawing.Size(183, 28);
            this.nodeTypeBox.TabIndex = 4;
            this.nodeTypeBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // forcePanel
            // 
            this.forcePanel.Controls.Add(this.label3);
            this.forcePanel.Controls.Add(this.forceValueBox);
            this.forcePanel.Controls.Add(this.label4);
            this.forcePanel.Controls.Add(this.forceAngleBox);
            this.forcePanel.Controls.Add(this.angleTypeBox);
            this.forcePanel.Location = new System.Drawing.Point(6, 120);
            this.forcePanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.forcePanel.Name = "forcePanel";
            this.forcePanel.Size = new System.Drawing.Size(306, 93);
            this.forcePanel.TabIndex = 6;
            this.forcePanel.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 28);
            this.label3.TabIndex = 0;
            this.label3.Text = "Значение силы";
            // 
            // forceValueBox
            // 
            this.forceValueBox.Location = new System.Drawing.Point(3, 32);
            this.forceValueBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.forceValueBox.Name = "forceValueBox";
            this.forceValueBox.Size = new System.Drawing.Size(146, 27);
            this.forceValueBox.TabIndex = 1;
            this.forceValueBox.Text = "5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(155, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 28);
            this.label4.TabIndex = 2;
            this.label4.Text = "Угол силы";
            // 
            // forceAngleBox
            // 
            this.forceAngleBox.Location = new System.Drawing.Point(3, 67);
            this.forceAngleBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.forceAngleBox.Name = "forceAngleBox";
            this.forceAngleBox.Size = new System.Drawing.Size(94, 27);
            this.forceAngleBox.TabIndex = 3;
            this.forceAngleBox.Text = "90";
            // 
            // angleTypeBox
            // 
            this.angleTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.angleTypeBox.FormattingEnabled = true;
            this.angleTypeBox.Items.AddRange(new object[] {
            "рад.",
            "град."});
            this.angleTypeBox.Location = new System.Drawing.Point(103, 67);
            this.angleTypeBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.angleTypeBox.Name = "angleTypeBox";
            this.angleTypeBox.Size = new System.Drawing.Size(87, 28);
            this.angleTypeBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(9, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 28);
            this.label1.TabIndex = 7;
            this.label1.Text = "Тип узла";
            // 
            // mainControl
            // 
            this.mainControl.Controls.Add(this.fem2D);
            this.mainControl.Location = new System.Drawing.Point(14, 16);
            this.mainControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mainControl.Name = "mainControl";
            this.mainControl.SelectedIndex = 0;
            this.mainControl.Size = new System.Drawing.Size(1097, 972);
            this.mainControl.TabIndex = 25;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1123, 1004);
            this.Controls.Add(this.mainControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "МКЭ";
            this.fem2D.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.forcePanel.ResumeLayout(false);
            this.forcePanel.PerformLayout();
            this.mainControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage fem2D;
        private System.Windows.Forms.Panel meshPanel;
        private System.Windows.Forms.Button showYDeformationsButton;
        private System.Windows.Forms.Button computeButton;
        private System.Windows.Forms.Button showXDeformationsButton;
        private System.Windows.Forms.Button clearNodesButton;
        private System.Windows.Forms.Button showStressesButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox coefsBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox dataPathBox;
        private System.Windows.Forms.Button loadMeshButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox elementsBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox nodesBox;
        private System.Windows.Forms.Button showDisplacements;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox nodeTypeBox;
        private System.Windows.Forms.FlowLayoutPanel forcePanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox forceValueBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox forceAngleBox;
        private System.Windows.Forms.ComboBox angleTypeBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl mainControl;
    }
}

