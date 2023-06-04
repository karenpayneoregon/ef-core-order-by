namespace NorthWindExampleApp3;

partial class Form1
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
            this.ColumnNamesComboBox = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DirectionComboBox = new System.Windows.Forms.ComboBox();
            this.PopulateButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ColumnNamesComboBox
            // 
            this.ColumnNamesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ColumnNamesComboBox.FormattingEnabled = true;
            this.ColumnNamesComboBox.Location = new System.Drawing.Point(12, 18);
            this.ColumnNamesComboBox.Name = "ColumnNamesComboBox";
            this.ColumnNamesComboBox.Size = new System.Drawing.Size(307, 28);
            this.ColumnNamesComboBox.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.DirectionComboBox);
            this.panel1.Controls.Add(this.PopulateButton);
            this.panel1.Controls.Add(this.ColumnNamesComboBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 392);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1324, 58);
            this.panel1.TabIndex = 1;
            // 
            // DirectionComboBox
            // 
            this.DirectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DirectionComboBox.FormattingEnabled = true;
            this.DirectionComboBox.Items.AddRange(new object[] {
            "Ascending",
            "Descending"});
            this.DirectionComboBox.Location = new System.Drawing.Point(348, 18);
            this.DirectionComboBox.Name = "DirectionComboBox";
            this.DirectionComboBox.Size = new System.Drawing.Size(151, 28);
            this.DirectionComboBox.TabIndex = 2;
            // 
            // PopulateButton
            // 
            this.PopulateButton.Location = new System.Drawing.Point(541, 18);
            this.PopulateButton.Name = "PopulateButton";
            this.PopulateButton.Size = new System.Drawing.Size(124, 29);
            this.PopulateButton.TabIndex = 1;
            this.PopulateButton.Text = "Populate";
            this.PopulateButton.UseVisualStyleBackColor = true;
            this.PopulateButton.Click += new System.EventHandler(this.PopulateButton_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 29;
            this.dataGridView1.Size = new System.Drawing.Size(1324, 392);
            this.dataGridView1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1324, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Code sample";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private ComboBox ColumnNamesComboBox;
    private Panel panel1;
    private Button PopulateButton;
    private ComboBox DirectionComboBox;
    private DataGridView dataGridView1;
}
